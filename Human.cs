using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WarGame
{
    public class Human : Creatures
    {
        
        public Human(
            Texture2D inputTexture, 
            int inputNowPositionX, 
            int inputNowPositionY,
            int inputFrameSizeX, 
            int inputFrameSizeY, 
            int inputNumberOfFramesX,
            int inputNumberOfFramesY, 
            int inputNowFrameNumberX, 
            int inputNowFrameNumberY,
            float inputSpeedX, 
            float inputSpeedY, 
            Player inputPlayer, 
            int inputHP, 
            int inputPower, 
            int inputStamina):
            base(
                inputTexture, 
                inputNowPositionX, 
                inputNowPositionY,
                inputFrameSizeX, 
                inputFrameSizeY, 
                inputNumberOfFramesX,
                inputNumberOfFramesY, 
                inputNowFrameNumberX, 
                inputNowFrameNumberY,
                inputSpeedX, 
                inputSpeedY, 
                inputPlayer, 
                inputHP, 
                inputPower, 
                inputStamina)
        { }
        public Human(
            Texture2D inputTexture, 
            Player inputPlayer, 
            int inputNowPositionY) :
            base(
                inputTexture,
                inputPlayer.HitBoxPositionX,
                inputNowPositionY,
                inputTexture.Width / 8,
                inputTexture.Height / 2,
                8,
                2,
                0,
                0,
                30,
                0,
                inputPlayer,
                100,
                20,
                1)
        { }
        public Human() :
            base()
        { }
        public override Texture2D SelfTexture 
        { 
            get => selfTexture; 
            set => selfTexture = value; 
        }
        public override int FrameSizeX 
        { 
            get => frameSizeX; 
            set => frameSizeX = value; 
        } 
        public override int FrameSizeY 
        { 
            get => frameSizeY; 
            set => frameSizeY = value; 
        }
        public override int NumberOfFramesX
        {
            get => numberOfFramesX;
            set => numberOfFramesX = value;
        }
        public override int NumberOfFramesY
        {
            get => numberOfFramesY;
            set => numberOfFramesY = value;
        }
        public override int NowFrameNumberX
        {
            get => nowFrameNumberX;
            set => nowFrameNumberX = value;
        }
        public override int NowFrameNumberY
        {
            get => nowFrameNumberY;
            set => nowFrameNumberY = value;
        }
        public override float NowPositionX
        {
            get => position.X;
            set => position.X = value;
        }
        public override float NowPositionY
        {
            get => position.Y;
            set => position.Y = value;
        }
        public override Vector2 NowPosition
        {
            get => position;
            set => position = value;
        }
        public override float SpeedX
        {
            get => speedX;
            set => speedX = value;
        }
        public override float SpeedY
        {
            get => speedY;
            set => speedY = value;
        }
        public override Player Player
        {
            get => player;
            set
            {
                player = value;
                if (value.Current == true)
                {
                    NowFrameNumberY = 0;
                }
                else
                {
                    NowFrameNumberY = 1;
                }
            }
        }
        public override int HP
        {
            get => hitPoints;
            set => hitPoints = value;
        }
        public override int Power
        {
            get => power;
            set => power = value;
        }
        public override int Stamina
        {
            get => stamina;
            set => stamina = value;
        }
        public override int Step()
        {
            if (player.Current == true)
            {
                position.X += SpeedX;
                nowFrameNumberX++;
                if (nowFrameNumberX == numberOfFramesX)
                {
                    nowFrameNumberX = 0;
                }
                if (position.X > 1920 + frameSizeX / 2)
                {
                    return Power;
                }
            }
            else
            {
                position.X -= SpeedX;
                if (nowFrameNumberX == 0)
                {
                    nowFrameNumberX = numberOfFramesX;
                }
                nowFrameNumberX--;
                if (position.X < 0 - frameSizeX / 2)
                {
                    return Power;
                }
            }
            return 0;

        }
        public override bool HitCreature(Creatures defender)
        {
            if (stamina > 0)
            {
                stamina--;
                defender.HP -= Power;
                if (defender.HP <= 0)
                {
                    defender.Power = 0;
                    return true;
                }
            }
            return false;
        }
        public override Rectangle GetRectangleImage()
        {
            return new Rectangle(NowFrameNumberX * FrameSizeX, nowFrameNumberY * FrameSizeY, FrameSizeX, FrameSizeY);
        }
        public override Rectangle GetRectanglePos()
        {
            return new Rectangle((int)NowPositionX, (int)NowPositionY, FrameSizeX, FrameSizeY);
        }
    }
}
