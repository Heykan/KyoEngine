using System;
using System.Collections.Generic;

namespace Kyo.Models;

public class Kyobot {
    private static Kyobot _instance;
    public static Kyobot Instance {
        get {
            if (_instance == null) _instance = new();
            return _instance;
        }
    }

    private List<Piece> _pieces;

    public Kyobot() {
        _pieces = new();
    }

    public void Update(PlayerPanel panel, Board board) {
        _pieces.Clear();
        foreach(var piece in panel.Panel) {
            if (piece.LegalMoves.Count > 0)
                _pieces.Add(piece);
        }

        if (_pieces.Count == 0) return;

        int rid = new Random().Next(0, _pieces.Count);
        int ridMove = new Random().Next(0, _pieces[rid].LegalMoves.Count);
        _pieces[rid].LegalMoves[ridMove].OnClick(EventArgs.Empty);
    }
}