using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace TalesOfSenylan.Models.Items
{
    public class Chest
    {
        private Vector2 position;
        public List<Item> items { get; set; }
        public RectangleF hitbox;
        private Texture2D sprite;
        private Dungeon.Dungeon dungeon;

        public Chest(Vector2 position, Dungeon.Dungeon dungeon)
        {
            this.position = position;
            this.dungeon = dungeon;
            LoadContent();
            items = new List<Item>();
            items.Add(new Potion("Health Potion (50)"));
            
            hitbox.X = position.X - sprite.Width / 2;
            hitbox.Y = position.Y - sprite.Height / 2;
        }
        
        public void LoadContent()
        {
            sprite = dungeon.content.Load<Texture2D>("chest");
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
                0.0f
            );

            //DrawHitbox(spriteBatch);
        }

        public void Destroy()
        {
            dungeon.player.inventory.AddItem(items.ToArray());
            items = new List<Item>();
        }
        
    }
}