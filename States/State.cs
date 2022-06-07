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
    public abstract class State
    {
        #region Fields
        /// <summary>
        /// TМенеджер контента игры
        /// </summary>
        protected ContentManager CurrentContentManager;
        /// <summary>
        /// Графическое устройство
        /// </summary>
        protected GraphicsDevice CurrentGraphicsDevice;
        /// <summary>
        /// TИнициализатор игры
        /// </summary>
        protected Initializer CurrentGame;
        /// <summary>
        /// Размеры экрана
        /// </summary>
        protected CoordinatesXY ThisScreenSize;
        #endregion
        #region Methods
        /// <summary>
        /// Конструктор класса <see cref="State" />.
        /// </summary>
        /// <param name="InputGame"> Класс-инициализатор</param>
        /// <param name="InputGraphicsDevice">Графическое устройство.</param>
        /// <param name="InputContentManager">Менеджер контента</param>
        /// <param name="InputScreenSize">Размеры экрана</param>
        public State(
            Initializer InputGame,
            GraphicsDevice InputGraphicsDevice,
            ContentManager InputContentManager,
            CoordinatesXY InputScreenSize)
        {
            CurrentGame = InputGame;
            CurrentGraphicsDevice = InputGraphicsDevice;
            CurrentContentManager = InputContentManager;
            ThisScreenSize = InputScreenSize;
        }
        /// <summary>
        /// Отрисовать экран
        /// </summary>
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        /// <param name="CurrentSpriteBatch">Объект оптимизатора графической отрисовки</param>
        public abstract void Draw(GameTime CurrentGameTime, SpriteBatch CurrentSpriteBatch);
        /// <summary>
        /// Дейтсвие после обновления
        /// </summary>
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public abstract void PostUpdate(GameTime CurrentGameTime);
        /// <summary>
        /// Обновить логику
        /// </summary>
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public abstract void Update(GameTime CurrentGameTime);
        #endregion
    }
}
