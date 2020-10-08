using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TalesOfSenylan.Models.Characters;
using TalesOfSenylan.Models.Utilities;

namespace TalesOfSenylan.Models.Dungeon
{
    public class Room
    {
        private Player Player { get; set; }
        public List<Enemy> Enemies { get; set; }

        private KeyboardState KeyboardState;
        public ContentManager ContentManager { get; }

        public Room(int dungeonNumber, Player player, ContentManager contentManager)
        {
            Player = player;
            Enemies = new List<Enemy>();
            ContentManager = contentManager;

            for (int i = 0; i < DungeonUtilities.GetNumberOfEnemies(dungeonNumber); i++)
            {
                Enemies.Add(new Enemy(GenerateRandomStartingPosition(), this));
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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Player.Draw(gameTime, spriteBatch);

            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(gameTime, spriteBatch);
            }
        }

        private static Vector2 GenerateRandomStartingPosition()
        {
            // Todo: position shouldn't be between 20 and 200 => make it respect dungeon bounds.
            int x = Utilities.Utilities.getRandomNumber(20, 200);
            int y = Utilities.Utilities.getRandomNumber(20, 200);

            return new Vector2(x, y);
        }

        //Player Attack Handling
        private int HandleAttack(GameTime gameTime)
        {
            return Player.GetDamagePoints(gameTime);
        }

        //Player Movement Handling
        private void HandleMovement(GameTime gameTime)
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
    }
}
