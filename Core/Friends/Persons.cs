using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class Persons
    {
        private Overseer overseer;

        private List<Friend> Friends = new List<Friend>();
        
        public Persons(Overseer overseer)
        {
            this.overseer = overseer;
            this.Reload();
        }

        public Friend GetFriendFromFacebookId(string id)
        {
            lock (Friends) {
                foreach (var friend in Friends)
                {
                    if (friend.Memory.Persistent.FacebookId == id)
                    {
                        return friend;
                    }
                }
                Friend f = new Friend(overseer);
                f.Memory.Persistent.FacebookId = id;
                Friends.Add(f);
                return f;
            }
        }
        public Friend GetFriendFromTelegramId(int fromId)
        {
            lock (Friends)
            {
                foreach (var friend in Friends)
                {
                    if (friend.Memory.Persistent.TelegramId == fromId)
                    {
                        return friend;
                    }
                }
                Friend f = new Friend(overseer);
                f.Memory.Persistent.TelegramId = fromId;
                Friends.Add(f);
                return f;
            }
        }

        public void Reload()
        {
            lock (Friends)
            {
                this.Friends.Clear();
                var memories = MemoryStorage.LoadFriendData();
                foreach (var memory in memories)
                {
                    Friends.Add(new Core.Friend(memory, overseer));
                }
            }
        }

        public IEnumerable<Friend> GetAllFriends()
        {
            List<Friend> copy = new List<Core.Friend>();
            lock (Friends)
            {
                copy = Friends.ToList();
            }
            return copy;
        }

        public void CreateFriend(Friend friend)
        {
            lock (Friends)
            {
                Friends.Add(friend);
            }
        }

       
    }
}