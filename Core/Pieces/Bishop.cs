using System.Collections.Generic;
using Kyo.Extension;
using Microsoft.Xna.Framework;

namespace Kyo.Core.Pieces;

public class Bishop : Piece 
{
    public Bishop(PieceColor color) : base(2, color)
    {
        
    }

    public override List<Vector2> GetAllMoves(Vector2 startPosition)
    {
        List<Vector2> moves = new List<Vector2>();

        // Diagonale haut droite
        for (int i = 0; i < 8; i++)
        {
            var nextCase = Board.Cases.Find(f => f.Position == new Vector2(startPosition.X + (i + 1), startPosition.Y - (i + 1)));

            if (nextCase == null)
                continue;
            
            moves.Add(nextCase.Position);

            if (nextCase.Piece != null)
                break;
        }

        // Diagonale bas droite
        for (int i = 0; i < 8; i++)
        {
            var nextCase = Board.Cases.Find(f => f.Position == new Vector2(startPosition.X + (i + 1), startPosition.Y + (i + 1)));

            if (nextCase == null)
                continue;
            
            moves.Add(nextCase.Position);

            if (nextCase.Piece != null)
                break;
        }

        // Diagonale bas gauche
        for (int i = 0; i < 8; i++)
        {
            var nextCase = Board.Cases.Find(f => f.Position == new Vector2(startPosition.X - (i + 1), startPosition.Y + (i + 1)));

            if (nextCase == null)
                continue;
            
            moves.Add(nextCase.Position);

            if (nextCase.Piece != null)
                break;
        }

        // Diagonale haut gauche
        for (int i = 0; i < 8; i++)
        {
            var nextCase = Board.Cases.Find(f => f.Position == new Vector2(startPosition.X - (i + 1), startPosition.Y - (i + 1)));

            if (nextCase == null)
                continue;
            
            moves.Add(nextCase.Position);

            if (nextCase.Piece != null)
                break;
        }

        return moves;
    }
}