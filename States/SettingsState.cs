// ***********************************************************************
// Assembly         : WarGame
// Author           : abros
// Created          : 06-03-2022
//
// Last Modified By : abros
// Last Modified On : 06-06-2022
// ***********************************************************************
// <copyright file="SettingsState.cs" company="Kurgan State University">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using AKSU.ScreenComponent;

namespace AKSU.States
{
    /// <summary>
    /// Класс настроек SettingsState.
    /// Implements the <see cref="AKSU.States.State" />
    /// </summary>
    /// <seealso cref="AKSU.States.State" />
    class SettingsState : State
    {
        /// <summary>
        /// Текстура надписи "размер экрана"
        /// </summary>
        private Texture2D ScreenSizeTexture;
        /// <summary>
        /// Компоненты экрана
        /// </summary>
        private List<Component> Components;
        /// <summary>
        /// Фоновое изображение
        /// </summary>
        private Texture2D BackGround;
        /// <summary>
        /// Имя игрока
        /// </summary>
        private string TextName;
        /// <summary>
        /// Шрифт кнопок
        /// </summary>
        SpriteFont ButtonFont;
        /// <summary>
        /// Конструктор класса <see cref="MenuState" />.
        /// </summary>
        /// <param name="InputGame">Инициализатор приложения</param>
        /// <param name="InputGraphicsDevice">Графическое устройство.</param>
        /// <param name="InputContentManager">Менеджер контента</param>
        /// <param name="InputScreenSize">Размер экрана</param>
        public SettingsState(
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
            Texture2D S1280x1024Texture = InputContentManager.Load<Texture2D>("Buttons/1280x1024");
            Texture2D S1366x768Texture = InputContentManager.Load<Texture2D>("Buttons/1366x768");
            Texture2D S1440x900Texture = InputContentManager.Load<Texture2D>("Buttons/1440x900");
            Texture2D S1600x900Texture = InputContentManager.Load<Texture2D>("Buttons/1600x900");
            Texture2D S1920x1080Texture = InputContentManager.Load<Texture2D>("Buttons/1920x1080");
            Texture2D fullscreenTexture = InputContentManager.Load<Texture2D>("Buttons/Fullscreen");
            Texture2D windowedTexture = InputContentManager.Load<Texture2D>("Buttons/Windowed");
            Texture2D newGameButtonTexture = InputContentManager.Load<Texture2D>("Buttons/NewGame");
            ScreenSizeTexture = InputContentManager.Load<Texture2D>("Buttons/ScreenSize");
            BackGround = InputContentManager.Load<Texture2D>("BackGrounds/Road");
            ButtonFont = InputContentManager.Load<SpriteFont>("Fonts/TimesNewRoman");

            //Если существует файл настроек, прочитать имя
            if (File.Exists("./Content/PlayerData.ini"))
                using (StreamReader Reader = new StreamReader("Content/PlayerData.ini"))
                {
                    TextName = Reader.ReadLine();
                    if (TextName.Length < 1)
                    {
                        TextName = "Player";
                    }
                }
            else //иначе принять по умолчанию
            {
                TextName = "Player";
            }
            CurrentGame.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 100);

            //Кнопки изменения размера экрана
            Button S1280x1024Button = new Button(S1280x1024Texture, ButtonFont)
            {
                Position = new Vector2(200, 150),
            };
            S1280x1024Button.Click += S1280x1024Button_Click;

            //Кнопки изменения размера экрана
            Button S1366x768Button = new Button(S1366x768Texture, ButtonFont)
            {
                Position = new Vector2(200, 200),
            };
            S1366x768Button.Click += S1366x768Button_Click;

            //Кнопки изменения размера экрана
            Button S1440x900Button = new Button(S1440x900Texture, ButtonFont)
            {
                Position = new Vector2(200, 250),
            };
            S1440x900Button.Click += S1440x900Button_Click;

            //Кнопки изменения размера экрана
            Button S1600x900Button = new Button(S1600x900Texture, ButtonFont)
            {
                Position = new Vector2(200, 300),
            };
            S1600x900Button.Click += S1600x900Button_Click;

            //Кнопки изменения размера экрана
            Button S1920x1080Button = new Button(S1920x1080Texture, ButtonFont)
            {
                Position = new Vector2(200, 350),
            };
            S1920x1080Button.Click += S1920x1080Button_Click;

            //Кнопка полноэкранного режима
            Button FullscreenButton = new Button(fullscreenTexture, ButtonFont)
            {
                Position = new Vector2(200, 400),
            };
            FullscreenButton.Click += FullscreenButton_Click;

            //Кнопка оконного игрока
            Button WindowedButton = new Button(windowedTexture, ButtonFont)
            {
                Position = new Vector2(200, 450),
            };
            WindowedButton.Click += WindowedButton_Click;

            //Кнопка выхода в меню
            Button MenuButton = new Button(InputContentManager.Load<Texture2D>("Buttons/Menu"), InputContentManager.Load<SpriteFont>("Fonts/TimesNewRomanSmall"))
            {
                Position = new Vector2(ThisScreenSize.CoordinateIntX * 0.01f, ThisScreenSize.CoordinateIntY * 0.1f)
            };
            MenuButton.Click += MenuButton_Click;

            //Компоненты экрана
            Components = new List<Component>()
            {
                S1280x1024Button,
                S1366x768Button,
                S1440x900Button,
                S1600x900Button,
                S1920x1080Button,
                FullscreenButton,
                WindowedButton,
                MenuButton
            };
        }


        /// <summary>
        /// Обновление логики 
        /// </summary>
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void Update(GameTime CurrentGameTime)
        {
            //Получить состояние клавиатуры
            KeyboardState CurrentKeyboardState = Keyboard.GetState();
            string Timed;
            //Если имя не стерто поностью
            if (TextName.Length > 0)
            {
                //Ползволить стереть символ
                if (CurrentKeyboardState.IsKeyDown(Keys.Back))
                {
                    TextName = TextName.Substring(0, TextName.Length - 1);
                }
            }
            //Получить нажатые клавиши
            foreach (Keys G in CurrentKeyboardState.GetPressedKeys())
            {
                //Если однаиз букв, то
                if (
                    G == Keys.A ||
                    G == Keys.B ||
                    G == Keys.C ||
                    G == Keys.D ||
                    G == Keys.E ||
                    G == Keys.F ||
                    G == Keys.G ||
                    G == Keys.H ||
                    G == Keys.I ||
                    G == Keys.J ||
                    G == Keys.K ||
                    G == Keys.L ||
                    G == Keys.M ||
                    G == Keys.N ||
                    G == Keys.O ||
                    G == Keys.P ||
                    G == Keys.Q ||
                    G == Keys.R ||
                    G == Keys.S ||
                    G == Keys.T ||
                    G == Keys.U ||
                    G == Keys.V ||
                    G == Keys.W ||
                    G == Keys.X ||
                    G == Keys.Y ||
                    G == Keys.Z ||
                    G == Keys.Space
                    )
                {
                    //Если нажата shift, то
                    if (CurrentKeyboardState.IsKeyDown(Keys.LeftShift))
                    {
                        TextName += G;
                    }
                    else
                    {
                        Timed = "" + G;
                        TextName += Timed.ToLower();
                    }
                }
            }
            foreach (var Component in Components)
                Component.Update(CurrentGameTime);
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
                new Rectangle(0, 0, ThisScreenSize.CoordinateIntX, ThisScreenSize.CoordinateIntY),
                Color.White,
                0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0f);

            //Отрисовать надпись "размер экрана"
            CurrentSpriteBatch.Draw(ScreenSizeTexture, new Vector2(150, 50), Color.White);

            //Отрисовать имя
            CurrentSpriteBatch.DrawString(ButtonFont, TextName, new Vector2(600, 170), Color.Blue);

            //Отрисовать компоненты
            foreach (var Component in Components)
                Component.Draw(CurrentGameTime, CurrentSpriteBatch);

            //Принять изменения экрана
            CurrentGame.MainGraphics.ApplyChanges();

            CurrentSpriteBatch.End();
        }


        /// <summary>
        /// Обработчик события нажатия кнопки выхода в меню
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void MenuButton_Click(object Sender, System.EventArgs e)
        {
            //Сохранить имя при выходе
            using (StreamWriter Writer = new StreamWriter("Content/PlayerData.ini", false))
            {
                if (TextName.Length > 0)
                    Writer.Write(TextName);
                Writer.Write("Player");
            }

            //Изменить частоту обновления экрана
            CurrentGame.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 50);

            //Сменить экран
            CurrentGame.ChangeState(new MenuState(
                CurrentGame,
                CurrentGraphicsDevice,
                CurrentContentManager,
                ThisScreenSize));
        }

        /// <summary>
        /// Обработчик события нажатия кнопки смены размера экрана
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void S1280x1024Button_Click(object Sender, System.EventArgs e)
        {
            ThisScreenSize = new CoordinatesXY(1280, 1024);
            CurrentGame.MainGraphics.PreferredBackBufferWidth = ThisScreenSize.CoordinateIntX;
            CurrentGame.MainGraphics.PreferredBackBufferHeight = ThisScreenSize.CoordinateIntY;
            CurrentGame.MainGraphics.ApplyChanges();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки смены размера экрана
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void S1366x768Button_Click(object Sender, System.EventArgs e)
        {
            ThisScreenSize = new CoordinatesXY(1366, 768);
            CurrentGame.MainGraphics.PreferredBackBufferWidth = ThisScreenSize.CoordinateIntX;
            CurrentGame.MainGraphics.PreferredBackBufferHeight = ThisScreenSize.CoordinateIntY;
            CurrentGame.MainGraphics.ApplyChanges();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки смены размера экрана
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void S1440x900Button_Click(object Sender, System.EventArgs e)
        {
            ThisScreenSize = new CoordinatesXY(1440, 900);
            CurrentGame.MainGraphics.PreferredBackBufferWidth = ThisScreenSize.CoordinateIntX;
            CurrentGame.MainGraphics.PreferredBackBufferHeight = ThisScreenSize.CoordinateIntY;
            CurrentGame.MainGraphics.ApplyChanges();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки смены размера экрана
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void S1600x900Button_Click(object Sender, System.EventArgs e)
        {
            ThisScreenSize = new CoordinatesXY(1600, 900);
            CurrentGame.MainGraphics.PreferredBackBufferWidth = ThisScreenSize.CoordinateIntX;
            CurrentGame.MainGraphics.PreferredBackBufferHeight = ThisScreenSize.CoordinateIntY;
            CurrentGame.MainGraphics.ApplyChanges();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки смены размера экрана
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void S1920x1080Button_Click(object Sender, System.EventArgs e)
        {
            ThisScreenSize = new CoordinatesXY(1920, 1080);
            CurrentGame.MainGraphics.PreferredBackBufferWidth = ThisScreenSize.CoordinateIntX;
            CurrentGame.MainGraphics.PreferredBackBufferHeight = ThisScreenSize.CoordinateIntY;
            CurrentGame.MainGraphics.ApplyChanges();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки перехода в полноэкранный режим
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void FullscreenButton_Click(object Sender, System.EventArgs e)
        {
            CurrentGame.MainGraphics.IsFullScreen = true;
            CurrentGame.MainGraphics.ApplyChanges();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки перехода в оконный режим
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void WindowedButton_Click(object Sender, System.EventArgs e)
        {
            CurrentGame.MainGraphics.IsFullScreen = false;
            CurrentGame.MainGraphics.ApplyChanges();
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
