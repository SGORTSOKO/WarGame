// ***********************************************************************
// Assembly         : WarGame
// Author           : abros
// Created          : 05-29-2022
//
// Last Modified By : abros
// Last Modified On : 06-06-2022
// ***********************************************************************
// <copyright file="State.cs" company="Kurgan State University">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AKSU.States
{
    /// <summary>
    /// Абстрактный класс. Потомки этого класса - окна приложения (game states):
    /// окно одиночной игры, таблица рекордов, меню
    /// </summary>
    public abstract class BaseState
    {
        #region Fields
        /// <summary>
        /// TМенеджер контента игры
        /// </summary>
        protected ContentManager currentContentManager;
        /// <summary>
        /// Графическое устройство
        /// </summary>
        protected GraphicsDevice currentGraphicsDevice;
        /// <summary>
        /// TИнициализатор игры
        /// </summary>
        protected Initializer currentGame;
        /// <summary>
        /// Размеры экрана
        /// </summary>
        protected CoordinatesXY thisScreenSize;
        #endregion
        #region Methods
        /// <summary>
        /// Конструктор класса <see cref="BaseState" />.
        /// </summary>
        /// <param name="inputGame"> Класс-инициализатор</param>
        /// <param name="inputGraphicsDevice">Графическое устройство.</param>
        /// <param name="inputContentManager">Менеджер контента</param>
        /// <param name="inputScreenSize">Размеры экрана</param>
        public BaseState(
            Initializer inputGame,
            GraphicsDevice inputGraphicsDevice,
            ContentManager inputContentManager,
            CoordinatesXY inputScreenSize)
        {
            currentGame = inputGame;
            currentGraphicsDevice = inputGraphicsDevice;
            currentContentManager = inputContentManager;
            thisScreenSize = inputScreenSize;
        }
        /// <summary>
        /// Отрисовать экран
        /// </summary>
        /// <param name="currentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        /// <param name="currentSpriteBatch">Объект оптимизатора графической отрисовки</param>
        public abstract void Draw(GameTime currentGameTime, SpriteBatch currentSpriteBatch);
        /// <summary>
        /// Дейтсвие после обновления
        /// </summary>
        /// <param name="currentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public abstract void PostUpdate(GameTime currentGameTime);
        /// <summary>
        /// Обновить логику
        /// </summary>
        /// <param name="currentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public abstract void Update(GameTime currentGameTime);
        #endregion
    }
}
