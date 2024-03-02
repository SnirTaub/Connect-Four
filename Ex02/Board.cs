using System;

namespace Ex02
{
    internal class Board
    {
        private char[,] m_Board = null;
        private readonly int r_RowSize, r_ColSize;

        internal Board(int i_Rows, int i_Cols)
        {
            r_RowSize = i_Rows;
            r_ColSize = i_Cols;
            m_Board = new char[i_Rows, i_Cols];
            ClearBoard();
        }

        internal int Column
        {
            get
            {
                return r_ColSize;
            }
        }

        internal int Row
        {
            get
            {
                return r_RowSize;
            }
        }

        internal static bool IsValidSize(int i_Size)
        {
            return i_Size >= 4 && i_Size <= 8;
        }

        internal void ClearBoard()
        {
            for (int i = 0; i < r_RowSize; i++)
            {
                for (int j = 0; j < r_ColSize; j++)
                {
                    m_Board[i, j] = ' ';
                }
            }
        }

        internal void PrintBoard()
        {
            for (int row = 0; row <= r_RowSize; row++)
            {
                for (int col = 0; col < r_ColSize; col++)
                {
                    if (row == 0)
                    {
                        Console.Write("  {0} ", col + 1);
                    }
                    else
                    {
                        Console.Write("| {0} ", GetCellValue(row - 1, col));
                        if (col == this.r_ColSize - 1)
                        {
                            Console.Write("|");
                        }
                    }
                }

                Console.WriteLine();
                if (row == 0)
                {
                    continue;
                }

                string seperatedLine = "====";
                for (int i = 0; i < this.r_ColSize; i++)
                {
                    Console.Write(seperatedLine);
                }

                Console.WriteLine("=");
            }
        }

        internal char GetCellValue(int i_Row, int i_Col)
        {
            return m_Board[i_Row, i_Col];
        }

        internal void AddNextMove(int i_Col, char i_Sign, out int o_Row)
        {
            int row = r_RowSize;
            bool setDone = false;

            while (row > 0 && !setDone)
            {
                if (m_Board[row - 1, i_Col - 1] != ' ')
                {
                    row--;
                }
                else
                {
                    SetCell(row - 1, i_Col - 1, i_Sign);
                    setDone = true;
                }
            }

            o_Row = row;
        }

        internal void SetCell(int i_Row, int i_Col, char i_Sign)
        {
            this.m_Board[i_Row, i_Col] = i_Sign;
        }

        internal bool IsDraw()
        {
            bool isDraw = true;

            for (int i = 0; i < r_RowSize; i++)
            {
                for (int j = 0; j < r_ColSize; j++)
                {
                    if (this.m_Board[i, j] == ' ')
                    {
                        isDraw = false;
                    }
                }
            }

            return isDraw;
        }

        internal bool IsWinningMove(int i_Row, int i_Col)
        {
            return isPartOfVerticalWin(i_Row, i_Col) || isPartOfHorizontalWin(i_Row, i_Col)
                || isPartOfDiagonalRightWin(i_Row, i_Col) || isPartOfDiagonalLeftWin(i_Row, i_Col);
        }

        private bool isPartOfHorizontalWin(int i_Row, int i_Col)
        {
            char sign = m_Board[i_Row - 1, i_Col - 1];
            int countSameSign = 1;

            for (int i = i_Col - 1; i > 0; i--)
            {
                if(m_Board[i_Row - 1, i - 1] != sign)
                {
                    break;
                }

                countSameSign++;
            }

            for (int i = i_Col + 1; i <= r_ColSize; i++)
            {
                if (m_Board[i_Row - 1, i - 1] != sign)
                {
                    break;
                }

                countSameSign++;
            }

            return countSameSign >= 4;
        }

        private bool isPartOfVerticalWin(int i_Row, int i_Col)
        {
            char sign = m_Board[i_Row - 1, i_Col - 1];
            int countSameSign = 1;

            for (int i = i_Row - 1; i > 0; i--)
            {
                if (this.m_Board[i - 1, i_Col - 1] != sign)
                {
                    break;
                }

                countSameSign++;
            }

            for (int i = i_Row + 1; i <= r_RowSize; i++)
            {
                if (this.m_Board[i - 1, i_Col - 1] != sign)
                {
                    break;
                }

                countSameSign++;
            }

            return countSameSign >= 4;
        }

        private bool isPartOfDiagonalRightWin(int i_Row, int i_Col)
        {
            char sign = m_Board[i_Row - 1, i_Col - 1];
            int countSameSign = 1;

            for (int i = i_Row - 1, j = i_Col + 1; i > 0 && j <= r_ColSize; i--, j++)
            {
                if (m_Board[i - 1, j - 1] != sign)
                {
                    break;
                }

                countSameSign++;
            }

            for (int i = i_Row + 1, j = i_Col - 1; i <= r_RowSize && j > 0; i++, j--)
            {
                if (m_Board[i - 1, j - 1] != sign)
                {
                    break;
                }

                countSameSign++;
            }

            return countSameSign >= 4;
        }

        private bool isPartOfDiagonalLeftWin(int i_Row, int i_Col)
        {
            char sign = this.m_Board[i_Row - 1, i_Col - 1];
            int countSameSign = 1;

            for (int i = i_Row - 1, j = i_Col - 1; i > 0 && j > 0; i--, j--)
            {
                if (m_Board[i - 1, j - 1] != sign)
                {
                    break;
                }

                countSameSign++;
            }

            for (int i = i_Row + 1, j = i_Col + 1; i <= r_RowSize && j <= r_ColSize; i++, j++)
            {
                if (m_Board[i - 1, j - 1] != sign)
                {
                    break;
                }

                countSameSign++;
            }

            return countSameSign >= 4;
        }


        internal int ScoreBoard()
        {
            int score = 0;

            for (int i = 0; i<r_RowSize; i++)
            {
                for (int j = 0; j < r_ColSize - 3; j++)
                {
                    char[] sequence = new char[4]
                                        {
                                            this.m_Board[i, j], this.m_Board[i, j + 1], this.m_Board[i, j + 2],
                                            this.m_Board[i, j + 3]
                                        };
                    score += getScoreSequence(sequence);
                }
            }

            for (int i = 0; i < r_RowSize - 3; i++)
            {
                for (int j = 0; j < r_ColSize - 3; j++)
                {
                    char[] seq = new char[4]
                                     {
                                         this.m_Board[i, j], this.m_Board[i + 1, j + 1], this.m_Board[i + 2, j + 2],
                                         this.m_Board[i + 3, j + 3]
                                     };
                    score += getScoreSequence(seq);
                }
            }

            for (int i = r_ColSize - 1; i >= 3; i--)
            {
                for (int j = 0; j < r_RowSize - 3; j++)
                {
                    char[] seq = new char[4]
                                     {
                                         this.m_Board[j, i], this.m_Board[j + 1, i - 1], this.m_Board[j + 2, i - 2],
                                         this.m_Board[j + 3, i - 3]
                                     };
                    score += getScoreSequence(seq);
                }
            }

            for (int i = 0; i < r_RowSize - 3; i++)
            {
                for (int j = 0; j < r_ColSize - 3; j++)
                {
                    char[] seq = new char[4]
                                     {
                                         this.m_Board[i, j], this.m_Board[i + 1, j + 1], this.m_Board[i + 2, j + 2],
                                         this.m_Board[i + 3, j + 3]
                                     };
                    score += getScoreSequence(seq);
                }
            }

            return score;
        }

        private int getScoreSequence(char[] i_SequenceArray)
        {
            int resultScore = 0;
            int empty = 0;
            int player = 0;
            int pc = 0;

            foreach(char value in i_SequenceArray)
            {
                switch (value)
                {
                    case '0':
                        pc++;
                        break;
                    case 'X':
                        player++;
                        break;
                    default:
                        empty++;
                        break;
                }
            }

            if (pc == 3 && empty == 1)
            {
                resultScore = 100;
            }
            else if (player == 3 && empty == 1)
            {
                resultScore = -200;
            }

            else if (pc == 2 && empty == 2)
            {
                resultScore = 50;
            }

            else if (player == 2 && empty == 2)
            {
                resultScore = -50;
            }

            return resultScore;
        }

        internal bool IsValidColumn(int i_Col)
        {
            return i_Col >= 1 && i_Col <= this.r_ColSize && m_Board[0, i_Col - 1] == ' ';
        }
    }
}
