using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace WarGame
{
    public class Player
    {
        private Color preferColor;
        private string name;
        private int playerHP;
        private bool type;

        public Player(string newName, Color color, int newHP, bool newType)
        {
            Name = newName;
            Color = color;
            HP = newHP;
            Current = newType;
        }
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
        public Color Color
        {
            get => preferColor;
            set => preferColor = value;
        }
        public void AttackMe(int power)
        {
            HP -= power;
        }
    }
}
