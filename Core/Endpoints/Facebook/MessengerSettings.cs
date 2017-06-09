using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MessengerSettings
{
    public class MessengerSettings
    {
        private readonly Overseer _overseer;
        public string GetStartedPayload = "Hello";
        public string GreetingText = "Hello {{user_first_name}}! I'm Daina. Do you think we can be friends?";
        public PersistentMenu PersistentMenu = new PersistentMenu()
        {
            Call_to_actions = new[]
            {
                MenuItem.Menu("Greetings", 
                    MenuItem.Button("Hello!"),
                    MenuItem.Button("Bye!")),
                MenuItem.Menu("Commands",
                    MenuItem.Button("Talk to Alice."),
                    MenuItem.Button("exit"),
                    MenuItem.Button("Restart conversation."))
            }
        };

        public MessengerSettings(Overseer overseer)
        {
            _overseer = overseer;
        }

        public async void UpdateMessengerSettings()
        {
            await _overseer.Facebook.PostJsonProfileApiMessage(new
            {
                Greeting = new[]
                {
                    new
                    {
                        Locale = "default",
                        Text = GreetingText
                    }
                }
            });
            await _overseer.Facebook.PostJsonProfileApiMessage(new
            {
                Get_started = new
                {
                    Payload = GetStartedPayload
                }
            });
            await _overseer.Facebook.PostJsonProfileApiMessage(new
            {
                Persistent_menu = new[]
                {
                    PersistentMenu
                }
            });
        }
    }

    public class PersistentMenu
    {
        public string Locale = "default";
        public MenuItem[] Call_to_actions;
    }

    public class MenuItem
    {
        public string Type;
        public string Title;
        public string Url;
        public string Payload;
        public MenuItem[] Call_to_actions;

        public static MenuItem Menu(string title, params MenuItem[] elements)
        {
            return new MenuItem()
            {
                Type = "nested",
                Title = title,
                Call_to_actions = elements
            };
        }
        public static MenuItem Button(string title)
        {
            return new MenuItem()
            {
                Title = title,
                Payload = title,
                Type = "postback"
            };
        }
    }
}
