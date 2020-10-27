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
        private Player player { get; set; }
        private Vector2 startPosition;
        public List<Room> rooms { get; set; }
        private Room currentRoom;

        private int dungeonNumber;

        public ContentManager content { get; }

        public Dungeon(IServiceProvider serviceProvider, int dungeonNumber)
        {
            startPosition = new Vector2(200, 200);
            content = new ContentManager(serviceProvider, "Content");
            this.dungeonNumber = dungeonNumber;
            InitializeLevel();
        }
        
        private void InitializeLevel()
        {
            player = new Player(this, startPosition);
            rooms = new List<Room>();

            rooms.Add(new Room(dungeonNumber, player, content));
            currentRoom = rooms[0];
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            currentRoom.Draw(gameTime, spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            currentRoom.Update(gameTime);
        }
    }
}
