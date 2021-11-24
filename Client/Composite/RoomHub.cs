using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Composite
{
    public class RoomHub : Room
    {
        List<Room> roomList;
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
            foreach(var room in roomList)
            {
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
    }
}
