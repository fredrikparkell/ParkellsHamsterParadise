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
                UserInterface userInterface = new UserInterface();
                userInterface.RunMainUI();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
