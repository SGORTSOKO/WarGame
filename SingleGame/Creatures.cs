// ***********************************************************************
// Assembly         : WarGame
// Author           : abros
// Created          : 06-06-2022
//
// Last Modified By : abros
// Last Modified On : 06-06-2022
// ***********************************************************************
// <copyright file="Creatures.cs" company="Kurgan State University">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AKSU.SingleGame
{
    /// <summary>
    /// Класс существ Creatures.
    /// </summary>
    public abstract class Creatures
    {
        #region Fields
        /// <summary>
        /// The creature texture
        /// </summary>
        protected Texture2D CreatureTexture;
        /// <summary>
        /// The position of creature
        /// </summary>
        protected Vector2 CreaturePosition;
        /// <summary>
        /// The creature frame size x
        /// </summary>
        protected int CreatureFrameSizeX;
        /// <summary>
        /// The creature frame size y
        /// </summary>
        protected int CreatureFrameSizeY;
        /// <summary>
        /// The creature number of frames x
        /// </summary>
        protected int CreatureNumberOfFramesX;
        /// <summary>
        /// The creature number of frames y
        /// </summary>
        protected int CreatureNumberOfFramesY;
        /// <summary>
        /// The creature current frame number x
        /// </summary>
        protected int CreatureNowFrameNumberX;
        /// <summary>
        /// The creature current frame number y
        /// </summary>
        protected int CreatureNowFrameNumberY;
        /// <summary>
        /// The creature`s speed x
        /// </summary>
        protected float CreatureSpeedX;
        /// <summary>
        /// The creature`s speed y
        /// </summary>
        protected float CreatureSpeedY;
        /// <summary>
        /// The creature`s player
        /// </summary>
        protected Player CreaturePlayer;
        /// <summary>
        /// The creature hit points
        /// </summary>
        protected int CreatureHitPoints;
        /// <summary>
        /// The creature power
        /// </summary>
        protected int CreaturePower;
        /// <summary>
        /// The creature stamina
        /// </summary>
        protected int CreatureStamina;
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
        /// Конструктор класса <see cref="Creatures" />.
        /// </summary>
        /// <param name="InputTexture">Текстура существа</param>
        /// <param name="InputNowPositionX">Позиция Х существа</param>
        /// <param name="InputNowPositionY">Позиция У сущетсва</param>
        /// <param name="InputFrameSizeX">Размер фрейма по Х</param>
        /// <param name="InputFrameSizeY">Размер фрейма по У</param>
        /// <param name="InputNumberOfFramesX">Всего фреймов по Х</param>
        /// <param name="InputNumberOfFramesY">Всего фреймов по У</param>
        /// <param name="InputNowFrameNumberX">Текущий фрейм по Х</param>
        /// <param name="InputNowFrameNumberY">Текущий фрейм по У</param>
        /// <param name="InputSpeedX">Скорость существа по Х</param>
        /// <param name="InputSpeedY">Скорость сущетства по У</param>
        /// <param name="InputPlayer">Чье существо</param>
        /// <param name="InputHP">Здоровье существа</param>
        /// <param name="InputPower">Сила атаки существа</param>
        /// <param name="InputStamina">Выносливость существа</param>
        public Creatures(
            Texture2D InputTexture,
            float InputNowPositionX,
            float InputNowPositionY,
            int InputFrameSizeX,
            int InputFrameSizeY,
            int InputNumberOfFramesX,
            int InputNumberOfFramesY,
            int InputNowFrameNumberX,
            int InputNowFrameNumberY,
            float InputSpeedX,
            float InputSpeedY,
            Player InputPlayer,
            int InputHP,
            int InputPower,
            int InputStamina)
        {
            SelfTexture = InputTexture;
            NowPositionX = InputNowPositionX;
            NowPositionY = InputNowPositionY;
            FrameSizeX = InputFrameSizeX;
            FrameSizeY = InputFrameSizeY;
            NumberOfFramesX = InputNumberOfFramesX;
            NumberOfFramesY = InputNumberOfFramesY;
            NowFrameNumberX = InputNowFrameNumberX;
            NowFrameNumberY = InputNowFrameNumberY;
            SpeedX = InputSpeedX;
            SpeedY = InputSpeedY;
            Player = InputPlayer;
            HP = InputHP;
            Power = InputPower;
            Stamina = InputStamina;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Конструктор класса <see cref="Creatures" />.
        /// </summary>
        public Creatures() { }
        /// <summary>
        /// Сдвинуть существ
        /// </summary>
        /// <returns>Урон существа, достигшего края экрана</returns>
        public abstract int Step();
        /// <summary>
        /// Размеры существа для отрисовки размера
        /// </summary>
        /// <returns>Rectangle</returns>
        public abstract Rectangle GetRectangleImage();
        /// <summary>
        /// Размеры существа для отрисовки положения
        /// </summary>
        /// <returns>Rectangle</returns>
        public abstract Rectangle GetRectanglePos();
        /// <summary>
        /// Атака существа существом
        /// </summary>
        /// <param name="Defender">Кто защищается</param>
        /// <returns><c>true</c> если защищающийся убит, иначе <c>false</c></returns>
        public abstract bool HitCreature(Creatures Defender);
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