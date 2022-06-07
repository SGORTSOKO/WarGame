// ***********************************************************************
// Assembly         : WarGame
// Author           : abros
// Created          : 06-06-2022
//
// Last Modified By : abros
// Last Modified On : 06-06-2022
// ***********************************************************************
// <copyright file="Button.cs" company="Kurgan State University">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AKSU.ScreenComponent
{
    /// <summary>
    /// Класс кнопки Button.
    /// Унаследовано от <see cref="AKSU.Component" />
    /// </summary>
    /// <seealso cref="AKSU.Component" />
    public class Button : Component
    {
        #region Fields
        /// <summary>
        /// Текущее состояние мыши
        /// </summary>
        private MouseState CurrentMouse;
        /// <summary>
        /// Шрифт текста
        /// </summary>
        private SpriteFont Font;
        /// <summary>
        /// Состояние выделения
        /// </summary>
        private bool IsHovering;
        /// <summary>
        /// Предыдущее состояние мыши
        /// </summary>
        private MouseState PreviousMouse;
        /// <summary>
        /// Текстура кнопки
        /// </summary>
        private Texture2D Texture;
        /// <summary>
        /// Сдвиг текста по X
        /// </summary>
        private float ShiftX = -10;
        /// <summary>
        /// Сдвиг текста по Y
        /// </summary>
        private float ShiftY = -10;
        #endregion

        #region Properties
        /// <summary>
        /// Событие по нажатию мыши по кнопке
        /// </summary>
        public event EventHandler Click;
        /// <summary>
        /// Нажата ли <see cref="Button" />.
        /// </summary>
        /// <value><c>true</c> если нажата; иначе, <c>false</c>.</value>
        public bool Clicked { get; private set; }
        /// <summary>
        /// Цвет текста
        /// </summary>
        /// <value>Color</value>
        public Color PenColour { get; set; }
        /// <summary>
        /// Положение кнопки
        /// </summary>
        /// <value>Vector2</value>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Область кнопки (размеры)
        /// </summary>
        /// <value>Rectangle</value>
        public Rectangle Rectangle
        {
            get => new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                Texture.Width,
                Texture.Height);
        }
        /// <summary>
        /// Установить или получить содержимое текста кнопки
        /// </summary>
        /// <value>string</value>
        public string Text { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Конструктор класса <see cref="Button" />.
        /// </summary>
        /// <param name="InputTexture">Текстура кнопки</param>
        /// <param name="InputFont">Шрифт кнопки</param>
        public Button(Texture2D InputTexture, SpriteFont InputFont)
        {
            Texture = InputTexture;
            Font = InputFont;
            PenColour = Color.Black;
        }
        /// <summary>
        /// Отрисовать кнопку
        /// </summary>
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        /// <param name="CurrentSpriteBatch">Объект оптимизатора графической отрисовки</param>
        public override void Draw(GameTime CurrentGameTime, SpriteBatch CurrentSpriteBatch)
        {
            var colour = Color.White;
            if (IsHovering)
                colour = Color.Gray;

            CurrentSpriteBatch.Draw(Texture, Rectangle, colour);

            //Отрисовать текст
            if (!string.IsNullOrEmpty(Text))
            {
                //Центрирование
                if (ShiftX <= 0)
                    ShiftX = (Rectangle.X + (Rectangle.Width / 2)) - 
                        (Font.MeasureString(Text).X / 2);
                if (ShiftY <= 0)
                    ShiftY = (Rectangle.Y + (Rectangle.Height / 2)) - 
                        (Font.MeasureString(Text).Y / 2);

                //Отрисовка
                CurrentSpriteBatch.DrawString(
                    Font,
                    Text,
                    new Vector2(ShiftX, ShiftY),
                    PenColour
                    );
            }
        }
        /// <summary>
        /// Расчет состояния кнопки
        /// </summary>
        /// <param name="gameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void Update(GameTime gameTime)
        {
            //Обновление состояния мыши
            PreviousMouse = CurrentMouse;
            CurrentMouse = Mouse.GetState();

            var MouseRectangle = new Rectangle(
                CurrentMouse.X,
                CurrentMouse.Y,
                1,
                1);

            IsHovering = false;
            //Находится ли мышь над кнопкой
            if (MouseRectangle.Intersects(Rectangle))
            {
                //Выделить кнопку
                IsHovering = true;
                //Если нажата и отпущена
                if (CurrentMouse.LeftButton == ButtonState.Released && 
                    PreviousMouse.LeftButton == ButtonState.Pressed)
                {
                    //Вызвать событие
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
        #endregion
    }
}
