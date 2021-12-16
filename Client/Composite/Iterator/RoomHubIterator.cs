using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Composite.Iterator
{
    public class RoomHubIterator: Iterator
    {
        private List<Room> roomList;
        int position = 0;

        public RoomHubIterator(List<Room> roomList)
        {
            this.roomList = roomList;
        }

        public Room next()
        {
            Room room = roomList[position];
            position = position + 1;

            return room;
        }

        public bool hasNext()
        {
            if (position >= roomList.Count || roomList[position] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Room first()
        {
            if (roomList[0] != null)
            {
                return roomList[0];
            }

            return null;
        }

        public Room currentItem()
        {
            if (roomList[position] != null)
            {
                return roomList[position];
            }

            return null;
        }
    }

}
