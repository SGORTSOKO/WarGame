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
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace WarGame.SingleGame
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
        private List<Creatures> mainList;
        /// <summary>
        /// Список убитых за ход существ (для удаления из mainList)
        /// </summary>
        private List<Creatures> toDelete;
        /// <summary>
        /// Левый (текущий) игрок
        /// </summary>
        private static Player leftPlayer;
        /// <summary>
        /// Правый игрок
        /// </summary>
        private static Player rightPlayer;
        /// <summary>
        /// Получить количество существ
        /// </summary>
        public int Count
        {
            get => mainList.Count;
        }
        /// <summary>
        ///Получить или установить существо <see cref="Creatures" /> по индексу.
        /// </summary>
        /// <param name="index1">Индекс</param>
        /// <returns><see cref="Creatures" /></returns>
        public Creatures this[int index1]
        {
            get => mainList[index1];
            set => mainList[index1] = value;
        }

        #endregion
        #region Constructors
        /// <summary>
        /// Конструктор класса <see cref="CreatureList" />.
        /// </summary>
        /// <param name="left">Левый (текущий) игрок</param>
        /// <param name="right">Правый игрок</param>
        public CreatureList(Player left, Player right)
        {
            leftPlayer = left;
            rightPlayer = right;
            mainList = new List<Creatures>();
            toDelete = new List<Creatures>();
        }
        #endregion
        #region Methods
        
        /// <summary>
        /// Добавить существо в список
        /// </summary>
        /// <param name="input">The new creature</param>
        public void Add(Creatures input)  //
        {
            if (!mainList.Contains(input))
            {
                mainList.Add(input);
            }
        }

        /// <summary>
        /// Добавить существо в список.
        /// </summary>
        /// <param name="input">Имя (существа из предопределенных)</param>
        /// <param name="inputPlayer">Кому принадлежит существо</param>
        /// <param name="inputNowPositionY">Позиция существа по Y</param>
        /// <param name="inputTexture">Текстура существа</param>
        public void Add(
            string input,
            Player inputPlayer,
            int inputNowPositionY,
            Texture2D inputTexture)  //
        {
            if (input == "Human")
            {
                mainList.Add(
                    new Human(
                        inputTexture,
                        inputPlayer,
                        inputNowPositionY
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
            foreach (Creatures Attaker in mainList)
            {
                //Каждое существо атакует лишь раз за фрейм
                bool attacking = false;
                //Если атакующий не был убит, то
                if (!toDelete.Contains(Attaker))
                {
                    //Защищающееся существо
                    foreach (Creatures Defender in mainList)
                    {
                        if (
                            !toDelete.Contains(Defender) && //Не атаковать труп
                            Attaker != Defender && //Не атаковать себя
                            Attaker.Player.Name != Defender.Player.Name && //Не атаковать союзника
                            Attaker.GetRectanglePos().Intersects(Defender.GetRectanglePos()) //атакующий и защищающийся рядом
                            )
                        {
                            attacking = true;
                            //Остановка на время сражения
                            Attaker.SpeedX = 0;
                            if (Attaker.HP > 0 && Defender.HP > 0)
                            {
                                //Если защищающийся убит
                                if (Attaker.HitCreature(Defender))
                                {
                                    toDelete.Add(Defender);
                                }
                            }
                        }
                    }
                }
                //Если за ход не атаковал
                if (attacking == false)
                {
                    Attaker.SpeedX = 50;
                }
            }
            //Очистить от убитых
            foreach (Creatures Creature in toDelete)
            {
                mainList.Remove(Creature);
            }
            toDelete.Clear();
        }
        /// <summary>
        /// Очистить список ввсех существ
        /// </summary>
        public void Clear()
        {
            mainList.Clear();
        }
        /// <summary>
        /// Удалить несколько существ по требованию
        /// </summary>
        /// <param name="count">Количество к удалению</param>
        public void DeleteSome(int count)
        {
            if (mainList.Count > count * 5)
            {
                for (int i = 0; i < count; i++)
                {
                    mainList.RemoveAt(i);
                }
            }
        }
        /// <summary>
        /// Рандомизация очереди обработки логики существ в списке
        /// </summary>
        public void RandSort()
        {
            if (mainList.Count != 0)
            {
                System.Random rand = new System.Random();
                for (int i = mainList.Count - 1; i >= 1; i--)
                {
                    int j = rand.Next(i + 1);
                    var temp = mainList[j];
                    mainList[j] = mainList[i];
                    mainList[i] = temp;
                }
            }
        }
        /// <summary>
        /// Восстановить параметры существ
        /// </summary>
        public void ResetStats()
        {
            foreach (Creatures Creature in mainList)
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
            foreach (Creatures Creature in mainList)
            {
                //Движение существа
                int attackPower = Creature.Step();
                //если достиг края экрана, то атакует на attackPower
                if (attackPower > 0)
                {
                    if (Creature.Player == leftPlayer)
                    {
                        rightPlayer.AttackMe(attackPower);
                    }
                    else
                    {
                        leftPlayer.AttackMe(attackPower);
                    }
                    //Удалить существо, достигшее края экрана
                    toDelete.Add(Creature);

                    if (leftPlayer.HP <= 0)
                    {
                        return rightPlayer;
                    }
                    else
                    {
                        if (rightPlayer.HP <= 0)
                        {
                            return leftPlayer;
                        }
                    }
                }
            }
            return null;
        }
        #endregion
    }
}
