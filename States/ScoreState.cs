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
        /// <param name="InputGame">Инициализатор приложения</param>
        /// <param name="InputGraphicsDevice">Графическое устройство</param>
        /// <param name="InputContentManager">Менеджер контента</param>
        /// <param name="InputScreenSize">Размеры экрана</param>
        public ScoreState(
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
            backGround = InputContentManager.Load<Texture2D>("BackGrounds/Road");
            font = InputContentManager.Load<SpriteFont>("Fonts/TimesNewRomanSmall");
            currentScoreManager = ScoreManager.Load();

            Button MenuButton = new Button(InputContentManager.Load<Texture2D>("Buttons/Menu"), 
                InputContentManager.Load<SpriteFont>("Fonts/TimesNewRomanSmall")) //Кнопка меню
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
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        /// <param name="CurrentSpriteBatch">Объект оптимизатора графической отрисовки</param>
        public override void Draw(GameTime CurrentGameTime, SpriteBatch CurrentSpriteBatch)
        {
            CurrentSpriteBatch.Begin();
             
            CurrentSpriteBatch.Draw( //Отрисовать фон
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
                Component.Draw(CurrentGameTime, CurrentSpriteBatch);

            CurrentSpriteBatch.DrawString( //Отрисовать таблицу рекордов
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

            CurrentSpriteBatch.End();
        }
        /// <summary>
        /// UОбновить логику компонентов
        /// </summary>
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void Update(GameTime CurrentGameTime)
        {
            foreach (var Component in components)
                Component.Update(CurrentGameTime);
        }
        /// <summary>
        /// Действие после обновления
        /// </summary>
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void PostUpdate(GameTime CurrentGameTime)
        {
        }
        /// <summary>
        /// Обработчик события нажатия клавиши выхода в меню
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void MenuButton_Click(object Sender, System.EventArgs e)
        {
            currentGame.ChangeState(new MenuState(
                currentGame,
                currentGraphicsDevice,
                currentContentManager,
                thisScreenSize));
        }
    }
}
