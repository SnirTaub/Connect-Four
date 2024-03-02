namespace Ex02
{
    internal class GameLogic
    {
        private Board m_Board;
        private Player m_Player1, m_Player2, m_CurrentPlayer;

        internal GameLogic(int i_Row, int i_Col, eGameMode i_GameMode)
        {
            m_Board = new Board(i_Row, i_Col);
            m_Player1 = new Player(Player.ePlayerType.Player1, 'X');
            
            if (i_GameMode == eGameMode.PlayerVsPc)
            {
                m_Player2 = new Player(Player.ePlayerType.Pc, 'O');
            }
            else
            {
                m_Player2 = new Player(Player.ePlayerType.Player2, 'O');
            }
        }

        internal Player Player1
        {
            get
            {
                return m_Player1;
            }

            set
            {
                m_Player1 = value;
            }
        }

        internal Player Player2
        {
            get
            {
                return m_Player2;
            }

            set
            {
                m_Player2 = value;
            }
        }

        internal Board MBoard
        {
            get
            {
                return this.m_Board;
            }
        }

        internal Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }

            set
            {
                m_CurrentPlayer = value;
            }
        }

        internal void HandleRoundOutcome(eGameStates i_CurrentState, Player i_CurrentPlayer)
        {
            switch (i_CurrentState)
            {
                case eGameStates.Quit:
                    {
                        if (i_CurrentPlayer == m_Player1)
                        {
                            m_Player2.Score++;
                        }
                        else
                        {
                            m_Player1.Score++;
                        }

                        break;
                    }

                case eGameStates.Lose:
                    {
                        i_CurrentPlayer.Score++;
                        break;
                    }
            }
        }

        internal bool IsValidInput(int i_UserColumn)
        {
            return m_Board.IsValidColumn(i_UserColumn);
        }

        internal int GenerateAiMove()
        {
            int bestMove = 0;
            int bestScore = int.MinValue;

            for (int col = 1; col <= this.m_Board.Column; col++)
            {
                if (!this.IsValidInput(col))
                {
                    continue;
                }

                m_Board.AddNextMove(col, this.m_CurrentPlayer.Sign, out int o_Row);
                if (this.m_Board.IsWinningMove(o_Row, col))
                {
                    bestMove = col;
                    this.m_Board.SetCell(o_Row - 1, col - 1, ' ');
                    break;
                }

                int score = this.miniMax(this.m_Board, 5, false, col, o_Row);
                this.m_Board.SetCell(o_Row - 1, col - 1, ' ');
                if (bestScore >= score)
                {
                    continue;
                }

                bestMove = col;
                bestScore = score;
            }

            return bestMove;
        }

        private int miniMax(Board i_GameBoard, int i_Depth, bool i_IsMaximizing, int i_LastCol, int i_LastRow)
        {
            int bestScore;
            bool isPlayerWon = i_GameBoard.IsWinningMove(i_LastRow, i_LastCol);
            bool isDraw = i_GameBoard.IsDraw();
            bool isOver = isDraw || isPlayerWon || i_Depth == 0;

            if (isOver)
            {
                if (isPlayerWon)
                {
                    bestScore = i_IsMaximizing ? -1000000 : 1000000;
                }
                else if (isDraw)
                {
                    bestScore = 0;
                }
                else
                {
                    bestScore = i_GameBoard.ScoreBoard();
                }
            }
            else
            {
                if (i_IsMaximizing)
                {
                    bestScore = int.MinValue;
                    for (int col = 1; col <= i_GameBoard.Column; col++)
                    {
                        if (!IsValidInput(col))
                        {
                            continue;
                        }

                        i_GameBoard.AddNextMove(col, 'O', out int o_Row);
                        int score = this.miniMax(i_GameBoard, i_Depth - 1, false, col, o_Row);
                        i_GameBoard.SetCell(o_Row - 1, col - 1, ' ');
                        bestScore = System.Math.Max(bestScore, score);
                    }
                }
                else
                {
                    bestScore = int.MaxValue;
                    for (int col = 1; col <= i_GameBoard.Column; col++)
                    {
                        if (!IsValidInput(col))
                        {
                            continue;
                        }

                        i_GameBoard.AddNextMove(col, 'X', out int o_Row);
                        int score = this.miniMax(i_GameBoard, i_Depth - 1, true, col, o_Row);
                        i_GameBoard.SetCell(o_Row - 1, col - 1, ' ');
                        bestScore = System.Math.Min(bestScore, score);
                    }
                }
            }

            return bestScore;
        }

        internal void MakeMove(int i_UserColumn, Player i_Player, out int o_InsertedRow)
        {
            m_Board.AddNextMove(i_UserColumn, i_Player.Sign, out o_InsertedRow);
        }

        internal eGameStates GetCurrentGameState( int i_LastInstertedRow, int i_LastInsertedColumn)
        {
            eGameStates stateResult = eGameStates.Continue;

            if (m_Board.IsWinningMove(i_LastInstertedRow, i_LastInsertedColumn))
            {
                stateResult = eGameStates.Lose;
            }
            else if (m_Board.IsDraw())
            {
                stateResult = eGameStates.Draw;
            }

            return stateResult;
        }

        internal void ResetBoard()
        {
            this.m_Board.ClearBoard();
        }

        internal void ShowBoard()
        {
            this.m_Board.PrintBoard();
        }

        internal enum eGameMode
        {
            PlayerVsPlayer = 1,
            PlayerVsPc
        }

        internal enum eGameStates
        {
            Continue,
            Lose,
            GameOver,
            Quit,
            Draw,
            ReTry
        }
    }
}
