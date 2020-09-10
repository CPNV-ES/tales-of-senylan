using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TalesOfSenylan
{
    public class Player
    {
        private Texture2D sprite;
        private Vector2 position;
        private float speed;

        private KeyboardState keyboardState;

        public Dungeon dungeon { get; }

        public Player(Dungeon dungeon, Vector2 position)
        {
            this.dungeon = dungeon;
            this.position = position;
            this.speed = 100;
            LoadContent();
        }

        public void LoadContent()
        {
            sprite = dungeon.content.Load<Texture2D>("ball");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                sprite,
                position,
                null,
                Color.White,
                0f,
                new Vector2(sprite.Width / 2, sprite.Height / 2),
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
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                position.Y -= speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                position.Y += speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                position.X -= speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                position.X += speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
