using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TalesOfSenylan.Models.Characters;

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
            Enemies.Add(new Enemy(this, StartPosition));
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
    }
}
