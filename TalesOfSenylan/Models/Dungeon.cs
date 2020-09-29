using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;
using System.Diagnostics;
using TalesOfSenylan.Models.Characters;

namespace TalesOfSenylan
{
    public class Dungeon
    {
        public Player Player { get; set; }
        public Enemy Enemy { get; set; }
        private Vector2 StartPosition;

        private int DungeonNumber;
        private KeyboardState KeyboardState;

        public ContentManager Content { get; }

        public Dungeon(IServiceProvider serviceProvider, int dungeonNumber)
        {
            Content = new ContentManager(serviceProvider, "Content");
            DungeonNumber = dungeonNumber;
            StartPosition = new Vector2(250, 250);
            InitializeLevel();
        }
        
        private void InitializeLevel()
        {
            Player = new Player(this, StartPosition);
            Enemy = new Enemy(this, new Vector2(400, 400));
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Player.Draw(gameTime, spriteBatch);
            Enemy.Draw(gameTime, spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState = Keyboard.GetState();

            if (Player.IsCollided(Enemy.getHitbox()) && KeyboardState.IsKeyDown(Keys.Space))
                HandleAttack(gameTime);
               
            HandleMovement(gameTime);

            Player.Update(gameTime);
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
        public void HandleAttack(GameTime gameTime)
        {
            if (KeyboardState.IsKeyDown(Keys.Space))
                Player.DoDamage(gameTime);
        }
    }
}
