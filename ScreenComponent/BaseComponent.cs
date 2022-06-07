// ***********************************************************************
// Assembly         : WarGame
// Author           : abros
// Created          : 06-06-2022
//
// Last Modified By : abros
// Last Modified On : 06-06-2022
// ***********************************************************************
// <copyright file="Component.cs" company="Kurgan State University">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AKSU.ScreenComponent
{
    /// <summary>
    /// Абстрактный класс Component.
    /// </summary>
    public abstract class BaseComponent
    {
        /// <summary>
        /// Отрисовать компонент
        /// </summary>
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        /// <param name="CurrentSpriteBatch">Объект оптимизатора графической отрисовки</param>
        public abstract void Draw(GameTime CurrentGameTime, SpriteBatch CurrentSpriteBatch);
        /// <summary>
        /// Обновить логику кнопки
        /// </summary>
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public abstract void Update(GameTime CurrentGameTime);
    }
}
