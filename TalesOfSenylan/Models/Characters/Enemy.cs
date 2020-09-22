using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace TalesOfSenylan.Models.Characters
{
    public class Enemy : Character
    {

        public Enemy(Dungeon dungeon, Vector2 position) : base(dungeon, position)
        {
            LoadContent();
            Hitbox = new RectangleF(Position.X - Sprite.Width / 2, Position.Y - Sprite.Height / 2, Sprite.Width, Sprite.Height);
        }

        public void LoadContent()
        {
            Sprite = Dungeon.Content.Load<Texture2D>("orc");
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
                0.1f
            );

            DrawHitbox(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {

        }
    }
}
