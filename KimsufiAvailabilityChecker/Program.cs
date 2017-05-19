using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading;
using KimsufiAvailabilityChecker.Json;
using Newtonsoft.Json;
using PushbulletSharp;
using PushbulletSharp.Models.Requests;

namespace KimsufiAvailabilityChecker
{
    class Program
    {
        /// <summary>
        /// This url return all availability of ovh server (kimsufi, soyoustart)
        /// </summary>
        private const string Url = "https://ws.ovh.com/dedicated/r2/ws.dispatcher/getAvailability2";
        
        /// <summary>
        /// Request limit is 500 per 3600 second, minimum is 7.2 second but 10 is a good value !
        /// </summary>
        private const int WaitTime = 10;

        static void Main(string[] args)
        {
            var accessToken = ConfigurationManager.AppSettings["PushbulletClientAccessToken"];
            PushbulletClient pbClient = null;
            if (!string.IsNullOrEmpty(accessToken))
            {
                pbClient = new PushbulletClient(accessToken);
            }

            var lookedServerList = ConfigurationManager.AppSettings["LookedServer"].Split(',');

            PushNotification(pbClient, "I start at " + DateTime.Now);

            while (true)
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        var rawData = client.DownloadString(Url);
                        var json = JsonConvert.DeserializeObject<Root>(rawData);

                        if(json.Error != null)
                        {
                            PushNotification(pbClient, json.Error.Message);
                            Thread.Sleep(WaitTime * 1000);
                            continue;
                        }

                        var products = json.Answer.Availabilities.Where(a => lookedServerList.Contains(a.Reference));

                        foreach (var product in products)
                        {
                            var available =
                                product.MetaZones.Any(m => m.Availability != "unavailable" && m.Availability != "unknow") ||
                                product.MetaZones.Any(m => m.Availability != "unavailable" && m.Availability != "unknow");

                            // If server available send notification with link to order directly
                            if (available)
                            {
                                PushNotification(pbClient, product.Reference + " available", "https://www.kimsufi.com/fr/commande/kimsufi.xml?reference=" + product.Reference + "&quantity=1");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    PushNotification(pbClient, "Error " + e);
                }

                Thread.Sleep(WaitTime * 1000);
            }
        }

        /// <summary>
        /// This write to console output and send notification to push bullet if client specified
        /// </summary>
        /// <param name="client">Pushbullet client to send notification</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Body of notification</param>
        private static void PushNotification(PushbulletClient client, string title, string body = null)
        {
            Console.WriteLine(DateTime.Now + ": " + title);

            if (client == null)
            {
                return;
            }

            var pushNote = new PushNoteRequest
            {
                Title = title,
                Body = body
            };

            client.PushNote(pushNote, true);
        }
    }
}
