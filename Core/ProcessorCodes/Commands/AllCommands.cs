using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Conversations;

namespace Core.ProcessorCodes.Commands
{
    class AllCommands
    {
        public static List<Command> All = new List<Command>()
        {
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
                string all = string.Join("\n", All.Select(cmd => cmd.Keyword + " - " + cmd.Description));
                await o.Speaking.SendSystemMessage(f, all);
            }),
            new Command("guid", "Prints the GUID of this friend.", async (f,s,o)=>
            {
                await o.Speaking.SendSystemMessage(f, f.Memory.Persistent.InternalId);
            }),
            new Command("merge", "Merges this IM account to another friend.", async (f, s, o) =>
            {
                await f.Memory.MoveConversationTo(MergeConversation.GetBeginning(), o);
            })
        };
    }
}
