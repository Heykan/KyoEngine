using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Kyo.Core.Pieces;

public class Knight : Piece 
{
    public Knight(PieceColor color) : base(1, color)
    {
        
    }

    /*public override bool CanMove(Vector2 startPosition, Vector2 endPosition)
    {
        
    }*/

    public override List<Vector2> GetAllMoves(Vector2 startPosition)
    {
        List<Vector2> moves = new List<Vector2>
        {
            // Haut gauche
            new Vector2(startPosition.X - 1, startPosition.Y - 2),
            // Haut droite
            new Vector2(startPosition.X + 1, startPosition.Y - 2),
            // Milieu gauche haut
            new Vector2(startPosition.X - 2, startPosition.Y - 1),
            // Milieu droite haut
            new Vector2(startPosition.X + 2, startPosition.Y - 1),
            // Milieu gauche bas
            new Vector2(startPosition.X - 2, startPosition.Y + 1),
            // Milieu droite bas
            new Vector2(startPosition.X + 2, startPosition.Y + 1),
            // Bas gauche
            new Vector2(startPosition.X - 1, startPosition.Y + 2),
            // Bas droite
            new Vector2(startPosition.X + 1, startPosition.Y + 2),
        };

        return moves;
    }
}