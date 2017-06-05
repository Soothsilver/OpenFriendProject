using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class MetaCommunicator
    {
        private int lastIdReceived = -1;
        private HttpClient _httpClient;
        private Overseer overseer;

        public MetaCommunicator(Overseer overseer)
        {
            this.overseer = overseer;
        }

        public Task StartLoop()
        {
            if (!Configuration.FacebookAvailable)
            {
                overseer.Speaking.Debug("Facebook configuration unavailable. Meta communicator will not run.");
                return Task.FromResult(0);
            }
            overseer.Speaking.Debug("Initing client...");
            _httpClient = new HttpClient();
            overseer.Speaking.Debug("Running loop task...");
            return Task.Run(() => Loop());
        }

        private async void Loop()
        {
            overseer.Speaking.Debug("Before first HTTP request...");
            string ids = await Get(GetUrlFromQuery("action=GetLastId"));
            lastIdReceived = int.Parse(ids);
            overseer.Speaking.Debug("Starting at ID " + lastIdReceived);
            while (true)
            {
#if !DEBUG
                try
                {
#endif
                    var response = await Get(GetUrlFromQuery("action=GetAllRowsFrom&id=" + (lastIdReceived + 1)));
                    foreach (var line in response.Split(new char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries))
                    {
                        string trimmed = line.Trim();
                        string[] split = trimmed.Split(new char[] {' '}, 2, StringSplitOptions.RemoveEmptyEntries);
                        lastIdReceived = int.Parse(split[0]);
                        await overseer.Senses.IncomingCallback(split[1]);
                    }
#if !DEBUG
                }
                catch (Exception)
                {
                    Console.WriteLine("Exception suppressed.");
                }
#endif
            }
        }

        private string GetUrlFromQuery(string query)
        {
            return Configuration.MetaCommunicatorGetAddress + "?" + query;
        }

        private async Task<string> Get(string address)
        {
            var response = await _httpClient.GetAsync(address);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
