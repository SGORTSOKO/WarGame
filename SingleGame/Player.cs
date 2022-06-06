// ***********************************************************************
// Assembly         : WarGame
// Author           : abros
// Created          : 06-06-2022
//
// Last Modified By : abros
// Last Modified On : 06-06-2022
// ***********************************************************************
// <copyright file="Player.cs" company="Kurgan State University">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;

namespace WarGame.SingleGame
{
    /// <summary>
    /// Class Player.
    /// </summary>
    public class Player
    {
        #region Fields
        /// <summary>
        /// Цвет существ игрока
        /// </summary>
        private Color preferColor;
        /// <summary>
        /// Имя игрока
        /// </summary>
        private string name;
        /// <summary>
        /// Здоровье игрока
        /// </summary>
        private int playerHP;
        /// <summary>
        /// Тип игрока
        /// false: бот (справа)
        /// true: Текущий (слева)
        /// </summary>
        private bool type;
        /// <summary>
        /// Положение "тела" игрока
        /// </summary>
        private int hitBoxPosition;
        /// <summary>
        /// Размер экрана по х
        /// </summary>
        private int screenSizeX;
        #endregion
        #region Constructors
        /// <summary>
        /// Конструктор класса <see cref="Player" />.
        /// </summary>
        /// <param name="newName">Имя игрока</param>
        /// <param name="color">Цвет существ игрока</param>
        /// <param name="newHP">Здоровье игрока</param>
        /// <param name="newType">Тип игрока</param>
        /// <param name="inputScreenSize">Размер экрана по х</param>
        public Player(
            string newName,
            Color color,
            int newHP,
            bool newType,
            CoordinatesXY inputScreenSize)
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
        /// Установить или вернуть цвет существ игрока
        /// </summary>
        public Color Color
        {
            get => preferColor;
            set => preferColor = value;
        }
        /// <summary>
        /// Тип игрока <see cref="Player" /> (текущий или нет).
        /// </summary>
        /// <value><c>true</c> если текущий (левый); иначе, <c>false</c>.</value>
        public bool Current
        {
            get => type;
            set => type = value;
        }
        /// <summary>
        /// Позиция "тела" игрока по Х
        /// </summary>
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
        /// GПолучить или вернуть здоровье игрока
        /// </summary>
        public int HP
        {
            get => playerHP;
            set => playerHP = value;
        }
        /// <summary>
        /// Установить или вернуть имя игрока
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value;
        }
        /// <summary>
        /// Установить или вернуть размер экрана по x
        /// </summary>
        public int ScreenSizeX
        {
            get => screenSizeX;
            set => screenSizeX = value;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Атаковать игрока
        /// </summary>
        /// <param name="power">Сила существа</param>
        public void AttackMe(int power)
        {
            HP -= power;
        }
        #endregion
    }
}
