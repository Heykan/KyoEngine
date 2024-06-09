using System;
using Microsoft.Xna.Framework;

namespace Kyo.Models.Pieces;

public class Queen : Piece
{
    public Queen(Sprite2D sprite, Vector2 position, ChessColor color, Board board) : base(sprite, position, color, board)
    {
        ChessPiece = ChessPiece.Queen;
    }

    public override void CalculateLegalMoves()
    {
        LegalMoves.Clear();

        // Haut
        for (int i = (int)Position.Y - 1; i >= 0; i--)
        {
            if (board.IsLegalMove(this, (int)Position.X, i))
            {
                AddLegalMove((int)Position.X, i);
            }
            if (!board.IsEmpty((int)Position.X, i)) break;
        }

        // Gauche
        for (int i = (int)Position.X - 1; i >= 0; i--)
        {
            if (board.IsLegalMove(this, i, (int)Position.Y))
            {
                AddLegalMove(i, (int)Position.Y);
            }
            if (!board.IsEmpty(i, (int)Position.Y)) break;
        }

        // Bas
        for (int i = (int)Position.Y + 1; i < 8; i++)
        {
            if (board.IsLegalMove(this, (int)Position.X, i))
            {
                AddLegalMove((int)Position.X, i);
            }
            if (!board.IsEmpty((int)Position.X, i)) break;
        }

        // Droite
        for (int i = (int)Position.X + 1; i < 8; i++)
        {
            if (board.IsLegalMove(this, i, (int)Position.Y))
            {
                AddLegalMove(i, (int)Position.Y);
            }
            if (!board.IsEmpty(i, (int)Position.Y)) break;
        }

        // Diagonale Haut-Gauche
        for (int i = 1; i <= Math.Min((int)Position.X, (int)Position.Y); i++)
        {
            if (board.IsLegalMove(this, (int)Position.X - i, (int)Position.Y - i))
            {
                AddLegalMove((int)Position.X - i, (int)Position.Y - i);
            }
            if (!board.IsEmpty((int)Position.X - i, (int)Position.Y - i)) break;
        }
        // Diagonale Bas-Droite
        for (int i = 1; i <= Math.Min(7 - (int)Position.X, 7 - (int)Position.Y); i++)
        {
            if (board.IsLegalMove(this, (int)Position.X + i, (int)Position.Y + i))
            {
                AddLegalMove((int)Position.X + i, (int)Position.Y + i);
            }
            if (!board.IsEmpty((int)Position.X + i, (int)Position.Y + i)) break;
        }
        // Diagonale Haut-Droite
        for (int i = 1; i <= Math.Min((int)Position.X, 7 - (int)Position.Y); i++)
        {
            if (board.IsLegalMove(this, (int)Position.X - i, (int)Position.Y + i))
            {
                AddLegalMove((int)Position.X - i, (int)Position.Y + i);
            }
            if (!board.IsEmpty((int)Position.X - i, (int)Position.Y + i)) break;
        }
        // Diagonale Bas-Gauche
        for (int i = 1; i <= Math.Min(7 - (int)Position.X, (int)Position.Y); i++)
        {
            if (board.IsLegalMove(this, (int)Position.X + i, (int)Position.Y - i))
            {
                AddLegalMove((int)Position.X + i, (int)Position.Y - i);
            }
            if (!board.IsEmpty((int)Position.X + i, (int)Position.Y - i)) break;
        }
    }

    public override bool SetsCheck()
    {
        // Haut
        for (int i = (int)Position.Y - 1; i >= 0; i--)
        {
            if (board.IsEmpty((int)Position.X, i)) continue;
            Piece piece = board.GetPiece((int)Position.X, i);
            if (piece.ChessColor != ChessColor && piece.ChessPiece == ChessPiece.King)
                return true;
            
            break;
        }

        // Gauche
        for (int i = (int)Position.X - 1; i >= 0; i--)
        {
            if (board.IsEmpty(i, (int)Position.Y)) continue;
            Piece piece = board.GetPiece(i, (int)Position.Y);
            if (piece.ChessColor != ChessColor && piece.ChessPiece == ChessPiece.King)
                return true;
            
            break;
        }

        // Bas
        for (int i = (int)Position.Y + 1; i < 8; i++)
        {
            if (board.IsEmpty((int)Position.X, i)) continue;
            Piece piece = board.GetPiece((int)Position.X, i);
            if (piece.ChessColor != ChessColor && piece.ChessPiece == ChessPiece.King)
                return true;
            
            break;
        }

        // Droite
        for (int i = (int)Position.X + 1; i < 8; i++)
        {
            if (board.IsEmpty(i, (int)Position.Y)) continue;
            Piece piece = board.GetPiece(i, (int)Position.Y);
            if (piece.ChessColor != ChessColor && piece.ChessPiece == ChessPiece.King)
                return true;
            
            break;
        }

        // Diagonale Haut-Gauche
        for (int i = 1; i <= Math.Min((int)Position.X, (int)Position.Y); i++)
        {
            if (board.IsEmpty((int)Position.X - i, (int)Position.Y - i)) continue;
            Piece p = board.GetPiece((int)Position.X - i, (int)Position.Y - i);
            if (p.ChessColor != ChessColor && p.ChessPiece == ChessPiece.King) return true;
            break;
        }
        
        // Diagonale Bas-Droite
        for (int i = 1; i <= Math.Min(7 - (int)Position.X, 7 - (int)Position.Y); i++)
        {
            if (board.IsEmpty((int)Position.X + i, (int)Position.Y + i)) continue;
            Piece p = board.GetPiece((int)Position.X + i, (int)Position.Y + i);
            if (p.ChessColor != ChessColor && p.ChessPiece == ChessPiece.King) return true;
            break;
        }
        
        // Diagonale Bas-Gauche
        for (int i = 1; i <= Math.Min((int)Position.X, 7 - (int)Position.Y); i++)
        {
            if (board.IsEmpty((int)Position.X - i, (int)Position.Y + i)) continue;
            Piece p = board.GetPiece((int)Position.X - i, (int)Position.Y + i);
            if (p.ChessColor != ChessColor && p.ChessPiece == ChessPiece.King) return true;
            break;
        }
        
        // Diagonale Haut-Droite
        for (int i = 1; i <= Math.Min(7 - (int)Position.X, (int)Position.Y); i++)
        {
            if (board.IsEmpty((int)Position.X + i, (int)Position.Y - i)) continue;
            Piece p = board.GetPiece((int)Position.X + i, (int)Position.Y - i);
            if (p.ChessColor != ChessColor && p.ChessPiece == ChessPiece.King) return true;
            break;
        }

        return false;
    }
}