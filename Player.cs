using Microsoft.Xna.Framework;

namespace WarGame
{
    /// <summary>
    /// Class Player.
    /// </summary>
    public class Player
    {
        #region Fields
        /// <summary>
        /// Color of Cretures
        /// </summary>
        private Color preferColor;
        /// <summary>
        /// The PlayerName
        /// </summary>
        private string name;
        /// <summary>
        /// The player hp
        /// </summary>
        private int playerHP;
        /// <summary>
        /// The player type 
        /// false: bot, or client
        /// true: host player
        /// </summary>
        private bool type;
        /// <summary>
        /// The hit box position in screen
        /// </summary>
        private int hitBoxPosition;
        /// <summary>
        /// The screen size x
        /// </summary>
        private int screenSizeX;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="newName">The new name.</param>
        /// <param name="color">The color of creatures</param>
        /// <param name="newHP">The player hp.</param>
        /// <param name="newType">player type</param>
        /// <param name="inputScreenSize">Size of the screen.</param>
        public Player(
            string newName, 
            Color color, 
            int newHP, 
            bool newType, 
            XY inputScreenSize)
        {
            Name = newName;
            Color = color;
            HP = newHP;
            Current = newType;
            HitBoxPositionX = inputScreenSize.X;
            ScreenSizeX = inputScreenSize.X;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get => name;
            set => name = value;
        }
        /// <summary>
        /// Gets or sets the hp.
        /// </summary>
        /// <value>The hp.</value>
        public int HP
        {
            get => playerHP;
            set => playerHP = value;
        }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Player"/> is current.
        /// </summary>
        /// <value><c>true</c> if current; otherwise, <c>false</c>.</value>
        public bool Current
        {
            get => type;
            set => type = value;
        }
        /// <summary>
        /// Gets or sets the hit box position x in screen
        /// </summary>
        /// <value>The hit box position x.</value>
        public int HitBoxPositionX
        {
            get => hitBoxPosition;
            set
            {
                if (Current == true)
                {
                    hitBoxPosition = 0;
                }
                else
                {
                    hitBoxPosition = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets the screen size x.
        /// </summary>
        /// <value>The screen size x.</value>
        public int ScreenSizeX
        {
            get => screenSizeX;
            set => screenSizeX = value;
        }
        /// <summary>
        /// Gets or sets the color of players creatures
        /// </summary>
        /// <value>The color.</value>
        public Color Color
        {
            get => preferColor;
            set => preferColor = value;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Attaks the player
        /// </summary>
        /// <param name="power">The power of creature</param>
        public void AttackMe(int power)
        {
            HP -= power;
        }
        #endregion
    }
}
