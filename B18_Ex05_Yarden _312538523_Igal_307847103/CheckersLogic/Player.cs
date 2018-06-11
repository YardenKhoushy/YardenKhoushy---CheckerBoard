using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CheckersLogic
{
    public class Player
    {
        private string m_Name;
        private string m_PieceType;
        private Pieces m_PlayerPieces;
        private int m_NumOfPieces = 0;
        private int m_PlayerPoints = 0;
        private Dictionary<Point, List<Point>> m_PossibleMoves;
        private Validation m_validator;

        public Player(string i_name, string i_PieceType, int i_sizeOfBoard)
        {
            m_Name = i_name;
            m_PieceType = i_PieceType;
            if (i_sizeOfBoard == 8)
            {
                m_NumOfPieces = 12;
            }
            else
            {
                if (i_sizeOfBoard == 10)
                {
                    m_NumOfPieces = 20;
                }
                else
                {
                    m_NumOfPieces = 6;
                }
            }

            m_PlayerPieces = new Pieces(m_NumOfPieces, m_PieceType, i_sizeOfBoard);
            m_validator = new Validation();      
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
        }

        public string PieceType
        {
            get
            {
                return this.m_PieceType;
            }
        }

        public Pieces GetPieces()
        {
            return this.m_PlayerPieces;
        }

        public int GetNumOfPieces()
        {
            return this.m_NumOfPieces;
        }

        public void CalculatePoints()
        {
            m_PlayerPoints = m_NumOfPieces;
            int pieceArrLength = m_PlayerPieces.GetNumOfPieces;
            Piece currentPiece;
            for (int i = 0; i < pieceArrLength; i++)
            {
                currentPiece = m_PlayerPieces.GetPiece(i);
                if (currentPiece == null)
                {
                    m_PlayerPoints--;
                }
                else
                {
                    if (m_PieceType == "X")
                    {
                        if (currentPiece.Type == "K")
                        {
                            m_PlayerPoints += 4;
                        }
                    }
                    else
                    {
                        if (currentPiece.Type == "U")
                        {
                            m_PlayerPoints += 4;
                        }
                    }
                }
            }
        }

        public int Points
        {
            get
            {
                return this.m_PlayerPoints;
            }

            set
            {
                this.m_PlayerPoints = value;
            }
        }

        public void PlayerPossibleMoves(Board i_CurrentBoard)
        {
            Piece currentPiece;
            m_PossibleMoves = new Dictionary<Point, List<Point>>();
            for(int i = 0; i < m_NumOfPieces; i++)
            {
                currentPiece = m_PlayerPieces.GetPiece(i);
                
                if(currentPiece != null)
                {
                    if (currentPiece.Type == "X")
                    {
                        CheckXOptions(currentPiece, i_CurrentBoard);
                    }
                    else
                    {
                        if(currentPiece.Type == "O")
                        {
                            CheckOptionsForO(currentPiece, i_CurrentBoard);
                        }
                        else
                        {
                            CheckXOptions(currentPiece, i_CurrentBoard);
                            CheckOptionsForO(currentPiece, i_CurrentBoard);
                        }
                    }
                }
            }
        }

        private void CheckXOptions(Piece i_CurrentPiece, Board i_CurrentBoard)
        {
            int nextPositionRow, nextPositionColumn;
            Point target = new Point();
            List<Point> PositionOptions = new List<Point>();
            nextPositionColumn = i_CurrentPiece.Column + 1;
            nextPositionRow = i_CurrentPiece.Row - 1;
            target.X = nextPositionRow;
            target.Y = nextPositionColumn;
            Possibilities(i_CurrentPiece, i_CurrentBoard, i_CurrentPiece.Type, target, PositionOptions);
            nextPositionColumn = i_CurrentPiece.Column + 2;
            nextPositionRow = i_CurrentPiece.Row - 2;
            target.X = nextPositionRow;
            target.Y = nextPositionColumn;
            Possibilities(i_CurrentPiece, i_CurrentBoard, i_CurrentPiece.Type, target, PositionOptions);
            nextPositionColumn = i_CurrentPiece.Column - 1;
            nextPositionRow = i_CurrentPiece.Row - 1;
            target.X = nextPositionRow;
            target.Y = nextPositionColumn;
            Possibilities(i_CurrentPiece, i_CurrentBoard, i_CurrentPiece.Type, target, PositionOptions);
            nextPositionColumn = i_CurrentPiece.Column - 2;
            nextPositionRow = i_CurrentPiece.Row - 2;
            target.X = nextPositionRow;
            target.Y = nextPositionColumn;
            Possibilities(i_CurrentPiece, i_CurrentBoard, i_CurrentPiece.Type, target, PositionOptions);
            if (PositionOptions.Count > 0)
            {
                Point key = new Point(i_CurrentPiece.Row, i_CurrentPiece.Column);
                if (m_PossibleMoves.ContainsKey(key))
                {
                    foreach (Point value in PositionOptions)
                    {
                        m_PossibleMoves[key].Add(value);
                    }
                }
                else
                {
                    m_PossibleMoves.Add(key, PositionOptions);
                }
            }
        }

        private void CheckOptionsForO(Piece i_CurrentPiece, Board i_CurrentBoard)
        {
            int nextPositionRow, nextPositionColumn;
            Point target = new Point();
            List<Point> PositionOptions = new List<Point>();
            nextPositionColumn = i_CurrentPiece.Column - 1;
            nextPositionRow = i_CurrentPiece.Row + 1;
            target.X = nextPositionRow;
            target.Y = nextPositionColumn;
            Possibilities(i_CurrentPiece, i_CurrentBoard, i_CurrentPiece.Type, target, PositionOptions);
            nextPositionColumn = i_CurrentPiece.Column - 2;
            nextPositionRow = i_CurrentPiece.Row + 2;
            target.X = nextPositionRow;
            target.Y = nextPositionColumn;
            Possibilities(i_CurrentPiece, i_CurrentBoard, i_CurrentPiece.Type, target, PositionOptions);
            nextPositionColumn = i_CurrentPiece.Column + 1;
            nextPositionRow = i_CurrentPiece.Row + 1;
            target.X = nextPositionRow;
            target.Y = nextPositionColumn;
            Possibilities(i_CurrentPiece, i_CurrentBoard, i_CurrentPiece.Type, target, PositionOptions);
            nextPositionColumn = i_CurrentPiece.Column + 2;
            nextPositionRow = i_CurrentPiece.Row + 2;
            target.X = nextPositionRow;
            target.Y = nextPositionColumn;
            Possibilities(i_CurrentPiece, i_CurrentBoard, i_CurrentPiece.Type, target, PositionOptions);
            if (PositionOptions.Count > 0)
            {
                Point key = new Point(i_CurrentPiece.Row, i_CurrentPiece.Column);
                if(m_PossibleMoves.ContainsKey(key))
                {
                    foreach(Point value in PositionOptions)
                    {
                        m_PossibleMoves[key].Add(value);
                    }
                }
                else
                {
                    m_PossibleMoves.Add(key, PositionOptions);
                }
            }
        }
        
        private bool Possibilities(Piece i_CurrentPiece, Board i_currentBoard, string i_PieceType, Point i_Target, List<Point> i_PositionOptions)
        {
            Point currentPosition = new Point(i_CurrentPiece.Row, i_CurrentPiece.Column);
            Pieces opponentPieces = i_currentBoard.GetOpponentPieces(i_PieceType);
            bool isValid = m_validator.MoveIsValid(currentPosition, i_Target, i_currentBoard, i_PieceType);
            if(isValid)
            {
                i_PositionOptions.Add(i_Target);
            }

            return isValid;
        }

        public Dictionary<Point, List<Point>> GetPlayerPossibleMoves()
        {
            return this.m_PossibleMoves;
        }
    }
}
