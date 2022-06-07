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
        private Color PreferColor;
        /// <summary>
        /// Имя игрока
        /// </summary>
        private string PlayerName;
        /// <summary>
        /// Здоровье игрока
        /// </summary>
        private int PlayerHP;
        /// <summary>
        /// Тип игрока
        /// false: бот (справа)
        /// true: Текущий (слева)
        /// </summary>
        private bool PlayerType;
        /// <summary>
        /// Положение "тела" игрока
        /// </summary>
        private int HitBoxPosition;
        /// <summary>
        /// Размер экрана по х
        /// </summary>
        private int ScreenSizeWidth;
        #endregion
        #region Constructors
        /// <summary>
        /// Конструктор класса <see cref="Player" />.
        /// </summary>
        /// <param name="InputPlayerName">Имя игрока</param>
        /// <param name="InputPlayerColor">Цвет существ игрока</param>
        /// <param name="InputPlayerHP">Здоровье игрока</param>
        /// <param name="InputPlayerType">Тип игрока</param>
        /// <param name="InputScreenSize">Размер экрана по х</param>
        public Player(
            string InputPlayerName,
            Color InputPlayerColor,
            int InputPlayerHP,
            bool InputPlayerType,
            CoordinatesXY InputScreenSize)
        {
            Name = InputPlayerName;
            Color = InputPlayerColor;
            HP = InputPlayerHP;
            Current = InputPlayerType;
            HitBoxPositionX = InputScreenSize.CoordinateIntX;
            ScreenSizeX = InputScreenSize.CoordinateIntX;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Установить или вернуть цвет существ игрока
        /// </summary>
        public Color Color
        {
            get => PreferColor;
            set => PreferColor = value;
        }
        /// <summary>
        /// Тип игрока <see cref="Player" /> (текущий или нет).
        /// </summary>
        /// <value><c>true</c> если текущий (левый); иначе, <c>false</c>.</value>
        public bool Current
        {
            get => PlayerType;
            set => PlayerType = value;
        }
        /// <summary>
        /// Позиция "тела" игрока по Х
        /// </summary>
        public int HitBoxPositionX
        {
            get => HitBoxPosition;
            set
            {
                if (Current == true)
                {
                    HitBoxPosition = 0;
                }
                else
                {
                    HitBoxPosition = value;
                }
            }
        }
        /// <summary>
        /// Получить или вернуть здоровье игрока
        /// </summary>
        public int HP
        {
            get => PlayerHP;
            set => PlayerHP = value;
        }
        /// <summary>
        /// Установить или вернуть имя игрока
        /// </summary>
        public string Name
        {
            get => PlayerName;
            set => PlayerName = value;
        }
        /// <summary>
        /// Установить или вернуть размер экрана по x
        /// </summary>
        public int ScreenSizeX
        {
            get => ScreenSizeWidth;
            set => ScreenSizeWidth = value;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Атаковать игрока
        /// </summary>
        /// <param name="Power">Сила существа</param>
        public void AttackMe(int Power)
        {
            HP -= Power;
        }
        #endregion
    }
}
