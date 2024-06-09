using System;
using Microsoft.Xna.Framework;

namespace Kyo.Models.Pieces;

public class King : Piece
{
    private bool _checked;
    public bool Checked 
    {
        get => _checked;
        set {
            _checked = value;
            if (_checked)
                sprite.Color = Color.Red;
            else
                sprite.Color = Color.White;
            
            CalculateLegalMoves();
        }
    }

    public King(Sprite2D sprite, Vector2 position, ChessColor color, Board board) : base(sprite, position, color, board)
    {
        ChessPiece = ChessPiece.King;
    }

    public override void CalculateLegalMoves()
    {
        LegalMoves.Clear();

        for (int i = Math.Max(0, (int)Position.Y - 1); i <= Math.Min(7, (int)Position.Y + 1); i++)
        {
            for (int j = Math.Max(0, (int)Position.X - 1); j <= Math.Min(7, (int)Position.X + 1); j++)
            {
                if (i == (int)Position.Y && (int)Position.X == j) continue;
                if(board.IsLegalMove(this, j, i))
                {
                    AddLegalMove(j, i);
                }
            }
        }

        if (Checked) return;

        //RocadeLeft
        if(NumberOfMoves == 0 && board.IsEmpty(1, (int)Position.Y) && board.IsEmpty(2, (int)Position.Y) && board.IsEmpty(3, (int)Position.Y) 
            && board.IsLegalMove(this, 4, (int)Position.Y) && board.IsLegalMove(this, 3, (int)Position.Y) && board.IsLegalMove(this, 2, (int)Position.Y) && !board.IsEmpty(0, (int)Position.Y))
        {
            Piece p = board.GetPiece(0, (int)Position.Y);
            if(p.NumberOfMoves == 0)
            {
                AddCastlingMove(p, 2, 3);
            }
        }
        //RocadeRight
        if (NumberOfMoves == 0 && board.IsEmpty(5, (int)Position.Y) && board.IsEmpty(6, (int)Position.Y)
            && board.IsLegalMove(this, 4, (int)Position.Y) && board.IsLegalMove(this, 5, (int)Position.Y) && board.IsLegalMove(this, 6, (int)Position.Y) && !board.IsEmpty(7, (int)Position.Y))
        {
            Piece p = board.GetPiece(7, (int)Position.Y);
            if (p.NumberOfMoves == 0)
            {
                AddCastlingMove(p, 6, 5);
            }
        }
    }

    public override bool SetsCheck()
    {
        for (int i = Math.Max(0, (int)Position.Y - 1); i <= Math.Min(7, (int)Position.Y + 1); i++)
        {
            for (int j = Math.Max(0, (int)Position.X - 1); j <= Math.Min(7, (int)Position.X + 1); j++)
            {
                if (i == (int)Position.Y && (int)Position.X == j) continue;
                if (board.IsEmpty(j, i)) continue;
                Piece p = board.GetPiece(j, i);
                if(p.ChessColor != ChessColor && p.ChessPiece == ChessPiece.King)
                {
                    return true;
                }
            }
        }
        return false;
    }

    protected void AddCastlingMove(Piece p, int x, int x2)
    {
        Button b = new Button(new Sprite2D(legalsTexture, new Rectangle(x * (int)Constants.Case.X, (int)Position.Y * (int)Constants.Case.Y, (int)Constants.Case.X, (int)Constants.Case.Y), Color.DarkSlateGray));
        b.Click += (s, e) => { Move(new Vector2(x, Position.Y)); p.Move(new Vector2(x2, Position.Y)); };
        b.Hover += (s, e) => { b.Color = Color.Black; };
        b.UnHover += (s, e) => { b.Color = Color.DarkSlateGray; };
        LegalMoves.Add(b);
    }
}