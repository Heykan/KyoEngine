using System.Collections.Generic;
using Kyo.Extension;
using Microsoft.Xna.Framework;

namespace Kyo.Core.Pieces;

public class Queen : Piece 
{
    public Queen(PieceColor color) : base(4, color)
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