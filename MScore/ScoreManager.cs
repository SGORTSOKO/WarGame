// ***********************************************************************
// Assembly         : WarGame
// Author           : abros
// Created          : 06-03-2022
//
// Last Modified By : abros
// Last Modified On : 06-03-2022
// ***********************************************************************
// <copyright file="ScoreManager.cs" company="Kurgan State University">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace AKSU.MScore
{
    /// <summary>
    /// Класс, отвечающий за сохранение результатов игры и вывод таблицы рекордов
    /// </summary>
    public class ScoreManager
    {
        /// <summary>
        /// Файл таблицы рекордов
        /// </summary>
        private static string fileName = "scores.xml";
        /// <summary>
        /// Только строки таблицы на вывод
        /// </summary>
        public List<Score> HighScores { get; private set; }
        /// <summary>
        /// Все строки таблицы
        /// </summary>
        public List<Score> Scores { get; private set; }
        /// <summary>
        /// Конструктор класса <see cref="ScoreManager" />.
        /// </summary>
        public ScoreManager()
            : this(new List<Score>())
        { }
        /// <summary>
        /// Конструктор класса <see cref="ScoreManager" />.
        /// </summary>
        /// <param name="inputScores">Таблица лидеров</param>
        public ScoreManager(List<Score> inputScores)
        {
            Scores = inputScores;

            UpdateHighscores();
        }
        /// <summary>
        /// Добавить строку таблицы.
        /// </summary>
        /// <param name="inputScore"> Строка таблицы лидеров </param>
        public void Add(Score inputScore)
        {
            Scores.Add(inputScore);
            Scores = Scores.OrderByDescending(c => c.Value).ToList();
            UpdateHighscores();
        }
        /// <summary>
        /// Загрузить таблицу из файла
        /// </summary>
        /// <returns>Менеджер таблицы</returns>
        public static ScoreManager Load()
        {
            if (!File.Exists(fileName)) // Если файл не существет, то создать пустой менеджер
                return new ScoreManager();

            using (var Reader = new StreamReader(
                new FileStream(fileName, FileMode.Open))) //Прочитать файл
            {
                var Serilizer = new XmlSerializer(typeof(List<Score>)); //Сериализатор записи

                var Scores = (List<Score>)Serilizer.Deserialize(Reader); //Считать данные из формата xml

                return new ScoreManager(Scores);
            }
        }
        /// <summary>
        /// Выделить 25 лучших результатов
        /// </summary>
        public void UpdateHighscores()
        {
            HighScores = Scores.Take(25).ToList();
        }

        /// <summary>
        /// Сохранить все результаты в файл
        /// </summary>
        /// <param name="scoreManager">Менеджер таблицы</param>
        public static void Save(ScoreManager scoreManager)
        {
            using (var Writer = new StreamWriter(
                new FileStream(fileName, FileMode.Create))) // Открыть файл для записи в режиме создания нового файла
            {
                var Serilizer = new XmlSerializer(typeof(List<Score>)); //Сериализатор записи
                
                Serilizer.Serialize(Writer, scoreManager.Scores); //Записать данные в xml формате
            }
        }
    }
}
