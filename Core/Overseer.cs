using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aiml;
using Core.Conversation;
using Core.Endpoints;
using Core.Endpoints.Telegram;

namespace Core
{
    public class Overseer
    {
        public Senses Senses;
        public Speaking Speaking;
        public MessageProcessor MessageProcessor;
        public Persons Persons;
        public MessengerSettings.MessengerSettings Settings;
        public AimlCore Aiml;
        public FacebookEndpoint Facebook;
        public TelegramEndpoint Telegram;
        public DialogueLoader DialogueLoader;
        public LoadedConversations LoadedConversations;
        public HomeEndpoint Home;

        public Overseer()
        {
            this.Senses = new Senses(this);
            this.Speaking = new Speaking(this);
            this.MessageProcessor = new Core.MessageProcessor(this);
            this.Persons = new Core.Persons(this);
            this.Settings = new MessengerSettings.MessengerSettings(this);
            this.Aiml = new Core.Aiml.AimlCore(this);
            this.Home = new HomeEndpoint(this);
            this.Facebook = new FacebookEndpoint(this);
            this.Telegram = new TelegramEndpoint(this);
            this.DialogueLoader = new DialogueLoader();
            this.LoadedConversations = new LoadedConversations(this);

            FacebookMetaCommunicator mc = new FacebookMetaCommunicator(this);
            mc.StartLoop();
            TelegramCommunicator tg = new TelegramCommunicator(this);
            tg.StartLoop();
        }

        public void Initialize()
        {
            this.Aiml.Initialize();
        }
    }
}
