using System;

namespace Ex02
{
    internal class UserMessage
    {
        public static void BoardSizeInput()
        {
            string userMessage = string.Format(@"Please enter a desired board size ( rows and cols between 4 - 8 ): ");
            Console.WriteLine(userMessage);
        }

        public static void RowsInput()
        {
            string userMessage = string.Format(@"Rows: ");
            Console.WriteLine(userMessage);
        }

        public static void ColsInput()
        {
            string userMessage = string.Format(@"Cols: ");
            Console.WriteLine(userMessage);
        }

        public static void BoardSizeFailureInput()
        {
            string userMessage = string.Format(@"Please enter a number between 4 - 8 (and then press 'enter'): ");
            Console.WriteLine(userMessage);
        }

        public static void GameModeInput()
        {
            string userMessage = string.Format(@"Please choose game mode:
1.Player vs Player
2.Player vs Computer");

            Console.WriteLine(userMessage);
        }

        public static string GetUserColumnInputOrQuit()
        {
            Console.WriteLine("Please enter column number or Q for quit this round: ");
            string userInput = Console.ReadLine();
            return userInput;
        }

        public static void AnotherRoundInput()
        {
            string userMessage = string.Format(@"Do you want to continue playing?
1. Yes
2. No");
            Console.WriteLine(userMessage);
        }

        public static void InvalidGameModeInput()
        {
            Console.WriteLine("invalid input for game mode, please try again:");
        }

        public static void InvalidColumnInput()
        {
            Console.WriteLine("The column you entered is not valid!");
        }

        public static void InvalidInput()
        {
            Console.WriteLine("Invalid input, try again");
        }

        public static void InvalidChoice()
        {
            Console.WriteLine("invalid choice, Please try again");
        }
    }
}
