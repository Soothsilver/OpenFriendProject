namespace Core.Endpoints.Telegram
{
    public class TelegramUpdates
    {
        public bool Ok;
        public string Description;
        public TelegramUpdate[] Result;
    }

    public class TelegramUpdate
    {
        public int Update_id;
        public TelegramMessage Message;
        public TelegramCallbackQuery Callback_query;
    }
    public class TelegramMessage
    {
        public int Message_id;
        public TelegramUser From;
        public int Date;
        public TelegramChat Chat;
        public string Text;
    }

    public class TelegramChat
    {
        public int Id;
        public string Type;
        public string Title;
        public string Username;
        public string First_name;
        public string Last_name;
    }

    public class TelegramCallbackQuery
    {
        public string Id;
        public TelegramUser From;
        public TelegramMessage Message;
        public string Inline_message_id;
        public string Data;
    }
    public class TelegramUser
    {
        public int Id;
        public string First_name;
        public string Last_name;
        public string Username;
        public string Language_code;
    }
}