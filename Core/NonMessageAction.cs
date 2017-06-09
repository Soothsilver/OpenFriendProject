namespace Core
{
    public enum NonMessageAction
    {
        /// <summary>
        /// Sent when the friend starts the delay before a message.
        /// </summary>
        TypingOn,
        /// <summary>
        /// Sent when the friend ends the delay before a message.
        /// </summary>
        TypingOff,
        /// <summary>
        /// Sent when the friend starts processing a message.
        /// </summary>
        MarkSeen
    }
}