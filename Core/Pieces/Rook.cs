using System.Collections.Generic;
using Kyo.Extension;
using Microsoft.Xna.Framework;

namespace Kyo.Core.Pieces;

public class Rook : Piece 
{
    public Rook(PieceColor color) : base(3, color)
    {
        
    }

    public override List<Vector2> GetAllMoves(Vector2 startPosition)
    {
        List<Vector2> moves = new List<Vector2>();

        // Haut
        for (int i = 0; i < 8; i++)
        {
            var nextCase = Board.Cases.Find(f => f.Position == new Vector2(startPosition.X, startPosition.Y - (i + 1)));

            if (nextCase == null)
                continue;
            
            moves.Add(nextCase.Position);

            if (nextCase.Piece != null)
                break;
        }
        
        // Bas
        for (int i = 0; i < 8; i++)
        {
            var nextCase = Board.Cases.Find(f => f.Position == new Vector2(startPosition.X, startPosition.Y + (i + 1)));

            if (nextCase == null)
                continue;
            
            moves.Add(nextCase.Position);

            if (nextCase.Piece != null)
                break;
        }

        // Droite
        for (int i = 0; i < 8; i++)
        {
            var nextCase = Board.Cases.Find(f => f.Position == new Vector2(startPosition.X + (i + 1), startPosition.Y));

            if (nextCase == null)
                continue;
            
            moves.Add(nextCase.Position);

            if (nextCase.Piece != null)
                break;
        }

        // Gauche
        for (int i = 0; i < 8; i++)
        {
            var nextCase = Board.Cases.Find(f => f.Position == new Vector2(startPosition.X - (i + 1), startPosition.Y));

            if (nextCase == null)
                continue;
            
            moves.Add(nextCase.Position);

            if (nextCase.Piece != null)
                break;
        }

        return moves;
    }
}