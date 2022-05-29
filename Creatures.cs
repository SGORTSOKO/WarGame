using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WarGame
{
   public abstract class Creatures
    {
        protected Texture2D selfTexture;
        protected Vector2 position;
        protected Color creatureColor;
        protected int frameSizeX;
        protected int frameSizeY;
        protected int numberOfFramesX;
        protected int numberOfFramesY;
        protected int nowFrameNumberX;
        protected int nowFrameNumberY;
        protected float speedX;
        protected float speedY;
        protected Player player;
        protected int hitPoints;
        protected int power;
        protected int stamina;
        public virtual Texture2D SelfTexture { get; set; }
        public virtual Vector2 NowPosition { get; set; }
        public virtual float SpeedX { get; set; }
        public virtual float SpeedY { get; set; }
        public virtual int FrameSizeX { get; set; }
        public virtual int FrameSizeY { get; set; }
        public virtual int NumberOfFramesX { get; set; }
        public virtual int NumberOfFramesY { get; set; }
        public virtual int NowFrameNumberX { get; set; }
        public virtual int NowFrameNumberY { get; set; }
        public virtual float NowPositionX { get; set; }
        public virtual float NowPositionY { get; set; }
        public virtual Player Player { get; set; }
        public virtual int HP { get; set; }
        public virtual int Power { get; set; }
        public virtual int Stamina { get; set; }
        public virtual Color CreatureColor { get; set; }
        /*
        public static bool operator ==(Creatures first, Creatures second)
        {
            if (new Rectangle((int)first.NowPositionX, (int)first.NowPositionY, first.FrameSizeX, first.FrameSizeY ).Intersects(new Rectangle((int)second.NowPositionX, (int)second.NowPositionY, second.FrameSizeX, second.FrameSizeY)))
                return true;
            else
                return false;
        }
        public static bool operator !=(Creatures first, Creatures second)
        {
            if (!new Rectangle((int)first.NowPositionX, (int)first.NowPositionY, first.FrameSizeX, first.FrameSizeY).Intersects(new Rectangle((int)second.NowPositionX, (int)second.NowPositionY, second.FrameSizeX, second.FrameSizeY)))
                return true;
            else
                return false;
        }
        */
        public Creatures(Texture2D inputTexture, int inputNowPositionX, int inputNowPositionY, 
            int inputFrameSizeX, int inputFrameSizeY, int inputNumberOfFramesX, 
            int inputNumberOfFramesY, int inputNowFrameNumberX, int inputNowFrameNumberY, 
            float inputSpeedX, float inputSpeedY, Player inputPlayer, 
            Color inputCreatureColor, int inputHP, int inputPower, int inputStamina)
        {
            SelfTexture = inputTexture;
            NowPositionX = inputNowPositionX;
            NowPositionY = inputNowPositionY;
            FrameSizeX = inputFrameSizeX;
            FrameSizeY = inputFrameSizeY;
            NumberOfFramesX = inputNumberOfFramesX;
            NumberOfFramesY = inputNumberOfFramesY;
            NowFrameNumberX = inputNowFrameNumberX;
            NowFrameNumberY = inputNowFrameNumberY;
            CreatureColor = inputCreatureColor;
            SpeedX = inputSpeedX;
            SpeedY = inputSpeedY;
            Player = inputPlayer;
            HP = inputHP;
            Power = inputPower;
            Stamina = inputStamina;
        }
        public Creatures() { }
        public abstract int Step();
        public abstract Rectangle GetRectangleImage();
        public abstract Rectangle GetRectanglePos();
        public abstract bool HitCreature(Creatures defender);
    }
}
