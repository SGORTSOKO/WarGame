// ***********************************************************************
// Assembly         : WarGame
// Author           : abros
// Created          : 05-29-2022
//
// Last Modified By : abros
// Last Modified On : 06-06-2022
// ***********************************************************************
// <copyright file="StateSingleplayer.cs" company="Kurgan State University">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using AKSU.MScore;
using AKSU.ScreenComponent;
using AKSU.SingleGame;
using AKSU.States;


namespace AKSU
{
    /// <summary>
    /// Класс одиночной игры StateSingleplayer.
    /// Унаследовано от <see cref="State" />
    /// </summary>
    /// <seealso cref="State" />
    class StateSingleplayer : State
    {
        #region Fields
        /// <summary>
        /// Список текстур существ
        /// </summary>
        private List<Texture2D> ListOfTextures = new List<Texture2D>();
        /// <summary>
        /// Музыка
        /// </summary>
        private Song Music;
        /// <summary>
        /// Звук окончания игры
        /// </summary>
        private Song SoundEnd;
        /// <summary>
        /// Фоновое  изображение
        /// </summary>
        private Texture2D BackGround;
        /// <summary>
        /// Рандомизатор
        /// </summary>
        private Random Rand = new Random();
        /// <summary>
        /// Шрифт надписей
        /// </summary>
        private SpriteFont TextBlock2;
        /// <summary>
        /// Последнее состояние мыши
        /// </summary>
        private MouseState LastMouseState;
        /// <summary>
        /// Текущее положение мыши
        /// </summary>
        private Vector2 CurrentMousePosition;
        /// <summary>
        /// Левый игрок (текущий)
        /// </summary>
        private Player Left;
        /// <summary>
        /// Правый игрок
        /// </summary>
        private Player Right;
        /// <summary>
        /// Победитель
        /// </summary>
        private Player Winner;
        /// <summary>
        /// Список существ
        /// </summary>
        private CreatureList PlayList;
        /// <summary>
        /// Компоненты экрана игры
        /// </summary>
        private List<Component> GameComponents;
        /// <summary>
        /// Менеджер таблицы рекордов
        /// </summary>
        private ScoreManager CurrentScoreManager;
        /// <summary>
        /// Таймер счёта игрока
        /// </summary>
        private int Timer1;
        /// <summary>
        /// Таймер появления существ правого игрока
        /// </summary>
        private int Timer2;
        /// <summary>
        /// Таймер появления существ левого игрока
        /// </summary>
        private int Timer3;
        /// <summary>
        /// Маркер окончания игры для единоразового исполнения кода 
        /// </summary>
        private bool OneDoingAfterWin;
        #endregion
        /// <summary>
        /// Конструктор класса <see cref="T:WarGame.StateSingleplayer" /> class.
        /// </summary>
        /// <param name="InputGame">Класс-инициализатор</param>
        /// <param name="InputGraphicsDevice">Графическое устройство</param>
        /// <param name="InputContentManager">Менеджер контента</param>
        /// <param name="InputScreenSize">Размер экрана</param>
        public StateSingleplayer(
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

            //счетчик очков
            Timer1 = 0;

            Timer2 = 0; //счетчик спавна существ юота
            Timer3 = 0; //счетчик спавна существ игрока

            OneDoingAfterWin = true; //флаг уникального действия при окончании игры

            CurrentScoreManager = ScoreManager.Load();
            TextBlock2 = InputContentManager.Load<SpriteFont>("Fonts/TimesNewRoman");
            ListOfTextures.Add(InputContentManager.Load<Texture2D>("Creatures/Human"));
            BackGround = InputContentManager.Load<Texture2D>("BackGrounds/Road");
            // Pfuheprf vepsrb
            Music = InputContentManager.Load<Song>("MainMusic");
            SoundEnd = InputContentManager.Load<Song>("LoseSound");
            // Начать проигрывание мелодии
            MediaPlayer.Play(Music);
            // Повторять после завершения
            MediaPlayer.IsRepeating = true;

            string TextNamePlayer;

            //Чтение имени игрока  из файла настроек
            if (File.Exists("Content/PlayerData.ini"))
                using (StreamReader Reader = new StreamReader("Content/PlayerData.ini"))
                {
                    TextNamePlayer = Reader.ReadLine();
                    if (TextNamePlayer.Length < 1)
                    {
                        TextNamePlayer = "PLAYER";
                    }
                }
            else
                using (StreamWriter Writer = new StreamWriter("Content/PlayerData.ini", false))
                {
                    //Восстановить файл настроек при его отсутствии
                    Writer.WriteLine("PLAYER");
                    TextNamePlayer = "PLAYER";
                }

            //Левый игрок (текущий)
            Left = new Player(
                TextNamePlayer,
                Color.Blue,
                1000,
                true,
                ThisScreenSize);

            //Правый игрок (бот)
            Right = new Player(
                "BOT",
                Color.Red,
                1000000,
                false,
                ThisScreenSize);

            //победитель
            Winner = null;

            //обработчик логики сражения
            PlayList = new CreatureList(Left, Right);

            //громкость музыки
            MediaPlayer.Volume = 0.8f;

            //Кнопка выхода в меню
            Button MenuButton = new Button(InputContentManager.Load<Texture2D>("Buttons/Menu"), 
                InputContentManager.Load<SpriteFont>("Fonts/TimesNewRomanSmall"))
            {
                Position = new Vector2(ThisScreenSize.CoordinateIntX * 0.01f, 
                ThisScreenSize.CoordinateIntY * 0.9f)
            };
            MenuButton.Click += MenuButton_Click;

            GameComponents = new List<Component>()
            {
                MenuButton,
            };
        }

        /// <summary>
        /// Обновление логики игры
        /// </summary>
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void Update(GameTime CurrentGameTime)
        {
            Timer2++;
            Timer3++;

            //Состояние мыши
            MouseState CurrentMouseState = Mouse.GetState();
            if (CurrentMouseState.X != LastMouseState.X || 
                CurrentMouseState.Y != LastMouseState.Y)
            {
                CurrentMousePosition = new Vector2(CurrentMouseState.X, 
                    CurrentMouseState.Y);
            }
            LastMouseState = CurrentMouseState;

            //Если победитель определен
            if (Winner != null)
            {
                if (OneDoingAfterWin)
                {
                    //Звуки
                    MediaPlayer.Stop();
                    MediaPlayer.IsRepeating = false;
                    MediaPlayer.Volume = 0.1f;
                    MediaPlayer.Play(SoundEnd);

                    //Очистка обработчика игры
                    OneDoingAfterWin = false;
                    PlayList.Clear();

                    //Обновление таблицы рекордов
                    CurrentScoreManager.Add(new Score()
                    {
                        PlayerName = Left.Name,
                        Value = Timer1 + (1000000 - Right.HP) * (int)Math.Log10(Timer1),
                    });
                    ScoreManager.Save(CurrentScoreManager);
                }
            }
            //Если победитель не определен
            else
            {
                //Случайная сортировка
                PlayList.RandSort();
                //Обновить характеристики существ
                PlayList.ResetStats();
                //Создание существ бота с уменьшащейся периодичностью
                if (Timer2 % (10 - (int)Math.Log10(Timer2)) == 0)
                {
                    PlayList.Add(
                        "Human",
                        Right,
                        Rand.Next(0, (int)(ThisScreenSize.CoordinateIntY * 0.8)),
                        ListOfTextures[0]);
                }

                //Раунд атак существ
                PlayList.AttackRound();

                //Если один игрок потерял всё здоровье
                Winner = PlayList.StepAll();

                //Создание существ игрока
                if (CurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (
                        0 <= CurrentMouseState.Y &&
                        CurrentMouseState.Y <= ThisScreenSize.CoordinateIntY * 0.8f &&
                        Timer3 > 8 - (int)Math.Log10(Timer2))
                    {
                        Timer3 = 0;
                        PlayList.Add(
                            "Human",
                            Left,
                            CurrentMouseState.Y,
                            ListOfTextures[0]);
                    }
                }
                Timer1++;
            }
            //Обновление игровых компонентов (кнопок)
            foreach (var Component in GameComponents)
                Component.Update(CurrentGameTime);
        }

        /// <summary>
        /// Отрисовать экран
        /// </summary>
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        /// <param name="CurrentSpriteBatch">Объект оптимизатора графической отрисовки</param>
        public override void Draw(GameTime CurrentGameTime, SpriteBatch CurrentSpriteBatch)
        {
            CurrentSpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            //Отрисовать фон
            CurrentSpriteBatch.Draw(
                BackGround,
                new Vector2(0, 0),
                new Rectangle(
                    0,
                    0,
                    ThisScreenSize.CoordinateIntX,
                    ThisScreenSize.CoordinateIntY),
                Color.White,
                0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0f);

            //Отрисовать игровые компоненты
            foreach (var Component in GameComponents)
            {
                Component.Draw(CurrentGameTime, CurrentSpriteBatch);
            }

            //Отрисовать существ
            for (int i = 0; i < PlayList.Count; i++)
            {
                CurrentSpriteBatch.Draw(
                    PlayList[i].SelfTexture,
                    PlayList[i].NowPosition,
                    PlayList[i].GetRectangleImage(),
                    PlayList[i].Player.Color,
                    0,
                    Vector2.Zero,
                    ThisScreenSize.CoordinateIntX / 1920f
                    , SpriteEffects.None,
                    0);
            }

            //Отрисовать надпись окончания игры
            if (Winner != null)
            {
                CurrentSpriteBatch.DrawString(
                    TextBlock2,
                    "GAME OVER",
                    new Vector2((ThisScreenSize.CoordinateIntX) / 2.0f - 
                    TextBlock2.MeasureString("GAME OVER").X, ThisScreenSize.CoordinateIntY * 0.8f),
                    Winner.Color);
            }
            else
            {
                //Отрисовывать здоровье игрока (левый)
                CurrentSpriteBatch.DrawString(
                    TextBlock2,
                    Convert.ToString(Left.HP),
                    new Vector2(ThisScreenSize.CoordinateIntX * 0.1f, ThisScreenSize.CoordinateIntY * 0.9f),
                    Left.Color);

                //Отрисовывать здоровье игрока (правй)
                CurrentSpriteBatch.DrawString(
                    TextBlock2,
                    Convert.ToString(Right.HP),
                    new Vector2(ThisScreenSize.CoordinateIntX * 0.9f - 
                        TextBlock2.MeasureString(Convert.ToString(Right.HP)).X, 
                        ThisScreenSize.CoordinateIntY * 0.9f),
                    Right.Color);

                //Отрисовывать текущий счёт
                CurrentSpriteBatch.DrawString(
                    TextBlock2,
                    Convert.ToString(Timer1),
                    new Vector2(ThisScreenSize.CoordinateIntX * 0.5f - 
                        TextBlock2.MeasureString(Convert.ToString(PlayList.Count)).X, 
                        ThisScreenSize.CoordinateIntY * 0.9f),
                    Color.Black);
            }
            CurrentSpriteBatch.End();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки выхода в меню
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void MenuButton_Click(object Sender, System.EventArgs e)
        {
            MediaPlayer.Stop();
            CurrentGame.ChangeState(new MenuState(
                CurrentGame,
                CurrentGraphicsDevice,
                CurrentContentManager,
                ThisScreenSize));
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
