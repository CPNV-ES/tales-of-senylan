using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TalesOfSenylan.Models.Characters
{
    public abstract class Character : Collidable
    {
        protected Texture2D Sprite;
        protected Vector2 Position;
        protected float Speed = 100;
        protected Dungeon Dungeon { get; }

        public Character(Dungeon dungeon, Vector2 initialPosition)
        {
            Dungeon = dungeon;
            Position = initialPosition;
        }

        public bool Collide(Collidable collidable)
        {
            return false;
        }
    }
}
