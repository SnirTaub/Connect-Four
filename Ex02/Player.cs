namespace Ex02
{
    internal class Player
    {
        private int m_Score = 0;
        private char m_Sign;
        private ePlayerType m_PlayerType;

        internal Player(ePlayerType i_Type, char i_Sign)
        {
            m_PlayerType = i_Type;
            m_Sign = i_Sign;
        }

        internal ePlayerType PlayerType
        {
            get
            {
                return m_PlayerType;
            }

            set
            {
                m_PlayerType = value;
            }
        }

        internal char Sign
        {
            get
            {
                return m_Sign;
            }

            set
            {
                m_Sign = value;
            }
        }

        internal int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        internal bool IsHuman()
        {
            return this.m_PlayerType == ePlayerType.Player1 || this.PlayerType == ePlayerType.Player2;
        }

        internal enum ePlayerType
        {
            Player1,
            Player2,
            Pc
        }
    }
}
