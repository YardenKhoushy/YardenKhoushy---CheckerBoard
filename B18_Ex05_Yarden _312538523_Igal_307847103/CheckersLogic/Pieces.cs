using System;
using System.Drawing;

namespace CheckersLogic
{
    public class Pieces
    {
        private Piece[] m_Pieces;
        private int m_numOfPieces = 0;
        private int m_boardSize;

        public Pieces(int i_numOfPieces, string i_pieceType, int i_boardSize)
        {
            m_numOfPieces = i_numOfPieces;
            this.m_Pieces = new Piece[i_numOfPieces];
            this.m_boardSize = i_boardSize;
            PieceDecision(i_pieceType);
        }

        private void PieceDecision(string i_pieceType)
        {
            if(i_pieceType == "X")
            {
                BuildPiecesPositionX(i_pieceType);
            }
            else
            {
                BuildPiecesPositionO(i_pieceType);
            }
        }

        private void BuildPiecesPositionX(string i_pieceType)
        {
            int numOfRows = (m_boardSize - 2) / 2;
            int index = 0;
            int row = numOfRows + 2;

            for(int i = m_boardSize - numOfRows; i < m_boardSize; i++)
            {
                int column = 0;
                for (int j = 0; j < m_boardSize; j++)
                {
                    if(i % 2 != j % 2)
                    {
                        m_Pieces[index] = new Piece(row, column, i_pieceType);
                        index++;
                    }

                    column++;
                }

                row++;
            }            
        }

        private void BuildPiecesPositionO(string i_pieceType)
        {
            int numOfRows = (m_boardSize - 2) / 2;
            int index = 0;

            for(int row = 0; row < numOfRows; row++)
            {
                for (int column = 0; column < m_boardSize; column++)
                {
                    if(row % 2 != column % 2)
                    {
                        m_Pieces[index] = new Piece(row, column, i_pieceType);
                        index++;
                    }
                }
            }
        }

        public Piece[] GetPieces()
        {
            return this.m_Pieces;
        }

        public int GetNumOfPieces
        {
            get
            {
                return m_Pieces.Length;
            }

            set
            {
                this.GetNumOfPieces = value;
            }
        }

        public Piece GetPiece(int i_PieceIndex)
        {
            return this.m_Pieces[i_PieceIndex];
        }

        public void SetPiece(int i_PieceIndex, Piece i_piece)
        {
            this.m_Pieces[i_PieceIndex] = i_piece;
        }

        public int GetIndexOfPiece(Point i_Position)
        {
            int pieceIndex = -1;
            for(int i = 0; i < m_Pieces.Length; i++)
            {
                Piece currentPiece = this.GetPiece(i);
                if (currentPiece != null)
                {
                    if (currentPiece.Position == i_Position)
                    {
                        pieceIndex = i;
                        break;
                    }
                }
            }

            return pieceIndex;
        }
    }
}