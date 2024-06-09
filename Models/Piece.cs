using System;
using System.Collections.Generic;
using Kyo.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kyo.Models;

public enum ChessPiece {
    Pawn,
    Knight,
    Bishop,
    Rook,
    Queen,
    King
}

public enum PieceState {
    Unmarked,
    Marked
}

public abstract class Piece : Button {
    public bool AllowClickToUnmark = true;

    public PieceState MarkedState {get; set;}

    public Vector2 Position {get; set;}
    public List<Button> LegalMoves {get; protected set;}
    protected Texture2D legalsTexture;

    public int NumberOfMoves {get; private set;} = 0;
    public ChessPiece ChessPiece { get; protected set; }
    protected Board board;
    private ChessColor color;
    internal ChessColor ChessColor { get => color; private set => color = value; }

    public Piece(Sprite2D sprite, Vector2 position, ChessColor color, Board board) : base(sprite) {
        LegalMoves = new();
        Position = position;
        ChessColor = color;
        this.board = board;
        legalsTexture = ContentService.Instance.Textures["circle"];
    }

    public abstract void CalculateLegalMoves();
    public abstract bool SetsCheck();

    public override void Update() {
        if (MarkedState == PieceState.Marked)
        {
            for (int i = 0; i < LegalMoves.Count; i++)
            {
                LegalMoves[i].Update();
            }
        }

        if (Contains(InputManager.MousePosition)) {
            if (InputManager.LeftButtonPressed) {
                State = ButtonCondition.Pressed;
                if (AllowClickToUnmark && MarkedState == PieceState.Marked)
                    UnMark();
                else if (MarkedState != PieceState.Marked)
                    Mark();

                OnClick(EventArgs.Empty);
            }
            else if (State != ButtonCondition.Hovered && MarkedState != PieceState.Marked) {
                if (State == ButtonCondition.None)
                    OnHover(EventArgs.Empty);
                
                State = ButtonCondition.Hovered;
            }
        }
        else if (State != ButtonCondition.None && MarkedState != PieceState.Marked) {
            OnUnHover(EventArgs.Empty);
            State = ButtonCondition.None;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }

    public void DrawLegalMoves(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < LegalMoves.Count; i++)
        {
            LegalMoves[i].Draw(spriteBatch);
        }
    }

    public virtual void Move(Vector2 position) {
        NumberOfMoves++;
        board.Move(this, position);
        Bounds = new Rectangle((int)position.X * (int)Constants.Case.X, (int)position.Y * (int)Constants.Case.Y, (int)Constants.Case.X, (int)Constants.Case.Y);
        Center(new Rectangle((int)position.X * (int)Constants.Case.X, (int)position.Y * (int)Constants.Case.Y, (int)Constants.Case.X, (int)Constants.Case.Y));
    }

    protected void AddLegalMove(int x, int y)
    {
        AddLegalMove(new Vector2(x, y));
    }

    protected void AddLegalMove(Vector2 position) {
        Button b = new Button(new Sprite2D(legalsTexture, new Rectangle((int)position.X * (int)Constants.Case.X, (int)position.Y * (int)Constants.Case.Y, (int)Constants.Case.X, (int)Constants.Case.Y), Color.DarkSlateGray));
        b.Click += (s, e) => { Move(position); };
        b.Hover += (s, e) => { b.Color = Color.Black; };
        b.UnHover += (s, e) => { b.Color = Color.DarkSlateGray; };
        LegalMoves.Add(b);
    }

    public void Mark()
    {
        MarkedState = PieceState.Marked;

    }

    public void UnMark()
    {
        MarkedState = PieceState.Unmarked;
    }
}