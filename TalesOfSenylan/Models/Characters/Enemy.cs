using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Diagnostics;
using TalesOfSenylan.Models.Dungeon;

namespace TalesOfSenylan.Models.Characters
{
    public class Enemy : Character
    {
        private Room room;

        private float movementDuration;
        private TimeSpan currentMovementDuration;
        
        private bool hasMovedLeft = false;
        private bool hasMovedRight = false;
        private bool hasMovedTop = false;
        private bool hasMovedBottom = false;

        private TimeSpan lastDirectionChangeTime;

        public Enemy(Vector2 position, Room room) : base(position)
        {
            this.room = room;
            movementDuration = Utilities.Utilities.GetRandomNumber(1, 3);
            currentMovementDuration = new TimeSpan();
            maxHealth = health = 200;
            speed = 100;
            LoadContent();
            hitbox = new RectangleF(base.position.X - sprite.Width / 2, base.position.Y - sprite.Height / 2, sprite.Width, sprite.Height);
        }

        public void LoadContent()
        {
            sprite = room.contentManager.Load<Texture2D>("orc");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
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
                0.1f
            );

            //DrawHitbox(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            currentMovementDuration = currentMovementDuration.Add(gameTime.ElapsedGameTime);
            
            if (!hasMovedRight)
            {
                position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (ShouldChangeDirection(gameTime))
                {
                    hasMovedRight = true;
                    lastDirectionChangeTime = gameTime.TotalGameTime;
                }
            } else if (!hasMovedBottom)
            {
                position.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (ShouldChangeDirection(gameTime))
                {
                    hasMovedBottom = true;
                    lastDirectionChangeTime = gameTime.TotalGameTime;
                }
            } else if (!hasMovedLeft)
            {
                position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (ShouldChangeDirection(gameTime))
                {
                    hasMovedLeft = true;
                    lastDirectionChangeTime = gameTime.TotalGameTime;
                }
            } else if (!hasMovedTop)
            {
                position.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (ShouldChangeDirection(gameTime))
                {
                    hasMovedTop = true;
                    lastDirectionChangeTime = gameTime.TotalGameTime;
                }

                if (HasCompletedlap())
                {
                    hasMovedRight = false;
                    hasMovedBottom = false;
                    hasMovedLeft = false;
                    hasMovedTop = false;
                }
            }
            SetHitbox(position.X, position.Y);
        }

        private bool ShouldChangeDirection(GameTime gameTime)
        {
            return gameTime.TotalGameTime.TotalSeconds - lastDirectionChangeTime.TotalSeconds >= movementDuration;
        }

        private bool HasCompletedlap()
        {
            return hasMovedRight && hasMovedBottom && hasMovedLeft && hasMovedTop;
        }
    }
}
