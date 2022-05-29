using System;

namespace WarGame
{
    /// <summary>
    /// Базовый класс приложения
    /// </summary>
    public static class Program
    {
        [STAThread]
        /// <summary>
        /// Точка входа приложения
        /// </summary>
        static void Main()
        {
        using (var game = new GameWindow())
            game.Run();
        }
    }
}
