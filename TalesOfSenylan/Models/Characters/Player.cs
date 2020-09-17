using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TalesOfSenylan.Models.Characters;
using MonoGame.Extended;
using System;
using System.Diagnostics;

namespace TalesOfSenylan
{

    public class Player : Character
    {
        private KeyboardState KeyboardState;
        private RectangleF rF;

        public Player(Dungeon dungeon, Vector2 initialPosition) : base(dungeon, initialPosition)
        {
            LoadContent();
        }

        public void LoadContent()
        {
            Sprite = Dungeon.Content.Load<Texture2D>("ball");
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            Texture2D t2;

            rF = new RectangleF(Position.X - Sprite.Width/2, Position.Y - Sprite.Height/2, Sprite.Width, Sprite.Height);

            spriteBatch.Draw(
                Sprite,
                Position,
                null,
                Color.White,
                0f,
                new Vector2(Sprite.Width / 2, Sprite.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );


            //Debug for outline rect hitbox
            t2 = new Texture2D(graphicsDevice, 1, 1);
            t2.SetData<Color>(new Color[] { Color.White });
            spriteBatch.Draw(t2, new Rectangle((int)rF.X, (int)rF.Y, 1, (int)rF.Height + 1), Color.Red);
            spriteBatch.Draw(t2, new Rectangle((int)rF.X, (int)rF.Y, (int)rF.Width + 1, 1), Color.Red);
            spriteBatch.Draw(t2, new Rectangle((int)rF.X + (int)rF.Width, (int)rF.Y, 1, (int)rF.Height + 1), Color.Red);
            spriteBatch.Draw(t2, new Rectangle((int)rF.X, (int)rF.Y + (int)rF.Height, (int)rF.Width + 1, 1), Color.Red);
        }

        public void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
        }
        
        private void HandleInput(GameTime gameTime)
        {
            KeyboardState = Keyboard.GetState();

            if (KeyboardState.IsKeyDown(Keys.Up) || KeyboardState.IsKeyDown(Keys.W))
                Position.Y -= Speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (KeyboardState.IsKeyDown(Keys.Down) || KeyboardState.IsKeyDown(Keys.S))
                Position.Y += Speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (KeyboardState.IsKeyDown(Keys.Left) || KeyboardState.IsKeyDown(Keys.A))
                Position.X -= Speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (KeyboardState.IsKeyDown(Keys.Right) || KeyboardState.IsKeyDown(Keys.D))
                Position.X += Speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
        }

    }
}
