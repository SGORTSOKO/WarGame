// ***********************************************************************
// Assembly         : WarGame
// Author           : abros
// Created          : 05-29-2022
//
// Last Modified By : abros
// Last Modified On : 06-06-2022
// ***********************************************************************
// <copyright file="MenuState.cs" company="Kurgan State University">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using AKSU.ScreenComponent;

namespace AKSU.States
{
    /// <summary>
    /// Класс игрового меню MenuState.
    /// Унаследовано от <see cref="AKSU.States.BaseState" />
    /// </summary>
    /// <seealso cref="AKSU.States.BaseState" />
    public class MenuState : BaseState
    {
        /// <summary>
        /// Компоненты экрана
        /// </summary>
        private List<BaseComponent> components;
        /// <summary>
        /// Фоновое изображение
        /// </summary>
        private Texture2D backGround;
        /// <summary>
        /// Конструктор класса <see cref="MenuState" />.
        /// </summary>
        /// <param name="inputGame">Инициализатор игры</param>
        /// <param name="inputGraphicsDevice">Графическое устройство</param>
        /// <param name="inputContentManager">Менеджер контента</param>
        /// <param name="inputScreenSize">Размеры экрана</param>
        public MenuState(
            Initializer inputGame,
            GraphicsDevice inputGraphicsDevice,
            ContentManager inputContentManager,
            CoordinatesXY inputScreenSize)
        : base(
              inputGame,
              inputGraphicsDevice,
              inputContentManager,
              inputScreenSize)
        {
            Texture2D NewGameButtonTexture = inputContentManager.Load<Texture2D>("Buttons/NewGame");
            Texture2D QuitButtonTexture = inputContentManager.Load<Texture2D>("Buttons/Exit");
            Texture2D ScoreButtonTexture = inputContentManager.Load<Texture2D>("Buttons/Scores");
            Texture2D FieldTexture = inputContentManager.Load<Texture2D>("Buttons/ClearFieldBig");
            Texture2D SettingsTexture = inputContentManager.Load<Texture2D>("Buttons/Settings");
            backGround = inputContentManager.Load<Texture2D>("BackGrounds/Road");
            SpriteFont ButtonFont = inputContentManager.Load<SpriteFont>("Fonts/TimesNewRomanSmall");

            Button NewGameButton = new Button(NewGameButtonTexture, ButtonFont) //Кнопка новой игры
            {
                Position = new Vector2(200, 200),
            };
            NewGameButton.Click += NewGameButton_Click;

            var ScoreButton = new Button(ScoreButtonTexture, ButtonFont) //Кнопка перехода к таблице рекордов
            {
                Position = new Vector2(200, 250),
            };
            ScoreButton.Click += ScoreButton_Click;

            var SettingsButton = new Button(SettingsTexture, ButtonFont) //переход к настройкам
            {
                Position = new Vector2(200, 300),
            };
            SettingsButton.Click += SettingsButton_Click;

            var QuitGameButton = new Button(QuitButtonTexture, ButtonFont) //Выход из игры
            {
                Position = new Vector2(200, 350),
            };
            QuitGameButton.Click += QuitGameButton_Click;

            components = new List<BaseComponent>() //список компонентов экрана
            {
                NewGameButton,
                SettingsButton,
                ScoreButton,
                QuitGameButton,
            };
        }
        /// <summary>
        /// Отрисовать экран
        /// </summary>
        /// <param name="currentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        /// <param name="currentSpriteBatch">Объект оптимизатора графической отрисовки</param>
        public override void Draw(GameTime currentGameTime, SpriteBatch currentSpriteBatch)
        {
            currentSpriteBatch.Begin();

            currentSpriteBatch.Draw( //отрисовать фон
                backGround,
                new Vector2(0, 0),
                new Rectangle(0, 0, thisScreenSize.CoordinateIntX, thisScreenSize.CoordinateIntY),
                Color.White,
                0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0f);

            foreach (var Component in components) //Отрисовать компоненты
                Component.Draw(currentGameTime, currentSpriteBatch);

            currentSpriteBatch.End();
        }

        /// <summary>
        /// Событие нажатия кнопки новой игры
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            currentGame.ChangeState(new StateSingleplayer(
                currentGame,
                currentGraphicsDevice,
                currentContentManager,
                thisScreenSize));
        }


        /// <summary>
        /// Событие нажатия кнопки настроек
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        private void SettingsButton_Click(object sender, EventArgs e)
        {
            currentGame.ChangeState(new SettingsState(
                currentGame,
                currentGraphicsDevice,
                currentContentManager,
                thisScreenSize));
        }

        /// <summary>
        /// Событие нажатия кнопки таблицы рекордов
        /// </summary>
        /// <param name="sender">Истоник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        private void ScoreButton_Click(object sender, EventArgs e)
        {
            currentGame.ChangeState(new ScoreState(
                currentGame,
                currentGraphicsDevice,
                currentContentManager,
                thisScreenSize));
        }

        /// <summary>
        /// Обновить логику компонентов
        /// </summary>
        /// <param name="currentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void Update(GameTime currentGameTime)
        {
            foreach (var Component in components)
                Component.Update(currentGameTime);
        }

        /// <summary>
        /// Событие нажатия кнопки выход
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            currentGame.Exit();
        }
        /// <summary>
        /// Действие после обновления
        /// </summary>
        /// <param name="currentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void PostUpdate(GameTime currentGameTime)
        {
        }
    }
}
