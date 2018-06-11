using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CheckersLogic
{
    public class Board
    {
        private int m_sizeOfBoard;
        private Piece[,] m_Board;
        private Pieces m_FirstPlayersPieces;
        private Pieces m_SecondPlayersPieces;

        public Board(int i_boardSize)
        {
            this.m_sizeOfBoard = i_boardSize;
        }

        public Board(int i_boardSize, Pieces i_FirstPlayersPieces, Pieces i_SecondPlayersPieces)
        {
            this.m_sizeOfBoard = i_boardSize;
            this.m_Board = new Piece[i_boardSize, i_boardSize];
            this.m_FirstPlayersPieces = i_FirstPlayersPieces;
            this.m_SecondPlayersPieces = i_SecondPlayersPieces;
            this.BuildBoard();
        }

        private void BuildBoard()
        {
            this.CheckerBoard(this.m_FirstPlayersPieces);
            this.CheckerBoard(this.m_SecondPlayersPieces);
        }

        public void CheckerBoard(Pieces i_pieces)
        {
            Piece currPiece;
            for (int indexOfPiece = 0; indexOfPiece < i_pieces.GetNumOfPieces; indexOfPiece++)
            {
                currPiece = i_pieces.GetPiece(indexOfPiece);
                if (currPiece != null)
                {
                    this.m_Board[currPiece.Row, currPiece.Column] = currPiece;
                }
            }
        }

        public void MovePieceOnBoard(Point i_startPoint, Point i_TargetPoint, string x)
        {
            int nextRow = i_TargetPoint.X;
            Piece currentPiece;
            if (x == "X" || x == "K")
            {
                int indexOfPiece = this.m_FirstPlayersPieces.GetIndexOfPiece(i_startPoint);
                currentPiece = this.m_FirstPlayersPieces.GetPiece(indexOfPiece);
                currentPiece.MovePiece(i_TargetPoint);
                if (nextRow == 0 && currentPiece.Type == "X")
                {
                    this.ChangeToKing(currentPiece);
                }

                this.m_Board[i_startPoint.X, i_startPoint.Y] = null;
            }
            else
            {
                int indexOfPiece = this.m_SecondPlayersPieces.GetIndexOfPiece(i_startPoint);
                currentPiece = this.m_SecondPlayersPieces.GetPiece(indexOfPiece);
                currentPiece.MovePiece(i_TargetPoint);
                if (nextRow == this.m_sizeOfBoard - 1 && currentPiece.Type == "O")
                {
                    ChangeToKing(currentPiece);
                }

                m_Board[i_startPoint.X, i_startPoint.Y] = null;
            }

            BuildBoard();
        }

        private void ChangeToKing(Piece i_currentPiece)
        {
            if (i_currentPiece.Type == "X")
            {
                i_currentPiece.Type = "K";
            }
            else
            {
                i_currentPiece.Type = "U";
            }
        }

        public int GetsizeOfBoard
        {
            get
            {
                return this.m_sizeOfBoard;
            }
        }

        public Piece[,] GetBoard()
        {
            return this.m_Board;
        }

        public Pieces GetOpponentPieces(string i_PieceType)
        {
            if (i_PieceType == "X")
            {
                return this.m_SecondPlayersPieces;
            }
            else
            {
                return this.m_FirstPlayersPieces;
            }
        }

        public void EatFormBoard(Point Position, string i_Type)
        {
            if (i_Type == "X" || i_Type == "K")
            {
                int indexOfPieceToRemove = m_SecondPlayersPieces.GetIndexOfPiece(Position);
                m_SecondPlayersPieces.SetPiece(indexOfPieceToRemove, null);
            }
            else
            {
                int indexOfPieceToRemove = m_FirstPlayersPieces.GetIndexOfPiece(Position);
                m_FirstPlayersPieces.SetPiece(indexOfPieceToRemove, null);
            }

            m_Board[Position.X, Position.Y] = null;
            BuildBoard();
        }
    }
}
