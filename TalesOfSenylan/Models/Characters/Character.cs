using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;
using TalesOfSenylan.Models.Dungeon;

namespace TalesOfSenylan.Models.Characters
{
    public abstract class Character
    {
        protected Texture2D sprite;
        public Vector2 position;
        public int maxHealth { get; set; }
        public int health { get; set; }
		public int maxMana { get; set; }
        public int mana { get; set; }
        public float speed { get; set; }
        protected RectangleF hitbox;
        protected Dungeon.Dungeon dungeon { get; }

        public Character(Dungeon.Dungeon dungeon, Vector2 initialPosition)
        {
            this.dungeon = dungeon;
            position = initialPosition;
        }

        public Character(Vector2 initialPosition)
        {
            position = initialPosition;
        }

        public RectangleF GetHitbox()
        {
            return hitbox;
        }

        public void SetHitbox(float x, float y)
		{
            hitbox.X = x - sprite.Width / 2;
            hitbox.Y = y - sprite.Height / 2;
		}

        //Function used to debug hitbox
        public void DrawHitbox(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(GetHitbox(), Color.Red);
        }

        public bool IsCollided(RectangleF E) => hitbox.Intersects(E);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    }
}
