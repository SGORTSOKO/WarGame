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

namespace AKSU.SingleGame
{
    /// <summary>
    /// Класс сущетсва Human.
    /// Унаследовано от <see cref="AKSU.Creatures" />
    /// </summary>
    /// <seealso cref="AKSU.Creatures" />
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
            get => CreatureTexture;
            set => CreatureTexture = value;
        }
        /// <summary>
        /// Установить или вернуть зармер фрейма по Х
        /// </summary>
        public override int FrameSizeX
        {
            get => CreatureFrameSizeX;
            set => CreatureFrameSizeX = value;
        }
        /// <summary>
        /// Установить или вернуть размер фрейма по У
        /// </summary>
        public override int FrameSizeY
        {
            get => CreatureFrameSizeY;
            set => CreatureFrameSizeY = value;
        }
        /// <summary>
        /// Установить или вернуть количество фреймов по Ч
        /// </summary>
        public override int NumberOfFramesX
        {
            get => CreatureNumberOfFramesX;
            set => CreatureNumberOfFramesX = value;
        }
        /// <summary>
        /// Установить или вернуть количество фреймов по У
        /// </summary>
        public override int NumberOfFramesY
        {
            get => CreatureNumberOfFramesY;
            set => CreatureNumberOfFramesY = value;
        }
        /// <summary>
        /// Установить или вернуть номер текущего фрейма по X
        /// </summary>
        public override int NowFrameNumberX
        {
            get => CreatureNowFrameNumberX;
            set => CreatureNowFrameNumberX = value;
        }
        /// <summary>
        /// Установить или вернуть номер текущего фрейма по У
        /// </summary>
        public override int NowFrameNumberY
        {
            get => CreatureNowFrameNumberY;
            set => CreatureNowFrameNumberY = value;
        }
        /// <summary>
        /// Установить или вернуть текущую позицию по Х
        /// </summary>
        public override float NowPositionX
        {
            get => CreaturePosition.X;
            set => CreaturePosition.X = value;
        }
        /// <summary>
        /// Установить или вернуть текущую позицию по У
        /// </summary>
        public override float NowPositionY
        {
            get => CreaturePosition.Y;
            set => CreaturePosition.Y = value;
        }
        /// <summary>
        /// Установить или вернуть текущую позицию
        /// </summary>
        /// <value> Vector2 позиция</value>
        public override Vector2 NowPosition
        {
            get => CreaturePosition;
            set => CreaturePosition = value;
        }
        /// <summary>
        /// Установить или вернуть скорость по Х
        /// </summary>
        public override float SpeedX
        {
            get => CreatureSpeedX;
            set => CreatureSpeedX = value;
        }
        /// <summary>
        /// Установить или вернуть скорость по У
        /// </summary>
        public override float SpeedY
        {
            get => CreatureSpeedY;
            set => CreatureSpeedY = value;
        }
        /// <summary>
        /// Установить или вернуть игрока существа
        /// </summary>
        public override Player Player
        {
            get => CreaturePlayer;
            set
            {
                CreaturePlayer = value;
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
            get => CreatureHitPoints;
            set => CreatureHitPoints = value;
        }
        /// <summary>
        /// Установить или вернуть силу атаки существа
        /// </summary>
        public override int Power
        {
            get => CreaturePower;
            set => CreaturePower = value;
        }
        /// <summary>
        /// Установить или вернуть выносливость
        /// </summary>
        public override int Stamina
        {
            get => CreatureStamina;
            set => CreatureStamina = value;
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
            if (CreaturePlayer.Current == true)
            {
                //Движение вправо
                CreaturePosition.X += SpeedX;
                CreatureNowFrameNumberX++;
                if (CreatureNowFrameNumberX == CreatureNumberOfFramesX)
                {
                    CreatureNowFrameNumberX = 0;
                }
                //Если достиг края экрана
                if (CreaturePosition.X > CreaturePlayer.ScreenSizeX + CreatureFrameSizeX / 2)
                {
                    return Power;
                }
            }
            else
            {
                //Движение влево
                CreaturePosition.X -= SpeedX;
                if (CreatureNowFrameNumberX == 0)
                {
                    CreatureNowFrameNumberX = CreatureNumberOfFramesX;
                }
                CreatureNowFrameNumberX--;
                //Если достиг края экрана
                if (CreaturePosition.X < 0 - CreatureFrameSizeX / 2)
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
            if (CreatureStamina > 0)
            {
                CreatureStamina--;
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
                CreatureNowFrameNumberY * FrameSizeY,
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
