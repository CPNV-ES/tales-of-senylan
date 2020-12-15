using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using TalesOfSenylan.Models.Characters;
using TalesOfSenylan.Models.Dungeon;
using TalesOfSenylan.Models.Items;
using TalesOfSenylan.Models.Items.Weapons;

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
        private readonly TimeSpan attackRate = new TimeSpan(0, 0, 1); //cooldown attack set to 1sec
        private int c = 1; //Debug counter of the attack
        private TimeSpan nextAttack;
        private Inventory inventory = new Inventory();
        

        public Player(Dungeon dungeon, Vector2 initialPosition) : base(dungeon, initialPosition)
        {
            LoadContent();
            speed = 200;
            maxHealth = health = 300;
            maxMana = mana = 200;
            hitbox = new RectangleF(position.X - sprite.Width / 2, position.Y - sprite.Height / 2, sprite.Width,
                sprite.Height);

            Sword s = new Sword("Holy Sword of Fire");
            Potion p = new Potion("Health Potion (50)");
            inventory.AddItem(s);
            inventory.AddItem(p);
            inventory.AddItem(p);
        }

        private State state { get; set; }

        public void LoadContent()
        {
            sprite = dungeon.content.Load<Texture2D>("ball");
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
                0.0f
            );

            //DrawHitbox(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            SetHitbox(position.X, position.Y);
        }

        public int GetDamagePoints(GameTime gameTime)
        {
            var dmgValue = 50;
            if (gameTime.TotalGameTime.TotalSeconds.CompareTo(nextAttack.TotalSeconds) == 1)
            {
                //Debug.WriteLine("ATTAQUE :" + c++ + " fois");
                nextAttack = gameTime.TotalGameTime.Add(attackRate);

                return dmgValue;
            }

            return 0;
        }
    }
}