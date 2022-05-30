namespace WarGame
{
    /// <summary>
    /// Класс-хранилище векторных координат
    /// </summary>
    public class XY
    {
        #region Fields
        /// <summary>
        /// Х координата
        /// </summary>
        private float x;
        /// <summary>
        /// Y координата
        /// </summary>
        private float y;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the xf.
        /// </summary>
        /// <value>float x</value>
        public float Xf
        {
            get => x;
            set => x = value;
        }
        /// <summary>
        /// Gets or sets the yf.
        /// </summary>
        /// <value>float y</value>
        public float Yf
        {
            get => y;
            set => y = value;
        }
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>int x</value>
        public int X
        {
            get => (int)x;
            set => x = value;
        }
        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>int y</value>
        public int Y
        {
            get => (int)y;
            set => y = value;
        }
        /// <summary>
        /// Gets the x0.
        /// </summary>
        /// <value>0</value>
        public float X0
        {
            get => 0;
        }
        /// <summary>
        /// Gets the y0.
        /// </summary>
        /// <value>0</value>
        public float Y0
        {
            get => 0;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="XY"/> class.
        /// </summary>
        /// <param name="x">float x</param>
        /// <param name="y">float y</param>
        public XY(float x, float y)
        {
            Xf = x;
            Yf = y;
        }
        #endregion
        #region Operators
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="first">The first XY</param>
        /// <param name="second">The second XY</param>
        /// <returns>The result of the operator.</returns>
        public static XY operator +(XY first, XY second)
        {
            return new XY(first.X + second.X, first.Y + second.Y);
        }
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="first">The first XY</param>
        /// <param name="second">The second XY</param>
        /// <returns>The result of the operator.</returns>
        public static XY operator -(XY first, XY second)
        {
            return new XY(first.X - second.X, first.Y - second.Y);
        }
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="first">The XY</param>
        /// <param name="X">int x</param>
        /// <returns>The result of the operator.</returns>
        public static XY operator +(XY first, int X)
        {
            return new XY(first.X + X, first.Y);
        }
        #endregion
    }
}