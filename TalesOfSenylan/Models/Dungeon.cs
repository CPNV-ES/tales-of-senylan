using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TalesOfSenylan
{
    public class Dungeon
    {
        public Player player;
        private Vector2 start;

        private int dungeonNumber;

        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        public Dungeon(IServiceProvider serviceProvider, int dungeonNumber)
        {
            content = new ContentManager(serviceProvider, "Content");
            this.dungeonNumber = dungeonNumber;
            this.start = new Vector2(250, 250);
            InitializeLevel();
        }
        
        private void InitializeLevel()
        {
            this.player = new Player(this, start);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            player.Draw(gameTime, spriteBatch);
        }

        public void HandleInput(GameTime gameTime, KeyboardState keyboardState)
        {
            player.HandleInput(gameTime, keyboardState);
        }
    }
}
