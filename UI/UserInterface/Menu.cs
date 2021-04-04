using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class Menu
    {
        private int selectedIndex;
        private string[] options;
        private string title;

        public Menu(string headTitle, string[] menuOptions)
        {
            title = headTitle;
            options = menuOptions;
            selectedIndex = 0;
        }
        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.CursorLeft = 0; Console.CursorTop = 0;
                WriteOutMenu();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                    if (selectedIndex == -1)
                    {
                        selectedIndex = options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    selectedIndex++;
                    if (selectedIndex == options.Length)
                    {
                        selectedIndex = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);

            return selectedIndex;
        }
        private void WriteOutMenu()
        {
            Console.CursorVisible = false;
            Console.WriteLine(title);

            for (int i = 0; i < options.Length; i++)
            {
                string currentOption = options[i];
                string prefix;
                string suffix;

                if (i == selectedIndex)
                {
                    prefix = " =>";
                    suffix = "";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = "  ";
                    suffix = " ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                var currPosition = Console.GetCursorPosition();
                Console.SetCursorPosition(3, currPosition.Top + 1); // currPosition.Left
                Console.Write($"{prefix} << {currentOption} >>{suffix}");
            }
            Console.ResetColor();
        }
    }
}
