using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TalesOfSenylan.Models.Dungeon
{
    public class Dungeon
    {
        private readonly Room currentRoom;
        private readonly Vector2 startPosition;

        public Dungeon(IServiceProvider serviceProvider, int dungeonNumber, int width, int height)
        {
            rooms = new List<List<Room>>();
            startPosition = new Vector2(200, 200);
            content = new ContentManager(serviceProvider, "Content");
            this.dungeonNumber = dungeonNumber;

            player = new Player(this, startPosition);

            this.width = width;
            this.height = height;

            for (var i = 0; i < height; i++)
            {
                var roomsRow = new List<Room>();
                for (var j = 0; j < width; j++)
                {
                    var r = new Room(new Vector2(j, i), dungeonNumber, player, content);
                    roomsRow.Add(r);
                }

                rooms.Add(roomsRow);
            }

            startingRoom = GetRandomRoom();
            VisitRoom(startingRoom);

            currentRoom = startingRoom;
        }

        private Player player { get; }
        public List<List<Room>> rooms { get; set; }

        private int dungeonNumber { get; }

        public ContentManager content { get; set; }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            currentRoom.Draw(gameTime, spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            currentRoom.Update(gameTime);
        }

        #region Only used by the maze generation algorithm

        private int width { get; }
        private int height { get; }

        private readonly Room startingRoom;

        private readonly Stack<Room> lastRooms = new Stack<Room>();

        #endregion

        #region Only used by the maze generation algorithm

        private void VisitRoom(Room room)
        {
            if (!room.visited) lastRooms.Push(room);

            room.visited = true;

            var adjacentUnvisitedRooms = GetAdjacentUnvisitedRooms(room);
            if (adjacentUnvisitedRooms.Count > 0)
            {
                var selectedExit = GetRandomAdjacentUnvisitedRoom(room);

                var cp = GetExitFromRoom(room, selectedExit);

                room.exits.Remove(cp);
                room.exits.Add(cp, selectedExit);

                selectedExit.exits.Remove(cp.Opposite());
                selectedExit.exits.Add(cp.Opposite(), room);

                VisitRoom(selectedExit);
            }
            else if (lastRooms.Count > 0 && startingRoom.Equals(lastRooms.Peek()))
            {
            }
            else
            {
                if (lastRooms.Count > 0)
                {
                    lastRooms.Pop();
                    VisitRoom(lastRooms.Peek());
                }
            }
        }

        private CardinalPoint GetExitFromRoom(Room startingRoom, Room targetRoom)
        {
            if (startingRoom.position.X.Equals(targetRoom.position.X))
            {
                if (startingRoom.position.Y - targetRoom.position.Y == 1) return CardinalPoint.NORTH;

                if (startingRoom.position.Y - targetRoom.position.Y == -1) return CardinalPoint.SOUTH;
            }
            else if (startingRoom.position.Y.Equals(targetRoom.position.Y))
            {
                if (startingRoom.position.X - targetRoom.position.X == 1) return CardinalPoint.WEST;

                if (startingRoom.position.X - targetRoom.position.X == -1) return CardinalPoint.EAST;
            }

            return CardinalPoint.NORTH; // This means there is an error (potential TODO)
        }

        private Room GetRandomAdjacentUnvisitedRoom(Room room)
        {
            var adjacentUnvisitedRooms = GetAdjacentUnvisitedRooms(room);
            return adjacentUnvisitedRooms[Utilities.Utilities.GetRandomNumber(0, adjacentUnvisitedRooms.Count)];
        }

        private List<Room> GetAdjacentUnvisitedRooms(Room room)
        {
            var adjacentUnvisitedRooms = new List<Room>();

            var adjacentRooms = GetAdjacentRoomsOfRoom(room);
            foreach (var adjacentRoom in adjacentRooms)
                if (!adjacentRoom.visited)
                    adjacentUnvisitedRooms.Add(adjacentRoom);

            return adjacentUnvisitedRooms;
        }

        private Room GetRandomRoom()
        {
            var randomX = Utilities.Utilities.GetRandomNumber(0, width);
            var randomY = Utilities.Utilities.GetRandomNumber(0, height);

            return rooms[randomX][randomY];
        }

        private List<Room> GetAdjacentRoomsOfRoom(Room room)
        {
            var adjacentRooms = new List<Room>();

            foreach (var roomsRow in rooms)
            foreach (var c in roomsRow)
                if (IsAdjacent(c, room))
                    adjacentRooms.Add(c);

            return adjacentRooms;
        }

        private static bool IsAdjacent(Room c1, Room c2)
        {
            if (AreOnDifferentColumnsAndRows(c1, c2)) return false;

            return IsAdjacentX(c1, c2) || IsAdjacentY(c1, c2);
        }

        private static bool IsAdjacentX(Room c1, Room c2)
        {
            if (c1.position.X + 1 == c2.position.X || c1.position.X - 1 == c2.position.X) return true;

            return false;
        }

        private static bool IsAdjacentY(Room c1, Room c2)
        {
            if (c1.position.Y + 1 == c2.position.Y || c1.position.Y - 1 == c2.position.Y) return true;

            return false;
        }

        private static bool AreOnDifferentColumnsAndRows(Room c1, Room c2)
        {
            return c1.position.X != c2.position.X && c1.position.Y != c2.position.Y;
        }

        #endregion
    }
}