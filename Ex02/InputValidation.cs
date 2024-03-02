using System;

namespace Ex02
{
    internal class InputValidation
    {
        internal static void RowsIsNumber(out int o_Rows)
        {
            bool rowsIsNumber = int.TryParse(Console.ReadLine(), out o_Rows);
            while (rowsIsNumber == false || Board.IsValidSize(o_Rows) == false)
            {
                UserMessage.BoardSizeFailureInput();
                rowsIsNumber = int.TryParse(Console.ReadLine(), out o_Rows);
            }
        }

        internal static void ColsIsNumber(out int o_Cols)
        {
            bool rowsIsNumber = int.TryParse(Console.ReadLine(), out o_Cols);
            while (rowsIsNumber == false || Board.IsValidSize(o_Cols) == false)
            {
                UserMessage.BoardSizeFailureInput();
                rowsIsNumber = int.TryParse(Console.ReadLine(), out o_Cols);
            }
        }

        internal static void IsValidGameMode(out int userChoice)
        {
            bool isValidInput = int.TryParse(Console.ReadLine(), out userChoice);
            while (isValidInput == false || (userChoice != 1 && userChoice != 2))
            {
                UserMessage.InvalidGameModeInput();
                isValidInput = int.TryParse(Console.ReadLine(), out userChoice);
            }
        }

        internal static void GetValidColumnFromUserInput(string i_UserInput, out int o_ColumnFromUser, Board i_GameBoard)
            {
                while ((i_UserInput != "Q" && !int.TryParse(i_UserInput, out o_ColumnFromUser)) ||
                  (int.TryParse(i_UserInput, out o_ColumnFromUser) && !i_GameBoard.IsValidColumn(o_ColumnFromUser)))
                {
                    if (int.TryParse(i_UserInput, out o_ColumnFromUser) && !i_GameBoard.IsValidColumn(o_ColumnFromUser))
                    {
                        UserMessage.InvalidColumnInput();
                    }
                    else
                    {
                        UserMessage.InvalidInput();
                    }

                    i_UserInput = Console.ReadLine();
                }
            }

        internal static void IsValidUserChoice(out int o_UserChoice)
        {
            while (!int.TryParse(Console.ReadLine(), out o_UserChoice) || (o_UserChoice < 1 || o_UserChoice > 2))
            {
                UserMessage.InvalidChoice();
            }
        }
    }
}
