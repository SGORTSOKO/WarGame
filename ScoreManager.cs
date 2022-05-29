﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WarGame
{
    public class ScoreManager
    {
        private static string filename = "scores.xml"; // Since we don't give a path, this'll be saved in the "bin" folder
        public List<Score> Highscores { get; private set; }
        public List<Score> Scores { get; private set; }
        public ScoreManager()
            : this(new List<Score>())
        {

        }
        public ScoreManager(List<Score> inputScores)
        {
            Scores = inputScores;

            UpdateHighscores();
        }
        public void Add(Score inputScore)
        {
            Scores.Add(inputScore);
            Scores = Scores.OrderByDescending(c => c.Value).ToList();
            UpdateHighscores();
        }
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
        public void UpdateHighscores()
        {
            Highscores = Scores.Take(5).ToList(); // Takes the first 5 elements
        }

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
