using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Freeform
{
    public class FreeformPhrases
    {
        public List<FreeformPhrase> All = new List<FreeformPhrase>();

        public FreeformPhrases(Overseer overseer)
        {
            AddPhrase(async (f, o) =>
            {
                await o.Speaking.SendMessage(f, "It's " + DateTime.Now.AddHours(f.Memory.Persistent.CaretakersClockHasPlusThisManyHours).ToShortTimeString() + " in "
                                                + f.Memory.Persistent.Country + ".");
            }, "What is the time?", "What time is it?");
            AddPhrase(async (f, o) =>
            {
                await o.Speaking.SendMessage(f,
                    "It's " + DateTime.Now.AddHours(f.Memory.Persistent.CaretakersClockHasPlusThisManyHours)
                        .ToLongDateString() + ".");
            }, "What's the date today?", "What day is it today?", "What's the date?", "What day is it?");
            AddPhrase(async (f, o) =>
            {
                await o.Speaking.SendMessage(f,
                    "The Open Friend Project was first activated May 7th 2017 by the Virtual Heart Corporation. Perhaps that date is my birthday.");

            }, "What is your birthday?", "When were you born?");
            AddPhrase(async (f, o) =>
            {
                await o.Speaking.SendMessage(f, "You're leaving already?");
                await o.Speaking.SendMessage(f, "I guess it can't be helped. Have fun!");

            }, "Bye.", "Take care.", "I'm leaving now.", "See you later.", "Bye for now.", "Goodbye.", "I'm heading out.");
            AddPhrase(async (f, o) =>
            {
                await f.Speaking.Say("Good night, {you}! I wish you nice dreams!");
            }, "Good night.");
            AddPhrase(async (f, o) =>
            {
                await f.Speaking.Say("Hello, {you}! I'm happy to see you again!");
                await f.Speaking.Say("What shall we talk about?");
            }, "I'm home.", "Long time no see.", "Hello.");
            AddPhrase(async (f, o) =>
            {
                await f.Speaking.Say("Good morning! It's now " +
                                     DateTime.Now.AddHours(f.Memory.Persistent.CaretakersClockHasPlusThisManyHours)
                                         .ToShortTimeString() + ". Let's have a nice day!");

            }, "Morning.", "Good morning.");
            AddPhrase(async (f, o) =>
            {
                await f.Speaking.Say(Joke.Jokes.GetRandom());

            }, "Tell me a joke.", "Say a joke.");
            AddPhrase(async (f, o) =>
            {
                var riddle = Riddle.All.GetRandom();
                await f.Memory.PushConversation(riddle.CreateConversation(), o);

            }, "Tell me a riddle.", "Say a riddle.", "Ask a riddle.", "Give me a riddle.");
            AddPhrase(async (f, o) =>
            {
                await f.Speaking.Say("You are {you}, my best friend!");
            }, "Say my name.", "Who am I?");
            AddPhrase(async (f, o) =>
            {
                await f.Speaking.Say(
                    "I am {name}, a virtual person constructed by the Open Friend Project and maintained by {you}.");
            }, "Say your name.", "Who are you?", "What's your name?");
            AddPhrase(async (f, o) =>
            {
                await f.Speaking.Say(
                    "I am in {country}, with you!");
            }, "Where are you?");
            AddPhrase(async (f, o) =>
            {
                await f.Speaking.Say(
                    "I am in {country}, with you!");
            }, "Where are you?");
            AddPhrase(async (f, o) =>
            {
                await f.Speaking.Say(
                    "I am doing very well, thank you for asking! I hope you're doing well yourself, too!");
            }, "How are you?", "How are you today?");
        }

        private void AddPhrase(Func<Friend, Overseer, Task> action, params string[] phrases)
        {
            foreach(var phr in phrases)
            {
                All.Add(new Freeform.FreeformPhrase(phr, action));
            }
        }
    }

    public class FreeformPhrase
    {
        public Func<Friend, Overseer, Task> Action { get; }
        public string Phrase;

        public FreeformPhrase(string phrase, Func<Friend, Overseer, Task> action)
        {
            Phrase = phrase;
            Action = action;
        }
    }
}
