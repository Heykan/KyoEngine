using System;
using System.Collections.Generic;
using Kyo.Extension;
using Microsoft.Xna.Framework;

namespace Kyo.Core.Pieces;

public class Pawn : Piece 
{
    public Pawn(PieceColor color) : base(0, color)
    {

    }

    public override bool CanMove(Vector2 startPosition, Vector2 endPosition)
    {

        return base.CanMove(startPosition, endPosition);
    }

    public override List<Vector2> GetAllMoves(Vector2 startPosition)
    {
        List<Vector2> moves = new List<Vector2>();
        var currentCase = Board.Cases.Find<Case>(f => f.Position == startPosition);

        switch (Color)
        {
            case PieceColor.White:
                var nextCase = Board.Cases.Find<Case>(f => f.Position == new Vector2(startPosition.X, startPosition.Y - 1));
                var next2Case = Board.Cases.Find<Case>(f => f.Position == new Vector2(startPosition.X, startPosition.Y - 2));
                var upperRight = Board.Cases.Find<Case>(f => f.Position == new Vector2(startPosition.X + 1, startPosition.Y - 1));
                var upperLeft = Board.Cases.Find<Case>(f => f.Position == new Vector2(startPosition.X - 1, startPosition.Y - 1));
                
                if (!AlreadyMoved && next2Case.Piece == null)
                    moves.Add(new Vector2(startPosition.X, startPosition.Y - 2));
                if (nextCase != null && nextCase.Piece == null)
                    moves.Add(nextCase.Position);
                if (upperRight != null && upperRight.Piece != null && upperRight.Piece is not King)
                    moves.Add(upperRight.Position);
                if (upperLeft != null && upperLeft.Piece != null && upperLeft.Piece is not King)
                    moves.Add(upperLeft.Position);
                break;

            case PieceColor.Black:
                nextCase = Board.Cases.Find<Case>(f => f.Position == new Vector2(startPosition.X, startPosition.Y + 1));
                next2Case = Board.Cases.Find<Case>(f => f.Position == new Vector2(startPosition.X, startPosition.Y + 2));
                upperRight = Board.Cases.Find<Case>(f => f.Position == new Vector2(startPosition.X + 1, startPosition.Y + 1));
                upperLeft = Board.Cases.Find<Case>(f => f.Position == new Vector2(startPosition.X - 1, startPosition.Y + 1));

                if (!AlreadyMoved && next2Case.Piece == null)
                    moves.Add(new Vector2(startPosition.X, startPosition.Y + 2));
                if (nextCase != null && nextCase.Piece == null)
                    moves.Add(nextCase.Position);
                if (upperRight != null && upperRight.Piece != null && upperRight.Piece is not King)
                    moves.Add(upperRight.Position);
                if (upperLeft != null && upperLeft.Piece != null && upperLeft.Piece is not King)
                    moves.Add(upperLeft.Position);
                break;
        }

        return moves;
    }
}