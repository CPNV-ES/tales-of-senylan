using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace TalesOfSenylan.Models.Characters
{
    public abstract class Character
    {
        protected RectangleF hitbox;
        public Vector2 position;
        protected Texture2D sprite;

        public Character(Dungeon.Dungeon dungeon, Vector2 initialPosition)
        {
            this.dungeon = dungeon;
            position = initialPosition;
        }

        public Character(Vector2 initialPosition)
        {
            position = initialPosition;
        }

        public int maxHealth { get; set; }
        public int health { get; set; }
        public int maxMana { get; set; }
        public int mana { get; set; }
        public float speed { get; set; }
        protected Dungeon.Dungeon dungeon { get; }

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

        public bool IsCollided(RectangleF E)
        {
            return hitbox.Intersects(E);
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}