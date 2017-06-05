using System.Collections.Generic;

namespace Core
{
    public class Persons
    {
        private Overseer overseer;

        private Dictionary<string, Friend> Friends = new Dictionary<string, Friend>();
        public Persons(Overseer overseer)
        {
            this.overseer = overseer;
        }

        public Friend GetFriend(string id)
        {
            if (!Friends.ContainsKey(id))
            {
                Friends[id] = new Core.Friend(id, this.overseer);
            }
            return Friends[id];
        }
    }
}