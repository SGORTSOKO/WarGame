// ***********************************************************************
// Assembly         : WarGame
// Author           : abros
// Created          : 05-16-2022
//
// Last Modified By : abros
// Last Modified On : 06-06-2022
// ***********************************************************************
// <copyright file="Initializer.cs" company="Kurgan State University">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WarGame.States;

namespace WarGame
{
    /// <summary>
    /// Класс GameWindow является наследником от Microsoft.Xna.Framework.Game
    /// Данный класс инициализирует приложение и выводит изображение
    /// </summary>
    public class Initializer : Game
    {
        /// <summary>
        /// Менеджер графического устройства приложения
        /// </summary>
        public GraphicsDeviceManager mainGraphics;
        /// <summary>
        /// Объект оптимизатора графической отрисовки
        /// </summary>
        public SpriteBatch spriteBatch;
        /// <summary>
        /// Открытая страница игры
        /// </summary>
        private State currentState;
        /// <summary>
        /// Следующая страница игры
        /// </summary>
        private State nextState;
        /// <summary>
        /// Размер экрана
        /// </summary>
        CoordinatesXY screenSize;

        /// <summary>
        /// Конструктор класса GameWindow
        /// </summary>
        public Initializer()
        {
            TargetElapsedTime = new System.TimeSpan(
                0,
                0,
                0,
                0,
                50
                );

            mainGraphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }
        /// <summary>
        /// Данный метод выполняется единожды, раньше всех остальных
        /// Выполняет инициализацию параметров графического устройства
        /// </summary>
        protected override void Initialize()
        {
            screenSize = new CoordinatesXY(1600, 900);

            mainGraphics.PreferredBackBufferWidth = screenSize.X;
            mainGraphics.PreferredBackBufferHeight = screenSize.Y;
            mainGraphics.ApplyChanges();

            IsMouseVisible = true;

            base.Initialize();
        }
        /// <summary>
        /// Данный метод выполняется, после Initialize()
        /// Необходим для эффективной загрузки и хранения данных в оперативной памяти
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            currentState = new MenuState(
                this,
                mainGraphics.GraphicsDevice,
                Content,
                screenSize
                );
        }
        /// <summary>
        /// Данный метод выполняется, после LoadContent() или сразу после выполнения Draw()
        /// Содержит бизнес-логику приложения
        /// </summary>
        /// <param name="gameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        protected override void Update(GameTime gameTime)
        {
            //Если определена следующая страница, то сменить
            if (nextState != null)
            {
                currentState = nextState;
                nextState = null;
            }
            //Обновить логику
            currentState.Update(gameTime);
            //Очистка
            currentState.PostUpdate(gameTime);

            base.Update(gameTime);
        }
        /// <summary>
        /// Данный метод выполняется, после Update()
        /// Генерирует графическое содержание окна игры на основе данных метода Update()
        /// </summary>
        /// <param name="gameTime">A <see cref="T:Microsoft.Xna.Framework.GameTime" /> Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Draw(Microsoft.Xna.Framework.GameTime)" /> и время от старта игры</param>
        protected override void Draw(GameTime gameTime)
        {
            //Настроить цвет фона
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Отрисовать текущую страницу
            currentState.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }
        /// <summary>
        /// Производит переход между игровыми меню и игровыми режимами> 
        /// </summary>
        /// <param name="state">Объект абстрактного класса режимов игры и игровых меню</param>
        public void ChangeState(State state)
        {
            nextState = state;
        }
    }
}