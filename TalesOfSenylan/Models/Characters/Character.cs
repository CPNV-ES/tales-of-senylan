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
        protected Vector2 Position;
        protected float Speed = 100;
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

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice);

        public void DrawHitbox(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            //Debug for outline rect hitbox
            Texture2D t2;
            t2 = new Texture2D(graphicsDevice, 1, 1);
            t2.SetData<Color>(new Color[] { Color.White });
            spriteBatch.Draw(t2, new Rectangle((int)Hitbox.X, (int)Hitbox.Y, 1, (int)Hitbox.Height + 1), Color.Red);
            spriteBatch.Draw(t2, new Rectangle((int)Hitbox.X, (int)Hitbox.Y, (int)Hitbox.Width + 1, 1), Color.Red);
            spriteBatch.Draw(t2, new Rectangle((int)Hitbox.X + (int)Hitbox.Width, (int)Hitbox.Y, 1, (int)Hitbox.Height + 1), Color.Red);
            spriteBatch.Draw(t2, new Rectangle((int)Hitbox.X, (int)Hitbox.Y + (int)Hitbox.Height, (int)Hitbox.Width + 1, 1), Color.Red);
        }
    }
}
