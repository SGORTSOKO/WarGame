using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace WarGame
{
    /// <summary>
    /// Class CreatureList.
    /// </summary>
    public class CreatureList
    {
        #region Fields
        /// <summary>
        /// The main list
        /// </summary>
        private List<Creatures> mainList;
        /// <summary>
        /// To delete
        /// </summary>
        private List<Creatures> toDelete;
        /// <summary>
        /// The left player
        /// </summary>
        private static Player leftPlayer;
        /// <summary>
        /// The right player
        /// </summary>
        private static Player rightPlayer;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatureList"/> class.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
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
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            mainList.Clear();
        }
        /// <summary>
        /// Adds the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public void Add(Creatures input)  //
        {
            if (!mainList.Contains(input))
            {
                mainList.Add(input);
            }
        }

        /// <summary>
        /// Adds the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="inputPlayer">The input player.</param>
        /// <param name="inputNowPositionX">The input now position x.</param>
        /// <param name="inputTexture">The input texture.</param>
        public void Add(string input, Player inputPlayer, int inputNowPositionX, Texture2D inputTexture)  //
        {
            if (input == "Human")
            {
                mainList.Add(
                    new Human(
                        inputTexture,
                        inputPlayer,
                        inputNowPositionX
                        ));
            }
        }
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get => mainList.Count;
        }

        /// <summary>
        /// Gets or sets the <see cref="Creatures"/> with the specified index1.
        /// </summary>
        /// <param name="index1">The index1.</param>
        /// <returns>Creatures.</returns>
        public Creatures this[int index1]
        {
            get => mainList[index1];
            set => mainList[index1] = value;
        }

        /// <summary>
        /// Rands the sort.
        /// </summary>
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
        /// <summary>
        /// Resets the stats.
        /// </summary>
        public void ResetStats()
        {
            foreach (Creatures Creature in mainList)
            {
                Creature.Stamina++;
            }
        }

        /// <summary>
        /// Deletes some.
        /// </summary>
        /// <param name="count">The count.</param>
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
        /// <summary>
        /// Steps all.
        /// </summary>
        /// <returns>Player.</returns>
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
        /// <summary>
        /// Attacks the round.
        /// </summary>
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
        #endregion
    }
}
