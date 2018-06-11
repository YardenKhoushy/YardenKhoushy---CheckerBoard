using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CheckersLogic
{
    public class Piece
    {
        private int m_row;
        private int m_column;
        private string m_pieceType;
        private Point m_currentPosition;

        public Piece(int i_row, int i_column, string i_pieceType)
        {        
            this.m_row = i_row;
            this.m_column = i_column;
            this.m_pieceType = i_pieceType;
            this.m_currentPosition = new Point(m_row, m_column);
        }

        public void MovePiece(Point i_nextPosition)
        {
            this.m_column = i_nextPosition.Y;
            this.m_row = i_nextPosition.X;
            this.m_currentPosition = i_nextPosition;
        }

        public int Row
        {
            get
            {
                return this.m_row;
            }

            set
            {
                this.m_row = value;
            }
        }

        public int Column
        {
            get
            {
                return this.m_column;
            }

            set
            {
                this.m_column = value;
            }
        }

        public string Type
        {
            get
            {
                return this.m_pieceType;
            }

            set
            {
                this.m_pieceType = value;
            }
        }

        public Point Position
        {
            get
            {
                return this.m_currentPosition;
            }

            set
            {
                this.m_currentPosition = value;
            }
        }
    }
}