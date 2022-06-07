// ***********************************************************************
// Assembly         : WarGame
// Author           : abros
// Created          : 06-06-2022
//
// Last Modified By : abros
// Last Modified On : 06-06-2022
// ***********************************************************************
// <copyright file="CreatureList.cs" company="Kurgan State University">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

namespace AKSU.SingleGame
{
    /// <summary>
    /// Класс CreatureList списка существ на поле.
    /// Обрабатывает логику их взаимодейтсвия.
    /// </summary>
    public class CreatureList
    {
        #region Fields
        /// <summary>
        /// Список существ на поле
        /// </summary>
        private List<Creatures> MainList;
        /// <summary>
        /// Список убитых за ход существ (для удаления из mainList)
        /// </summary>
        private List<Creatures> ToDelete;
        /// <summary>
        /// Левый (текущий) игрок
        /// </summary>
        private static Player LeftPlayer;
        /// <summary>
        /// Правый игрок
        /// </summary>
        private static Player RightPlayer;
        /// <summary>
        /// Получить количество существ
        /// </summary>
        public int Count
        {
            get => MainList.Count;
        }
        /// <summary>
        ///Получить или установить существо <see cref="Creatures" /> по индексу.
        /// </summary>
        /// <param name="Index">Индекс</param>
        /// <returns><see cref="Creatures" /></returns>
        public Creatures this[int Index]
        {
            get => MainList[Index];
            set => MainList[Index] = value;
        }

        #endregion
        #region Constructors
        /// <summary>
        /// Конструктор класса <see cref="CreatureList" />.
        /// </summary>
        /// <param name="Left">Левый (текущий) игрок</param>
        /// <param name="Right">Правый игрок</param>
        public CreatureList(Player Left, Player Right)
        {
            LeftPlayer = Left;
            RightPlayer = Right;
            MainList = new List<Creatures>();
            ToDelete = new List<Creatures>();
        }
        #endregion
        #region Methods
        
        /// <summary>
        /// Добавить существо в список
        /// </summary>
        /// <param name="InputCreature">The new creature</param>
        public void Add(Creatures InputCreature)  //
        {
            if (!MainList.Contains(InputCreature))
            {
                MainList.Add(InputCreature);
            }
        }

        /// <summary>
        /// Добавить существо в список.
        /// </summary>
        /// <param name="InputCreatureName">Имя (существа из предопределенных)</param>
        /// <param name="InputPlayer">Кому принадлежит существо</param>
        /// <param name="InputNowPositionY">Позиция существа по Y</param>
        /// <param name="InputTexture">Текстура существа</param>
        public void Add(
            string InputCreatureName,
            Player InputPlayer,
            int InputNowPositionY,
            Texture2D InputTexture)  //
        {
            if (InputCreatureName == "Human")
            {
                MainList.Add(
                    new Human(
                        InputTexture,
                        InputPlayer,
                        InputNowPositionY
                        )
                    );
            }
        }
        /// <summary>
        /// Атаки существ по друг-другу
        /// </summary>
        public void AttackRound()
        {
            //Атакующее существо
            foreach (Creatures Attaker in MainList)
            {
                //Каждое существо атакует лишь раз за фрейм
                bool Attacking = false;
                //Если атакующий не был убит, то
                if (!ToDelete.Contains(Attaker))
                {
                    //Защищающееся существо
                    foreach (Creatures Defender in MainList)
                    {
                        if (
                            !ToDelete.Contains(Defender) && //Не атаковать труп
                            Attaker != Defender && //Не атаковать себя
                            Attaker.Player.Name != Defender.Player.Name && //Не атаковать союзника
                            Attaker.GetRectanglePos().Intersects(Defender.GetRectanglePos()) //атакующий и защищающийся рядом
                            )
                        {
                            Attacking = true;
                            //Остановка на время сражения
                            Attaker.SpeedX = 0;
                            if (Attaker.HP > 0 && Defender.HP > 0)
                            {
                                //Если защищающийся убит
                                if (Attaker.HitCreature(Defender))
                                {
                                    ToDelete.Add(Defender);
                                }
                            }
                        }
                    }
                }
                //Если за ход не атаковал
                if (Attacking == false)
                {
                    Attaker.SpeedX = 50;
                }
            }
            //Очистить от убитых
            foreach (Creatures Creature in ToDelete)
            {
                MainList.Remove(Creature);
            }
            ToDelete.Clear();
        }
        /// <summary>
        /// Очистить список ввсех существ
        /// </summary>
        public void Clear()
        {
            MainList.Clear();
        }
        /// <summary>
        /// Удалить несколько существ по требованию
        /// </summary>
        /// <param name="Count">Количество к удалению</param>
        public void DeleteSome(int Count)
        {
            if (MainList.Count > Count * 5)
            {
                for (int i = 0; i < Count; i++)
                {
                    MainList.RemoveAt(i);
                }
            }
        }
        /// <summary>
        /// Рандомизация очереди обработки логики существ в списке
        /// </summary>
        public void RandSort()
        {
            if (MainList.Count != 0)
            {
                System.Random Rand = new System.Random();
                for (int i = MainList.Count - 1; i >= 1; i--)
                {
                    int j = Rand.Next(i + 1);
                    var Temp = MainList[j];
                    MainList[j] = MainList[i];
                    MainList[i] = Temp;
                }
            }
        }
        /// <summary>
        /// Восстановить параметры существ
        /// </summary>
        public void ResetStats()
        {
            foreach (Creatures Creature in MainList)
            {
                Creature.Stamina++;
            }
        }

        /// <summary>
        /// Движение существ (и атака игрока придостижении экрана)
        /// </summary>
        /// <returns>Победивший <c>Player</c>, если здоровье одного из них достигло 0</returns>
        public Player StepAll()
        {
            foreach (Creatures Creature in MainList)
            {
                //Движение существа
                int AttackPower = Creature.Step();
                //если достиг края экрана, то атакует на attackPower
                if (AttackPower > 0)
                {
                    if (Creature.Player == LeftPlayer)
                    {
                        RightPlayer.AttackMe(AttackPower);
                    }
                    else
                    {
                        LeftPlayer.AttackMe(AttackPower);
                    }
                    //Удалить существо, достигшее края экрана
                    ToDelete.Add(Creature);

                    if (LeftPlayer.HP <= 0)
                    {
                        return RightPlayer;
                    }
                    else
                    {
                        if (RightPlayer.HP <= 0)
                        {
                            return LeftPlayer;
                        }
                    }
                }
            }
            return null;
        }
        #endregion
    }
}
