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

namespace AKSU.SingleGame
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
        private string playerName;
        /// <summary>
        /// Здоровье игрока
        /// </summary>
        private int playerHP;
        /// <summary>
        /// Тип игрока
        /// false: бот (справа)
        /// true: Текущий (слева)
        /// </summary>
        private bool playerType;
        /// <summary>
        /// Положение "тела" игрока
        /// </summary>
        private int hitBoxPosition;
        /// <summary>
        /// Размер экрана по х
        /// </summary>
        private int screenSizeWidth;
        #endregion
        #region Constructors
        /// <summary>
        /// Конструктор класса <see cref="Player" />.
        /// </summary>
        /// <param name="inputPlayerName">Имя игрока</param>
        /// <param name="inputPlayerColor">Цвет существ игрока</param>
        /// <param name="inputPlayerHP">Здоровье игрока</param>
        /// <param name="inputPlayerType">Тип игрока</param>
        /// <param name="inputScreenSize">Размер экрана по х</param>
        public Player(
            string inputPlayerName,
            Color inputPlayerColor,
            int inputPlayerHP,
            bool inputPlayerType,
            CoordinatesXY inputScreenSize)
        {
            Name = inputPlayerName;
            Color = inputPlayerColor;
            HP = inputPlayerHP;
            Current = inputPlayerType;
            HitBoxPositionX = inputScreenSize.CoordinateIntX;
            ScreenSizeX = inputScreenSize.CoordinateIntX;
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
            get => playerType;
            set => playerType = value;
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
        /// Получить или вернуть здоровье игрока
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
            get => playerName;
            set => playerName = value;
        }
        /// <summary>
        /// Установить или вернуть размер экрана по x
        /// </summary>
        public int ScreenSizeX
        {
            get => screenSizeWidth;
            set => screenSizeWidth = value;
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
