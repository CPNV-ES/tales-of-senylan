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

        public Dungeon Dungeon
        {
            get { return dungeon; }
        }
        Dungeon dungeon;

        public Player(Dungeon dungeon, Vector2 position)
        {
            this.dungeon = dungeon;
            this.position = position;
            this.speed = 100;
            LoadContent();
        }

        public void LoadContent()
        {
            sprite = Dungeon.Content.Load<Texture2D>("ball");
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
        
        public void HandleInput(GameTime gameTime, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Up))
                position.Y -= speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Down))
                position.Y += speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Left))
                position.X -= speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Right))
                position.X += speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
        }

    }
}
