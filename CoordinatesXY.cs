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
namespace AKSU
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
        private float coordinateX;
        /// <summary>
        /// Y координата в float
        /// </summary>
        private float coordinateY;
        #endregion
        #region Properties
        /// <summary>
        /// Установить или вернуть X
        /// </summary>
        /// <value>float x</value>
        public float CoordinateFloatX
        {
            get => coordinateX;
            set => coordinateX = value;
        }
        /// <summary>
        /// Установить или вернуть Y
        /// </summary>
        /// <value>float y</value>
        public float CoordinateFloatY
        {
            get => coordinateY;
            set => coordinateY = value;
        }
        /// <summary>
        /// Установить или вернуть X
        /// </summary>
        /// <value>int x</value>
        public int CoordinateIntX
        {
            get => (int)coordinateX;
            set => coordinateX = value;
        }
        /// <summary>
        /// Установить или вернуть Y
        /// </summary>
        /// <value>int y</value>
        public int CoordinateIntY
        {
            get => (int)coordinateY;
            set => coordinateY = value;
        }
        /// <summary>
        /// Вернуть нулевую координату X
        /// </summary>
        /// <value>float x0</value>
        public float CoordinateFloatZeroX
        {
            get => 0;
        }
        /// <summary>
        /// Вернуть нулевую координату Y
        /// </summary>
        /// <value>float y0</value>
        public float CoordinateFloatZeroY
        {
            get => 0;
        }
        /// <summary>
        /// Конструктор класса <see cref="CoordinatesXY" />.
        /// </summary>
        /// <param name="InputX">Позиция x</param>
        /// <param name="InputY">Позиция y</param>
        public CoordinatesXY(float InputX, float InputY)
        {
            CoordinateFloatX = InputX;
            CoordinateFloatY = InputY;
        }
        #endregion
        #region Operators
        /// <summary>
        /// Оператор сложения координат
        /// </summary>
        /// <param name="First">Первый CoordinatesXY</param>
        /// <param name="Second">Второй CoordinatesXY</param>
        /// <returns>CoordinatesXY</returns>
        public static CoordinatesXY operator +(CoordinatesXY First, CoordinatesXY Second)
        {
            return new CoordinatesXY(First.CoordinateIntX + Second.CoordinateIntX, First.CoordinateIntY + Second.CoordinateIntY);
        }
        /// <summary>
        /// Оператор разности координат
        /// </summary>
        /// <param name="First">Первый CoordinatesXY</param>
        /// <param name="Second">Второй CoordinatesXY</param>
        /// <returns>CoordinatesXY</returns>
        public static CoordinatesXY operator -(CoordinatesXY First, CoordinatesXY Second)
        {
            return new CoordinatesXY(First.CoordinateIntX - Second.CoordinateIntX, First.CoordinateIntY - Second.CoordinateIntY);
        }
        #endregion
    }
}