using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Kyo.Core.Pieces;
using Kyo.Extension;
using Kyo.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Kyo.Core;

public class Board 
{
    public const int WIDTH = 8;
    public const int HEIGHT = 8;

    private bool _init;

    private static bool _blackTurn;
    private static Case[,] _cases;
    private int _currentTheme = 0;
    private string _currentCaseText;
    private Case _currentCase;
    private Case _selectedCase;

    private Texture2D[] _whiteCases;
    private Texture2D[] _blackCases;
    private Texture2D[] _whitePiecesTextures;
    private Texture2D[] _blackPiecesTextures;
    private static Piece[] _whitePieces;
    private static Piece[] _blackPieces;
    private SpriteFont _debugFont;

    public static Case[,] Cases => _cases;
    public static bool BlackTurn => _blackTurn;
    public static Piece[] BlackPieces => _blackPieces;
    public static Piece[] WhitePieces => _whitePieces;

    public Board() 
    {
        _whiteCases = new Texture2D[2];
        _blackCases = new Texture2D[2];
        _whitePiecesTextures = new Texture2D[6];
        _blackPiecesTextures = new Texture2D[6];

        _whitePieces = new Piece[16];
        _blackPieces = new Piece[16];

        _cases = new Case[WIDTH, HEIGHT];
        _blackTurn = false;

        InputManager.MouseLeftButtonPressed += MouseLeftButtonPressed;
    }

    public Task Load(ContentManager content)
    {
        // Chargement des textures de case blanche en mémoire
        _whiteCases[0] = content.Load<Texture2D>($"Images/{Case.WIDTH}/square brown light_png_shadow_{Case.WIDTH}px");
        _whiteCases[1] = content.Load<Texture2D>($"Images/{Case.WIDTH}/square gray light _png_shadow_{Case.WIDTH}px");

        // Chargement des textures de case noire en mémoire
        _blackCases[0] = content.Load<Texture2D>($"Images/{Case.WIDTH}/square brown dark_png_shadow_{Case.WIDTH}px");
        _blackCases[1] = content.Load<Texture2D>($"Images/{Case.WIDTH}/square gray dark _png_shadow_{Case.WIDTH}px");

        // Chargement des textures des pièces
        _whitePiecesTextures[0] = content.Load<Texture2D>($"Images/{Case.WIDTH}/w_pawn_png_shadow_{Case.WIDTH}px");
        _whitePiecesTextures[1] = content.Load<Texture2D>($"Images/{Case.WIDTH}/w_knight_png_shadow_{Case.WIDTH}px");
        _whitePiecesTextures[2] = content.Load<Texture2D>($"Images/{Case.WIDTH}/w_bishop_png_shadow_{Case.WIDTH}px");
        _whitePiecesTextures[3] = content.Load<Texture2D>($"Images/{Case.WIDTH}/w_rook_png_shadow_{Case.WIDTH}px");
        _whitePiecesTextures[4] = content.Load<Texture2D>($"Images/{Case.WIDTH}/w_queen_png_shadow_{Case.WIDTH}px");
        _whitePiecesTextures[5] = content.Load<Texture2D>($"Images/{Case.WIDTH}/w_king_png_shadow_{Case.WIDTH}px");

        _blackPiecesTextures[0] = content.Load<Texture2D>($"Images/{Case.WIDTH}/b_pawn_png_shadow_{Case.WIDTH}px");
        _blackPiecesTextures[1] = content.Load<Texture2D>($"Images/{Case.WIDTH}/b_knight_png_shadow_{Case.WIDTH}px");
        _blackPiecesTextures[2] = content.Load<Texture2D>($"Images/{Case.WIDTH}/b_bishop_png_shadow_{Case.WIDTH}px");
        _blackPiecesTextures[3] = content.Load<Texture2D>($"Images/{Case.WIDTH}/b_rook_png_shadow_{Case.WIDTH}px");
        _blackPiecesTextures[4] = content.Load<Texture2D>($"Images/{Case.WIDTH}/b_queen_png_shadow_{Case.WIDTH}px");
        _blackPiecesTextures[5] = content.Load<Texture2D>($"Images/{Case.WIDTH}/b_king_png_shadow_{Case.WIDTH}px");

        // Chargement de la font de debug
        _debugFont = content.Load<SpriteFont>("Fonts/Debug28");

        InitBoard();

        return Task.CompletedTask;
    }

    public void InitBoard()
    {
        _init = false;
        // Création des cases de l'échiquier
        for (int i = 0; i < HEIGHT; i++)
        {
            for (int j = 0; j < WIDTH; j++) 
            {
                // Ici le x et y sont inversé du fait que le code parcours 
                // de haut en bas puis de gauche à droite avec la boucle
                _cases[j, i] = new Case(new Vector2(j, i));
            }
        }
        _currentCaseText = $"{_cases[0, 0]}";

        // Initialisation des pions
        for (int i = 0; i < 8; i++) 
        {
            _whitePieces[i] = new Pawn(PieceColor.White) { Position = new Vector2(i, 6) };
            _blackPieces[i] = new Pawn(PieceColor.Black) { Position = new Vector2(i, 1) };

            _cases[i, 6].Piece = _whitePieces[i];
            _cases[i, 1].Piece = _blackPieces[i];
        }

        // Initialisation des pièces
        _blackPieces[8] = new Rook(PieceColor.Black) { Position = new Vector2(0, 0) };
        _blackPieces[9] = new Knight(PieceColor.Black) { Position = new Vector2(1, 0) };
        _blackPieces[10] = new Bishop(PieceColor.Black) { Position = new Vector2(2, 0) };
        _blackPieces[11] = new Queen(PieceColor.Black) { Position = new Vector2(3, 0) };
        _blackPieces[12] = new King(PieceColor.Black) { Position = new Vector2(4, 0) };
        _blackPieces[13] = new Bishop(PieceColor.Black) { Position = new Vector2(5, 0) };
        _blackPieces[14] = new Knight(PieceColor.Black) { Position = new Vector2(6, 0) };
        _blackPieces[15] = new Rook(PieceColor.Black) { Position = new Vector2(7, 0) };

        _whitePieces[8] = new Rook(PieceColor.White) { Position = new Vector2(0, 7) };
        _whitePieces[9] = new Knight(PieceColor.White) { Position = new Vector2(1, 7) };
        _whitePieces[10] = new Bishop(PieceColor.White) { Position = new Vector2(2, 7) };
        _whitePieces[11] = new Queen(PieceColor.White) { Position = new Vector2(3, 7) };
        _whitePieces[12] = new King(PieceColor.White) { Position = new Vector2(4, 7) };
        _whitePieces[13] = new Bishop(PieceColor.White) { Position = new Vector2(5, 7) };
        _whitePieces[14] = new Knight(PieceColor.White) { Position = new Vector2(6, 7) };
        _whitePieces[15] = new Rook(PieceColor.White) { Position = new Vector2(7, 7) };

        for (int i = 8; i < 16; i++)
        {
            var w = _whitePieces[i];
            var b = _blackPieces[i];
            
            _cases[(int)w.Position.X, (int)w.Position.Y].Piece = w;
            _cases[(int)b.Position.X, (int)b.Position.Y].Piece = b;
        }
        _init = true;
    }

    public void Update(GameTime gameTime)
    {
        if (!_init) return;

        // Scale pour les calculs
        float scaleX = (float)ScreenManager.Width / (WIDTH * Case.WIDTH);
        float scaleY = (float)ScreenManager.Height / (HEIGHT * Case.HEIGHT);

        // Récupération de la souris et sa position sur l'échiquier
        var mouse = Mouse.GetState();
        int x = Math.Clamp((int)Math.Floor(mouse.X / (Case.WIDTH * scaleX)), 0, 7);
        int y = Math.Clamp((int)Math.Floor(mouse.Y / (Case.WIDTH * scaleY)), 0, 7);
        Vector2 currentCasePos = new Vector2(x, y);
        _currentCase = _cases[(int)currentCasePos.X, (int)currentCasePos.Y];
        _currentCaseText = $"{_currentCase}";

        // Suppression des pièces manger
        for (int i = WhitePieces.Length - 1; i >= 0; i--)
        {
            var wp = _whitePieces[i];
            var bp = _blackPieces[i];

            if (wp != null && wp.IsDestroyed)
                _whitePieces[i] = null;
            if (bp != null && bp.IsDestroyed)
                _blackPieces[i] = null;
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!_init) return;

        // Récupération du scale pour adapter les sprites à l'écran
        float scaleX = (float)ScreenManager.Width / (WIDTH * Case.WIDTH);
        float scaleY = (float)ScreenManager.Height / (HEIGHT * Case.HEIGHT);

        DrawBoard(spriteBatch, scaleX, scaleY);
        DrawMoves(spriteBatch, scaleX, scaleY);
        DrawPieces(spriteBatch, scaleX, scaleY, _whitePieces);
        DrawPieces(spriteBatch, scaleX, scaleY, _blackPieces);

        spriteBatch.DrawString(_debugFont, _currentCaseText, new Vector2(30, 30), Color.White);
    }

    private void DrawBoard(SpriteBatch spriteBatch, float scaleX, float scaleY)
    {
        // Définition de si la case est noire ou non
        bool isDark = true;

        // Boucle imbriqué pour l'affichage
        for (int i = 0; i < HEIGHT; i++)
        {
            // Modification pour le 1/2 couleur
            isDark = !isDark;

            for (int j = 0; j < WIDTH; j++) 
            {
                // Ici le x et y sont inversé du fait que le code parcours 
                // de haut en bas puis de gauche à droite avec la boucle
                Case currentCase = _cases[j, i];
                Texture2D texture = isDark ? _blackCases[_currentTheme] : _whiteCases[_currentTheme];

                Vector2 casePosition = new Vector2(j * Case.WIDTH * scaleX, i * Case.HEIGHT * scaleY);
                // Changement de couleur lors de la sélection
                Color color = currentCase.Selected ? Color.MediumPurple : Color.White;
                if (currentCase.Selected)
                    texture = _whiteCases[_currentTheme];

                spriteBatch.Draw(texture, 
                                    casePosition, 
                                    null,
                                    color, 
                                    0, 
                                    Vector2.Zero, 
                                    new Vector2(scaleX, scaleY), 
                                    SpriteEffects.None, 
                                    1.0f
                                );

                
                // Modification pour le 1/2 couleur
                isDark = !isDark;
            }
        }
    }

    private void DrawMoves(SpriteBatch spriteBatch, float scaleX, float scaleY)
    {
        // Si la case est sélectionner, dessine la position possible
        // des pièces
        if (_selectedCase == null) return;
        if (!_selectedCase.Selected) return;

        var moves = _selectedCase.Piece.GetLegalMoves(_selectedCase.Position);
        var king = _blackTurn ? BlackPieces.First(f => f is King) : WhitePieces.First(f => f is King);

        if (king.IsKingInCheck())
            moves = _selectedCase.Piece.GetMovesToDefendKing(king.Position, moves);

        foreach(var move in moves)
        {
            Vector2 casePosition = new Vector2(move.X * Case.WIDTH * scaleX, move.Y * Case.HEIGHT * scaleY);
            Texture2D texture = _whiteCases[_currentTheme];
            spriteBatch.Draw(texture, 
                            casePosition, 
                            null, 
                            Color.LimeGreen * 0.5f,
                            0,
                            Vector2.Zero,
                            new Vector2(scaleX, scaleY),
                            SpriteEffects.None,
                            1.0f
            );
        }
    }

    private void DrawPieces(SpriteBatch spriteBatch, float scaleX, float scaleY, Piece[] pieces)
    {
        for (int i = pieces.Length - 1; i >= 0; i--)
        {
            var piece  = pieces[i];
            if (piece == null) continue;

            Texture2D pieceTexture = piece.Color == PieceColor.Black ? _blackPiecesTextures[piece.Id] : _whitePiecesTextures[piece.Id];
            
            // Check de si le roi est check ou pas
            Color checkColor = Color.White;
            if (piece is King && piece.Color == PieceColor.White && piece.IsKingInCheck())
                checkColor = Color.DarkRed;
            if (piece is King && piece.Color == PieceColor.Black && piece.IsKingInCheck())
                checkColor = Color.DarkRed;

            // Centre de la case
            float centerX = piece.Position.X * Case.WIDTH * scaleX + Case.WIDTH * scaleX / 2;
            float centerY = piece.Position.Y * Case.HEIGHT * scaleY + Case.HEIGHT * scaleY / 2;

            // Offset pour centrer la pièce
            float offsetX = centerX - pieceTexture.Width * scaleX / 2;
            float offsetY = centerY - pieceTexture.Height * scaleY / 2;

            Vector2 position = new Vector2(offsetX, offsetY);

            spriteBatch.Draw(pieceTexture, 
                                position, 
                                null,
                                checkColor,
                                0,
                                Vector2.Zero, 
                                new Vector2(scaleX, scaleY), 
                                SpriteEffects.None, 
                                1.0f
                            );
        }
    }

    private void MouseLeftButtonPressed()
    {
        if (!MouseInsideWindow())
            return;

        // Déselectionne les cases
        foreach(Case ccase in _cases)
            ccase.Selected = false;

        // Sélectionne la pièce et la déplace sur la case désiré
        if (_selectedCase != null && _selectedCase != _currentCase)
        {
            // Change de sélection si la case à une pièce de même couleur
            if (_currentCase.Piece != null && _currentCase.Piece.Color == _selectedCase.Piece.Color)
            {
                _selectedCase = _currentCase;
                _selectedCase.Selected = true;
                return;
            }

            // Récupération des déplacements légaux
            var moves = _selectedCase.Piece.GetLegalMoves(_selectedCase.Position);
            // On annule le mouvement si ce n'est pas légal
            if (!moves.Contains(_currentCase.Position))
            {
                _selectedCase.Selected = true;
                _selectedCase = null;
                return;
            }

            _selectedCase.Piece.Move(_selectedCase, _currentCase);
            _selectedCase = null;
            _blackTurn = !_blackTurn;
            return;
        }

        // Annule lors d'un nouveau click de souris
        if (_selectedCase == _currentCase)
        {
            _selectedCase = null;
            return;
        }

        // Sélectionne la case sous la souris
        _selectedCase = null;
        if (_currentCase != null && _currentCase.Piece != null)
        {
            _selectedCase = _currentCase;
            // Check le tour du joueur
            if ((!_blackTurn && _selectedCase.Piece.Color == PieceColor.Black) || (_blackTurn && _selectedCase.Piece.Color == PieceColor.White))
            {
                _selectedCase = null;
                return;
            }

            _currentCase.Selected = true;
        }
    }

    private bool MouseInsideWindow() 
    {
        var mouse = Mouse.GetState();
        return mouse.X >= 0 && mouse.X <= ScreenManager.Width
            && mouse.Y >= 0 && mouse.Y <= ScreenManager.Height;
    }
}