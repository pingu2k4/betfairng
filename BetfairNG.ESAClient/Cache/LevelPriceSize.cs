using System.Collections.Generic;

namespace Betfair.ESAClient.Cache
{
    /// <summary>
    /// Immutable triple of level, price size.
    /// </summary>
    public class LevelPriceSize
    {
        public static readonly IList<LevelPriceSize> EmptyList = new LevelPriceSize[0];
        private readonly int _level;
        private readonly double _price;
        private readonly double _size;

        public LevelPriceSize(List<double?> levelPriceSize)
        {
            _level = (int)levelPriceSize[0];
            _price = (double)levelPriceSize[1];
            _size = (double)levelPriceSize[2];
        }

        public LevelPriceSize(int level, double price, double size)
        {
            _level = level;
            _price = price;
            _size = size;
        }

        public int Level
        {
            get { return _level; }
        }

        public double Price
        {
            get { return _price; }
        }

        public double Size
        {
            get { return _size; }
        }

        public override string ToString()
        {
            return _level + ": " + _size + "@" + _price;
        }
    }
}