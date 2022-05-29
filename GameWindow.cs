using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using WarGame.States;

namespace WarGame
{
    public class GameWindow : Game
    {

        public GraphicsDeviceManager mainGraphics;
        SpriteBatch spriteBatch;
        private State currentState;
        private State nextState;
        XY screenSize;

        public GameWindow()
        {
            TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 70);
            mainGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            screenSize = new XY(1920, 1080);
            //GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
            mainGraphics.PreferredBackBufferWidth = screenSize.X;
            mainGraphics.PreferredBackBufferHeight = screenSize.Y;
            mainGraphics.ApplyChanges();
            IsMouseVisible = true;
            base.Initialize();

        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            currentState = new MenuState(this, mainGraphics.GraphicsDevice, Content, screenSize);
        }

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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            currentState.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
        public void ChangeState(State state)
        {
            nextState = state;
        }
    }
}