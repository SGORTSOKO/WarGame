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
    /// Унаследовано от <see cref="BaseState" />
    /// </summary>
    /// <seealso cref="BaseState" />
    class StateSingleplayer : BaseState
    {
        #region Fields
        /// <summary>
        /// Список текстур существ
        /// </summary>
        private List<Texture2D> listOfTextures = new List<Texture2D>();
        /// <summary>
        /// Музыка
        /// </summary>
        private Song music;
        /// <summary>
        /// Звук окончания игры
        /// </summary>
        private Song soundEnd;
        /// <summary>
        /// Фоновое  изображение
        /// </summary>
        private Texture2D backGround;
        /// <summary>
        /// Рандомизатор
        /// </summary>
        private Random rand = new Random();
        /// <summary>
        /// Шрифт надписей
        /// </summary>
        private SpriteFont textBlock2;
        /// <summary>
        /// Последнее состояние мыши
        /// </summary>
        private MouseState lastMouseState;
        /// <summary>
        /// Текущее положение мыши
        /// </summary>
        private Vector2 currentMousePosition;
        /// <summary>
        /// Левый игрок (текущий)
        /// </summary>
        private Player leftPlayer;
        /// <summary>
        /// Правый игрок
        /// </summary>
        private Player rightPlayer;
        /// <summary>
        /// Победитель
        /// </summary>
        private Player winner;
        /// <summary>
        /// Список существ
        /// </summary>
        private CreatureList playList;
        /// <summary>
        /// Компоненты экрана игры
        /// </summary>
        private List<BaseComponent> gameComponents;
        /// <summary>
        /// Менеджер таблицы рекордов
        /// </summary>
        private ScoreManager currentScoreManager;
        /// <summary>
        /// Таймер счёта игрока
        /// </summary>
        private int timer1;
        /// <summary>
        /// Таймер появления существ правого игрока
        /// </summary>
        private int timer2;
        /// <summary>
        /// Таймер появления существ левого игрока
        /// </summary>
        private int timer3;
        /// <summary>
        /// Маркер окончания игры для единоразового исполнения кода 
        /// </summary>
        private bool oneDoingAfterWin;
        #endregion
        /// <summary>
        /// Конструктор класса <see cref="T:WarGame.StateSingleplayer" /> class.
        /// </summary>
        /// <param name="inputGame">Класс-инициализатор</param>
        /// <param name="inputGraphicsDevice">Графическое устройство</param>
        /// <param name="inputContentManager">Менеджер контента</param>
        /// <param name="inputScreenSize">Размер экрана</param>
        public StateSingleplayer(
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

            //счетчик очков
            timer1 = 0;

            timer2 = 0; //счетчик спавна существ юота
            timer3 = 0; //счетчик спавна существ игрока

            oneDoingAfterWin = true; //флаг уникального действия при окончании игры

            currentScoreManager = ScoreManager.Load();

            textBlock2 = inputContentManager.Load<SpriteFont>("Fonts/TimesNewRoman"); //Загрузка шрифта
            
            listOfTextures.Add(inputContentManager.Load<Texture2D>("Creatures/Human")); //Инициализаия списка существ с текстурами
            
            backGround = inputContentManager.Load<Texture2D>("BackGrounds/Road"); //Загрузка фона
            
            music = inputContentManager.Load<Song>("MainMusic"); // Загрузка музыки

            soundEnd = inputContentManager.Load<Song>("LoseSound"); //Загрузка звука окончания игры
            
            MediaPlayer.Play(music); // Начать проигрывание мелодии
            
            MediaPlayer.IsRepeating = true; // Повторять после завершения

            string TextNamePlayer;

            if (File.Exists("Content/PlayerData.ini")) //Чтение имени игрока из файла настроек
                using (StreamReader Reader = new StreamReader("Content/PlayerData.ini"))
                {
                    TextNamePlayer = Reader.ReadLine();
                }
            else
                using (StreamWriter Writer = new StreamWriter("Content/PlayerData.ini", false))
                {
                    TextNamePlayer = "PLAYER";
                }

            leftPlayer = new Player(
                TextNamePlayer,
                Color.Blue,
                1000,
                true,
                thisScreenSize); //Левый игрок (текущий)

            rightPlayer = new Player(
                "BOT",
                Color.Red,
                1000000,
                false,
                thisScreenSize); //Правый игрок (бот)

            winner = null; //победитель

            playList = new CreatureList(leftPlayer, rightPlayer); //обработчик логики сражения

            MediaPlayer.Volume = 0.8f; //громкость музыки

            Button MenuButton = new Button(inputContentManager.Load<Texture2D>("Buttons/Menu"), 
                inputContentManager.Load<SpriteFont>("Fonts/TimesNewRomanSmall")) //Кнопка выхода в меню
            {
                Position = new Vector2(thisScreenSize.CoordinateIntX * 0.01f, 
                thisScreenSize.CoordinateIntY * 0.9f)
            };
            MenuButton.Click += MenuButton_Click;

            gameComponents = new List<BaseComponent>()
            {
                MenuButton,
            };
        }

        /// <summary>
        /// Обновление логики игры
        /// </summary>
        /// <param name="currentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void Update(GameTime currentGameTime)
        {
            timer2++;
            timer3++;

            MouseState CurrentMouseState = Mouse.GetState(); //Состояние мыши
            if (CurrentMouseState.X != lastMouseState.X || 
                CurrentMouseState.Y != lastMouseState.Y)
            {
                currentMousePosition = new Vector2(CurrentMouseState.X, 
                    CurrentMouseState.Y);
            }
            lastMouseState = CurrentMouseState;

            if (winner != null) //Если победитель определен
            {
                if (oneDoingAfterWin)
                {
                    MediaPlayer.Stop(); //Звуки
                    MediaPlayer.IsRepeating = false;
                    MediaPlayer.Volume = 0.1f;
                    MediaPlayer.Play(soundEnd);

                    oneDoingAfterWin = false; //Очистка обработчика игры
                    playList.Clear();

                    currentScoreManager.Add(new Score() //Обновление таблицы рекордов
                    {
                        PlayerName = leftPlayer.Name,
                        Value = timer1 + (1000000 - rightPlayer.HP) * (int)Math.Log10(timer1),
                    });
                    ScoreManager.Save(currentScoreManager);
                }
            }
            else //Если победитель не определен
            {
                playList.RandSort(); //Случайная сортировка
                
                playList.ResetStats(); //Обновить характеристики существ
                
                if (timer2 % (10 - (int)Math.Log10(timer2)) == 0) //Создание существ бота с уменьшащейся периодичностью
                {
                    playList.Add(
                        "Human",
                        rightPlayer,
                        rand.Next(0, (int)(thisScreenSize.CoordinateIntY * 0.8)),
                        listOfTextures[0]);
                }

                playList.AttackRound(); //Раунд атак существ

                winner = playList.StepAll(); //Если один игрок потерял всё здоровье

                if (CurrentMouseState.LeftButton == ButtonState.Pressed) //Создание существ игрока
                {
                    if (
                        0 <= CurrentMouseState.Y &&
                        CurrentMouseState.Y <= thisScreenSize.CoordinateIntY * 0.8f &&
                        timer3 > 8 - (int)Math.Log10(timer2))
                    {
                        timer3 = 0;
                        playList.Add(
                            "Human",
                            leftPlayer,
                            CurrentMouseState.Y,
                            listOfTextures[0]);
                    }
                }
                timer1++;
            }
            foreach (var Component in gameComponents) //Обновление игровых компонентов (кнопок)
                Component.Update(currentGameTime);
        }

        /// <summary>
        /// Отрисовать экран
        /// </summary>
        /// <param name="currentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        /// <param name="currentSpriteBatch">Объект оптимизатора графической отрисовки</param>
        public override void Draw(GameTime currentGameTime, SpriteBatch currentSpriteBatch)
        {
            currentSpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            currentSpriteBatch.Draw(
                backGround,
                new Vector2(0, 0),
                new Rectangle(
                    0,
                    0,
                    thisScreenSize.CoordinateIntX,
                    thisScreenSize.CoordinateIntY),
                Color.White,
                0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0f); //Отрисовать фон

            foreach (var Component in gameComponents) //Отрисовать игровые компоненты
            {
                Component.Draw(currentGameTime, currentSpriteBatch);
            }

            for (int i = 0; i < playList.Count; i++) //Отрисовать существ
            {
                currentSpriteBatch.Draw(
                    playList[i].SelfTexture,
                    playList[i].NowPosition,
                    playList[i].GetRectangleImage(),
                    playList[i].Player.Color,
                    0,
                    Vector2.Zero,
                    thisScreenSize.CoordinateIntX / 1920f
                    , SpriteEffects.None,
                    0);
            }

            if (winner != null) //Отрисовать надпись окончания игры
            {
                currentSpriteBatch.DrawString(
                    textBlock2,
                    "GAME OVER",
                    new Vector2((thisScreenSize.CoordinateIntX) / 2.0f - 
                    textBlock2.MeasureString("GAME OVER").X, thisScreenSize.CoordinateIntY * 0.8f),
                    winner.Color);
            }
            else
            {
                currentSpriteBatch.DrawString(
                    textBlock2,
                    Convert.ToString(leftPlayer.HP),
                    new Vector2(thisScreenSize.CoordinateIntX * 0.1f, thisScreenSize.CoordinateIntY * 0.9f),
                    leftPlayer.Color); //Отрисовывать здоровье игрока (левый)

                currentSpriteBatch.DrawString(
                    textBlock2,
                    Convert.ToString(rightPlayer.HP),
                    new Vector2(thisScreenSize.CoordinateIntX * 0.9f - 
                        textBlock2.MeasureString(Convert.ToString(rightPlayer.HP)).X, 
                        thisScreenSize.CoordinateIntY * 0.9f),
                    rightPlayer.Color); //Отрисовывать здоровье игрока (правй)

                currentSpriteBatch.DrawString(
                    textBlock2,
                    Convert.ToString(timer1),
                    new Vector2(thisScreenSize.CoordinateIntX * 0.5f - 
                        textBlock2.MeasureString(Convert.ToString(playList.Count)).X, 
                        thisScreenSize.CoordinateIntY * 0.9f),
                    Color.Black); //Отрисовывать текущий счёт
            }
            currentSpriteBatch.End();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки выхода в меню
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void MenuButton_Click(object sender, System.EventArgs e)
        {
            MediaPlayer.Stop();
            currentGame.ChangeState(new MenuState(
                currentGame,
                currentGraphicsDevice,
                currentContentManager,
                thisScreenSize));
        }
        /// <summary>
        /// Действие после обновления
        /// </summary>
        /// <param name="currentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void PostUpdate(GameTime currentGameTime)
        {
        }
    }
}
