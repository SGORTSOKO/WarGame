using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WarGame
{
    /// <summary>
    /// Class Creatures.
    /// </summary>
    public abstract class Creatures
    {
        #region Fields
        /// <summary>
        /// The creature texture
        /// </summary>
        protected Texture2D selfTexture;
        /// <summary>
        /// The position of creature
        /// </summary>
        protected Vector2 position;
        /// <summary>
        /// The creature frame size x
        /// </summary>
        protected int frameSizeX;
        /// <summary>
        /// The creature frame size y
        /// </summary>
        protected int frameSizeY;
        /// <summary>
        /// The creature number of frames x
        /// </summary>
        protected int numberOfFramesX;
        /// <summary>
        /// The creature number of frames y
        /// </summary>
        protected int numberOfFramesY;
        /// <summary>
        /// The creature current frame number x
        /// </summary>
        protected int nowFrameNumberX;
        /// <summary>
        /// The creature current frame number y
        /// </summary>
        protected int nowFrameNumberY;
        /// <summary>
        /// The creature`s speed x
        /// </summary>
        protected float speedX;
        /// <summary>
        /// The creature`s speed y
        /// </summary>
        protected float speedY;
        /// <summary>
        /// The creature`s player
        /// </summary>
        protected Player player;
        /// <summary>
        /// The creature hit points
        /// </summary>
        protected int hitPoints;
        /// <summary>
        /// The creature power
        /// </summary>
        protected int power;
        /// <summary>
        /// The creature stamina
        /// </summary>
        protected int stamina;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the self texture.
        /// </summary>
        /// <value>The creature texture.</value>
        public virtual Texture2D SelfTexture { get; set; }
        /// <summary>
        /// Gets or sets the creature position.
        /// </summary>
        /// <value>The creature position.</value>
        public virtual Vector2 NowPosition { get; set; }
        /// <summary>
        /// Gets or sets the creature speed x.
        /// </summary>
        /// <value>The creature speed x.</value>
        public virtual float SpeedX { get; set; }
        /// <summary>
        /// Gets or sets the creature speed y.
        /// </summary>
        /// <value>The creature speed y.</value>
        public virtual float SpeedY { get; set; }
        /// <summary>
        /// Gets or sets the creature frame size x.
        /// </summary>
        /// <value>The creature frame size x.</value>
        public virtual int FrameSizeX { get; set; }
        /// <summary>
        /// Gets or sets the creature frame size y.
        /// </summary>
        /// <value>The creature frame csize y.</value>
        public virtual int FrameSizeY { get; set; }
        /// <summary>
        /// Gets or sets the creature number of frames x.
        /// </summary>
        /// <value>The creature number of frames x.</value>
        public virtual int NumberOfFramesX { get; set; }
        /// <summary>
        /// Gets or sets the creature number of frames y.
        /// </summary>
        /// <value>The creature number of frames y.</value>
        public virtual int NumberOfFramesY { get; set; }
        /// <summary>
        /// Gets or sets the creature frame number x.
        /// </summary>
        /// <value>The creature frame number x.</value>
        public virtual int NowFrameNumberX { get; set; }
        /// <summary>
        /// Gets or sets the creature frame number y.
        /// </summary>
        /// <value>The creature frame number y.</value>
        public virtual int NowFrameNumberY { get; set; }
        /// <summary>
        /// Gets or sets the creature now position x.
        /// </summary>
        /// <value>The creature now position x.</value>
        public virtual float NowPositionX { get; set; }
        /// <summary>
        /// Gets or sets creature the now position y.
        /// </summary>
        /// <value>The creature now position y.</value>
        public virtual float NowPositionY { get; set; }
        /// <summary>
        /// Gets or sets the creature player.
        /// </summary>
        /// <value>The creature player.</value>
        public virtual Player Player { get; set; }
        /// <summary>
        /// Gets or sets the creature hp.
        /// </summary>
        /// <value>The creature hp.</value>
        public virtual int HP { get; set; }
        /// <summary>
        /// Gets or sets the creature power.
        /// </summary>
        /// <value>The creature power.</value>
        public virtual int Power { get; set; }
        /// <summary>
        /// Gets or sets the creature stamina.
        /// </summary>
        /// <value>The creature stamina.</value>
        public virtual int Stamina { get; set; }
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Creatures"/> class.
        /// </summary>
        /// <param name="inputTexture">The input creature texture.</param>
        /// <param name="inputNowPositionX">The input creature now position x.</param>
        /// <param name="inputNowPositionY">The input creature now position y.</param>
        /// <param name="inputFrameSizeX">The input creature frame size x.</param>
        /// <param name="inputFrameSizeY">The input creature frame size y.</param>
        /// <param name="inputNumberOfFramesX">The input creature number of frames x.</param>
        /// <param name="inputNumberOfFramesY">The input creature number of frames y.</param>
        /// <param name="inputNowFrameNumberX">The input now creature frame number x.</param>
        /// <param name="inputNowFrameNumberY">The input now creature frame number y.</param>
        /// <param name="inputSpeedX">The input creature speed x.</param>
        /// <param name="inputSpeedY">The input creature speed y.</param>
        /// <param name="inputPlayer">The input creature player.</param>
        /// <param name="inputHP">The input creature hp.</param>
        /// <param name="inputPower">The input creature power.</param>
        /// <param name="inputStamina">The input creature stamina.</param>
        public Creatures(
            Texture2D inputTexture,
            float inputNowPositionX,
            float inputNowPositionY,
            int inputFrameSizeX,
            int inputFrameSizeY,
            int inputNumberOfFramesX,
            int inputNumberOfFramesY,
            int inputNowFrameNumberX,
            int inputNowFrameNumberY,
            float inputSpeedX,
            float inputSpeedY,
            Player inputPlayer,
            int inputHP,
            int inputPower,
            int inputStamina)
        {
            SelfTexture = inputTexture;
            NowPositionX = inputNowPositionX;
            NowPositionY = inputNowPositionY;
            FrameSizeX = inputFrameSizeX;
            FrameSizeY = inputFrameSizeY;
            NumberOfFramesX = inputNumberOfFramesX;
            NumberOfFramesY = inputNumberOfFramesY;
            NowFrameNumberX = inputNowFrameNumberX;
            NowFrameNumberY = inputNowFrameNumberY;
            SpeedX = inputSpeedX;
            SpeedY = inputSpeedY;
            Player = inputPlayer;
            HP = inputHP;
            Power = inputPower;
            Stamina = inputStamina;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="Creatures"/> class.
        /// </summary>
        public Creatures() { }
        /// <summary>
        /// Steps this instance.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public abstract int Step();
        /// <summary>
        /// Gets the rectangle image.
        /// </summary>
        /// <returns>Rectangle.</returns>
        public abstract Rectangle GetRectangleImage();
        /// <summary>
        /// Gets the rectangle position.
        /// </summary>
        /// <returns>Rectangle.</returns>
        public abstract Rectangle GetRectanglePos();
        /// <summary>
        /// Hits the creature.
        /// </summary>
        /// <param name="defender">The defender.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public abstract bool HitCreature(Creatures defender);
        #endregion
    }
}
#region Sketches
/*
public static bool operator ==(Creatures first, Creatures second)
{
    if (new Rectangle((int)first.NowPositionX, (int)first.NowPositionY, first.FrameSizeX, first.FrameSizeY ).Intersects(new Rectangle((int)second.NowPositionX, (int)second.NowPositionY, second.FrameSizeX, second.FrameSizeY)))
        return true;
    else
        return false;
}
public static bool operator !=(Creatures first, Creatures second)
{
    if (!new Rectangle((int)first.NowPositionX, (int)first.NowPositionY, first.FrameSizeX, first.FrameSizeY).Intersects(new Rectangle((int)second.NowPositionX, (int)second.NowPositionY, second.FrameSizeX, second.FrameSizeY)))
        return true;
    else
        return false;
}
*/
#endregion