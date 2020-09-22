using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TalesOfSenylan.Models.Characters;
using MonoGame.Extended;
using System;
using System.Diagnostics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Collisions;

namespace TalesOfSenylan
{

    public class Player : Character
    {
        private KeyboardState KeyboardState;

        public Player(Dungeon dungeon, Vector2 initialPosition) : base(dungeon, initialPosition)
        {
            LoadContent();
            Hitbox = new RectangleF(Position.X - Sprite.Width / 2, Position.Y - Sprite.Height / 2, Sprite.Width, Sprite.Height);

        }

        public void LoadContent()
        {
            Sprite = Dungeon.Content.Load<Texture2D>("ball");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Sprite,
                Position,
                null,
                Color.White,
                0f,
                new Vector2(Sprite.Width / 2, Sprite.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0.0f
            );

            DrawHitbox(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            Hitbox.X = Position.X - Sprite.Width / 2;
            Hitbox.Y = Position.Y - Sprite.Height / 2;
        }

        private void HandleInput(GameTime gameTime)
        {
            KeyboardState = Keyboard.GetState();

            // Keys Input 
            if (KeyboardState.IsKeyDown(Keys.Up) || KeyboardState.IsKeyDown(Keys.W))
                Position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (KeyboardState.IsKeyDown(Keys.Down) || KeyboardState.IsKeyDown(Keys.S))
                Position.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (KeyboardState.IsKeyDown(Keys.Left) || KeyboardState.IsKeyDown(Keys.A))
                Position.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (KeyboardState.IsKeyDown(Keys.Right) || KeyboardState.IsKeyDown(Keys.D))
                Position.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void DoDamage()
        {
            Debug.WriteLine("ATTACK");
        }
    }
}
