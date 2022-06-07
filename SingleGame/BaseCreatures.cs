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
    public abstract class BaseCreatures
    {
        #region Fields
        /// <summary>
        /// The creature texture
        /// </summary>
        protected Texture2D сreatureTexture;
        /// <summary>
        /// The position of creature
        /// </summary>
        protected Vector2 сreaturePosition;
        /// <summary>
        /// The creature frame size x
        /// </summary>
        protected int сreatureFrameSizeX;
        /// <summary>
        /// The creature frame size y
        /// </summary>
        protected int сreatureFrameSizeY;
        /// <summary>
        /// The creature number of frames x
        /// </summary>
        protected int сreatureNumberOfFramesX;
        /// <summary>
        /// The creature number of frames y
        /// </summary>
        protected int сreatureNumberOfFramesY;
        /// <summary>
        /// The creature current frame number x
        /// </summary>
        protected int сreatureNowFrameNumberX;
        /// <summary>
        /// The creature current frame number y
        /// </summary>
        protected int сreatureNowFrameNumberY;
        /// <summary>
        /// The creature`s speed x
        /// </summary>
        protected float сreatureSpeedX;
        /// <summary>
        /// The creature`s speed y
        /// </summary>
        protected float сreatureSpeedY;
        /// <summary>
        /// The creature`s player
        /// </summary>
        protected Player сreaturePlayer;
        /// <summary>
        /// The creature hit points
        /// </summary>
        protected int сreatureHitPoints;
        /// <summary>
        /// The creature power
        /// </summary>
        protected int сreaturePower;
        /// <summary>
        /// The creature stamina
        /// </summary>
        protected int сreatureStamina;
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
        /// Конструктор класса <see cref="BaseCreatures" />.
        /// </summary>
        /// <param name="inputTexture">Текстура существа</param>
        /// <param name="inputNowPositionX">Позиция Х существа</param>
        /// <param name="inputNowPositionY">Позиция У сущетсва</param>
        /// <param name="inputFrameSizeX">Размер фрейма по Х</param>
        /// <param name="inputFrameSizeY">Размер фрейма по У</param>
        /// <param name="inputNumberOfFramesX">Всего фреймов по Х</param>
        /// <param name="inputNumberOfFramesY">Всего фреймов по У</param>
        /// <param name="inputNowFrameNumberX">Текущий фрейм по Х</param>
        /// <param name="inputNowFrameNumberY">Текущий фрейм по У</param>
        /// <param name="inputSpeedX">Скорость существа по Х</param>
        /// <param name="inputSpeedY">Скорость сущетства по У</param>
        /// <param name="inputPlayer">Чье существо</param>
        /// <param name="inputHP">Здоровье существа</param>
        /// <param name="inputPower">Сила атаки существа</param>
        /// <param name="inputStamina">Выносливость существа</param>
        public BaseCreatures(
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
        /// Конструктор класса <see cref="BaseCreatures" />.
        /// </summary>
        public BaseCreatures() { }
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
        /// <param name="defender">Кто защищается</param>
        /// <returns><c>true</c> если защищающийся убит, иначе <c>false</c></returns>
        public abstract bool HitCreature(BaseCreatures defender);
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