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
        private Player Player { get; set; }
        private Vector2 StartPosition;
        public List<Room> Rooms { get; set; }
        private Room CurrentRoom;

        private int DungeonNumber;

        public ContentManager Content { get; }

        public Dungeon(IServiceProvider serviceProvider, int dungeonNumber)
        {
            StartPosition = new Vector2(200, 200);
            Content = new ContentManager(serviceProvider, "Content");
            DungeonNumber = dungeonNumber;
            InitializeLevel();
        }
        
        private void InitializeLevel()
        {
            Player = new Player(this, StartPosition);
            Rooms = new List<Room>();

            Rooms.Add(new Room(DungeonNumber, Player, Content));
            CurrentRoom = Rooms[0];
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            CurrentRoom.Draw(gameTime, spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            CurrentRoom.Update(gameTime);
        }
    }
}
