using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Conversation;
using Core.Endpoints;
using Core.Endpoints.Telegram;
using Core.Freeform;

namespace Core
{
    public class Overseer
    {
        public Senses Senses;
        public Speaking Speaking;
        public MessageProcessor MessageProcessor;
        public Persons Persons;
        public MessengerSettings.MessengerSettings Settings;
        public FacebookEndpoint Facebook;
        public TelegramEndpoint Telegram;
        public DialogueLoader DialogueLoader;
        public LoadedConversations LoadedConversations;
        public HomeEndpoint Home;
        public FreeformPhrases FreeformPhrases;

        public Overseer()
        {
            this.Senses = new Senses(this);
            this.Speaking = new Speaking(this);
            this.MessageProcessor = new Core.MessageProcessor(this);
            this.Persons = new Core.Persons(this);
            this.Settings = new MessengerSettings.MessengerSettings(this);
            this.Home = new HomeEndpoint(this);
            this.Facebook = new FacebookEndpoint(this);
            this.Telegram = new TelegramEndpoint(this);
            this.DialogueLoader = new DialogueLoader();
            this.FreeformPhrases = new Freeform.FreeformPhrases(this);
            this.LoadedConversations = new LoadedConversations(this);
            Riddle.Load();
            Joke.Load();

            FacebookMetaCommunicator mc = new FacebookMetaCommunicator(this);
            mc.StartLoop();
            TelegramCommunicator tg = new TelegramCommunicator(this);
            tg.StartLoop();
        }
    }
}
