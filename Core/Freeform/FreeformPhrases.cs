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
                await o.Speaking.SendMessage(f,
                    "You're leaving already?");
                await o.Speaking.SendMessage(f,
                    "I guess it can't be helped. Have fun!");
            }, "Bye.", "I'm leaving now.", "See you later.", "Bye for now.", "Goodbye.", "I'm heading out.");
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
