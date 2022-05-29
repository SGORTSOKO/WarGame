using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WarGame
{
    /// <summary>
    /// Class Creatures.
    /// </summary>
    public abstract class Creatures
    {
        #region Fields
        /// <summary>
        /// The self texture
        /// </summary>
        protected Texture2D selfTexture;
        /// <summary>
        /// The position
        /// </summary>
        protected Vector2 position;
        /// <summary>
        /// The frame size x
        /// </summary>
        protected int frameSizeX;
        /// <summary>
        /// The frame size y
        /// </summary>
        protected int frameSizeY;
        /// <summary>
        /// The number of frames x
        /// </summary>
        protected int numberOfFramesX;
        /// <summary>
        /// The number of frames y
        /// </summary>
        protected int numberOfFramesY;
        /// <summary>
        /// The now frame number x
        /// </summary>
        protected int nowFrameNumberX;
        /// <summary>
        /// The now frame number y
        /// </summary>
        protected int nowFrameNumberY;
        /// <summary>
        /// The speed x
        /// </summary>
        protected float speedX;
        /// <summary>
        /// The speed y
        /// </summary>
        protected float speedY;
        /// <summary>
        /// The player
        /// </summary>
        protected Player player;
        /// <summary>
        /// The hit points
        /// </summary>
        protected int hitPoints;
        /// <summary>
        /// The power
        /// </summary>
        protected int power;
        /// <summary>
        /// The stamina
        /// </summary>
        protected int stamina;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the self texture.
        /// </summary>
        /// <value>The self texture.</value>
        public virtual Texture2D SelfTexture { get; set; }
        /// <summary>
        /// Gets or sets the now position.
        /// </summary>
        /// <value>The now position.</value>
        public virtual Vector2 NowPosition { get; set; }
        /// <summary>
        /// Gets or sets the speed x.
        /// </summary>
        /// <value>The speed x.</value>
        public virtual float SpeedX { get; set; }
        /// <summary>
        /// Gets or sets the speed y.
        /// </summary>
        /// <value>The speed y.</value>
        public virtual float SpeedY { get; set; }
        /// <summary>
        /// Gets or sets the frame size x.
        /// </summary>
        /// <value>The frame size x.</value>
        public virtual int FrameSizeX { get; set; }
        /// <summary>
        /// Gets or sets the frame size y.
        /// </summary>
        /// <value>The frame size y.</value>
        public virtual int FrameSizeY { get; set; }
        /// <summary>
        /// Gets or sets the number of frames x.
        /// </summary>
        /// <value>The number of frames x.</value>
        public virtual int NumberOfFramesX { get; set; }
        /// <summary>
        /// Gets or sets the number of frames y.
        /// </summary>
        /// <value>The number of frames y.</value>
        public virtual int NumberOfFramesY { get; set; }
        /// <summary>
        /// Gets or sets the now frame number x.
        /// </summary>
        /// <value>The now frame number x.</value>
        public virtual int NowFrameNumberX { get; set; }
        /// <summary>
        /// Gets or sets the now frame number y.
        /// </summary>
        /// <value>The now frame number y.</value>
        public virtual int NowFrameNumberY { get; set; }
        /// <summary>
        /// Gets or sets the now position x.
        /// </summary>
        /// <value>The now position x.</value>
        public virtual float NowPositionX { get; set; }
        /// <summary>
        /// Gets or sets the now position y.
        /// </summary>
        /// <value>The now position y.</value>
        public virtual float NowPositionY { get; set; }
        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>The player.</value>
        public virtual Player Player { get; set; }
        /// <summary>
        /// Gets or sets the hp.
        /// </summary>
        /// <value>The hp.</value>
        public virtual int HP { get; set; }
        /// <summary>
        /// Gets or sets the power.
        /// </summary>
        /// <value>The power.</value>
        public virtual int Power { get; set; }
        /// <summary>
        /// Gets or sets the stamina.
        /// </summary>
        /// <value>The stamina.</value>
        public virtual int Stamina { get; set; }
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Creatures"/> class.
        /// </summary>
        /// <param name="inputTexture">The input texture.</param>
        /// <param name="inputNowPositionX">The input now position x.</param>
        /// <param name="inputNowPositionY">The input now position y.</param>
        /// <param name="inputFrameSizeX">The input frame size x.</param>
        /// <param name="inputFrameSizeY">The input frame size y.</param>
        /// <param name="inputNumberOfFramesX">The input number of frames x.</param>
        /// <param name="inputNumberOfFramesY">The input number of frames y.</param>
        /// <param name="inputNowFrameNumberX">The input now frame number x.</param>
        /// <param name="inputNowFrameNumberY">The input now frame number y.</param>
        /// <param name="inputSpeedX">The input speed x.</param>
        /// <param name="inputSpeedY">The input speed y.</param>
        /// <param name="inputPlayer">The input player.</param>
        /// <param name="inputHP">The input hp.</param>
        /// <param name="inputPower">The input power.</param>
        /// <param name="inputStamina">The input stamina.</param>
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