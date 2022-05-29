using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace WarGame
{
    public class CreatureList
    {
        private List<Creatures> mainList;
        private List<Creatures> toDelete;
        private static Player leftPlayer;
        private static Player rightPlayer;
        public CreatureList(Player left, Player right) 
        {
            leftPlayer = left;
            rightPlayer = right;
            mainList = new List<Creatures>();
            toDelete = new List<Creatures>();
        }
        public void Clear()
        {
            mainList.Clear();
        }
        public void Add(Creatures input)  //
        {
            if (!mainList.Contains(input))
            {
                mainList.Add(input);
            }
        }

        public int Count
        {
            get => mainList.Count;
        }

        public Creatures this[int index1]
        {
            get => mainList[index1];
            set => mainList[index1] = value;
        }

        public void RandSort()
        {
            if (mainList.Count != 0)
            {
                System.Random rand = new System.Random();
                for (int i = mainList.Count - 1; i >= 1; i--)
                {
                    int j = rand.Next(i + 1);
                    // обменять значения data[j] и data[i]
                    var temp = mainList[j];
                    mainList[j] = mainList[i];
                    mainList[i] = temp;
                }
            }
        }
        public void ResetStats()
        {
            foreach (Creatures Creature in mainList)
            {
                Creature.Stamina++;
            }
        }

        public void DeleteSome(int count)
        {
            if (mainList.Count > count*5)
            {
                for (int i = 0; i < count; i++)
                {
                    mainList.RemoveAt(i);
                } 
            }
        }
        public Player StepAll()
        {
            foreach (Creatures Creature in mainList)
            {
                int attackPower = Creature.Step();
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
        public void AttackRound()
        {
            foreach (Creatures Attaker in mainList)
            {
                bool attacking = false;
                if (!toDelete.Contains(Attaker))
                {
                    foreach (Creatures Defender in mainList)
                    {
                        if (!toDelete.Contains(Defender) && Attaker != Defender && Attaker.Player.Name != Defender.Player.Name && Attaker.GetRectanglePos().Intersects(Defender.GetRectanglePos()))
                        {
                            attacking = true;
                            Attaker.SpeedX = 0;
                            if (Attaker.HP > 0 && Defender.HP > 0)
                            {
                                if (Attaker.HitCreature(Defender))
                                {
                                    toDelete.Add(Defender);
                                }
                            }
                        }
                    }
                }
                if (attacking == false)
                {
                    Attaker.SpeedX = 50;
                }
            }
            foreach (Creatures Creature in toDelete)
            {
                mainList.Remove(Creature);
            }
            toDelete.Clear();
        }
    }
}
