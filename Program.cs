// ***********************************************************************
// Assembly         : WarGame
// Author           : abros
// Created          : 05-16-2022
//
// Last Modified By : abros
// Last Modified On : 06-06-2022
// ***********************************************************************
// <copyright file="Program.cs" company="Kurgan State University">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace AKSU
{
    /// <summary>
    /// Базовый класс приложения
    /// </summary>
    public static class Program
    {
        [STAThread]
        /// <summary>
        /// Точка входа приложения
        /// </summary>
        static void Main()
        {
            using (var game = new Initializer())
                game.Run();
        }
    }
}
