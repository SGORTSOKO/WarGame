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

using AKSU.States;

namespace AKSU
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
        public GraphicsDeviceManager MainGraphics;
        /// <summary>
        /// Объект оптимизатора графической отрисовки
        /// </summary>
        public SpriteBatch CurrentSpriteBatch;
        /// <summary>
        /// Открытая страница игры
        /// </summary>
        private BaseState CurrentState;
        /// <summary>
        /// Следующая страница игры
        /// </summary>
        private BaseState NextState;
        /// <summary>
        /// Размер экрана
        /// </summary>
        public CoordinatesXY CurrentScreenSize;

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

            MainGraphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }
        /// <summary>
        /// Данный метод выполняется единожды, раньше всех остальных
        /// Выполняет инициализацию параметров графического устройства
        /// </summary>
        protected override void Initialize()
        {
            CurrentScreenSize = new CoordinatesXY(1600, 900);

            MainGraphics.PreferredBackBufferWidth = CurrentScreenSize.CoordinateIntX;
            MainGraphics.PreferredBackBufferHeight = CurrentScreenSize.CoordinateIntY;
            MainGraphics.ApplyChanges();

            IsMouseVisible = true;

            base.Initialize();
        }
        /// <summary>
        /// Данный метод выполняется, после Initialize()
        /// Необходим для эффективной загрузки и хранения данных в оперативной памяти
        /// </summary>
        protected override void LoadContent()
        {
            CurrentSpriteBatch = new SpriteBatch(GraphicsDevice);

            CurrentState = new MenuState(
                this,
                MainGraphics.GraphicsDevice,
                Content,
                CurrentScreenSize
                );
        }
        /// <summary>
        /// Данный метод выполняется, после LoadContent() или сразу после выполнения Draw()
        /// Содержит бизнес-логику приложения
        /// </summary>
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        protected override void Update(GameTime CurrentGameTime)
        {
            if (NextState != null) //Если определена следующая страница, то сменить
            {
                CurrentState = NextState;
                NextState = null;
            }
            
            CurrentState.Update(CurrentGameTime); //Обновить логику
            
            CurrentState.PostUpdate(CurrentGameTime); //Очистка

            base.Update(CurrentGameTime);
        }
        /// <summary>
        /// Данный метод выполняется, после Update()
        /// Генерирует графическое содержание окна игры на основе данных метода Update()
        /// </summary>
        /// <param name="CurrentgameTime">A <see cref="T:Microsoft.Xna.Framework.GameTime" /> Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Draw(Microsoft.Xna.Framework.GameTime)" /> и время от старта игры</param>
        protected override void Draw(GameTime CurrentgameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); //Настроить цвет фона
            
            CurrentState.Draw(CurrentgameTime, CurrentSpriteBatch); //Отрисовать текущую страницу

            base.Draw(CurrentgameTime);
        }
        /// <summary>
        /// Производит переход между игровыми меню и игровыми режимами> 
        /// </summary>
        /// <param name="ChangeStateToNext">Объект абстрактного класса режимов игры и игровых меню</param>
        public void ChangeState(BaseState ChangeStateToNext)
        {
            NextState = ChangeStateToNext;
        }
    }
}