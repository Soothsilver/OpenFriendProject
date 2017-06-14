using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Conversation;
using Core.Conversations;

namespace Core.ProcessorCodes.Commands
{
    class AllCommands
    {
        public static List<Command> All = new List<Command>()
        {
            new Command("start", "Initiates the first conversation.", async (f,m,o)=>
            {
                await o.Speaking.SendMessage(f,
                    "Hello! Let's talk. Type '/help' for a list of commands or '/tips' to get see some phrases that I respond to well.");
            }),
            new Command("tips", "Says some random sentence that the friend responds to.", async(f,m,o)=>
            {
                var phrases = o.FreeformPhrases.All.GetRandoms(3).ToList();
                await o.Speaking.SendMessage(f,
                    "Type '/books' to start a conversation about books or '/alice' to start a freeform conversation with an AIML subsystem. Alternatively, try one of these phrases: " + Environment.NewLine + Environment.NewLine

                    + string.Join(Environment.NewLine, phrases.Select(phr => phr.Phrase)),
                    phrases.Select(phr => new QuickReply(phr.Phrase)).ToArray()
                    );
            }),
            new Command("alice", "Enters Alice mode.", async (f,m,o)=>
            {
                if (!f.Memory.TalkingToAlice)
                {
                    f.Memory.CurrentConversation = null;
                    f.Memory.TalkingToAlice = true;
                    await o.Speaking.SendMessage(f, "Alright! I'm initializing the Alice subsystem.");
                    await o.Speaking.SendMessage(f, "Alice... I sometimes don't make too much sense when using that subsystem.");
                    await o.Speaking.SendMessage(f, "It's a little embarrassing so please don't judge me.");
                    await o.Speaking.SendMessage(f, "And type 'exit' or 'quit' any time you want to end the subsystem!");
                    f.Memory.Alice = o.Aiml.CreateAliceFor(f);
                    await o.Speaking.SendMessage(f, "Done. Say hello to Alice!");
                }
                else
                {
                    await o.Speaking.SendSystemMessage(f, "You are already speaking to Alice. Type '/exit' to stop.");
                }
            }),
            new Command("ayt", "Are you there?", async (f,m,o)=>
            {
                await o.Speaking.SendMessage(f, "I am still here!");
            }),
            new Command("rename", "Renames the friend.", async (f,m,o)=>
            {
                if (m.Length > "/rename ".Length)
                {

                    f.Memory.Persistent.CommonName = m.Substring("/rename ".Length);
                    f.SavePersistentMemory();
                    await o.Speaking.SendMessage(f,
                        "Alright! I am " + f.Memory.Persistent.CommonName + " from now on! Good name!");
                }
                else
                {
                    await o.Speaking.SendSystemMessage(f, "Type '/rename [new-name]' to rename the friend.");
                }
            }),
            new Command("name", "Says her name.", async (f,m,o)=>
            {
                await o.Speaking.SendMessage(f, "My name is '" + f.Memory.Persistent.CommonName + "'.");
            }),
            new Command("exit", "Exits Alice mode.", async (f, m, o) =>
            {
                if (f.Memory.TalkingToAlice)
                {
                    f.Memory.TalkingToAlice = false;
                    f.Memory.Alice.Dispose();
                    await o.Speaking.SendMessage(f, "Phew.");
                    await o.Speaking.SendMessage(f, "Well, that was fun ^^.");
                }
                else
                {
                    await o.Speaking.SendSystemMessage(f, "You are currently not talking to Alice.");
                }
            }),
            new Command("books", "Start talking about books", async (f,m,o)=>
            {
                await f.Memory.MoveConversationTo(BookConversation.GetBeginning(), o);
            }),
            new Command("help", "Prints all commands.", async (f,s,o)=>
            {
                string all = string.Join("\n", All.OrderBy(cmd => cmd.Keyword).Select(cmd => cmd.Keyword + " - " + cmd.Description));
                await o.Speaking.SendSystemMessage(f, all);
            }),
            new Command("guid", "Prints the GUID of this friend.", async (f,s,o)=>
            {
                await o.Speaking.SendSystemMessage(f, f.Memory.Persistent.InternalId);
            }),
            new Command("merge", "Merges this IM account to another friend.", async (f, s, o) =>
            {
                await f.Memory.MoveConversationTo(MergeConversation.GetBeginning(), o);
            }),
            new Command("export", "Exports the full memory of the friend to a file.", async (f,s,o)=>
            {
                await o.Speaking.SendFile(f, MemoryStorage.GetFilename(f));
                await o.Speaking.SendMessage(f, "That's all the non-hardwired information I possess.");
            }),
            new Command("disconnect", "Type '/disconnect Facebook' or '/disconnect Telegram' to end the connection between that account and this friend.",
                async (f, m, o) =>
                {
                    if (m.Length > "/disconnect ".Length)
                    {

                        string what = m.Substring("/disconnect ".Length);
                        if (what.ToLower() == "facebook")
                        {
                            await o.Speaking.SendSystemMessage(f, "Disconnecting Facebook.");
                            f.Memory.Persistent.FacebookId = null;
                            f.SavePersistentMemory();
                        }
                        else if (what.ToLower() == "telegram")
                        {
                            await o.Speaking.SendSystemMessage(f, "Disconnecting Telegram.");
                            f.Memory.Persistent.TelegramId = 0;
                            f.SavePersistentMemory();
                        }
                        else
                        {
                            await o.Speaking.SendSystemMessage(f, "That service is not known.");
                        }
                    }
                    else
                    {
                        await o.Speaking.SendSystemMessage(f, "Type '/disconnect Facebook' or '/disconnect Telegram' to end the connection between that account and this friend.");
                    }
                }),
            new Command("setlocale", "Changes the friend's real-world location and your timezone.", async (f,s,o)=>
            {
                await f.Memory.MoveConversationTo(o.LoadedConversations.All["set-locale"], o);
            }),
            new Command("bio", "Says basic vital stats.", async (f,s,o)=>
            {
                await o.Speaking.SendMessage(f, "Hello! My name is " + f.Memory.Persistent.CommonName + ".");
                await o.Speaking.SendMessage(f, "My ID is " + f.Memory.Persistent.InternalId + ".");
                await o.Speaking.SendMessage(f, "My caretaker is my " + f.Memory.Persistent.CaretakerName + ".");
                await o.Speaking.SendMessage(f, "I live in " + f.Memory.Persistent.Country + ".");
                await o.Speaking.SendMessage(f,
                    "My time offset from Open Friend Project servers is " +
                    f.Memory.Persistent.CaretakersClockHasPlusThisManyHours + " hours.");
            })
        };
    }
}
