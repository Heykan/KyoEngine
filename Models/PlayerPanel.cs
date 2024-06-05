using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Kyo.Models;

public class PlayerPanel {
    private List<Piece> _panel;

    public PlayerPanel() {
        _panel = new List<Piece>();
    }

    public int GetMarkedIndex() {
        for (int i = 0; i < _panel.Count; i++) {
            if (_panel[i].MarkedState == PieceState.Marked)
                return i;
        }
        return -1;
    }

    public void SetMarked(int id) {
        if (id >= _panel.Count)
            throw new IndexOutOfRangeException();

        for (int i = 0; i < _panel.Count; i++) {
            if (i != id)
                _panel[i].UnMark();

            _panel[id].Mark();
        }
    }

    public void UnmarkAll() {
        for (int i = 0; i < _panel.Count; i++) {
            _panel[i].UnMark();
        }
    }

    public Piece GetMarked() {
        int id = GetMarkedIndex();

        if (id == -1)
            return _panel[0];

        return _panel[id];
    }

    public void Add(Piece piece) {
        _panel.Add(piece);
        _panel[_panel.Count - 1].AllowClickToUnmark = false;
    }

    public void Remove(Piece piece) {
        _panel.Remove(piece);
        UnmarkAll();
    }

    public Piece this[int index] {
        get {
            if (_panel.Count <= index)
                throw new IndexOutOfRangeException();
            
            return _panel[index];
        }

        set {
            if (_panel.Count >= index)
                throw new IndexOutOfRangeException();

            _panel[index] = value;
            _panel[index].AllowClickToUnmark = false;
        }
    }

    public int Count => _panel.Count;

    public void Update() {
        int marked = -1;
        for (int i = 0; i < _panel.Count; i++) {
            Piece piece  = _panel[i];
            PieceState state = piece.MarkedState;
            piece.Update();
            if (piece.MarkedState != state)
                marked = i;
        }
        if (marked != -1) {
            for (int i = 0; i < _panel.Count; i++) {
                if (i != marked)
                    _panel[i].UnMark();
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch) {
        foreach(var piece in _panel)
            piece.Draw(spriteBatch);
    }
}