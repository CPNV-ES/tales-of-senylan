using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Diagnostics;

namespace TalesOfSenylan.Models.Characters
{
    public class Enemy : Character
    {
        private float MovementDuration;
        private TimeSpan CurrentMovementDuration;
        
        private bool HasMovedLeft = false;
        private bool HasMovedRight = false;
        private bool HasMovedTop = false;
        private bool HasMovedBottom = false;

        private TimeSpan LastDirectionChangeTime;

        public Enemy(Dungeon dungeon, Vector2 position) : base(dungeon, position)
        {
            MovementDuration = Utilities.Utilities.getRandomNumber(1, 3);
            CurrentMovementDuration = new TimeSpan();
            MaxHealth = Health = 200;
            Speed = 100;
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

            //DrawHitbox(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            CurrentMovementDuration = CurrentMovementDuration.Add(gameTime.ElapsedGameTime);
            
            if (!HasMovedRight)
            {
                Position.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (ShouldChangeDirection(gameTime))
                {
                    HasMovedRight = true;
                    LastDirectionChangeTime = gameTime.TotalGameTime;
                }
            } else if (!HasMovedBottom)
            {
                Position.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (ShouldChangeDirection(gameTime))
                {
                    HasMovedBottom = true;
                    LastDirectionChangeTime = gameTime.TotalGameTime;
                }
            } else if (!HasMovedLeft)
            {
                Position.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (ShouldChangeDirection(gameTime))
                {
                    HasMovedLeft = true;
                    LastDirectionChangeTime = gameTime.TotalGameTime;
                }
            } else if (!HasMovedTop)
            {
                Position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (ShouldChangeDirection(gameTime))
                {
                    HasMovedTop = true;
                    LastDirectionChangeTime = gameTime.TotalGameTime;
                }

                if (HasCompletedlap())
                {
                    HasMovedRight = false;
                    HasMovedBottom = false;
                    HasMovedLeft = false;
                    HasMovedTop = false;
                }
            }
            setHitbox(Position.X, Position.Y);
        }

        private bool ShouldChangeDirection(GameTime gameTime)
        {
            return gameTime.TotalGameTime.TotalSeconds - LastDirectionChangeTime.TotalSeconds >= MovementDuration;
        }

        private bool HasCompletedlap()
        {
            return HasMovedRight && HasMovedBottom && HasMovedLeft && HasMovedTop;
        }
    }
}
