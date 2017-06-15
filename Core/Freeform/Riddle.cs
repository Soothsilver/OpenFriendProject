using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Conversation;
using Core.Conversations;

namespace Core
{
    class Riddle
    {
        public string Question;
        public string Answer;

        public static List<Riddle> All = new List<Riddle>();
        private static bool Loaded = false;

        public static void Load()
        {
            if (Loaded) return;
            Loaded = true;
            create("What starts with “P” and ends with “E” and has more than 1000 letters?",
                "A post office!", "post office");
            Riddle.create("What loses its head in the morning but gets it back at night?", "A pillow",
                "pillow");
            Riddle.create("Can you name the two days starting with T besides Tuesday and Thursday?",
                "Today and tomorrow.", "today and tomorrow");
            Riddle.create("What is round on both sides but high in the middle?", "Ohio.",
                "ohio");
            Riddle.create("If two’s company and three’s a crowd, what are four and five?", "Nine!",
                "nine");
            Riddle.create("What is the center of Gravity?", "The letter V.", "V");
            Riddle.create("What is the last thing you take off before bed?",
                "Your feet off the floor.", "feet");
            Riddle.create(
                "A lawyer, a plumber and a hat maker were walking down the street. Who had the biggest hat?",
                "The one with the biggest head.", "biggest head");
            Riddle.create(
                "I have keys but no locks. I have space but no room. You can enter but can’t go outside. What am I?",
                "A Keyboard", "keyboard");
            Riddle.create("What is next in this sequence: JFMAMJJASON_ ?",
                "The letter D. The sequence contains the first letter of each month.", "D");
            Riddle.create(
                "A man was cleaning the windows of a 25 story building. He slipped and fell off the ladder, but wasn’t hurt. How did he do it?",
                "He fell off the 2nd step.", "2nd step");
            Riddle.create("How many seconds are there in a year?",
                "12. (January 2nd, February 2nd, March 2nd….)", "12");
            Riddle.create(
                "One night, a butcher, a baker and a candlestick maker go to a hotel. When they get their bill, however, it’s for four people. Who’s the fourth person?",
                "One night can also mean one knight. That makes four: one knight, a butcher, a baker and a candlestick maker!",
                "knight");
            Riddle.create("What instrument can you hear but never see?",
                "Your voice! You can sing with your voice like an instrument and hear it, but no one can see it!",
                "voice");
            Riddle.create("What kind of room has no doors or windows?", "A mushroom.",
                "mushroom");
            Riddle.create("What loses its head in the morning and gets it back at night?",
                "A pillow.", "pillow");
            Riddle.create("What has 3 feet but cannot walk?", "A yardstick.", "yardstick");
            Riddle.create("What belongs to you but others use it more than you do?", "Your name.",
                "name");
            Riddle.create("What kind of coat can only be put on when wet?", "A coat of paint.",
                "paint");
            Riddle.create("Why is a raven like a writing desk?",
                "Edgar Allan Poe wrote on both of them.", "Edgar Allan Poe");
            Riddle.create("What can be swallowed, but can swallow you?", "Pride", "Pride");
            Riddle.create("What is so delicate that even mentioning it breaks it?", "Silence",
                "silence");
            Riddle.create("What goes up, lets out a load, then goes back down?",
                "An elevator! (Or penis)", "elevator");
            Riddle.create("What has ten letters and starts with gas?", "An automobile",
                "automobile");
            Riddle.create(
                "I am the beginning of the end, the end of every place. I am the beginning of eternity, the end of time and space. What am I?",
                "The letter E.", "E");
            Riddle.create("I am very easy to get into,but it is hard to get out of me.What am I?",
                "Trouble", "trouble");
            Riddle.create("What heavy seven letter word can you take two away from and be left with eight?",
                "Weights", "weights");
            Riddle.create("What is the coolest letter in the alphabet?",
                "'B', because it's always surrounded by AC.", "B");
            Riddle.create("What has teeth but can't bite?", "A comb.", "comb");
            Riddle.create(
                "What always murmurs but never talks, Always runs but never walks, Has a bed but never sleeps, Has a mouth but never speaks?",
                "A river.", "river");
            Riddle.create("What tastes better than it smells?", "A tongue.", "tongue");
            Riddle.create("What two things can you never eat for breakfast?", "Lunch and breakfast.",
                "lunch breakfast");
            Riddle.create("What happens when you throw a blue rock into the yellow sea?", "It sinks.",
                "sinks");
            Riddle.create(
                "What gets longer when pulled,fits between breasts, slides neatly into a hole,has choked people when used improperly,and works best when jerked?",
                "A seatbelt.", "seatbelt");
            Riddle.create(
                "Imagine you are swimming in the ocean and a bunch of hungry sharks surround you. How do you get out alive?",
                "Stop imagining.", "imagining");
            Riddle.create("I build bridges of silver and crowns of gold. Who am I?", "A dentist",
                "dentist");
            Riddle.create(
                "Though I live beneath a roof, I never seem to dry. If you will only hold me, I swear I will not lie.",
                "A tongue.", "tongue");
            Riddle.create("What is it that nobody wants, yet nobody wants to lose?", "A lawsuit.",
                "lawsuit");
            Riddle.create(
                " I can be flipped and broken but I never move. I can be closed, and opened, and sometimes removed. I am sealed by hands. What am I?",
                "A deal.", "deal");
            Riddle.create("What has a foot but no legs?", "A snail", "snail");
            Riddle.create("Poor people have it. Rich people need it. If you eat it you die. What is it?",
                "Nothing", "nothing");
            Riddle.create("What comes down but never goes up?", "Rain", "rain");
            Riddle.create("I’m tall when I’m young and I’m short when I’m old. What am I?",
                "A candle", "candle");
            Riddle.create(
                "Mary’s father has 5 daughters – Nana, Nene, Nini, Nono. What is the fifth daughters name?",
                "Mary", "mary");
            Riddle.create("How can a pants pocket be empty and still have something in it?",
                "It can have a hole in it.", "hole");
            Riddle.create("What goes up when rain comes down?", "An umbrella!", "umbrella");
            Riddle.create("What is the longest word in the dictionary?",
                "Smiles, because there is a mile between each ‘s’", "smiles");
            Riddle.create("If I drink, I die. If I eat, I am fine. What am I?", "A fire!",
                "fire");
            Riddle.create(
                "Throw away the outside and cook the inside, then eat the outside and throw away the inside. What is it?",
                "Corn on the CoB, because you throw away the husk, cook and eat the kernels, and throw away the CoB.",
                "corn");
            Riddle.create("What word becomes shorter when you add two letters to it?", "Short",
                "short");
            Riddle.create("What travels around the world but stays in one spot?", "A stamp!",
                "stamp");
            Riddle.create("What occurs once in a minute, twice in a moment and never in one thousand years?",
                "The letter M", "M");
            Riddle.create("What has 4 eyes but can’t see?", "Mississippi", "Mississippi");
        }

        private static void create(string question, string answer, string keyword)
        {
            All.Add(new Core.Riddle()
            {
                Question = question,
                Answer = answer
            });
        }

        public ConversationNode CreateConversation()
        {
            return new Branch(this.Question,
                new PossibleReply("What is the answer?",
                    new Line(this.Answer)));
        }
    }
}