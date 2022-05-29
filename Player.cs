using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace WarGame
{
    public class Player
    {
        #region Fields
        private Color preferColor;
        private string name;
        private int playerHP;
        private bool type;
        private int hitBoxPosition;
        private int screenSizeX;
        #endregion
        #region Constructors
        public Player(string newName, Color color, int newHP, bool newType, XY inputScreenSize)
        {
            Name = newName;
            Color = color;
            HP = newHP;
            Current = newType;
            HitBoxPositionX = inputScreenSize.X;
            ScreenSizeX = inputScreenSize.X;
        }
        #endregion
        #region Properties
        public string Name
        {
            get => name;
            set => name = value;
        }
        public int HP 
        { 
            get => playerHP; 
            set => playerHP = value;
        }
        public bool Current
        {
            get => type;
            set => type = value;
        }
        public int HitBoxPositionX
        {
            get => hitBoxPosition;
            set
            {
                if (Current == true)
                {
                    hitBoxPosition = 0;
                }
                else
                {
                    hitBoxPosition = value;
                }
            }
        }
        public int ScreenSizeX
        {
            get => screenSizeX;
            set => screenSizeX = value;
        }
        public Color Color
        {
            get => preferColor;
            set => preferColor = value;
        }
        #endregion
        #region Methods
        public void AttackMe(int power)
        {
            HP -= power;
        }
        #endregion
    }
}
