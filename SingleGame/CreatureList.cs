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
        private List<BaseCreatures> mainList;
        /// <summary>
        /// Список убитых за ход существ (для удаления из mainList)
        /// </summary>
        private List<BaseCreatures> toDelete;
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
        ///Получить или установить существо <see cref="BaseCreatures" /> по индексу.
        /// </summary>
        /// <param name="Index">Индекс</param>
        /// <returns><see cref="BaseCreatures" /></returns>
        public BaseCreatures this[int Index]
        {
            get => mainList[Index];
            set => mainList[Index] = value;
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
            leftPlayer = Left;
            rightPlayer = Right;
            mainList = new List<BaseCreatures>();
            toDelete = new List<BaseCreatures>();
        }
        #endregion
        #region Methods
        
        /// <summary>
        /// Добавить существо в список
        /// </summary>
        /// <param name="InputCreature">The new creature</param>
        public void Add(BaseCreatures InputCreature)
        {
            if (!mainList.Contains(InputCreature)) //Запрет дублирования  существ
            {
                mainList.Add(InputCreature);
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
            Texture2D InputTexture)
        {
            if (InputCreatureName == "Human")
            {
                mainList.Add(
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
            foreach (BaseCreatures Attaker in mainList) //Атакующее существо
            {
                bool Attacking = false; //Каждое существо атакует лишь раз за фрейм
                
                if (!toDelete.Contains(Attaker)) //Если атакующий не был убит, то
                {
                    foreach (BaseCreatures Defender in mainList) //Защищающееся существо
                    {
                        if (
                            !toDelete.Contains(Defender) && //Не атаковать труп
                            Attaker != Defender && //Не атаковать себя
                            Attaker.Player.Name != Defender.Player.Name && //Не атаковать союзника
                            Attaker.GetRectanglePos().Intersects(Defender.GetRectanglePos()) //атакующий и защищающийся рядом
                            )
                        {
                            Attacking = true;
                            
                            Attaker.SpeedX = 0; //Остановка на время сражения
                            if (Attaker.HP > 0 && Defender.HP > 0)
                            {
                                if (Attaker.HitCreature(Defender)) //Если защищающийся убит
                                {
                                    toDelete.Add(Defender); //Удалить убитого
                                }
                            }
                        }
                    }
                }
                
                if (Attacking == false) //Если за ход не атаковал
                {
                    Attaker.SpeedX = 50;
                }
            }
            
            foreach (BaseCreatures Creature in toDelete) //Очистить от убитых
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
        /// <param name="Count">Количество к удалению</param>
        public void DeleteSome(int Count)
        {
            if (mainList.Count > Count * 5)
            {
                for (int i = 0; i < Count; i++)
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
                System.Random Rand = new System.Random();
                for (int i = mainList.Count - 1; i >= 1; i--)
                {
                    int j = Rand.Next(i + 1);
                    var Temp = mainList[j];
                    mainList[j] = mainList[i];
                    mainList[i] = Temp;
                }
            }
        }
        /// <summary>
        /// Восстановить параметры существ
        /// </summary>
        public void ResetStats()
        {
            foreach (BaseCreatures Creature in mainList)
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
            foreach (BaseCreatures Creature in mainList)
            {
                int AttackPower = Creature.Step(); //Движение существа
                
                if (AttackPower > 0) //если достиг края экрана, то атакует на attackPower
                {
                    if (Creature.Player == leftPlayer)
                    {
                        rightPlayer.AttackMe(AttackPower);
                    }
                    else
                    {
                        leftPlayer.AttackMe(AttackPower);
                    }
                    
                    toDelete.Add(Creature); //Удалить существо, достигшее края экрана

                    if (leftPlayer.HP <= 0) //Вернуть погибшего игрока
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
