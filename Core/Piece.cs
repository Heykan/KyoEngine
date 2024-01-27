using System;
using System.Collections.Generic;
using System.Linq;
using Kyo.Core.Pieces;
using Kyo.Extension;
using Microsoft.Xna.Framework;

namespace Kyo.Core;

public abstract class Piece 
{
    public int Id { get; private set; }
    public int Value { get; private set; }
    public bool IsDestroyed { get; private set; }
    public PieceColor Color { get; set; }
    public bool AlreadyMoved { get; set; }
    public Vector2 Position { get; set; }

    public Piece(int id, PieceColor color)
    {
        Id = id;
        Color = color;
        AlreadyMoved = false;
        IsDestroyed = false;

        switch(Id) 
        {
            case 0: Value = 100; break;
            case 1: Value = 320; break;
            case 2: Value = 330; break;
            case 3: Value = 500; break;
            case 4: Value = 900; break;
            case 5: Value = 20000; break;
        }
    }

    public virtual bool CanMove(Vector2 startPosition, Vector2 endPosition)
    {
        var moves = GetLegalMoves(startPosition);

        // Vérification de l'état du roi
        if (IsKingInCheck())
        {
            moves = GetMovesToDefendKing(startPosition, moves);
            if (moves.Count <= 0)
                return false;
        }

        if (!moves.Contains(endPosition))
            return false;

        return true;
    }

    /// <summary>
    /// Cette fonction sert à tester tout les déplacements et se stop à la rencontre d'une pièce.
    /// </summary>
    /// <param name="startPosition">La position de départ sur l'échiquier</param>
    /// <returns>Liste de vecteur contennant tout les déplacements possible</returns>
    public abstract List<Vector2> GetAllMoves(Vector2 startPosition);

    /// <summary>
    /// Corrige les déplacements afin de ne pas manger ses propres pièces
    /// </summary>
    /// <param name="startPosition">La position de départ sur l'échiquier</param>
    /// <returns>Liste de vecteur contennant tout les déplacements possible corrigé</returns>
    public virtual List<Vector2> GetLegalMoves(Vector2 startPosition)
    {
        var moves = GetAllMoves(startPosition);
        for (int i = moves.Count - 1; i >= 0; i--)
        {
            Vector2 move = moves[i];
            Case currentCase = Board.Cases.Find(f => f.Position == move);

            // Si la case est hors du tableau ou null on passe à la suite
            if (currentCase == null)
            {
                moves.Remove(move);
                continue;
            }

            // Si la case contient une de nos pièce on la supprime de la liste
            if (currentCase.Piece != null && currentCase.Piece.Color == Color)
                moves.Remove(move);
        }

        return moves;
    }

    public List<Vector2> GetMovesToDefendKing(Vector2 kingPosition, List<Vector2> legalMoves)
    {
        List<Vector2> movesToDefendKing = new List<Vector2>();

        foreach(var move in legalMoves)
        {
            Case destinationCase = Board.Cases.Find(f => f.Position == move);

            // Simulation du mouvement pour vérifier si le roi est protéger
            Piece originalPiece = destinationCase.Piece;
            destinationCase.Piece = this;
            Case kingCase = Board.Cases.Find(f => f.Position == kingPosition);
            bool kingDefended = !IsKingInCheck();
            destinationCase.Piece = originalPiece;

            if (kingDefended)
                movesToDefendKing.Add(move);
        }

        return movesToDefendKing;
    }

    public bool IsKingInCheck()
    {
        Piece king = Color == PieceColor.White ? Board.WhitePieces.First(f => f is King) : Board.BlackPieces.First(f => f is King);

        for (int i = GetOpponentPieces().Count - 1; i >= 0; i--) 
        {
            var opponentPiece = GetOpponentPieces()[i];
            if (opponentPiece == null) continue;

            var opponentMoves = opponentPiece.GetLegalMoves(opponentPiece.Position);
            if (opponentMoves.Contains(king.Position))
            {
                if (opponentPiece is King)
                    return false;
                return true;
            }
        }

        return false;
    }

    private List<Piece> GetOpponentPieces() 
    {
        PieceColor opponentColor = Color == PieceColor.White ? PieceColor.Black : PieceColor.White; 
        return opponentColor == PieceColor.Black ? Board.BlackPieces.ToList() : Board.WhitePieces.ToList();
    }

    public void Move(Case from, Case to)
    {
        if (!CanMove(from.Position, to.Position))
        {
            from.Selected = false;
            return;
        }

        if (to.Piece != null)
            to.Piece.Destroy();

        from.Piece = null;
        from.Selected = false;
        to.Piece = this;
        Position = to.Position;

        if (!AlreadyMoved)
            AlreadyMoved = true;
    }

    public void Destroy()
    {
        IsDestroyed = true;
    }
}

public enum PieceColor 
{
    Black,
    White
}