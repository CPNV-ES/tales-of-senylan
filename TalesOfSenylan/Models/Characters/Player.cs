using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TalesOfSenylan.Models.Characters;

namespace TalesOfSenylan
{

    public class Player : Character
    {
        private KeyboardState KeyboardState;

        public Player(Dungeon dungeon, Vector2 initialPosition) : base(dungeon, initialPosition)
        {
            LoadContent();
        }

        public void LoadContent()
        {
            Sprite = Dungeon.Content.Load<Texture2D>("ball");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
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
                0f
            );
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

        public new bool Collide(Collidable collidable)
        {
            return true;
        }
    }
}
