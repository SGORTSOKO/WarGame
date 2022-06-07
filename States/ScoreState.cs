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
    /// Унаследовано от <see cref="AKSU.States.State" />
    /// </summary>
    /// <seealso cref="AKSU.States.State" />
    class ScoreState : State
    {
        /// <summary>
        /// Шрифт текста
        /// </summary>
        private SpriteFont Font;
        /// <summary>
        /// Список компонентов (кнопки)
        /// </summary>
        private List<Component> Components;
        /// <summary>
        /// Фоновое изображение
        /// </summary>
        private Texture2D BackGround;
        /// <summary>
        /// Менеджер таблицы рекордов
        /// </summary>
        private ScoreManager CurrentScoreManager;
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
            BackGround = InputContentManager.Load<Texture2D>("BackGrounds/Road");
            Font = InputContentManager.Load<SpriteFont>("Fonts/TimesNewRomanSmall");
            CurrentScoreManager = ScoreManager.Load();

            //Кнопка меню
            Button MenuButton = new Button(InputContentManager.Load<Texture2D>("Buttons/Menu"), InputContentManager.Load<SpriteFont>("Fonts/TimesNewRomanSmall"))
            {
                Position = new Vector2(ThisScreenSize.CoordinateIntX * 0.01f, ThisScreenSize.CoordinateIntY * 0.9f)
            };
            MenuButton.Click += MenuButton_Click;

            ///Список компонентов
            Components = new List<Component>()
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
             //Отрисовать фон
            CurrentSpriteBatch.Draw(
                BackGround,
                new Vector2(0, 0),
                new Rectangle(0,
                    0,
                    ThisScreenSize.CoordinateIntX,
                    ThisScreenSize.CoordinateIntY),
                Color.White,
                0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0f);

            //Отрисовать компоненты
            foreach (var Component in Components)
                Component.Draw(CurrentGameTime, CurrentSpriteBatch);

            //Отрисовать таблицу рекордов
            CurrentSpriteBatch.DrawString(
                Font,
                "Highscores:\n" + 
                string.Join(
                    "\n", 
                    CurrentScoreManager.Highscores.Select(
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
            foreach (var Component in Components)
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
            CurrentGame.ChangeState(new MenuState(
                CurrentGame,
                CurrentGraphicsDevice,
                CurrentContentManager,
                ThisScreenSize));
        }
    }
}
