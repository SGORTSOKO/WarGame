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
        /// <param name="InputGame">Инициализатор игры</param>
        /// <param name="InputGraphicsDevice">Графическое устройство</param>
        /// <param name="InputContentManager">Менеджер контента</param>
        /// <param name="InputScreenSize">Размеры экрана</param>
        public MenuState(
            Initializer InputGame,
            GraphicsDevice InputGraphicsDevice,
            ContentManager InputContentManager,
            CoordinatesXY InputScreenSize)
        : base(
              InputGame,
              InputGraphicsDevice,
              InputContentManager,
              InputScreenSize)
        {
            Texture2D NewGameButtonTexture = InputContentManager.Load<Texture2D>("Buttons/NewGame");
            Texture2D QuitButtonTexture = InputContentManager.Load<Texture2D>("Buttons/Exit");
            Texture2D ScoreButtonTexture = InputContentManager.Load<Texture2D>("Buttons/Scores");
            Texture2D FieldTexture = InputContentManager.Load<Texture2D>("Buttons/ClearFieldBig");
            Texture2D SettingsTexture = InputContentManager.Load<Texture2D>("Buttons/Settings");
            backGround = InputContentManager.Load<Texture2D>("BackGrounds/Road");
            SpriteFont ButtonFont = InputContentManager.Load<SpriteFont>("Fonts/TimesNewRomanSmall");

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
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        /// <param name="CurrentSpriteBatch">Объект оптимизатора графической отрисовки</param>
        public override void Draw(GameTime CurrentGameTime, SpriteBatch CurrentSpriteBatch)
        {
            CurrentSpriteBatch.Begin();

            CurrentSpriteBatch.Draw( //отрисовать фон
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
                Component.Draw(CurrentGameTime, CurrentSpriteBatch);

            CurrentSpriteBatch.End();
        }

        /// <summary>
        /// Событие нажатия кнопки новой игры
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        private void NewGameButton_Click(object Sender, EventArgs e)
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
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        private void SettingsButton_Click(object Sender, EventArgs e)
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
        /// <param name="Sender">Истоник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        private void ScoreButton_Click(object Sender, EventArgs e)
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
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void Update(GameTime CurrentGameTime)
        {
            foreach (var Component in components)
                Component.Update(CurrentGameTime);
        }

        /// <summary>
        /// Событие нажатия кнопки выход
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        private void QuitGameButton_Click(object Sender, EventArgs e)
        {
            currentGame.Exit();
        }
        /// <summary>
        /// Действие после обновления
        /// </summary>
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void PostUpdate(GameTime CurrentGameTime)
        {
        }
    }
}
