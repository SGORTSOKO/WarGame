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

namespace WarGame.States
{
    /// <summary>
    /// Абстрактный класс. Потомки этого класса - окна приложения (game states):
    /// окно одиночной игры, таблица рекордов, меню
    /// </summary>
    public abstract class State
    {
        #region Fields
        /// <summary>
        /// TМенеджер контента игры
        /// </summary>
        protected ContentManager thisContent;
        /// <summary>
        /// Графическое устройство
        /// </summary>
        protected GraphicsDevice thisGraphicsDevice;
        /// <summary>
        /// TИнициализатор игры
        /// </summary>
        protected Initializer thisGame;
        /// <summary>
        /// Размеры экрана
        /// </summary>
        protected CoordinatesXY thisScreenSize;
        #endregion
        #region Methods
        /// <summary>
        /// Конструктор класса <see cref="State" />.
        /// </summary>
        /// <param name="inputGame"> Класс-инициализатор</param>
        /// <param name="inputGraphicsDevice">Графическое устройство.</param>
        /// <param name="inputContent">Менеджер контента</param>
        /// <param name="inputScreenSize">Размеры экрана</param>
        public State(
            Initializer inputGame,
            GraphicsDevice inputGraphicsDevice,
            ContentManager inputContent,
            CoordinatesXY inputScreenSize)
        {
            thisGame = inputGame;
            thisGraphicsDevice = inputGraphicsDevice;
            thisContent = inputContent;
            thisScreenSize = inputScreenSize;
        }
        /// <summary>
        /// Отрисовать экран
        /// </summary>
        /// <param name="gameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        /// <param name="spriteBatch">Объект оптимизатора графической отрисовки</param>
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        /// <summary>
        /// Дейтсвие после обновления
        /// </summary>
        /// <param name="gameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public abstract void PostUpdate(GameTime gameTime);
        /// <summary>
        /// Обновить логику
        /// </summary>
        /// <param name="gameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public abstract void Update(GameTime gameTime);
        #endregion
    }
}
