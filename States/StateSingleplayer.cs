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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using WarGame.MScore;
using WarGame.ScreenComponent;
using WarGame.SingleGame;
using WarGame.States;


namespace WarGame
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
        /// The list of textures
        /// </summary>
        private List<Texture2D> listOfTextures = new List<Texture2D>();
        /// <summary>
        /// The music
        /// </summary>
        private Song music;
        /// <summary>
        /// The sound end
        /// </summary>
        private Song soundEnd;
        /// <summary>
        /// The back ground
        /// </summary>
        private Texture2D backGround;
        /// <summary>
        /// The rand
        /// </summary>
        private Random rand = new Random();
        /// <summary>
        /// The text block2
        /// </summary>
        private SpriteFont textBlock2;
        /// <summary>
        /// The last mouse state
        /// </summary>
        private MouseState lastMouseState;
        /// <summary>
        /// The mouse position
        /// </summary>
        private Vector2 mousePosition;
        /// <summary>
        /// The left
        /// </summary>
        private Player left;
        /// <summary>
        /// The right
        /// </summary>
        private Player right;
        /// <summary>
        /// The winner
        /// </summary>
        private Player winner;
        /// <summary>
        /// The play list
        /// </summary>
        private CreatureList playList;
        /// <summary>
        /// The game components
        /// </summary>
        private List<Component> gameComponents;
        /// <summary>
        /// The score manager
        /// </summary>
        private ScoreManager scoreManager;
        /// <summary>
        /// The timer
        /// </summary>
        private int timer, timer2, timer3;
        /// <summary>
        /// The single
        /// </summary>
        private bool single;
        #endregion
        /// <summary>
        /// Конструктор класса <see cref="T:WarGame.StateSingleplayer" /> class.
        /// </summary>
        /// <param name="inputGame">Класс-инициализатор</param>
        /// <param name="inputGraphicsDevice">Графическое устройство</param>
        /// <param name="inputContent">Менеджер контента</param>
        /// <param name="inputScreenSize">Размер экрана</param>
        public StateSingleplayer(
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

            //счетчик очков
            timer = 0;

            //счетчики спавна существ
            timer2 = 0;
            timer3 = 0;

            //флаг уникального действия при окончании игры
            single = true;

            scoreManager = ScoreManager.Load();
            textBlock2 = inputContent.Load<SpriteFont>("Fonts/TimesNewRoman");
            listOfTextures.Add(inputContent.Load<Texture2D>("Creatures/Human"));
            backGround = inputContent.Load<Texture2D>("BackGrounds/Road");
            // Pfuheprf vepsrb
            music = inputContent.Load<Song>("MainMusic");
            soundEnd = inputContent.Load<Song>("LoseSound");
            // Начать проигрывание мелодии
            MediaPlayer.Play(music);
            // Повторять после завершения
            MediaPlayer.IsRepeating = true;

            string textNamePlayer;

            //Чтение имени игрока  из файла настроек
            if (File.Exists("Content/PlayerData.ini"))
                using (StreamReader reader = new StreamReader("Content/PlayerData.ini"))
                {
                    textNamePlayer = reader.ReadLine();
                    if (textNamePlayer.Length < 1)
                    {
                        textNamePlayer = "PLAYER";
                    }
                }
            else
                using (StreamWriter writer = new StreamWriter("Content/PlayerData.ini", false))
                {
                    //Восстановить файл настроек при его отсутствии
                    writer.WriteLine("PLAYER");
                    textNamePlayer = "PLAYER";
                }

            //Левый игрок (текущий)
            left = new Player(
                textNamePlayer,
                Color.Blue,
                1000,
                true,
                thisScreenSize);

            //Правый игрок (бот)
            right = new Player(
                "BOT",
                Color.Red,
                1000000,
                false,
                thisScreenSize);

            //победитель
            winner = null;

            //обработчик логики сражения
            playList = new CreatureList(left, right);

            //громкость музыки
            MediaPlayer.Volume = 0.8f;

            //Кнопка выхода в меню
            Button menuButton = new Button(inputContent.Load<Texture2D>("Buttons/Menu"), inputContent.Load<SpriteFont>("Fonts/TimesNewRomanSmall"))
            {
                Position = new Vector2(thisScreenSize.X * 0.01f, thisScreenSize.Y * 0.9f)
            };
            menuButton.Click += MenuButton_Click;

            gameComponents = new List<Component>()
            {
                menuButton,
            };
        }

        /// <summary>
        /// Обновление логики игры
        /// </summary>
        /// <param name="gameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void Update(GameTime gameTime)
        {
            timer2++;
            timer3++;

            //Состояние мыши
            MouseState currentMouseState = Mouse.GetState();
            if (currentMouseState.X != lastMouseState.X || currentMouseState.Y != lastMouseState.Y)
            {
                mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);
            }
            lastMouseState = currentMouseState;

            //Если победитель определен
            if (winner != null)
            {
                if (single)
                {
                    //Звуки
                    MediaPlayer.Stop();
                    MediaPlayer.IsRepeating = false;
                    MediaPlayer.Volume = 0.1f;
                    MediaPlayer.Play(soundEnd);

                    //Очистка обработчика игры
                    single = false;
                    playList.Clear();

                    //Обновление таблицы рекордов
                    scoreManager.Add(new Score()
                    {
                        PlayerName = left.Name,
                        Value = timer + (1000000 - right.HP) * (int)Math.Log10(timer),
                    });
                    ScoreManager.Save(scoreManager);
                }
            }
            //Если победитель не определен
            else
            {
                //Случайная сортировка
                playList.RandSort();
                //Обновить характеристики существ
                playList.ResetStats();
                //Создание существ бота с уменьшащейся периодичностью
                if (timer2 % (10 - (int)Math.Log10(timer2)) == 0)
                {
                    playList.Add(
                        "Human",
                        right,
                        rand.Next(0, (int)(thisScreenSize.Y * 0.8)),
                        listOfTextures[0]);
                }

                //Раунд атак существ
                playList.AttackRound();

                //Если один игрок потерял всё здоровье
                winner = playList.StepAll();

                //Создание существ игрока
                if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (
                        0 <= currentMouseState.Y &&
                        currentMouseState.Y <= thisScreenSize.Y * 0.8f &&
                        timer3 > 8 - (int)Math.Log10(timer2))
                    {
                        timer3 = 0;
                        playList.Add(
                            "Human",
                            left,
                            currentMouseState.Y,
                            listOfTextures[0]);
                    }
                }
                timer++;
            }
            //Обновление игровых компонентов (кнопок)
            foreach (Component component in gameComponents)
                component.Update(gameTime);
        }

        /// <summary>
        /// Отрисовать экран
        /// </summary>
        /// <param name="gameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        /// <param name="spriteBatch">Объект оптимизатора графической отрисовки</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            //Отрисовать фон
            spriteBatch.Draw(
                backGround,
                new Vector2(0, 0),
                new Rectangle(
                    0,
                    0,
                    thisScreenSize.X,
                    thisScreenSize.Y),
                Color.White,
                0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0f);

            //Отрисовать игровые компоненты
            foreach (Component component in gameComponents)
            {
                component.Draw(gameTime, spriteBatch);
            }

            //Отрисовать существ
            for (int i = 0; i < playList.Count; i++)
            {
                spriteBatch.Draw(
                    playList[i].SelfTexture,
                    playList[i].NowPosition,
                    playList[i].GetRectangleImage(),
                    playList[i].Player.Color,
                    0,
                    Vector2.Zero,
                    thisScreenSize.X / 1920f
                    , SpriteEffects.None,
                    0);
            }

            //Отрисовать надпись окончания игры
            if (winner != null)
            {
                spriteBatch.DrawString(
                    textBlock2,
                    "GAME OVER",
                    new Vector2((thisScreenSize.X) / 2.0f - textBlock2.MeasureString("GAME OVER").X, thisScreenSize.Y * 0.8f),
                    winner.Color);
            }
            else
            {
                //Отрисовывать здоровье игрока (левый)
                spriteBatch.DrawString(
                    textBlock2,
                    Convert.ToString(left.HP),
                    new Vector2(thisScreenSize.X * 0.1f, thisScreenSize.Y * 0.9f),
                    left.Color);

                //Отрисовывать здоровье игрока (правй)
                spriteBatch.DrawString(
                    textBlock2,
                    Convert.ToString(right.HP),
                    new Vector2(thisScreenSize.X * 0.9f - textBlock2.MeasureString(Convert.ToString(right.HP)).X, thisScreenSize.Y * 0.9f),
                    right.Color);

                //Отрисовывать текущий счёт
                spriteBatch.DrawString(
                    textBlock2,
                    Convert.ToString(timer),
                    new Vector2(thisScreenSize.X * 0.5f - textBlock2.MeasureString(Convert.ToString(playList.Count)).X, thisScreenSize.Y * 0.9f),
                    Color.Black);
            }
            spriteBatch.End();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки выхода в меню
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void MenuButton_Click(object sender, System.EventArgs e)
        {
            MediaPlayer.Stop();
            thisGame.ChangeState(new MenuState(
                thisGame,
                thisGraphicsDevice,
                thisContent,
                thisScreenSize));
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
