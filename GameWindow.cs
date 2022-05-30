using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using WarGame.States;

namespace WarGame
{
    /// <summary>
    /// Класс GameWindow является наследником от Microsoft.Xna.Framework.Game
    /// Данный класс инициализирует приложение и выводит изображение
    /// </summary>
    public class GameWindow : Game
    {
        /// <summary>
        /// The main graphics device
        /// </summary>
        public GraphicsDeviceManager mainGraphics;
        /// <summary>
        /// The sprite batch
        /// </summary>
        SpriteBatch spriteBatch;
        /// <summary>
        /// The current state
        /// </summary>
        private State currentState;
        /// <summary>
        /// The next state
        /// </summary>
        private State nextState;
        /// <summary>
        /// The screen size
        /// </summary>
        XY screenSize;

        /// <summary>
        /// Конструктор класса GameWindow
        /// </summary>
        public GameWindow()
        {
            TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 80);
            mainGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        /// <summary>
        /// Данный метод выполняется единожды, раньше всех остальных
        /// Выполняет инициализацию параметров графического устройства
        /// </summary>
        protected override void Initialize()
        {
            screenSize = new XY(1600, 900);
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
            currentState = new MenuState(this, mainGraphics.GraphicsDevice, Content, screenSize);
        }
        /// <summary>
        /// Данный метод выполняется, после LoadContent() или сразу после выполнения Draw()
        /// Содержит бизнес-логику приложения
        /// <paramref name="gameTime" /> Хранит временное состояние игры
        /// </summary>
        /// <param name="gameTime">The elapsed time since the last call to <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        protected override void Update(GameTime gameTime)
        {
            if (nextState != null)
            {
                currentState = nextState;
                nextState = null;
            }
            currentState.Update(gameTime);
            currentState.PostUpdate(gameTime);
            base.Update(gameTime);
        }
        /// <summary>
        /// Данный метод выполняется, после Update()
        /// Генерирует графическое содержание окна игры на основе данных метода Update()
        /// <paramref name="gameTime" /> Чранит временное состояние игры
        /// </summary>
        /// <param name="gameTime">A <see cref="T:Microsoft.Xna.Framework.GameTime" /> instance containing the elapsed time since the last call to <see cref="M:Microsoft.Xna.Framework.Game.Draw(Microsoft.Xna.Framework.GameTime)" /> and the total time elapsed since the game started.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            currentState.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
        /// <summary>
        /// Производит переход между игровыми меню и игровыми режимами   
        /// <paramref name="state" /> Объект абстрактного класса режимов игры и игровых меню
        /// </summary>
        /// <param name="state">The state.</param>
        public void ChangeState(State state)
        {
            nextState = state;
        }
    }
}