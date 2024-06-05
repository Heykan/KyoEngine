using Microsoft.Xna.Framework;

namespace Kyo.Models.Pieces;

public class Pawn : Piece
{
    public Pawn(Sprite2D sprite, Vector2 position, ChessColor color, Board board) : base(sprite, position, color, board)
    {
        ChessPiece = ChessPiece.Pawn;
    }

    public override void CalculateLegalMoves()
    {
        LegalMoves.Clear();
        // Premier mouvement de 2 cases
        if (NumberOfMoves == 0) {
            if (ChessColor == ChessColor.White 
            && board.IsEmpty((int)Position.X, 5) 
            && board.IsEmpty((int)Position.X, 4)) 
            {
                if (board.IsLegalMove(this, new Vector2((int)Position.X, 4)))
                    AddLegalMove(new Vector2(Position.X, 4));
            }

            else if (ChessColor == ChessColor.Black 
            && board.IsEmpty((int)Position.X, 2) 
            && board.IsEmpty((int)Position.X, 3)) 
            {
                if (board.IsLegalMove(this, new Vector2((int)Position.X, 3)))
                    AddLegalMove(new Vector2(Position.X, 3));
            }
        }
        // Mouvement normal
        if (ChessColor == ChessColor.White 
        && board.IsEmpty((int)Position.X, (int)Position.Y - 1))
        {
            if (board.IsLegalMove(this, new Vector2((int)Position.X, (int)Position.Y - 1)))
                AddLegalMove(new Vector2((int)Position.X, (int)Position.Y - 1));
        }
        else if (ChessColor == ChessColor.Black 
        && board.IsEmpty((int)Position.X, (int)Position.Y + 1))
        {
            if (board.IsLegalMove(this, new Vector2((int)Position.X, (int)Position.Y + 1)))
                AddLegalMove(new Vector2((int)Position.X, (int)Position.Y + 1));
        }
        // Manger
        if ((int)Position.X != 0)
        {
            if (ChessColor == ChessColor.White 
            && !board.IsEmpty((int)Position.X - 1, (int)Position.Y - 1) 
            && board.GetPiece((int)Position.X - 1, (int)Position.Y - 1).ChessColor != ChessColor)
            {
                if (board.IsLegalMove(this, new Vector2((int)Position.X - 1, (int)Position.Y - 1)))
                {
                    AddLegalMove(new Vector2((int)Position.X - 1, (int)Position.Y - 1));
                }
            }
            else if (ChessColor == ChessColor.Black 
            && !board.IsEmpty((int)Position.X - 1, (int)Position.Y + 1) 
            && board.GetPiece((int)Position.X - 1, (int)Position.Y + 1).ChessColor != ChessColor)
            {
                if (board.IsLegalMove(this, new Vector2((int)Position.X - 1, (int)Position.Y + 1)))
                {
                    AddLegalMove(new Vector2((int)Position.X - 1, (int)Position.Y + 1));
                }
            }
        }
        if ((int)Position.X != 7)
        {
            if (ChessColor == ChessColor.White 
            && !board.IsEmpty((int)Position.X + 1, (int)Position.Y - 1) 
            && board.GetPiece((int)Position.X + 1, (int)Position.Y - 1).ChessColor != ChessColor)
            {
                if (board.IsLegalMove(this, new Vector2((int)Position.X + 1, (int)Position.Y - 1)))
                {
                    AddLegalMove(new Vector2((int)Position.X + 1, (int)Position.Y - 1));
                }
            }
            else if (ChessColor == ChessColor.Black 
            && !board.IsEmpty((int)Position.X + 1, (int)Position.Y + 1) 
            && board.GetPiece((int)Position.X + 1, (int)Position.Y + 1).ChessColor != ChessColor)
            {
                if (board.IsLegalMove(this, new Vector2((int)Position.X + 1, (int)Position.Y + 1)))
                {
                    AddLegalMove(new Vector2((int)Position.X + 1, (int)Position.Y + 1));
                }
            }
        }
        // En passant
        if ((int)Position.X != 0)
        {
            if ((int)Position.Y == 3 
            && ChessColor == ChessColor.White 
            && !board.IsEmpty((int)Position.X - 1, (int)Position.Y) 
            && board.IsEmpty((int)Position.X - 1, (int)Position.Y - 1))
            {
                Piece p = board.GetPiece((int)Position.X - 1, (int)Position.Y);
                if (p.ChessColor != ChessColor 
                && p.NumberOfMoves == 1 
                && board.LastPieceMoved == p 
                && p.ChessPiece == ChessPiece.Pawn 
                && board.IsLegalMove(this, new Vector2((int)Position.X - 1, (int)Position.Y - 1), p))
                {
                    AddEnPassantMove(p, (int)Position.X - 1, (int)Position.Y - 1);
                }
            }
            else if ((int)Position.Y == 4 
            && ChessColor == ChessColor.Black 
            && !board.IsEmpty((int)Position.X - 1, (int)Position.Y) 
            && board.IsEmpty((int)Position.X - 1, (int)Position.Y + 1))
            {
                Piece p = board.GetPiece((int)Position.X - 1, (int)Position.Y);
                if (p.ChessColor != ChessColor 
                && p.NumberOfMoves == 1 
                && board.LastPieceMoved == p 
                && p.ChessPiece == ChessPiece.Pawn 
                && board.IsLegalMove(this, new Vector2((int)Position.X - 1, (int)Position.Y + 1), p))
                {
                    AddEnPassantMove(p, (int)Position.X - 1, (int)Position.Y + 1);
                }
            }
        }
        if ((int)Position.X != 7)
        {
            if ((int)Position.Y == 3 
            && ChessColor == ChessColor.White 
            && !board.IsEmpty((int)Position.X + 1, (int)Position.Y) 
            && board.IsEmpty((int)Position.X + 1, (int)Position.Y - 1))
            {
                Piece p = board.GetPiece((int)Position.X + 1, (int)Position.Y);
                if (p.ChessColor != ChessColor 
                && p.NumberOfMoves == 1 
                && board.LastPieceMoved == p 
                && p.ChessPiece == ChessPiece.Pawn 
                && board.IsLegalMove(this, new Vector2((int)Position.X + 1, (int)Position.Y - 1), p))
                {
                    AddEnPassantMove(p, (int)Position.X + 1, (int)Position.Y - 1);
                }
            }
            else if ((int)Position.Y == 4 
            && ChessColor == ChessColor.Black 
            && !board.IsEmpty((int)Position.X + 1, (int)Position.Y) 
            && board.IsEmpty((int)Position.X + 1, (int)Position.Y + 1))
            {
                Piece p = board.GetPiece((int)Position.X + 1, (int)Position.Y);
                if (p.ChessColor != ChessColor 
                && p.NumberOfMoves == 1 
                && board.LastPieceMoved == p 
                && p.ChessPiece == ChessPiece.Pawn 
                && board.IsLegalMove(this, new Vector2((int)Position.X + 1, (int)Position.Y + 1), p))
                {
                    AddEnPassantMove(p, (int)Position.X + 1, (int)Position.Y + 1);

                }
            }
        }
    }

    public override bool SetsCheck()
    {
        if ((int)Position.X != 0)
        {
            if (ChessColor == ChessColor.White 
            && !board.IsEmpty((int)Position.X - 1, (int)Position.Y - 1) 
            && board.GetPiece((int)Position.X - 1, (int)Position.Y - 1).ChessColor != ChessColor 
            && board.GetPiece((int)Position.X - 1, (int)Position.Y - 1).ChessPiece == ChessPiece.King)
            {
                return true;
            }
            else if (ChessColor == ChessColor.Black 
            && !board.IsEmpty((int)Position.X - 1, (int)Position.Y + 1) 
            && board.GetPiece((int)Position.X - 1, (int)Position.Y + 1).ChessColor != ChessColor 
            && board.GetPiece((int)Position.X - 1, (int)Position.Y + 1).ChessPiece == ChessPiece.King)
            {
                return true;
            }
        }
        if ((int)Position.X != 7)
        {
            if (ChessColor == ChessColor.White 
            && !board.IsEmpty((int)Position.X + 1, (int)Position.Y - 1) 
            && board.GetPiece((int)Position.X + 1, (int)Position.Y - 1).ChessColor != ChessColor 
            && board.GetPiece((int)Position.X + 1, (int)Position.Y - 1).ChessPiece == ChessPiece.King)
            {
                return true;
            }
            else if (ChessColor == ChessColor.Black 
            && !board.IsEmpty((int)Position.X + 1, (int)Position.Y + 1) 
            && board.GetPiece((int)Position.X + 1, (int)Position.Y + 1).ChessColor != ChessColor 
            && board.GetPiece((int)Position.X + 1, (int)Position.Y + 1).ChessPiece == ChessPiece.King)
            {
                return true;
            }
        }
        return false;
    }

    protected void AddEnPassantMove(Piece p, int x, int y)
    {
        Button b = new Button(new Sprite2D(legalsTexture, new Rectangle(x * (int)Constants.Case.X, y * (int)Constants.Case.Y, (int)Constants.Case.X, (int)Constants.Case.Y), Color.DarkSlateGray));
        b.Click += (s, e) => { p.Move(new Vector2(x, y)); Move(new Vector2(x, y));  };
        b.Hover += (s, e) => { b.Color = Color.Black; };
        b.UnHover += (s, e) => { b.Color = Color.DarkSlateGray; };
        LegalMoves.Add(b);
    }
}