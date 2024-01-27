using System.Collections.Generic;
using Kyo.Extension;
using Microsoft.Xna.Framework;

namespace Kyo.Core.Pieces;

public class King : Piece 
{
    public King(PieceColor color) : base(5, color)
    {
        
    }

    public override List<Vector2> GetAllMoves(Vector2 startPosition)
    {
        List<Vector2> moves = new List<Vector2>();
        
        // Haut gauche
        moves.Add(new Vector2(startPosition.X - 1, startPosition.Y - 1));
        // Haut
        moves.Add(new Vector2(startPosition.X, startPosition.Y - 1));
        // Haut droite
        moves.Add(new Vector2(startPosition.X + 1, startPosition.Y - 1));
        // Droite
        moves.Add(new Vector2(startPosition.X + 1, startPosition.Y));
        // Bas droite
        moves.Add(new Vector2(startPosition.X + 1, startPosition.Y + 1));
        // Bas
        moves.Add(new Vector2(startPosition.X, startPosition.Y + 1));
        // Bas gauche
        moves.Add(new Vector2(startPosition.X - 1, startPosition.Y + 1));
        // Gauche
        moves.Add(new Vector2(startPosition.X - 1, startPosition.Y));

        // Roque
        switch (this.Color)
        {
            case PieceColor.White:
                var rightRookCase = Board.Cases.Find(f => f.Position == new Vector2(7, 7));
                var leftRookCase = Board.Cases.Find(f => f.Position == new Vector2(0, 7));
                if (!AlreadyMoved)
                {
                    if (rightRookCase.Piece != null && !rightRookCase.Piece.AlreadyMoved)
                        moves.Add(new Vector2(startPosition.X + 2, startPosition.Y));
                    if (leftRookCase.Piece != null && !leftRookCase.Piece.AlreadyMoved)
                        moves.Add(new Vector2(startPosition.X - 2, startPosition.Y));
                }
                break;
            case PieceColor.Black:
                rightRookCase = Board.Cases.Find(f => f.Position == new Vector2(7, 7));
                leftRookCase = Board.Cases.Find(f => f.Position == new Vector2(0, 7));
                if (!AlreadyMoved)
                {
                    if (rightRookCase.Piece != null && !rightRookCase.Piece.AlreadyMoved)
                        moves.Add(new Vector2(startPosition.X + 2, startPosition.Y));
                    if (leftRookCase.Piece != null && !leftRookCase.Piece.AlreadyMoved)
                        moves.Add(new Vector2(startPosition.X - 2, startPosition.Y));
                }
                break;
        }
        
        return moves;
    }
}