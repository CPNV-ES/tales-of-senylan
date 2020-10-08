using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TalesOfSenylan.Models.Characters;
using MonoGame.Extended;
using System;
using System.Diagnostics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Collisions;

namespace TalesOfSenylan
{
    public enum State
    {
        Idle,
        Attacking,
        Walking
    }
    public class Player : Character
    {
        private int c = 1; //Debug counter of the attack
        private TimeSpan attackRate = new TimeSpan(0, 0, 1); //cooldown attack set to 1sec
        private TimeSpan nextAttack = new TimeSpan();

		public Player(Dungeon dungeon, Vector2 initialPosition) : base(dungeon, initialPosition)
        {
            LoadContent();
            Speed = 200;
            MaxHealth = Health = 300;
            MaxMana = Mana = 200;
            Hitbox = new RectangleF(Position.X - Sprite.Width / 2, Position.Y - Sprite.Height / 2, Sprite.Width, Sprite.Height);
        }

		private State state { get; set; }

		public void LoadContent()
        {
            Sprite = Dungeon.Content.Load<Texture2D>("ball");
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
                0.0f
            );

            //DrawHitbox(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            setHitbox(Position.X, Position.Y);
        }

        public int GetDamagePoints(GameTime gameTime)
        {
            int dmgValue = 50;
            if (gameTime.TotalGameTime.TotalSeconds.CompareTo(nextAttack.TotalSeconds) == 1)
			{
                //Debug.WriteLine("ATTAQUE :" + c++ + " fois");
                nextAttack = gameTime.TotalGameTime.Add(attackRate);

                return dmgValue;
			}
			else
			{
                return 0;
			}
        }
    }
}
