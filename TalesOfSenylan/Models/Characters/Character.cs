using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;


namespace TalesOfSenylan.Models.Characters
{
    public abstract class Character
    {
        protected Texture2D Sprite;
        public Vector2 Position;
        public float Speed { get; set; }
        protected RectangleF Hitbox;
        protected Dungeon Dungeon { get; }

        public Character(Dungeon dungeon, Vector2 initialPosition)
        {
            Dungeon = dungeon;
            Position = initialPosition;

        }
        public RectangleF getHitbox()
        {
            return Hitbox;
        }

        public void setHitbox(float x, float y)
		{
            Hitbox.X = x - Sprite.Width / 2;
            Hitbox.Y = y - Sprite.Height / 2;
		}

        //Function used to debug hitbox
        public void DrawHitbox(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(getHitbox(), Color.Red);
        }

        public bool IsCollided(RectangleF E) => Hitbox.Intersects(E);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    }
}
