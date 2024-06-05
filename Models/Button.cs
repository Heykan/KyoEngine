using System;
using Kyo.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kyo.Models;

public enum ButtonCondition {
    None,
    Hovered,
    Pressed
}

public class Button {
    public ButtonCondition State { get; protected set; }
    protected Sprite2D sprite;

    public event EventHandler Click;
    public event EventHandler Hover;
    public event EventHandler UnHover;

    public Color Color
    {
        get { return sprite.Color; }
        set { sprite.Color = value; }
    }

    public Rectangle Bounds
    {
        get { return sprite.Bounds; }
        set { sprite.Bounds = value; }
    }

    public float Angle
    {
        get { return sprite.Angle; }
        set { sprite.Angle = value; }
    }
    public Texture2D Texture
    {
        set
        {
            sprite.Texture = value;
        }
    }

    public Button(Sprite2D sprite) {
        this.sprite = sprite;
    }

    public virtual void Update() {
        if (Contains(InputManager.MousePosition)) {
            if (InputManager.LeftButtonPressed) {
                State = ButtonCondition.Pressed;
                OnClick(EventArgs.Empty);
            }
            else if (State != ButtonCondition.Hovered) {
                if (State == ButtonCondition.None)
                    OnHover(EventArgs.Empty);
                State = ButtonCondition.Hovered;
            }
        }
        else if (State != ButtonCondition.None) {
            OnUnHover(EventArgs.Empty);
            State = ButtonCondition.None;
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch) {
        sprite.Draw(spriteBatch);
    }

    protected virtual void OnClick(EventArgs e)
        {
            EventHandler handler = Click;
            handler?.Invoke(this, e);
        }

        protected virtual void OnHover(EventArgs e)
        {
            EventHandler handler = Hover;
            handler?.Invoke(this, e);
        }

        protected virtual void OnUnHover(EventArgs e)
        {
            EventHandler handler = UnHover;
            handler?.Invoke(this, e);
        }

        public bool Contains(Vector2 pos)
        {
            return sprite.Contains(pos);
        }

        public bool Contains(Point pos)
        {
            return sprite.Contains(pos);
        }

        public void Center(Rectangle bounds)
        {
            sprite.Center(bounds);
        }
}