using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TalesOfSenylan.Models.Characters;
using TalesOfSenylan.Models.Utilities;

namespace TalesOfSenylan.Models.Dungeon
{
    public class Dungeon
    {
        #region Only used by the maze generation algorithm
        private int width { get; set; }
        private int height { get; set; }
        
        private Room startingRoom;
        
        private Stack<Room> lastRooms = new Stack<Room>();
        #endregion
        
        private Player player { get; set; }
        private Vector2 startPosition;
        public List<List<Room>> rooms { get; set; }
        private Room currentRoom;

        private int dungeonNumber { get; set; }

        public ContentManager content { get; set; }

        public Dungeon(IServiceProvider serviceProvider, int dungeonNumber, int width, int height)
        {
            rooms = new List<List<Room>>();
            startPosition = new Vector2(200, 200);
            content = new ContentManager(serviceProvider, "Content");
            this.dungeonNumber = dungeonNumber;
            
            player = new Player(this, startPosition);
            
            this.width = width;
            this.height = height;
            
            for (int i = 0; i < height; i++)
            {
                List<Room> roomsRow = new List<Room>();
                for (int j = 0; j < width; j++)
                {
                    Room r = new Room(new Vector2(j, i), dungeonNumber, player, content);
                    roomsRow.Add(r);
                }
                rooms.Add(roomsRow);
            }
            
            startingRoom = GetRandomRoom();
            VisitRoom(startingRoom);

            currentRoom = startingRoom;
        }
        
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            currentRoom.Draw(gameTime, spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            currentRoom.Update(gameTime);
        }
        
        #region Only used by the maze generation algorithm
        private void VisitRoom(Room room)
        {
            if (!room.visited)
            {
                lastRooms.Push(room);
            }
            room.visited = true;
            
            List<Room> adjacentUnvisitedRooms = GetAdjacentUnvisitedRooms(room);
            if (adjacentUnvisitedRooms.Count > 0)
            {
                // S'il y a au moins une possibilité, on en choisit une au hasard, on ouvre le mur et on recommence avec la nouvelle roomule. 
                Room selectedExit = GetRandomAdjacentUnvisitedRoom(room);

                CardinalPoint cp = GetExitFromRoom(room, selectedExit);
                
                room.exits.Remove(cp);
                room.exits.Add(cp, selectedExit);

                selectedExit.exits.Remove(cp.Opposite());
                selectedExit.exits.Add(cp.Opposite(), room);
                
                VisitRoom(selectedExit);
            }
            else if (lastRooms.Count > 0 && startingRoom.Equals(lastRooms.Peek()))
            {
                return;
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
                if (startingRoom.position.Y - targetRoom.position.Y == 1)
                {
                    return CardinalPoint.NORTH;
                }
                if (startingRoom.position.Y - targetRoom.position.Y == -1)
                {
                    return CardinalPoint.SOUTH;
                }
            }
            else if (startingRoom.position.Y.Equals(targetRoom.position.Y))
            {
                if (startingRoom.position.X - targetRoom.position.X == 1)
                {
                    return CardinalPoint.WEST;
                }
                if (startingRoom.position.X - targetRoom.position.X == -1)
                {
                    return CardinalPoint.EAST;
                }
            }

            return CardinalPoint.NORTH; // This means there is an error (potential TODO)
        }

        private Room GetRandomAdjacentUnvisitedRoom(Room room)
        {
            List<Room> adjacentUnvisitedRooms = GetAdjacentUnvisitedRooms(room);
            return adjacentUnvisitedRooms[Utilities.Utilities.GetRandomNumber(0, adjacentUnvisitedRooms.Count)];
        }

        private List<Room> GetAdjacentUnvisitedRooms(Room room)
        {
            List<Room> adjacentUnvisitedRooms = new List<Room>();

            List<Room> adjacentRooms = GetAdjacentRoomsOfRoom(room);
            foreach (Room adjacentRoom in adjacentRooms)
            {
                if (!adjacentRoom.visited)
                {
                    adjacentUnvisitedRooms.Add(adjacentRoom);
                }
            }
            
            return adjacentUnvisitedRooms;
        }

        private Room GetRandomRoom()
        {
            int randomX = Utilities.Utilities.GetRandomNumber(0, width);
            int randomY = Utilities.Utilities.GetRandomNumber(0, height);

            return rooms[randomX][randomY];
        }

        private List<Room> GetAdjacentRoomsOfRoom(Room room)
        {
            List<Room> adjacentRooms = new List<Room>();
            
            foreach (List<Room> roomsRow in rooms)
            {
                foreach (Room c in roomsRow)
                {
                    if (IsAdjacent(c, room))
                    {
                        adjacentRooms.Add(c);
                    }
                }
            }
            
            return adjacentRooms;
        }

        private static bool IsAdjacent(Room c1, Room c2)
        {
            if (AreOnDifferentColumnsAndRows(c1, c2))
            {
                return false;
            }
            
            return IsAdjacentX(c1, c2) || IsAdjacentY(c1, c2);
        }

        private static bool IsAdjacentX(Room c1, Room c2)
        {
            if (c1.position.X + 1 == c2.position.X || c1.position.X - 1 == c2.position.X)
            {
                return true;
            }

            return false;
        }
        
        private static bool IsAdjacentY(Room c1, Room c2)
        {
            if (c1.position.Y + 1 == c2.position.Y || c1.position.Y - 1 == c2.position.Y)
            {
                return true;
            }
            
            return false;
        }

        private static bool AreOnDifferentColumnsAndRows(Room c1, Room c2)
        {
            return c1.position.X != c2.position.X && c1.position.Y != c2.position.Y;
        }
        #endregion
    }
}
