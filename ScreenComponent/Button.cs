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
    public class Button : BaseComponent
    {
        #region Fields
        /// <summary>
        /// Текущее состояние мыши
        /// </summary>
        private MouseState currentMouse;
        /// <summary>
        /// Шрифт текста
        /// </summary>
        private SpriteFont font;
        /// <summary>
        /// Состояние выделения
        /// </summary>
        private bool isHovering;
        /// <summary>
        /// Предыдущее состояние мыши
        /// </summary>
        private MouseState previousMouse;
        /// <summary>
        /// Текстура кнопки
        /// </summary>
        private Texture2D texture;
        /// <summary>
        /// Сдвиг текста по X
        /// </summary>
        private float shiftX = -10;
        /// <summary>
        /// Сдвиг текста по Y
        /// </summary>
        private float shiftY = -10;
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
                texture.Width,
                texture.Height);
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
        /// <param name="inputTexture">Текстура кнопки</param>
        /// <param name="inputFont">Шрифт кнопки</param>
        public Button(Texture2D inputTexture, SpriteFont inputFont)
        {
            texture = inputTexture;
            font = inputFont;
            PenColour = Color.Black;
        }
        /// <summary>
        /// Отрисовать кнопку
        /// </summary>
        /// <param name="currentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        /// <param name="currentSpriteBatch">Объект оптимизатора графической отрисовки</param>
        public override void Draw(GameTime currentGameTime, SpriteBatch currentSpriteBatch)
        {
            var colour = Color.White;
            if (isHovering)
                colour = Color.Gray;

            currentSpriteBatch.Draw(texture, Rectangle, colour);

            
            if (!string.IsNullOrEmpty(Text)) //Если текст есть, то 
            {
                if (shiftX <= 0) //Центрирование по X
                    shiftX = (Rectangle.X + (Rectangle.Width / 2)) - 
                        (font.MeasureString(Text).X / 2);
                if (shiftY <= 0) //Центрирование по Y
                    shiftY = (Rectangle.Y + (Rectangle.Height / 2)) - 
                        (font.MeasureString(Text).Y / 2);

                currentSpriteBatch.DrawString( //Отрисовка текста
                    font,
                    Text,
                    new Vector2(shiftX, shiftY),
                    PenColour
                    );
            }
        }
        /// <summary>
        /// Расчет состояния кнопки
        /// </summary>
        /// <param name="currentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void Update(GameTime currentGameTime)
        {
            previousMouse = currentMouse; //Обновление состояния мыши
            currentMouse = Mouse.GetState();

            var MouseRectangle = new Rectangle(
                currentMouse.X,
                currentMouse.Y,
                1,
                1);

            isHovering = false;
            
            if (MouseRectangle.Intersects(Rectangle)) //Находится ли мышь над кнопкой
            {
                isHovering = true; //Выделить кнопку
                
                if (currentMouse.LeftButton == ButtonState.Released &&  //Если нажата и отпущена
                    previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs()); //Вызвать событие
                }
            }
        }
        #endregion
    }
}
