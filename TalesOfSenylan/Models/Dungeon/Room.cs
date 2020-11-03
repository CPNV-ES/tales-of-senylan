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
        private Player player { get; set; }
        public List<Enemy> enemies { get; set; }

        private KeyboardState keyboardState;
        public ContentManager contentManager { get; }

        public Room(int dungeonNumber, Player player, ContentManager contentManager)
        {
            this.player = player;
            enemies = new List<Enemy>();
            this.contentManager = contentManager;

            for (int i = 0; i < DungeonUtilities.GetNumberOfEnemies(dungeonNumber); i++)
            {
                enemies.Add(new Enemy(GenerateRandomStartingPosition(), this));
            }
        }

        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            HandleMovement(gameTime);
            player.Update(gameTime);
            Debug.WriteLine("Le joueur a: " + player.health + " Points de vie");

            //ToList() to make a copy of the list and remove an item safely from the original list
            foreach (Enemy enemy in enemies.ToList())
            {
                if (player.IsCollided(enemy.GetHitbox()) && (keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.K)))
                {
                    enemy.health -= HandleAttack(gameTime);

                    if (enemy.health <= 0)
                    {
                        enemies.Remove(enemy);
                    }
                }
                enemy.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            player.Draw(gameTime, spriteBatch);

            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(gameTime, spriteBatch);
            }
        }

        private static Vector2 GenerateRandomStartingPosition()
        {
            int x = Utilities.Utilities.GetRandomNumber(20, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width);
            int y = Utilities.Utilities.GetRandomNumber(20, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

            return new Vector2(x, y);
        }

        //player Attack Handling
        private int HandleAttack(GameTime gameTime)
        {
            return player.GetDamagePoints(gameTime);
        }

        //player Movement Handling
        private void HandleMovement(GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                player.position.Y -= player.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                player.position.Y += player.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                player.position.X -= player.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                player.position.X += player.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
