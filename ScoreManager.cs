﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WarGame
{
    /// <summary>
    /// Class ScoreManager.
    /// </summary>
    public class ScoreManager
    {
        /// <summary>
        /// The filename
        /// </summary>
        private static string filename = "scores.xml"; // Since we don't give a path, this'll be saved in the "bin" folder
        /// <summary>
        /// Gets the highscores.
        /// </summary>
        /// <value>The highscores.</value>
        public List<Score> Highscores { get; private set; }
        /// <summary>
        /// Gets the scores.
        /// </summary>
        /// <value>The scores.</value>
        public List<Score> Scores { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreManager"/> class.
        /// </summary>
        public ScoreManager()
            : this(new List<Score>())
        {
            
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreManager"/> class.
        /// </summary>
        /// <param name="inputScores">The input scores.</param>
        public ScoreManager(List<Score> inputScores)
        {
            Scores = inputScores;

            UpdateHighscores();
        }
        /// <summary>
        /// Adds the specified input score.
        /// </summary>
        /// <param name="inputScore">The input score.</param>
        public void Add(Score inputScore)
        {
            Scores.Add(inputScore);
            Scores = Scores.OrderByDescending(c => c.Value).ToList();
            UpdateHighscores();
        }
        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns>ScoreManager.</returns>
        public static ScoreManager Load()
        {
            // If there isn't a file to load - create a new instance of "ScoreManager"
            if (!File.Exists(filename))
                return new ScoreManager();
            // Otherwise we load the file
            using (var reader = new StreamReader(new FileStream(filename, FileMode.Open)))
            {
                var serilizer = new XmlSerializer(typeof(List<Score>));
                var scores = (List<Score>)serilizer.Deserialize(reader);
                return new ScoreManager(scores);
            }
        }
        /// <summary>
        /// Updates the highscores.
        /// </summary>
        public void UpdateHighscores()
        {
            Highscores = Scores.Take(5).ToList(); // Takes the first 5 elements
        }

        /// <summary>
        /// Saves the specified score manager.
        /// </summary>
        /// <param name="scoreManager">The score manager.</param>
        public static void Save(ScoreManager scoreManager)
        {
            // Overrides the file if it alreadt exists
            using (var writer = new StreamWriter(new FileStream(filename, FileMode.Create)))
            {
                var serilizer = new XmlSerializer(typeof(List<Score>));

                serilizer.Serialize(writer, scoreManager.Scores);
            }
        }
    }
}
