using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WarGame
{
    /// <summary>
    /// Class Human.
    /// Implements the <see cref="WarGame.Creatures" />
    /// </summary>
    /// <seealso cref="WarGame.Creatures" />
    public class Human : Creatures
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Human"/> class.
        /// </summary>
        /// <param name="inputTexture">The human texture</param>
        /// <param name="inputNowPositionX">The input human position x.</param>
        /// <param name="inputNowPositionY">The input human position y.</param>
        /// <param name="inputFrameSizeX">The input human frame size x.</param>
        /// <param name="inputFrameSizeY">The input human frame size y.</param>
        /// <param name="inputNumberOfFramesX">The input human number of frames x.</param>
        /// <param name="inputNumberOfFramesY">The input human number of frames y.</param>
        /// <param name="inputNowFrameNumberX">The input human now frame number x.</param>
        /// <param name="inputNowFrameNumberY">The input human now frame number y.</param>
        /// <param name="inputSpeedX">The input human speed x.</param>
        /// <param name="inputSpeedY">The input human speed y.</param>
        /// <param name="inputPlayer">The input human player.</param>
        /// <param name="inputHP">The input human hp.</param>
        /// <param name="inputPower">The input human power.</param>
        /// <param name="inputStamina">The input human stamina.</param>
        public Human(
            Texture2D inputTexture, 
            int inputNowPositionX, 
            int inputNowPositionY,
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
            int inputStamina):
            base(
                inputTexture, 
                inputNowPositionX, 
                inputNowPositionY,
                inputFrameSizeX, 
                inputFrameSizeY, 
                inputNumberOfFramesX,
                inputNumberOfFramesY, 
                inputNowFrameNumberX, 
                inputNowFrameNumberY,
                inputSpeedX, 
                inputSpeedY, 
                inputPlayer, 
                inputHP, 
                inputPower, 
                inputStamina)
        { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Human"/> class.
        /// </summary>
        /// <param name="inputTexture">The human texture.</param>
        /// <param name="inputPlayer">The human player.</param>
        /// <param name="inputNowPositionY">The input human position y.</param>
        public Human(
            Texture2D inputTexture, 
            Player inputPlayer, 
            int inputNowPositionY) :
            base(
                inputTexture,
                inputPlayer.HitBoxPositionX - inputTexture.Width / 16,
                inputNowPositionY,
                inputTexture.Width / 8,
                inputTexture.Height / 2,
                8,
                2,
                0,
                0,
                30,
                0,
                inputPlayer,
                100,
                20,
                1)
        { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Human"/> class.
        /// </summary>
        public Human() :
            base()
        { }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the self texture.
        /// </summary>
        /// <value>The self texture.</value>
        public override Texture2D SelfTexture 
        { 
            get => selfTexture; 
            set => selfTexture = value; 
        }
        /// <summary>
        /// Gets or sets the frame size x.
        /// </summary>
        /// <value>The frame size x.</value>
        public override int FrameSizeX 
        { 
            get => frameSizeX; 
            set => frameSizeX = value; 
        }
        /// <summary>
        /// Gets or sets the frame size y.
        /// </summary>
        /// <value>The frame size y.</value>
        public override int FrameSizeY 
        { 
            get => frameSizeY; 
            set => frameSizeY = value; 
        }
        /// <summary>
        /// Gets or sets the number of frames x.
        /// </summary>
        /// <value>The number of frames x.</value>
        public override int NumberOfFramesX
        {
            get => numberOfFramesX;
            set => numberOfFramesX = value;
        }
        /// <summary>
        /// Gets or sets the number of frames y.
        /// </summary>
        /// <value>The number of frames y.</value>
        public override int NumberOfFramesY
        {
            get => numberOfFramesY;
            set => numberOfFramesY = value;
        }
        /// <summary>
        /// Gets or sets the now frame number x.
        /// </summary>
        /// <value>The now frame number x.</value>
        public override int NowFrameNumberX
        {
            get => nowFrameNumberX;
            set => nowFrameNumberX = value;
        }
        /// <summary>
        /// Gets or sets the now frame number y.
        /// </summary>
        /// <value>The now frame number y.</value>
        public override int NowFrameNumberY
        {
            get => nowFrameNumberY;
            set => nowFrameNumberY = value;
        }
        /// <summary>
        /// Gets or sets the now position x.
        /// </summary>
        /// <value>The now position x.</value>
        public override float NowPositionX
        {
            get => position.X;
            set => position.X = value;
        }
        /// <summary>
        /// Gets or sets the now position y.
        /// </summary>
        /// <value>The now position y.</value>
        public override float NowPositionY
        {
            get => position.Y;
            set => position.Y = value;
        }
        /// <summary>
        /// Gets or sets the now position.
        /// </summary>
        /// <value>The now position.</value>
        public override Vector2 NowPosition
        {
            get => position;
            set => position = value;
        }
        /// <summary>
        /// Gets or sets the speed x.
        /// </summary>
        /// <value>The speed x.</value>
        public override float SpeedX
        {
            get => speedX;
            set => speedX = value;
        }
        /// <summary>
        /// Gets or sets the speed y.
        /// </summary>
        /// <value>The speed y.</value>
        public override float SpeedY
        {
            get => speedY;
            set => speedY = value;
        }
        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>The player.</value>
        public override Player Player
        {
            get => player;
            set
            {
                player = value;
                if (value.Current == true)
                {
                    NowFrameNumberY = 0;
                }
                else
                {
                    NowFrameNumberY = 1;
                }
            }
        }
        /// <summary>
        /// Gets or sets the hp.
        /// </summary>
        /// <value>The hp.</value>
        public override int HP
        {
            get => hitPoints;
            set => hitPoints = value;
        }
        /// <summary>
        /// Gets or sets the power.
        /// </summary>
        /// <value>The power.</value>
        public override int Power
        {
            get => power;
            set => power = value;
        }
        /// <summary>
        /// Gets or sets the stamina.
        /// </summary>
        /// <value>The stamina.</value>
        public override int Stamina
        {
            get => stamina;
            set => stamina = value;
        }
        #endregion
        #region Methods
        /// <summary>
        /// move human in the screen
        /// </summary>
        /// <returns>System.Int32. - power to attack enemy`s player</returns>
        public override int Step()
        {
            if (player.Current == true)
            {
                position.X += SpeedX;
                nowFrameNumberX++;
                if (nowFrameNumberX == numberOfFramesX)
                {
                    nowFrameNumberX = 0;
                }
                if (position.X > player.ScreenSizeX + frameSizeX / 2)
                {
                    return Power;
                }
            }
            else
            {
                position.X -= SpeedX;
                if (nowFrameNumberX == 0)
                {
                    nowFrameNumberX = numberOfFramesX;
                }
                nowFrameNumberX--;
                if (position.X < 0 - frameSizeX / 2)
                {
                    return Power;
                }
            }
            return 0;

        }
        /// <summary>
        /// Hits the creature of another player.
        /// </summary>
        /// <param name="defender">The defender creature</param>
        /// <returns><c>true</c> if defender now is dead, <c>false</c> otherwise.</returns>
        public override bool HitCreature(Creatures defender)
        {
            if (stamina > 0)
            {
                stamina--;
                defender.HP -= Power;
                if (defender.HP <= 0)
                {
                    defender.Power = 0;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Gets the Rectangle object for check of creatures intersection
        /// </summary>
        /// <returns>Rectangle.</returns>
        public override Rectangle GetRectangleImage()
        {
            return new Rectangle(NowFrameNumberX * FrameSizeX, nowFrameNumberY * FrameSizeY, FrameSizeX, FrameSizeY);
        }
        /// <summary>
        /// Gets the human Rectangle position.
        /// </summary>
        /// <returns>Rectangle.</returns>
        public override Rectangle GetRectanglePos()
        {
            return new Rectangle((int)NowPositionX, (int)NowPositionY, FrameSizeX, FrameSizeY);
        }
#endregion
    }
}
