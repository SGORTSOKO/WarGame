using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarGame
{
    class XY
    {
        #region Fields
        private int x;
        private int y;
        #endregion
        #region Properties
        public int X
        {
            get => x;
            set => x = value;
        }
        public int Y
        {
            get => y;
            set => y = value;
        }
        public XY(int x, int y)
        {
            X = x;
            Y = y;
        }
        #endregion
        #region Operators
        public static XY operator +(XY first, XY second)
        {
            return new XY(first.X + second.X, first.Y + second.Y);
        }
        public static XY operator -(XY first, XY second)
        {
            return new XY(first.X - second.X, first.Y - second.Y);
        }
        public static XY operator +(XY first, int X)
        {
            return new XY(first.X + X, first.Y);
        }
        #endregion
    }
}