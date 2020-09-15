using System;
using System.Collections.Generic;
using System.Text;

namespace TalesOfSenylan
{
    public interface Collidable
    {
        bool Collide(Collidable collidable);
    }
}
