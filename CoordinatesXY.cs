// ***********************************************************************
// Assembly         : WarGame
// Author           : abros
// Created          : 05-29-2022
//
// Last Modified By : abros
// Last Modified On : 06-06-2022
// ***********************************************************************
// <copyright file="CoordinatesXY.cs" company="Kurgan State University">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace WarGame
{
    /// <summary>
    /// Класс-хранилище векторных координат
    /// </summary>
    public class CoordinatesXY
    {
        #region Fields
        /// <summary>
        /// Х координата в float
        /// </summary>
        private float x;
        /// <summary>
        /// Y координата в float
        /// </summary>
        private float y;
        #endregion
        #region Properties
        /// <summary>
        /// Установить или вернуть X
        /// </summary>
        /// <value>float x</value>
        public float Xf
        {
            get => x;
            set => x = value;
        }
        /// <summary>
        /// Установить или вернуть Y
        /// </summary>
        /// <value>float y</value>
        public float Yf
        {
            get => y;
            set => y = value;
        }
        /// <summary>
        /// Установить или вернуть X
        /// </summary>
        /// <value>int x</value>
        public int X
        {
            get => (int)x;
            set => x = value;
        }
        /// <summary>
        /// Установить или вернуть Y
        /// </summary>
        /// <value>int y</value>
        public int Y
        {
            get => (int)y;
            set => y = value;
        }
        /// <summary>
        /// Вернуть нулевую координату X
        /// </summary>
        /// <value>float x0</value>
        public float X0
        {
            get => 0;
        }
        /// <summary>
        /// Вернуть нулевую координату Y
        /// </summary>
        /// <value>float y0</value>
        public float Y0
        {
            get => 0;
        }
        /// <summary>
        /// Конструктор класса <see cref="CoordinatesXY" />.
        /// </summary>
        /// <param name="x">Позиция x</param>
        /// <param name="y">Позиция y</param>
        public CoordinatesXY(float x, float y)
        {
            Xf = x;
            Yf = y;
        }
        #endregion
        #region Operators
        /// <summary>
        /// Оператор сложения координат
        /// </summary>
        /// <param name="first">Первый CoordinatesXY</param>
        /// <param name="second">Второй CoordinatesXY</param>
        /// <returns>CoordinatesXY</returns>
        public static CoordinatesXY operator +(CoordinatesXY first, CoordinatesXY second)
        {
            return new CoordinatesXY(first.X + second.X, first.Y + second.Y);
        }
        /// <summary>
        /// Оператор разности координат
        /// </summary>
        /// <param name="first">Первый CoordinatesXY</param>
        /// <param name="second">Второй CoordinatesXY</param>
        /// <returns>CoordinatesXY</returns>
        public static CoordinatesXY operator -(CoordinatesXY first, CoordinatesXY second)
        {
            return new CoordinatesXY(first.X - second.X, first.Y - second.Y);
        }
        /// <summary>
        /// Переопределение увеличения координаты x
        /// </summary>
        /// <param name="first">The CoordinatesXY</param>
        /// <param name="X">int x</param>
        /// <returns>CoordinatesXY</returns>
        public static CoordinatesXY operator +(CoordinatesXY first, int X)
        {
            return new CoordinatesXY(first.X + X, first.Y);
        }
        #endregion
    }
}