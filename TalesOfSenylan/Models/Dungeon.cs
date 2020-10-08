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

namespace TalesOfSenylan
{
    public class Dungeon
    {
        public Player Player { get; set; }
        public List<Enemy> Enemies { get; set; }
        private Vector2 StartPosition;

        private int DungeonNumber;
        private KeyboardState KeyboardState;

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
            KeyboardState = Keyboard.GetState();
            
            HandleMovement(gameTime);
            Player.Update(gameTime);
            Debug.WriteLine("Le joueur a: " + Player.Health + " Points de vie");

            //ToList() to make a copy of the list and remove an item safely from the original list
            foreach (Enemy enemy in Enemies.ToList())
            {
                if (Player.IsCollided(enemy.getHitbox()) && (KeyboardState.IsKeyDown(Keys.Space) || KeyboardState.IsKeyDown(Keys.K)))
                {
                    enemy.Health -= HandleAttack(gameTime);

                    if (enemy.Health <= 0)
                    {
                        Enemies.Remove(enemy);
                    }
                }
                enemy.Update(gameTime);
            }
        }

        //Player Movement Handling
        public void HandleMovement(GameTime gameTime)
        {
            if (KeyboardState.IsKeyDown(Keys.Up) || KeyboardState.IsKeyDown(Keys.W))
                Player.Position.Y -= Player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (KeyboardState.IsKeyDown(Keys.Down) || KeyboardState.IsKeyDown(Keys.S))
                Player.Position.Y += Player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (KeyboardState.IsKeyDown(Keys.Left) || KeyboardState.IsKeyDown(Keys.A))
                Player.Position.X -= Player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (KeyboardState.IsKeyDown(Keys.Right) || KeyboardState.IsKeyDown(Keys.D))
                Player.Position.X += Player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        //Player Attack Handling
        public int HandleAttack(GameTime gameTime)
        { 
            return Player.GetDamagePoints(gameTime);
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
