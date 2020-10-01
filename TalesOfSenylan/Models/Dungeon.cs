using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TalesOfSenylan.Models.Characters;
using TalesOfSenylan.Models.Utilities;

namespace TalesOfSenylan
{
    public class Dungeon
    {
        public Player Player { get; set; }
        public List<Enemy> Enemies { get; set; }
        private Vector2 StartPosition;

        private int DungeonNumber;

        public ContentManager Content { get; }

        public Dungeon(IServiceProvider serviceProvider, int dungeonNumber)
        {
            Content = new ContentManager(serviceProvider, "Content");
            DungeonNumber = dungeonNumber;
            StartPosition = new Vector2(200, 200);
            InitializeLevel();
        }
        
        private void InitializeLevel()
        {
            Player = new Player(this, StartPosition);
            Enemies = new List<Enemy>();
            // Todo: right now, the number of enemies per level is (DungeonNumber * 5) => make it more "intelligent"
            // I tried to do it in an Enum before (by hard-coding number of enemies for each level), but didn't find much success.
            for (int i = 0; i < this.DungeonNumber * 5; i++)
            {
                Enemies.Add(new Enemy(this, GenerateRandomStartingPosition()));
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Player.Draw(gameTime, spriteBatch);
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(gameTime, spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            Player.Update(gameTime);
            foreach (Enemy enemy in Enemies)
            {
                enemy.Update(gameTime);
            }
        }

        public static Vector2 GenerateRandomStartingPosition()
        {
            // Todo: position shouldn't be between 20 and 200 => make it respect dungeon bounds.
            int x = Utilities.getRandomNumber(20, 200);
            int y = Utilities.getRandomNumber(20, 200);

            return new Vector2(x, y);
        }
    }
}
