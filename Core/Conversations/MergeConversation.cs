using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Conversation;
using Core.ProcessorCodes;

namespace Core.Conversations
{
    class MergeConversation
    {
        public static ConversationNode GetBeginning()
        {
            var enterGuid =
                new Line("Type in the GUID of the friend you wish to merge with this account now.",
                    new ActionNode((o, f) =>
                    {
                        f.AddTemporaryProcessor(new MergeGuidProcessorCode(o));
                    }));
            return
                new Line("Hello there, {you}!",
                    new Branch("Is this the first time you're talking to me?",
                        new PossibleReply("Yes",
                            new Branch("Then I'm happy to meet you! Would you like to talk about books?",
                                new PossibleReply("Yes, I like books.",
                                    new Line("Awesome!", BookConversation.GetBeginning)),
                                new PossibleReply("Not now.",
                                    new Line(
                                        "Okay. Anyway, I'm happy to meet you^^. Type '/help' if you want to explore options."))
                            )),
                        new PossibleReply("No",
                            new Branch("Do you want to connect to a virtual friend you've spoken to before?",
                                new PossibleReply("Yes, please",
                                    new Branch("Do you know the friend's GUID identifier?",
                                        new PossibleReply("Yes.", enterGuid),
                                        new PossibleReply("What is it?",
                                            new Line("It's an alphanumeric code. It uniquely identifies her.",
                                            new Line("To determine it, type '/guid' when talking to her.",
                                            new Branch("Are you read to enter the GUID now?",
                                                new PossibleReply("Yes", enterGuid),
                                                new PossibleReply("No",
                                                            new Line("Okay. Type '/help' if you want to explore other options.")))
                                                            ))))),

                                new PossibleReply("No",
                                    new Line("Okay. Type '/help' if you want to explore other options."))))));
        }
    }
}
