// ***********************************************************************
// Assembly         : WarGame
// Author           : abros
// Created          : 06-03-2022
//
// Last Modified By : abros
// Last Modified On : 06-03-2022
// ***********************************************************************
// <copyright file="Score.cs" company="Kurgan State University">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace WarGame.MScore
{
    /// <summary>
    /// Структура строки таблицы рекордов
    /// </summary>
    public struct Score
    {
        /// <summary>
        /// Установить или получить имя игрока
        /// </summary>
        public string PlayerName { get; set; }
        /// <summary>
        /// Установить или получить счёт игрока
        /// </summary>
        public int Value { get; set; }
    }
}
