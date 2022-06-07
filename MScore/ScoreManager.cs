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
        private static string Filename = "scores.xml";
        /// <summary>
        /// Только строки таблицы на вывод
        /// </summary>
        public List<Score> Highscores { get; private set; }
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
        public ScoreManager(List<Score> InputScores)
        {
            Scores = InputScores;

            UpdateHighscores();
        }
        /// <summary>
        /// Добавить строку таблицы.
        /// </summary>
        /// <param name="InputScore"> Строка таблицы лидеров </param>
        public void Add(Score InputScore)
        {
            Scores.Add(InputScore);
            Scores = Scores.OrderByDescending(c => c.Value).ToList();
            UpdateHighscores();
        }
        /// <summary>
        /// Загрузить таблицу из файла
        /// </summary>
        /// <returns>Менеджер таблицы</returns>
        public static ScoreManager Load()
        {
            // Если файл не существет, то создать пустой менеджер
            if (!File.Exists(Filename))
                return new ScoreManager();

            //Прочитать файл
            using (var Reader = new StreamReader(new FileStream(Filename, FileMode.Open)))
            {
                //Сериализатор записи
                var Serilizer = new XmlSerializer(typeof(List<Score>));
                //Считать данные из формата xml
                var Scores = (List<Score>)Serilizer.Deserialize(Reader);

                return new ScoreManager(Scores);
            }
        }
        /// <summary>
        /// Выделить 25 лучших результатов
        /// </summary>
        public void UpdateHighscores()
        {
            Highscores = Scores.Take(25).ToList();
        }

        /// <summary>
        /// Сохранить все результаты в файл
        /// </summary>
        /// <param name="ScoreManager">Менеджер таблицы</param>
        public static void Save(ScoreManager ScoreManager)
        {
            // Открыть файл для записи в режиме создания нового файла
            using (var Writer = new StreamWriter(new FileStream(Filename, FileMode.Create)))
            {
                //Сериализатор записи
                var Serilizer = new XmlSerializer(typeof(List<Score>));
                //Записать данные в xml формате
                Serilizer.Serialize(Writer, ScoreManager.Scores);
            }
        }
    }
}
