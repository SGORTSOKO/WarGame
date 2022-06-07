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
    public class Human : BaseCreatures
    {
        #region Constructors
        /// <summary>
        /// Конструктор класса <see cref="BaseCreatures" />.
        /// </summary>
        /// <param name="InputTexture">Текстура существа</param>
        /// <param name="InputNowPositionX">Позиция Х существа</param>
        /// <param name="InputNowPositionY">Позиция У сущетсва</param>
        /// <param name="InputFrameSizeX">Размер фрейма по Х</param>
        /// <param name="InputFrameSizeY">Размер фрейма по У</param>
        /// <param name="InputNumberOfFramesX">Всего фреймов по Х</param>
        /// <param name="InputNumberOfFramesY">Всего фреймов по У</param>
        /// <param name="InputNowFrameNumberX">Текущий фрейм по Х</param>
        /// <param name="InputNowFrameNumberY">Текущий фрейм по У</param>
        /// <param name="InputSpeedX">Скорость существа по Х</param>
        /// <param name="InputSpeedY">Скорость сущетства по У</param>
        /// <param name="InputPlayer">Чье существо</param>
        /// <param name="InputHP">Здоровье существа</param>
        /// <param name="InputPower">Сила атаки существа</param>
        /// <param name="InputStamina">Выносливость существа</param>
        public Human(
            Texture2D InputTexture,
            int InputNowPositionX,
            int InputNowPositionY,
            int InputFrameSizeX,
            int InputFrameSizeY,
            int InputNumberOfFramesX,
            int InputNumberOfFramesY,
            int InputNowFrameNumberX,
            int InputNowFrameNumberY,
            float InputSpeedX,
            float InputSpeedY,
            Player InputPlayer,
            int InputHP,
            int InputPower,
            int InputStamina) :
            base(
                InputTexture,
                InputNowPositionX,
                InputNowPositionY,
                InputFrameSizeX,
                InputFrameSizeY,
                InputNumberOfFramesX,
                InputNumberOfFramesY,
                InputNowFrameNumberX,
                InputNowFrameNumberY,
                InputSpeedX,
                InputSpeedY,
                InputPlayer,
                InputHP,
                InputPower,
                InputStamina)
        { }
        /// <summary>
        /// Конструктор класса <see cref="Human" />.
        /// </summary>
        /// <param name="InputTexture">Текстура существа</param>
        /// <param name="InputPlayer">Чье существо</param>
        /// <param name="InputNowPositionY">Позиция У сущетсва</param>
        public Human(
            Texture2D InputTexture,
            Player InputPlayer,
            int InputNowPositionY) :
            base(
                InputTexture,
                InputPlayer.HitBoxPositionX - 
                    InputTexture.Width / 16,
                InputNowPositionY,
                InputTexture.Width / 8,
                InputTexture.Height / 2,
                8,
                2,
                0,
                0,
                30,
                0,
                InputPlayer,
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
            get => сreatureTexture;
            set => сreatureTexture = value;
        }
        /// <summary>
        /// Установить или вернуть зармер фрейма по Х
        /// </summary>
        public override int FrameSizeX
        {
            get => сreatureFrameSizeX;
            set => сreatureFrameSizeX = value;
        }
        /// <summary>
        /// Установить или вернуть размер фрейма по У
        /// </summary>
        public override int FrameSizeY
        {
            get => сreatureFrameSizeY;
            set => сreatureFrameSizeY = value;
        }
        /// <summary>
        /// Установить или вернуть количество фреймов по Ч
        /// </summary>
        public override int NumberOfFramesX
        {
            get => сreatureNumberOfFramesX;
            set => сreatureNumberOfFramesX = value;
        }
        /// <summary>
        /// Установить или вернуть количество фреймов по У
        /// </summary>
        public override int NumberOfFramesY
        {
            get => сreatureNumberOfFramesY;
            set => сreatureNumberOfFramesY = value;
        }
        /// <summary>
        /// Установить или вернуть номер текущего фрейма по X
        /// </summary>
        public override int NowFrameNumberX
        {
            get => сreatureNowFrameNumberX;
            set => сreatureNowFrameNumberX = value;
        }
        /// <summary>
        /// Установить или вернуть номер текущего фрейма по У
        /// </summary>
        public override int NowFrameNumberY
        {
            get => сreatureNowFrameNumberY;
            set => сreatureNowFrameNumberY = value;
        }
        /// <summary>
        /// Установить или вернуть текущую позицию по Х
        /// </summary>
        public override float NowPositionX
        {
            get => сreaturePosition.X;
            set => сreaturePosition.X = value;
        }
        /// <summary>
        /// Установить или вернуть текущую позицию по У
        /// </summary>
        public override float NowPositionY
        {
            get => сreaturePosition.Y;
            set => сreaturePosition.Y = value;
        }
        /// <summary>
        /// Установить или вернуть текущую позицию
        /// </summary>
        /// <value> Vector2 позиция</value>
        public override Vector2 NowPosition
        {
            get => сreaturePosition;
            set => сreaturePosition = value;
        }
        /// <summary>
        /// Установить или вернуть скорость по Х
        /// </summary>
        public override float SpeedX
        {
            get => сreatureSpeedX;
            set => сreatureSpeedX = value;
        }
        /// <summary>
        /// Установить или вернуть скорость по У
        /// </summary>
        public override float SpeedY
        {
            get => сreatureSpeedY;
            set => сreatureSpeedY = value;
        }
        /// <summary>
        /// Установить или вернуть игрока существа
        /// </summary>
        public override Player Player
        {
            get => сreaturePlayer;
            set
            {
                сreaturePlayer = value;
                
                if (value.Current == true) //Автоматически определить текущий фрейм по Y
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
            get => сreatureHitPoints;
            set => сreatureHitPoints = value;
        }
        /// <summary>
        /// Установить или вернуть силу атаки существа
        /// </summary>
        public override int Power
        {
            get => сreaturePower;
            set => сreaturePower = value;
        }
        /// <summary>
        /// Установить или вернуть выносливость
        /// </summary>
        public override int Stamina
        {
            get => сreatureStamina;
            set => сreatureStamina = value;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Движение существа
        /// </summary>
        /// <returns>Сила атаки по игроку</returns>
        public override int Step()
        { 
            if (сreaturePlayer.Current == true) //Если левый (текущий), то
            {
                сreaturePosition.X += SpeedX; //Движение вправо
                сreatureNowFrameNumberX++; //Следующий фрейм
                if (сreatureNowFrameNumberX == сreatureNumberOfFramesX)
                {
                    сreatureNowFrameNumberX = 0;
                }
                if (сreaturePosition.X > сreaturePlayer.ScreenSizeX + 
                    сreatureFrameSizeX / 2) //Если достиг края экрана
                {
                    return Power;
                }
            }
            else
            {
                сreaturePosition.X -= SpeedX; //Движение влево
                if (сreatureNowFrameNumberX == 0)
                {
                    сreatureNowFrameNumberX = сreatureNumberOfFramesX;
                }
                сreatureNowFrameNumberX--; //Следующий фрейм
                
                if (сreaturePosition.X < 0 - сreatureFrameSizeX / 2) //Если достиг края экрана
                {
                    return Power;
                }
            }
            return 0;

        }
        /// <summary>
        /// Атаковать иное существо
        /// </summary>
        /// <param name="Вefender">Защищающееся существо</param>
        /// <returns><c>true</c> если защищающийся мертв, иначе <c>false</c>.</returns>
        public override bool HitCreature(BaseCreatures Вefender)
        {
            if (сreatureStamina > 0) //Если атакующий еще имеет выносливость
            {
                сreatureStamina--;
                Вefender.HP -= Power;
                
                if (Вefender.HP <= 0) //Если здоровье защищающегося ниже 0
                {
                    Вefender.Power = 0;
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
                сreatureNowFrameNumberY * FrameSizeY,
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
