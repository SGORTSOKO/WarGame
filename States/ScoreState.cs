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

using System.Collections.Generic;
using System.Linq;
using AKSU.MScore;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using AKSU.ScreenComponent;

namespace AKSU.States
{
    /// <summary>
    /// Класс таблицы рекордов ScoreState.
    /// Унаследовано от <see cref="AKSU.States.BaseState" />
    /// </summary>
    /// <seealso cref="AKSU.States.BaseState" />
    class ScoreState : BaseState
    {
        /// <summary>
        /// Шрифт текста
        /// </summary>
        private SpriteFont font;
        /// <summary>
        /// Список компонентов (кнопки)
        /// </summary>
        private List<BaseComponent> components;
        /// <summary>
        /// Фоновое изображение
        /// </summary>
        private Texture2D backGround;
        /// <summary>
        /// Менеджер таблицы рекордов
        /// </summary>
        private ScoreManager currentScoreManager;
        /// <summary>
        /// Конструктор класса <see cref="ScoreState" />.
        /// </summary>
        /// <param name="inputGame">Инициализатор приложения</param>
        /// <param name="inputGraphicsDevice">Графическое устройство</param>
        /// <param name="inputContentManager">Менеджер контента</param>
        /// <param name="inputScreenSize">Размеры экрана</param>
        public ScoreState(
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
            backGround = inputContentManager.Load<Texture2D>("BackGrounds/Road");
            font = inputContentManager.Load<SpriteFont>("Fonts/TimesNewRomanSmall");
            currentScoreManager = ScoreManager.Load();

            Button MenuButton = new Button(inputContentManager.Load<Texture2D>("Buttons/Menu"), 
                inputContentManager.Load<SpriteFont>("Fonts/TimesNewRomanSmall")) //Кнопка меню
            {
                Position = new Vector2(thisScreenSize.CoordinateIntX * 0.01f, thisScreenSize.CoordinateIntY * 0.9f)
            };
            MenuButton.Click += MenuButton_Click;

            components = new List<BaseComponent>() //Список компонентов
            {
            MenuButton,
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
             
            currentSpriteBatch.Draw( //Отрисовать фон
                backGround,
                new Vector2(0, 0),
                new Rectangle(0,
                    0,
                    thisScreenSize.CoordinateIntX,
                    thisScreenSize.CoordinateIntY),
                Color.White,
                0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0f);

            foreach (var Component in components) //Отрисовать компоненты
                Component.Draw(currentGameTime, currentSpriteBatch);

            currentSpriteBatch.DrawString( //Отрисовать таблицу рекордов
                font,
                "Highscores:\n" + 
                string.Join(
                    "\n", 
                    currentScoreManager.HighScores.Select(
                        C => C.PlayerName + 
                        ": " + 
                        C.Value
                        ).ToArray()),
                new Vector2(300, 10),
                Color.Black);

            currentSpriteBatch.End();
        }
        /// <summary>
        /// UОбновить логику компонентов
        /// </summary>
        /// <param name="currentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void Update(GameTime currentGameTime)
        {
            foreach (var Component in components)
                Component.Update(currentGameTime);
        }
        /// <summary>
        /// Действие после обновления
        /// </summary>
        /// <param name="currentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void PostUpdate(GameTime currentGameTime)
        {
        }
        /// <summary>
        /// Обработчик события нажатия клавиши выхода в меню
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void MenuButton_Click(object sender, System.EventArgs e)
        {
            currentGame.ChangeState(new MenuState(
                currentGame,
                currentGraphicsDevice,
                currentContentManager,
                thisScreenSize));
        }
    }
}
