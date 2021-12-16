using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using Client.Composite.Iterator;

namespace Client.Composite
{
    public class RoomHub : Room
    {
        private readonly List<Room> roomList;
        public RoomHub(string name) : base(name, "")
        {
            roomList = new List<Room>();
        }

        public void AddRoom(Room room)
        {
            roomList.Add(room);
        }

        public void RemoveRoom(Room room)
        {
            roomList.Remove(room);
        }

        public List<Room> GetRooms()
        {
            return roomList;
        }

        public Room GetRoom(string name)
        {
            Iterator.Iterator roomIteLists = CreateIterator();

            while (roomIteLists.hasNext())
            {
                Room room = roomIteLists.next();

                if (room.GetName().Equals(name))
                {
                    return room;
                }
            }

            return null;
        }

        public override bool IsComposite()
        {
            return true;
        }

        public override void JoinRoom(HubConnection connection)
        {
        }

        public override void LeaveRoom(HubConnection connection)
        {
        }

        public override Iterator.Iterator CreateIterator()
        {
            return new RoomHubIterator(roomList);
        }
    }
}
