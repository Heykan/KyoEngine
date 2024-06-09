using System;
using Microsoft.Xna.Framework;

namespace Kyo.Models.Pieces;

public class Knight : Piece
{
    // Définir les huit mouvements possibles pour un cavalier
    int[,] knightMoves = new int[,] {
        { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 },
        { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 }
    };

    public Knight(Sprite2D sprite, Vector2 position, ChessColor color, Board board) : base(sprite, position, color, board)
    {
        ChessPiece = ChessPiece.Knight;
    }

    public override void CalculateLegalMoves()
    {
        LegalMoves.Clear();
        for (int k = 0; k < knightMoves.GetLength(0); k++) {
            int newY = (int)Position.Y + knightMoves[k, 0];
            int newX = (int)Position.X + knightMoves[k, 1];

            if (board.InGrid(newX, newY)) {
                if (board.IsLegalMove(this, new Vector2(newX, newY))) {
                    AddLegalMove(new Vector2(newX, newY));
                }
            }
        }
    }

    public override bool SetsCheck()
    {
        for (int k = 0; k < knightMoves.GetLength(0); k++) {
            int newY = (int)Position.Y + knightMoves[k, 0];
            int newX = (int)Position.X + knightMoves[k, 1];

            if (board.InGrid(newX, newY) && !board.IsEmpty(newX, newY)) {
                Piece piece = board.GetPiece(newX, newY);
                if (piece.ChessColor != ChessColor
                && piece.ChessPiece == ChessPiece.King)
                    return true;
            }
        }
        return false;
    }
}