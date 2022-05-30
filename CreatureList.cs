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
        /// The main list of creatures
        /// </summary>
        private List<Creatures> mainList;
        /// <summary>
        /// List To delete creatures from mainList
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
        /// <param name="left">The left (current) player</param>
        /// <param name="right">The right player</param>
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
        /// Clears list of cretures (that delete them all).
        /// </summary>
        public void Clear()
        {
            mainList.Clear();
        }
        /// <summary>
        /// Adds the creature in List (initialize).
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
        /// Adds the creature in List (initialize).
        /// </summary>
        /// <param name="input">The new creature</param>
        /// <param name="inputPlayer">The creature player.</param>
        /// <param name="inputNowPositionY">The creature input now position y.</param>
        /// <param name="inputTexture">The input creature texture.</param>
        public void Add(string input, Player inputPlayer, int inputNowPositionY, Texture2D inputTexture)  //
        {
            if (input == "Human")
            {
                mainList.Add(
                    new Human(
                        inputTexture,
                        inputPlayer,
                        inputNowPositionY
                        ));
            }
        }
        /// <summary>
        /// Gets the count of List.
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
        /// List random sort
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
        /// Resets the stats of creatures (now is only stamina).
        /// </summary>
        public void ResetStats()
        {
            foreach (Creatures Creature in mainList)
            {
                Creature.Stamina++;
            }
        }

        /// <summary>
        /// Deletes some creatures.
        /// </summary>
        /// <param name="count">The count of creatures to delete</param>
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
        /// Move all creatures and try to attack another player
        /// </summary>
        /// <returns><c>defeated player</c> if defeated player now is dead, <c>false</c> otherwise</returns>
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
        /// Attacks nearby creatures
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
