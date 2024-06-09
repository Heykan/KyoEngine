using System;
using System.Linq;
using Kyo.Extension;
using Kyo.Manager;
using Kyo.Models.Pieces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kyo.Models;

public enum Turn {
    Player1,
    Player2
}

public enum ChessColor {
    Black,
    White,
    None
}

public class Board {
    private bool moveMade = false;
    private PlayerPanel whites;
    private PlayerPanel blacks;

    private Sprite2D[,] grid;
    private Piece[,] board;
    private SpriteFont spriteFont;
    private ChessColor winner;

    public Piece LastPieceMoved;

    public Turn Turn { get; private set; } = Turn.Player1;

    public Turn HumanPlayer { get; private set; } = Turn.Player1;

    public ChessColor CheckedKing { get; private set; } = ChessColor.None;

    public Board() {
        // Texture des cases
        Texture2D whiteCase = ContentService.Instance.Textures["square_brown_light_128"];
        Texture2D blackCase = ContentService.Instance.Textures["square_brown_dark_128"];
        spriteFont = ContentService.Instance.Fonts["debug28"];
        winner = ChessColor.None;

        // Initialise la grille
        grid = new Sprite2D[8,8];
        bool isWhiteDraw = false;

        for (int i = 0; i < 8; i++) {
            isWhiteDraw = !isWhiteDraw;
            for (int j = 0; j < 8; j++) {
                grid[i, j] = new Sprite2D(isWhiteDraw ? whiteCase : blackCase, new Rectangle(j * (int)Constants.Case.X, i * (int)Constants.Case.Y, (int)Constants.Case.X, (int)Constants.Case.Y));
                isWhiteDraw = !isWhiteDraw;
            }
        }

        // Initialise les pièces
        InitializePieces();
    }

    private void InitializePieces() {
        board = new Piece[8,8];

        whites = new PlayerPanel();
        blacks = new PlayerPanel();

        // Initialise les pions blancs
        for (int i = 0; i < 8; i++) {
            var temp = new Pawn(new Sprite2D(ContentService.Instance.Textures["wpawn"], new Rectangle(0, 0, (int)Constants.Case.X, (int)Constants.Case.Y)), new Vector2(i, 6), ChessColor.White, this);
            temp.Center(grid[6, i].Bounds);
            board[6, i] = temp;
        }

        // Initialisation des pièces blanches
        Piece x = new Rook(new Sprite2D(ContentService.Instance.Textures["wrook"], new Rectangle(0, 0, (int)Constants.Case.X, (int)Constants.Case.Y)), new Vector2(0, 7), ChessColor.White, this);
        x.Center(grid[7, 0].Bounds);
        board[7, 0] = x;
        x = new Rook(new Sprite2D(ContentService.Instance.Textures["wrook"], new Rectangle(0, 0, (int)Constants.Case.X, (int)Constants.Case.Y)), new Vector2(7, 7), ChessColor.White, this);
        x.Center(grid[7, 7].Bounds);
        board[7, 7] = x;

        x = new Knight(new Sprite2D(ContentService.Instance.Textures["wknight"], new Rectangle(0, 0, (int)Constants.Case.X, (int)Constants.Case.Y)), new Vector2(1, 7), ChessColor.White, this);
        x.Center(grid[7, 1].Bounds);
        board[7, 1] = x;
        x = new Knight(new Sprite2D(ContentService.Instance.Textures["wknight"], new Rectangle(0, 0, (int)Constants.Case.X, (int)Constants.Case.Y)), new Vector2(6, 7), ChessColor.White, this);
        x.Center(grid[7, 6].Bounds);
        board[7, 6] = x;

        x = new Bishop(new Sprite2D(ContentService.Instance.Textures["wbishop"], new Rectangle(0, 0, (int)Constants.Case.X, (int)Constants.Case.Y)), new Vector2(2, 7), ChessColor.White, this);
        x.Center(grid[7, 2].Bounds);
        board[7, 2] = x;
        x = new Bishop(new Sprite2D(ContentService.Instance.Textures["wbishop"], new Rectangle(0, 0, (int)Constants.Case.X, (int)Constants.Case.Y)), new Vector2(5, 7), ChessColor.White, this);
        x.Center(grid[7, 5].Bounds);
        board[7, 5] = x;

        x = new Queen(new Sprite2D(ContentService.Instance.Textures["wqueen"], new Rectangle(0, 0, (int)Constants.Case.X, (int)Constants.Case.Y)), new Vector2(3, 7), ChessColor.White, this);
        x.Center(grid[7, 3].Bounds);
        board[7, 3] = x;
        x = new King(new Sprite2D(ContentService.Instance.Textures["wking"], new Rectangle(0, 0, (int)Constants.Case.X, (int)Constants.Case.Y)), new Vector2(4, 7), ChessColor.White, this);
        x.Center(grid[7, 4].Bounds);
        board[7, 4] = x;


        // Initialise les pions noirs
        for (int i = 0; i < 8; i++) {
            var temp = new Pawn(new Sprite2D(ContentService.Instance.Textures["bpawn"], new Rectangle(0, 0, (int)Constants.Case.X, (int)Constants.Case.Y)), new Vector2(i, 1), ChessColor.Black, this);
            temp.Center(grid[1, i].Bounds);
            board[1, i] = temp;
        }

        // Initialisation des pièces noires
        x = new Rook(new Sprite2D(ContentService.Instance.Textures["brook"], new Rectangle(0, 0, (int)Constants.Case.X, (int)Constants.Case.Y)), new Vector2(0, 0), ChessColor.Black, this);
        x.Center(grid[0, 0].Bounds);
        board[0, 0] = x;
        x = new Rook(new Sprite2D(ContentService.Instance.Textures["brook"], new Rectangle(0, 0, (int)Constants.Case.X, (int)Constants.Case.Y)), new Vector2(7, 0), ChessColor.Black, this);
        x.Center(grid[0, 7].Bounds);
        board[0, 7] = x;

        x = new Knight(new Sprite2D(ContentService.Instance.Textures["bknight"], new Rectangle(0, 0, (int)Constants.Case.X, (int)Constants.Case.Y)), new Vector2(1, 0), ChessColor.Black, this);
        x.Center(grid[0, 1].Bounds);
        board[0, 1] = x;
        x = new Knight(new Sprite2D(ContentService.Instance.Textures["bknight"], new Rectangle(0, 0, (int)Constants.Case.X, (int)Constants.Case.Y)), new Vector2(6, 0), ChessColor.Black, this);
        x.Center(grid[0, 6].Bounds);
        board[0, 6] = x;

        x = new Bishop(new Sprite2D(ContentService.Instance.Textures["bbishop"], new Rectangle(0, 0, (int)Constants.Case.X, (int)Constants.Case.Y)), new Vector2(2, 0), ChessColor.Black, this);
        x.Center(grid[0, 2].Bounds);
        board[0, 2] = x;
        x = new Bishop(new Sprite2D(ContentService.Instance.Textures["bbishop"], new Rectangle(0, 0, (int)Constants.Case.X, (int)Constants.Case.Y)), new Vector2(5, 0), ChessColor.Black, this);
        x.Center(grid[0, 5].Bounds);
        board[0, 5] = x;

        x = new Queen(new Sprite2D(ContentService.Instance.Textures["bqueen"], new Rectangle(0, 0, (int)Constants.Case.X, (int)Constants.Case.Y)), new Vector2(3, 0), ChessColor.Black, this);
        x.Center(grid[0, 3].Bounds);
        board[0, 3] = x;
        x = new King(new Sprite2D(ContentService.Instance.Textures["bking"], new Rectangle(0, 0, (int)Constants.Case.X, (int)Constants.Case.Y)), new Vector2(4, 0), ChessColor.Black, this);
        x.Center(grid[0, 4].Bounds);
        board[0, 4] = x;

        // Ajout aux équipes
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                if (board[i, j] == null) continue;
                if (board[i, j].ChessColor == ChessColor.White) whites.Add(board[i, j]);
                else blacks.Add(board[i, j]);

                board[i, j].CalculateLegalMoves();
            }
        }
    }

    public bool IsEmpty(int x, int y) {
        return IsEmpty(new Vector2(x, y));
    }

    public bool IsEmpty(Vector2 position) {
        return board[(int)position.Y, (int)position.X] == null;
    }

    public void Move(Piece piece, Vector2 position) {
        CheckedKing = ChessColor.None;
        Vector2 v = piece.Position;
        LastPieceMoved = piece;

        if (!IsEmpty(position)){
            if (piece.ChessColor == ChessColor.White)
                blacks.Remove(board[(int)position.Y, (int)position.X]);
            else
                whites.Remove(board[(int)position.Y, (int)position.X]);
        }

        board[(int)position.Y, (int)position.X] = board[(int)v.Y, (int)v.X];
        board[(int)v.Y, (int)v.X] = null;
        board[(int)position.Y, (int)position.X].Position = position;

        if (board[(int)position.Y, (int)position.X].ChessPiece == ChessPiece.Pawn
        && (board[(int)position.Y, (int)position.X].Position.Y == 0 || board[(int)position.Y, (int)position.X].Position.Y == 7))
        {
            int y = (int)position.Y;
            int x = (int)position.X;
            if (piece.ChessColor == ChessColor.Black)
            {
                Queen qpiece = new Queen(
                    new Sprite2D(ContentService.Instance.Textures["bqueen"],
                    new Rectangle(x * (int)Constants.Case.X, y * (int)Constants.Case.Y, (int)Constants.Case.X, (int)Constants.Case.Y)),
                    new Vector2(x, y), ChessColor.Black, this
                );
                blacks.Add(qpiece);
                blacks.Remove(board[y, x]);
                board[y, x] = qpiece;
            }
            else
            {
                Queen qpiece = new Queen(
                    new Sprite2D(ContentService.Instance.Textures["wqueen"],
                    new Rectangle(x * (int)Constants.Case.X, y * (int)Constants.Case.Y, (int)Constants.Case.X, (int)Constants.Case.Y)),
                    new Vector2(x, y), ChessColor.White, this
                );
                whites.Add(qpiece);
                whites.Remove(board[y, x]);
                board[y, x] = qpiece;
            }
        }

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j] != null)
                {
                    board[i, j].CalculateLegalMoves();
                }
            }
        }
        moveMade = true;
    }

    public bool IsLegalMove(Piece piece, int x, int y) => IsLegalMove(piece, new Vector2(x, y));

    public bool IsLegalMove(Piece piece, Vector2 newPosition) {
        bool ret = false;

        if (IsEmpty(newPosition)
        || board[(int)newPosition.Y, (int)newPosition.X].ChessColor != piece.ChessColor
        || board[(int)newPosition.Y, (int)newPosition.X] == piece)
        {
            Piece temp = board[(int)newPosition.Y, (int)newPosition.X];
            board[(int)newPosition.Y, (int)newPosition.X] = piece;
            board[(int)piece.Position.Y, (int)piece.Position.X] = null;
            ret = true;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == null) continue;
                    if (board[i, j].ChessColor != piece.ChessColor && board[i, j].SetsCheck())
                    {
                        ret = false;
                        CheckedKing = piece.ChessColor;
                    }
                }
            }
            
            board[(int)piece.Position.Y, (int)piece.Position.X] = piece;
            board[(int)newPosition.Y, (int)newPosition.X] = temp;
        }

        return ret;
    }

    public bool IsLegalMove(Piece piece, Vector2 newPosition, Piece piece2) {
        board[(int)piece2.Position.Y, (int)piece2.Position.X] = null;
        bool ret = IsLegalMove(piece, newPosition);
        board[(int)piece2.Position.Y, (int)piece2.Position.X] = piece2;
        return ret;
    }

    public bool InGrid(int x, int y)
    {
        return x >= 0 && x < 8 && y >= 0 && y < 8;
    }

    public Piece GetPiece(int x, int y)
    {
        return board[y, x];
    }

    public void Update(GameTime gameTime) {
        if (winner != ChessColor.None) return;

        switch (Turn) {
            case Turn.Player1:
                if (HumanPlayer == Turn.Player1)
                    whites.Update();
                else
                    Kyobot.Instance.Update(whites, this);
                break;
            case Turn.Player2:
                if (HumanPlayer == Turn.Player2)
                    blacks.Update();
                else
                    Kyobot.Instance.Update(blacks, this);
                break;
        }

        // Mise en échec des roi
        King wKing = whites.Panel.First(f => f.ChessPiece == ChessPiece.King) as King;
        wKing.Checked = CheckedKing == ChessColor.White;

        King bKing = blacks.Panel.First(f => f.ChessPiece == ChessPiece.King) as King;
        bKing.Checked = CheckedKing == ChessColor.Black;

        if (moveMade) {
            whites.UnmarkAll();
            blacks.UnmarkAll();

            int whiteLegalMoves = whites.Panel.Sum(piece => piece.LegalMoves.Count);
            int blackLegalMoves = blacks.Panel.Sum(piece => piece.LegalMoves.Count);

            if (whiteLegalMoves == 0)
                winner = ChessColor.Black;

            if (blackLegalMoves == 0)
                winner = ChessColor.White;

            Turn = (Turn == Turn.Player1) ? Turn.Player2 : Turn.Player1;

            moveMade = false;
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                grid[i, j].Draw(spriteBatch);
            }
        }

        whites.DrawLegal(spriteBatch);
        blacks.DrawLegal(spriteBatch);
        whites.Draw(spriteBatch);
        blacks.Draw(spriteBatch);

        if (winner == ChessColor.White) {
            string text = "Victoire des blancs !";
            Vector2 stringSize = spriteFont.MeasureString(text);

            spriteBatch.DrawString(spriteFont, text, new Vector2(ScreenManager.Width/2 - stringSize.X/2 - 3, ScreenManager.Height/2 - stringSize.Y/2 - 3), Color.Black, 0, Vector2.Zero, 1.02f, SpriteEffects.None, 0);
            spriteBatch.DrawString(spriteFont, text, new Vector2(ScreenManager.Width/2 - stringSize.X/2, ScreenManager.Height/2 - stringSize.Y/2), Color.White);
        }
        if (winner == ChessColor.Black) {
            string text = "Victoire des noirs !";
            Vector2 stringSize = spriteFont.MeasureString(text);

            spriteBatch.DrawString(spriteFont, text, new Vector2(ScreenManager.Width/2 - stringSize.X/2 - 3, ScreenManager.Height/2 - stringSize.Y/2 - 3), Color.Black, 0, Vector2.Zero, 1.02f, SpriteEffects.None, 0);
            spriteBatch.DrawString(spriteFont, text, new Vector2(ScreenManager.Width/2 - stringSize.X/2, ScreenManager.Height/2 - stringSize.Y/2), Color.White);
        }
    }
}