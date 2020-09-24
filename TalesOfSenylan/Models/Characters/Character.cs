﻿using Microsoft.Xna.Framework;
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
        protected float Speed = 200;
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

        //Function used to debug hitbox
        public void DrawHitbox(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(getHitbox(), Color.Red);
        }

        public bool IsCollided(RectangleF E) => Hitbox.Intersects(E);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    }
}
