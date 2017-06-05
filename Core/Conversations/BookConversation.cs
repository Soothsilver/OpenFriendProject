using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Conversation;

namespace Core.Conversations
{
    class BookConversation
    {
        static Line endChat = new Line("Anyway, I need to go now. There will be a town council meeting I have to attend.",
            new Branch("Thank you for the chat! It's very fun talking to you!",
                new PossibleReply("I like it, too.",
                    new Line("That makes me so happy!",
                        new Line("Can I hug you?", 
                        new ImageLine("https://vintage.ponychan.net/chan/files/src/140296726352.png",
                        new Line("Oh, right, I have to go.", new Line("See you later!")))))),
                new PossibleReply("Have a nice day!", new Line("You bet I will!",
                    new Line("Until next time! See you!")))));
        public static ConversationNode GetActualPick()
        {
            var afterPick2 = new Line("Today, it was books, but...",
                new Line("...",
                    new Line("Next time, perhaps you could give me some more personal advice?",
                        new Branch("I would appreciate it.",
                            new PossibleReply("Of course, Daina.", new Line("Yay!", endChat)),
                            new PossibleReply("I'd rather not.", new Line("I understand.", endChat))))));

            var afterPick = new Branch("You know, you're pretty good at giving advice.",
                new PossibleReply("Of course!",
                    afterPick2),
                new PossibleReply("What?!",
                    new Line("Well, you gave me advice on a new book easily.",
                    new Line("And you don't dislike me, right?",
                    afterPick2))));

            return new Branch("Which do you recommend?",
                new PossibleReply("Daring Do.", new Line("Thank you. It's a good choice, I think.",
                    new Line("A little expensive, but I'm not exactly poor.", new Line("I'll go buy it tomorrow.",
                        afterPick)))),
                    new PossibleReply("Love's Sunrise.", new Line("It's by an amateur author, but at least that means it's cheap.",
                        new Line("And I haven't reached a similar book in a long time.",
                            new Line("It's a good choice. I'll buy it tomorrow.", afterPick)))),
                    new PossibleReply("Tell me about them.",
                        new Line("Daring Do is an adventure book. She's a treasure hunter who prevents evil villains from obtaining dangerous artifacts.",
                            new ImageLine("http://1.bp.blogspot.com/-PKt-zll2A94/VC3IWKaiOmI/AAAAAAACBwA/ZCQbrIjxK24/s1600/1.jpg",
                            new Line("Love's Sunrise is a romance novel. I know little about it, it's from an up-and-coming author.",
                            new ImageLine("https://cdn-img.fimfiction.net/story/gg0u-1485920753-362655-medium",
                            new Line("So what do you think?",
                            () => GetActualPick())))))));
        }
        public static ConversationNode GetBeginning()
        {
           

           

            var actualPick = GetActualPick();
            var helpPick2 =
                new Line(
                    "I wonder whether I should read 'Daring Do and the Marked Thief of Marapore' or 'Love's Sunrise'.",
                    actualPick);
            var helpPick = new Line("I like romance novels but also high adventure stories.",
                new Branch("Which genre is your favourite?", 
                    new PossibleReply("Romance.", new Line("I know, right?", new Line("They're a little old-fashioned, but still so adorable.",
                    new Line("I can't stop myself from smiling and giggling when there's a happy scene.",
                    new Line("I mean, I read tragedies, too, but not so much...",
                    new Line("Anyway, we have a book to pick.", helpPick2)))))),
                    new PossibleReply("High adventure.", new Line("They're so cool, aren't they?",
                        new Line("I love epic fights of good against evil.",
                        new Line("In fiction, that is.",
                        new Line("Anyway, would you help me pick a book?", helpPick2))))),
                    new PossibleReply("Non-fiction.", new Line("Wow. Very few of my friends can say that! I'm so happy to meet you, then!", 
                    new Line("My area of expertise is rock science, obviously. What is yours?",
                    new Line("Wait! No! We mustn't get sidetracked!", helpPick2)))),
                    new PossibleReply("We're not alike.", 
                        new Line("Maybe not. But maybe you would like these genres, too, if you read a couple of such books :P.",
                        new Branch("Would you be willing to help me with choosing my next book anyway?",
                            new PossibleReply("Sure.", helpPick2),
                            new PossibleReply("I can't.", new Line("I guess you're right. It wouldn't make much sense to have you help me.", new Line("I'll try soeone else.", endChat))))))));

            var begin = new Line("So... I really like books. And gems.",
                new Line("Like, you know, gemstones. Pretty things lying about in caverns.",
                    new Line("Anyway~~ About books, I'm looking for a new one to read.",
                        new Branch("Do you read books yourself?", new[]
                        {
                            new PossibleReply("I love books.", new Line("Really? That's amazing!", helpPick)),
                            new PossibleReply("Only rarely.", new Line("I see. Well, it's true there is a lot of other entertainment.",
                            new Line("But you still know enough to help me, right?", helpPick))),
                            new PossibleReply("Ew. I hate them.", new Line("What? B-but... but books are the love of my live.", 
                            new Line("You're a big mean jerk, you! Go away!",
                            new Line("...",
                            new Line("...um, I... may have overreacted a bit.",
                            new Line("...Would you be willing to start over again?"))))))

                        }))));
            return new Branch("Hello! I’m Daina ^^. Could we talk for a while?",
                new PossibleReply("Yes!", begin),
                new PossibleReply("Give me a minute.",
                    new Line("Alright. Let's talk in 60 seconds again, okay? :)", new ActionNode((os, fr) =>
                    {
                        fr.ScheduleWithDelay(TimeSpan.FromSeconds(55), async () =>
                        {
                           await fr.Memory.MoveConversationTo(new Line("Hello again! Long time no see :P.", begin), os);
                        });
                        fr.Memory.CurrentConversation = null;
                    }))),
                new PossibleReply("Nope.",
                    new Line("Oh, okay. Anyway, if you ever want to, just say ‘hi’, okay?"))
            );

        }
    }
}
