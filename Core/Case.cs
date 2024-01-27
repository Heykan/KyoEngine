using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Kyo.Core;

public class Case 
{
    private Dictionary<int, char> _numToChar;
    private Dictionary<int, int> _numToWhiteNum;

    public const int WIDTH = 128;
    public const int HEIGHT = 128;

    public int Number { get; private set; }
    public char Letter { get; private set; }
    public Piece Piece { get; set; }
    public bool Selected { get; set; }
    public Vector2 Position { get; set; }

    public Case(Vector2 position, bool blackPosition = false)
    {
        // Initialisation du dictionnaire pour convertir la valeur en lettre
        _numToChar = new Dictionary<int, char>()
        {
            {0, 'a'},
            {1, 'b'},
            {2, 'c'},
            {3, 'd'},
            {4, 'e'},
            {5, 'f'},
            {6, 'g'},
            {7, 'h'},
        };

        _numToWhiteNum = new Dictionary<int, int>()
        {
            {0, 8},
            {1, 7},
            {2, 6},
            {3, 5},
            {4, 4},
            {5, 3},
            {6, 2},
            {7, 1},
        };

        Position = position;
        Number = blackPosition ? (int)position.Y + 1 : _numToWhiteNum[(int)position.Y];
        Letter = _numToChar[(int)position.X];
        Piece = null;
    }

    public override string ToString()
    {
        return $"{Letter.ToString().ToUpper()}{Number}";
    }
}