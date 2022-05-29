using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarGame
{
    public class XY
    {
        #region Fields
        private float x;
        private float y;
        #endregion
        #region Properties
        public float Xf
        {
            get => x;
            set => x = value;
        }
        public float Yf
        {
            get => y;
            set => y = value;
        }
        public int X
        {
            get => (int)x;
            set => x = value;
        }
        public int Y
        {
            get => (int)y;
            set => y = value;
        }
        public float X0
        {
            get => 0;
        }
        public float Y0
        {
            get => 0;
        }
        public XY(float x, float y)
        {
            Xf = x;
            Yf = y;
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