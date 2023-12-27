using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace module_8_task_3
{
    internal static class Misc
    {
        internal static void DisplayErrorMessages(string message, ConsoleColor backroundColor = ConsoleColor.Red, ConsoleColor foregroundColor = ConsoleColor.Black)
        {
            Console.BackgroundColor = backroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine($"\n{message}");
            Console.ResetColor();
        }
    }
}
