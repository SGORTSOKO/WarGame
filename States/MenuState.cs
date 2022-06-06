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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using WarGame.ScreenComponent;

namespace WarGame.States
{
    /// <summary>
    /// Класс игрового меню MenuState.
    /// Унаследовано от <see cref="WarGame.States.State" />
    /// </summary>
    /// <seealso cref="WarGame.States.State" />
    public class MenuState : State
    {
        /// <summary>
        /// Компоненты экрана
        /// </summary>
        private List<Component> components;
        /// <summary>
        /// Фоновое изображение
        /// </summary>
        private Texture2D backGround;
        /// <summary>
        /// Конструктор класса <see cref="MenuState" />.
        /// </summary>
        /// <param name="inputGame">Инициализатор игры</param>
        /// <param name="inputGraphicsDevice">Графическое устройство</param>
        /// <param name="inputContent">Менеджер контента</param>
        /// <param name="inputScreenSize">Размеры экрана</param>
        public MenuState(
            Initializer inputGame,
            GraphicsDevice inputGraphicsDevice,
            ContentManager inputContent,
            CoordinatesXY inputScreenSize)
        : base(
              inputGame,
              inputGraphicsDevice,
              inputContent,
              inputScreenSize)
        {
            Texture2D newGameButtonTexture = inputContent.Load<Texture2D>("Buttons/NewGame");
            Texture2D quitButtonTexture = inputContent.Load<Texture2D>("Buttons/Exit");
            Texture2D scoreButtonTexture = inputContent.Load<Texture2D>("Buttons/Scores");
            Texture2D fieldTexture = inputContent.Load<Texture2D>("Buttons/ClearFieldBig");
            Texture2D settingsTexture = inputContent.Load<Texture2D>("Buttons/Settings");
            backGround = inputContent.Load<Texture2D>("BackGrounds/Road");
            SpriteFont buttonFont = inputContent.Load<SpriteFont>("Fonts/TimesNewRomanSmall");

            //Кнопка новой игры
            Button newGameButton = new Button(newGameButtonTexture, buttonFont)
            {
                Position = new Vector2(200, 200),
            };
            newGameButton.Click += NewGameButton_Click;

            //Кнопка перехода к таблице рекордов
            var scoreButton = new Button(scoreButtonTexture, buttonFont)
            {
                Position = new Vector2(200, 250),
            };
            scoreButton.Click += ScoreButton_Click;

            //переход к настройкам
            var settingsButton = new Button(settingsTexture, buttonFont)
            {
                Position = new Vector2(200, 300),
            };
            settingsButton.Click += SettingsButton_Click;

            //Выход из игры
            var quitGameButton = new Button(quitButtonTexture, buttonFont)
            {
                Position = new Vector2(200, 350),
            };
            quitGameButton.Click += QuitGameButton_Click;

            //список компонентов экрана
            components = new List<Component>()
            {
                newGameButton,
                settingsButton,
                scoreButton,
                quitGameButton,
            };
        }
        /// <summary>
        /// Отрисовать экран
        /// </summary>
        /// <param name="gameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        /// <param name="spriteBatch">Объект оптимизатора графической отрисовки</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //отрисовать фон
            spriteBatch.Draw(
                backGround,
                new Vector2(0, 0),
                new Rectangle(0, 0, thisScreenSize.X, thisScreenSize.Y),
                Color.White,
                0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0f);

            //Отрисовать компоненты
            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        /// <summary>
        /// Событие нажатия кнопки новой игры
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            thisGame.ChangeState(new StateSingleplayer(
                thisGame,
                thisGraphicsDevice,
                thisContent,
                thisScreenSize));
        }


        /// <summary>
        /// Событие нажатия кнопки настроек
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        private void SettingsButton_Click(object sender, EventArgs e)
        {
            thisGame.ChangeState(new SettingsState(
                thisGame,
                thisGraphicsDevice,
                thisContent,
                thisScreenSize));
        }

        /// <summary>
        /// Событие нажатия кнопки таблицы рекордов
        /// </summary>
        /// <param name="sender">Истоник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        private void ScoreButton_Click(object sender, EventArgs e)
        {
            thisGame.ChangeState(new ScoreState(
                thisGame,
                thisGraphicsDevice,
                thisContent,
                thisScreenSize));
        }

        /// <summary>
        /// Обновить логику компонентов
        /// </summary>
        /// <param name="gameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
                component.Update(gameTime);
        }

        /// <summary>
        /// Событие нажатия кнопки выход
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            thisGame.Exit();
        }
        /// <summary>
        /// Действие после обновления
        /// </summary>
        /// <param name="gameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void PostUpdate(GameTime gameTime)
        {
        }
    }
}
