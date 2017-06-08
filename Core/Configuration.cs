using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Configuration
    {
        public static string MetaCommunicatorGetAddress { get; private set; } = "";

        public static string PageAccessToken { get; private set; } = "";

        public static bool FacebookAvailable { get; private set; } = false;
        public static bool TelegramAvailable { get; private set; } = false;

        public static string TelegramToken { get; private set; } = "";


        static Configuration()
        {
            try
            {
                Ini ini = new global::Ini("config.ini");
                MetaCommunicatorGetAddress =
                    ini.GetValue("MetaCommunicatorGetAddress", "Facebook", "fail");
                PageAccessToken =
                    ini.GetValue("PageAccessToken", "Facebook", "fail");
                FacebookAvailable =
                    PageAccessToken != "fail";
                TelegramToken =
                    ini.GetValue("Token", "Telegram", "fail");
                TelegramAvailable =
                    TelegramToken != "fail";
            }
            catch (Exception)
            {
                // No file loaded.
            }
        }
    }
}
