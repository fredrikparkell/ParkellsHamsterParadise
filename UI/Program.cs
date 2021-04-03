using System;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.SetWindowSize(180, 50);
                UserInterface.RunMainUI();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
