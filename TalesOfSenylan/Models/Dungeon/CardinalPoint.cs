using System;

namespace TalesOfSenylan.Models.Dungeon
{
    public enum CardinalPoint
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }

    static class CardinalPointExtension
    {
        public static CardinalPoint Opposite(this CardinalPoint cardinalPoint)
        {
            switch (cardinalPoint)
            {
                case CardinalPoint.NORTH: return CardinalPoint.SOUTH;
                case CardinalPoint.SOUTH: return CardinalPoint.NORTH;
                case CardinalPoint.EAST: return CardinalPoint.WEST;
                case CardinalPoint.WEST: return CardinalPoint.EAST;
                default: throw new Exception(cardinalPoint + " doesn't have any opposite.");
            }
        }
    }
}