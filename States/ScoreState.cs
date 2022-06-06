// ***********************************************************************
// Assembly         : WarGame
// Author           : abros
// Created          : 05-30-2022
//
// Last Modified By : abros
// Last Modified On : 06-06-2022
// ***********************************************************************
// <copyright file="ScoreState.cs" company="Kurgan State University">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using WarGame.MScore;
using WarGame.ScreenComponent;

namespace WarGame.States
{
    /// <summary>
    /// Класс таблицы рекордов ScoreState.
    /// Унаследовано от <see cref="WarGame.States.State" />
    /// </summary>
    /// <seealso cref="WarGame.States.State" />
    class ScoreState : State
    {
        /// <summary>
        /// Шрифт текста
        /// </summary>
        private SpriteFont font;
        /// <summary>
        /// Список компонентов (кнопки)
        /// </summary>
        private List<Component> components;
        /// <summary>
        /// Фоновое изображение
        /// </summary>
        private Texture2D backGround;
        /// <summary>
        /// Менеджер таблицы рекордов
        /// </summary>
        private ScoreManager scoreManager;
        /// <summary>
        /// Конструктор класса <see cref="ScoreState" />.
        /// </summary>
        /// <param name="inputGame">Инициализатор приложения</param>
        /// <param name="inputGraphicsDevice">Графическое устройство</param>
        /// <param name="inputContent">Менеджер контента</param>
        /// <param name="inputScreenSize">Размеры экрана</param>
        public ScoreState(
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
            backGround = inputContent.Load<Texture2D>("BackGrounds/Road");
            font = inputContent.Load<SpriteFont>("Fonts/TimesNewRomanSmall");
            scoreManager = ScoreManager.Load();

            //Кнопка меню
            Button menuButton = new Button(inputContent.Load<Texture2D>("Buttons/Menu"), inputContent.Load<SpriteFont>("Fonts/TimesNewRomanSmall"))
            {
                Position = new Vector2(thisScreenSize.X * 0.01f, thisScreenSize.Y * 0.9f)
            };
            menuButton.Click += MenuButton_Click;

            ///Список компонентов
            components = new List<Component>()
            {
            menuButton,
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
             //Отрисовать фон
            spriteBatch.Draw(
                backGround,
                new Vector2(0, 0),
                new Rectangle(0,
                    0,
                    thisScreenSize.X,
                    thisScreenSize.Y),
                Color.White,
                0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0f);

            //Отрисовать компоненты
            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);

            //Отрисовать таблицу рекордов
            spriteBatch.DrawString(
                font,
                "Highscores:\n" + string.Join("\n", scoreManager.Highscores.Select(c => c.PlayerName + ": " + c.Value).ToArray()),
                new Vector2(300, 10),
                Color.Black);

            spriteBatch.End();
        }
        /// <summary>
        /// UОбновить логику компонентов
        /// </summary>
        /// <param name="gameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
                component.Update(gameTime);
        }
        /// <summary>
        /// Действие после обновления
        /// </summary>
        /// <param name="gameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void PostUpdate(GameTime gameTime)
        {
        }
        /// <summary>
        /// Обработчик события нажатия клавиши выхода в меню
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void MenuButton_Click(object sender, System.EventArgs e)
        {
            thisGame.ChangeState(new MenuState(
                thisGame,
                thisGraphicsDevice,
                thisContent,
                thisScreenSize));
        }
    }
}
