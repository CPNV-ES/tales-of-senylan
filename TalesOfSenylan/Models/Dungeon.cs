using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;
using System.Diagnostics;
using TalesOfSenylan.Models.Characters;

namespace TalesOfSenylan
{
    public class Dungeon
    {
        public Player Player { get; set; }
        public Enemy Enemy { get; set; }
        private Vector2 StartPosition;

        private bool spacePressed = false;
        private int DungeonNumber;
        private KeyboardState KeyboardState;

        public ContentManager Content { get; }

        public Dungeon(IServiceProvider serviceProvider, int dungeonNumber)
        {
            Content = new ContentManager(serviceProvider, "Content");
            DungeonNumber = dungeonNumber;
            StartPosition = new Vector2(250, 250);
            InitializeLevel();
        }
        
        private void InitializeLevel()
        {
            Player = new Player(this, StartPosition);
            Player.state = State.Idle;
            Enemy = new Enemy(this, new Vector2(400, 400));
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Player.Draw(gameTime, spriteBatch);
            Enemy.Draw(gameTime, spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState = Keyboard.GetState();

            if (KeyboardState.GetPressedKeyCount() > 0)
                Player.state = State.Walking;
            else
                Player.state = State.Idle;
            
            if (Player.IsCollided(Enemy.getHitbox()) && KeyboardState.IsKeyDown(Keys.Space) && !spacePressed)
                Player.state = State.Attacking;
                spacePressed = true;
			
            if (KeyboardState.IsKeyUp(Keys.Space))
                spacePressed = false;

            Player.Update(gameTime);
        }
    }
}
