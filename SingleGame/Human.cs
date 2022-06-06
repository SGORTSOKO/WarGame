// ***********************************************************************
// Assembly         : WarGame
// Author           : abros
// Created          : 06-06-2022
//
// Last Modified By : abros
// Last Modified On : 06-06-2022
// ***********************************************************************
// <copyright file="Human.cs" company="Kurgan State University">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WarGame.SingleGame
{
    /// <summary>
    /// Класс сущетсва Human.
    /// Унаследовано от <see cref="WarGame.Creatures" />
    /// </summary>
    /// <seealso cref="WarGame.Creatures" />
    public class Human : Creatures
    {
        #region Constructors
        /// <summary>
        /// Конструктор класса <see cref="Creatures" />.
        /// </summary>
        /// <param name="inputTexture">Текстура существа</param>
        /// <param name="inputNowPositionX">Позиция Х существа</param>
        /// <param name="inputNowPositionY">Позиция У сущетсва</param>
        /// <param name="inputFrameSizeX">Размер фрейма по Х</param>
        /// <param name="inputFrameSizeY">Размер фрейма по У</param>
        /// <param name="inputNumberOfFramesX">Всего фреймов по Х</param>
        /// <param name="inputNumberOfFramesY">Всего фреймов по У</param>
        /// <param name="inputNowFrameNumberX">Текущий фрейм по Х</param>
        /// <param name="inputNowFrameNumberY">Текущий фрейм по У</param>
        /// <param name="inputSpeedX">Скорость существа по Х</param>
        /// <param name="inputSpeedY">Скорость сущетства по У</param>
        /// <param name="inputPlayer">Чье существо</param>
        /// <param name="inputHP">Здоровье существа</param>
        /// <param name="inputPower">Сила атаки существа</param>
        /// <param name="inputStamina">Выносливость существа</param>
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
            int inputStamina) :
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
        /// <summary>
        /// Конструктор класса <see cref="Human" />.
        /// </summary>
        /// <param name="inputTexture">Текстура существа</param>
        /// <param name="inputPlayer">Чье существо</param>
        /// <param name="inputNowPositionY">Позиция У сущетсва</param>
        public Human(
            Texture2D inputTexture,
            Player inputPlayer,
            int inputNowPositionY) :
            base(
                inputTexture,
                inputPlayer.HitBoxPositionX - inputTexture.Width / 16,
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
        /// <summary>
        /// Конструктор класса <see cref="Human" />.
        /// </summary>
        public Human() :
            base()
        { }
        #endregion
        #region Properties
        /// <summary>
        /// Установить или вернуть текстуру существа
        /// </summary>
        public override Texture2D SelfTexture
        {
            get => selfTexture;
            set => selfTexture = value;
        }
        /// <summary>
        /// Установить или вернуть зармер фрейма по Х
        /// </summary>
        public override int FrameSizeX
        {
            get => frameSizeX;
            set => frameSizeX = value;
        }
        /// <summary>
        /// Установить или вернуть размер фрейма по У
        /// </summary>
        public override int FrameSizeY
        {
            get => frameSizeY;
            set => frameSizeY = value;
        }
        /// <summary>
        /// Установить или вернуть количество фреймов по Ч
        /// </summary>
        public override int NumberOfFramesX
        {
            get => numberOfFramesX;
            set => numberOfFramesX = value;
        }
        /// <summary>
        /// Установить или вернуть количество фреймов по У
        /// </summary>
        public override int NumberOfFramesY
        {
            get => numberOfFramesY;
            set => numberOfFramesY = value;
        }
        /// <summary>
        /// Установить или вернуть номер текущего фрейма по X
        /// </summary>
        public override int NowFrameNumberX
        {
            get => nowFrameNumberX;
            set => nowFrameNumberX = value;
        }
        /// <summary>
        /// Установить или вернуть номер текущего фрейма по У
        /// </summary>
        public override int NowFrameNumberY
        {
            get => nowFrameNumberY;
            set => nowFrameNumberY = value;
        }
        /// <summary>
        /// Установить или вернуть текущую позицию по Х
        /// </summary>
        public override float NowPositionX
        {
            get => position.X;
            set => position.X = value;
        }
        /// <summary>
        /// Установить или вернуть текущую позицию по У
        /// </summary>
        public override float NowPositionY
        {
            get => position.Y;
            set => position.Y = value;
        }
        /// <summary>
        /// Установить или вернуть текущую позицию
        /// </summary>
        /// <value> Vector2 позиция</value>
        public override Vector2 NowPosition
        {
            get => position;
            set => position = value;
        }
        /// <summary>
        /// Установить или вернуть скорость по Х
        /// </summary>
        public override float SpeedX
        {
            get => speedX;
            set => speedX = value;
        }
        /// <summary>
        /// Установить или вернуть скорость по У
        /// </summary>
        public override float SpeedY
        {
            get => speedY;
            set => speedY = value;
        }
        /// <summary>
        /// Установить или вернуть игрока существа
        /// </summary>
        public override Player Player
        {
            get => player;
            set
            {
                player = value;
                //Автоматически определить текущий фрейм по Y
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
        /// <summary>
        /// Установить или вернуть здоровье существа
        /// </summary>
        public override int HP
        {
            get => hitPoints;
            set => hitPoints = value;
        }
        /// <summary>
        /// Установить или вернуть силу атаки существа
        /// </summary>
        public override int Power
        {
            get => power;
            set => power = value;
        }
        /// <summary>
        /// Установить или вернуть выносливость
        /// </summary>
        public override int Stamina
        {
            get => stamina;
            set => stamina = value;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Движение существа
        /// </summary>
        /// <returns>Сила атаки по игроку</returns>
        public override int Step()
        { 
            //Если левый (текущий), то
            if (player.Current == true)
            {
                //Движение вправо
                position.X += SpeedX;
                nowFrameNumberX++;
                if (nowFrameNumberX == numberOfFramesX)
                {
                    nowFrameNumberX = 0;
                }
                //Если достиг края экрана
                if (position.X > player.ScreenSizeX + frameSizeX / 2)
                {
                    return Power;
                }
            }
            else
            {
                //Движение влево
                position.X -= SpeedX;
                if (nowFrameNumberX == 0)
                {
                    nowFrameNumberX = numberOfFramesX;
                }
                nowFrameNumberX--;
                //Если достиг края экрана
                if (position.X < 0 - frameSizeX / 2)
                {
                    return Power;
                }
            }
            return 0;

        }
        /// <summary>
        /// Атаковать иное существо
        /// </summary>
        /// <param name="defender">Защищающееся существо</param>
        /// <returns><c>true</c> если защищающийся мертв, иначе <c>false</c>.</returns>
        public override bool HitCreature(Creatures defender)
        {
            //Если атакующий еще имеет выносливость
            if (stamina > 0)
            {
                stamina--;
                defender.HP -= Power;
                //Если здоровье защищающегося ниже 0
                if (defender.HP <= 0)
                {
                    defender.Power = 0;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// размеры фрейма отрисовки
        /// </summary>
        /// <returns>Rectangle</returns>
        public override Rectangle GetRectangleImage()
        {
            return new Rectangle(
                NowFrameNumberX * FrameSizeX,
                nowFrameNumberY * FrameSizeY,
                FrameSizeX,
                FrameSizeY);
        }
        /// <summary>
        /// размеры фрейма движения
        /// </summary>
        /// <returns>Rectangle.</returns>
        public override Rectangle GetRectanglePos()
        {
            return new Rectangle(
                (int)NowPositionX,
                (int)NowPositionY,
                FrameSizeX,
                FrameSizeY);
        }
        #endregion
    }
}
