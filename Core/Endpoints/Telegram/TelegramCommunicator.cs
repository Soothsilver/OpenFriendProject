using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.Endpoints.Telegram
{
    class TelegramCommunicator
    {
        private Overseer overseer;
        private HttpClient _httpClient;
        private int lastUpdateId = -1;

        public TelegramCommunicator(Overseer overseer)
        {
            this.overseer = overseer;
        }

        public void StartLoop()
        {
            if (!Configuration.TelegramAvailable)
            {
                overseer.Speaking.Debug("Telegram configuration unavailable. Telegram communicator will not run.");
                return;
            }
            overseer.Speaking.Debug("Initing client...");
            _httpClient = new HttpClient();
            overseer.Speaking.Debug("Running loop task...");
            Task.Run(() => Loop());
        }
        private string GetUrlFromQuery(string methodName, string query)
        {
            return "https://api.telegram.org/bot" + Configuration.TelegramToken + "/" + methodName + (query != null ? " ? " + query : "");
        }

        private async Task<string> Get(string address)
        {
            var response = await _httpClient.GetAsync(address);
            return await response.Content.ReadAsStringAsync();
        }

        private async void Loop()
        {
            overseer.Speaking.Debug("Before first HTTP request...");
            while (true)
            {
#if !DEBUG
                try
                {
#endif
                var response = await Get(GetUrlFromQuery("getUpdates",
                        lastUpdateId == -1 ? null : "offset=" + (lastUpdateId + 1)))
                    ;
                var updates = JsonConvert.DeserializeObject<TelegramUpdates>(response, Auxiliary.JsonSerializerSettings);
                foreach(var update in updates.Result)
                {

                }
                ;
#if !DEBUG
                }
                catch (Exception)
                {
                    Console.WriteLine("Exception suppressed.");
                }
#endif
            }
        }
    }
}
