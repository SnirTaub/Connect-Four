using System;
using Ex02.ConsoleUtils;
using System.Threading;

namespace Ex02
{
    internal class GameManager
    {
        internal void InitializeGame()
        {
            getBoardSize(out int rows, out int cols);
            GameLogic game = new GameLogic(rows, cols, getGameMode());
            bool isGameOver = false;

            while (!isGameOver)
            {
                gameRound(ref isGameOver, game);
                game.ResetBoard();
            }

            Screen.Clear();
            gameOver(game);
            Console.WriteLine("Press any key to exit . . .");
            Console.ReadLine();
        }

        private void gameRound(ref bool io_GameOver, GameLogic i_Game)
        {
            GameLogic.eGameStates currentState = GameLogic.eGameStates.Continue;

            i_Game.CurrentPlayer = i_Game.Player1;
            while (!io_GameOver)
            {
                int nextMove;

                Screen.Clear();
                i_Game.ShowBoard();
                showTurn(i_Game.CurrentPlayer);
                if (i_Game.CurrentPlayer.IsHuman())
                {
                    nextMove = getUserMove(out bool isQuit, i_Game.MBoard);
                    if (!isQuit)
                    {
                        i_Game.MakeMove(nextMove, i_Game.CurrentPlayer, out int o_RowInserted);
                        currentState = i_Game.GetCurrentGameState(o_RowInserted, nextMove);
                    }
                    else
                    {
                        currentState = GameLogic.eGameStates.Quit;
                    }
                }
                else
                {
                    nextMove = i_Game.GenerateAiMove();
                    i_Game.MakeMove(nextMove, i_Game.CurrentPlayer, out int o_RowInserted);
                    currentState = i_Game.GetCurrentGameState(o_RowInserted, nextMove);
                }

                io_GameOver = currentState == GameLogic.eGameStates.Draw
                              || currentState == GameLogic.eGameStates.Lose
                              || currentState == GameLogic.eGameStates.Quit;

                if (!io_GameOver)
                {
                    i_Game.CurrentPlayer = i_Game.CurrentPlayer == i_Game.Player1 ? i_Game.Player2 : i_Game.Player1;
                }
            }

            Screen.Clear();
            i_Game.ShowBoard();
            endOfRound(ref currentState, i_Game);
            io_GameOver = currentState != GameLogic.eGameStates.ReTry;
        }

        private void getBoardSize(out int o_Rows, out int o_Cols)
        {
            UserMessage.BoardSizeInput();
            UserMessage.RowsInput();
            InputValidation.RowsIsNumber(out o_Rows);
            UserMessage.ColsInput();
            InputValidation.ColsIsNumber(out o_Cols);
        }

        private GameLogic.eGameMode getGameMode()
        {
            UserMessage.GameModeInput();
            InputValidation.IsValidGameMode(out int userChoice);
            return (GameLogic.eGameMode)userChoice;
        }

        private int getUserMove(out bool o_QisEntered, Board i_GameBoard)
        {
            int columnFromUser;
            o_QisEntered = false;
            string userInput = UserMessage.GetUserColumnInputOrQuit();
            InputValidation.GetValidColumnFromUserInput(userInput, out columnFromUser, i_GameBoard);

            if (userInput == "Q")
            {
                o_QisEntered = true;
            }

            return columnFromUser;
        }

        private void endOfRound(ref GameLogic.eGameStates i_CurrentState, GameLogic i_Game)
        {
            i_Game.HandleRoundOutcome(i_CurrentState, i_Game.CurrentPlayer);
            switch (i_CurrentState)
            {
                case GameLogic.eGameStates.Quit:
                    {
                        quit(i_Game.CurrentPlayer.PlayerType);
                        break;
                    }

                case GameLogic.eGameStates.Draw:
                    {
                        draw();
                        break;
                    }

                case GameLogic.eGameStates.Lose:
                    {
                        lose(i_Game.CurrentPlayer.PlayerType);
                        break;
                    }
            }

            showScore(i_Game.Player1, i_Game.Player2);
            i_CurrentState = anotherRoundQuery() ? GameLogic.eGameStates.ReTry : GameLogic.eGameStates.GameOver;
        }

        private void lose(Player.ePlayerType i_WinnerPlayerType)
        {
            Console.WriteLine(i_WinnerPlayerType + " win!");
        }

        private void draw()
        {
            Console.WriteLine("Its a Draw!");
        }

        private void quit(Player.ePlayerType i_QuitPlayerType)
        {
            Console.WriteLine(i_QuitPlayerType + " quit!");
        }

        private bool anotherRoundQuery()
        {
            int userChoice;
            UserMessage.AnotherRoundInput();
            InputValidation.IsValidUserChoice(out userChoice);
            return userChoice == 1;
        }

        private void showScore(Player i_Player1, Player i_Player2)
        {
            Console.WriteLine(
                @"Score:
=======
{0} - {1}
{2} - {3}",
                i_Player1.PlayerType,
                i_Player1.Score,
                i_Player2.PlayerType,
                i_Player2.Score);
        }

        private void showTurn(Player i_CurrentPlayer)
        {
            switch (i_CurrentPlayer.PlayerType)
            {
                case Player.ePlayerType.Player1:
                    {
                        Console.WriteLine("Player 1 turn");
                        break;
                    }

                case Player.ePlayerType.Player2:
                    {
                        Console.WriteLine("Player 2 turn");
                        break;
                    }

                case Player.ePlayerType.Pc:
                    {
                        Console.WriteLine("Computer  turn:");
                        Console.WriteLine("Computer thinking...");
                        Thread.Sleep(400);
                        break;
                    }
            }
        }

        private void gameOver(GameLogic i_Game)
        {
            Player winnerPlayer = i_Game.Player1.Score > i_Game.Player2.Score ? i_Game.Player1 : i_Game.Player2;
            Player loserPlayer = winnerPlayer == i_Game.Player1 ? i_Game.Player2 : i_Game.Player1;

            Console.WriteLine("Game Over!");
            if (i_Game.Player1.Score == i_Game.Player2.Score)
            {
                Console.WriteLine("It's a Draw!");
            }
            else
            {
                Console.WriteLine(
                @"The Winner is:
{0} with  {1} points
{2} Good luck next time!",
                    winnerPlayer.PlayerType,
                    winnerPlayer.Score,
                    loserPlayer.PlayerType);
            }
        }

    }
}
