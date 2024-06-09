using Kyo.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kyo.Models;


public class Sprite2D {
    public Color Color;
    public Texture2D Texture;

    private float _angle;
    public float Angle {
        get => _angle;
        set {
            _angle = value;
        }
    }

    private float _layer;
    public float Layer {
        get => _layer;
        set {
            _layer = value;
        }
    }

    private Vector2 _location;
    public Vector2 Location {
        get => _location;
        set {
            _location = value;
            _bounds.X = (int)_location.X;
            _bounds.Y = (int)_location.Y;
        }
    }

    private Rectangle _bounds;
    public Rectangle Bounds {
        get => _bounds;
        set {
            _bounds = value;
            _location = _bounds.Location.ToVector2();
        }
    }

    public int Width { get => _bounds.Width; set => _bounds.Width = value; }
    public int Height { get => _bounds.Height; set => _bounds.Height = value; }
    public bool Show { get; set; } = true;


    public Sprite2D(Texture2D texture) : this(texture, Rectangle.Empty)
    { }

    public Sprite2D(Texture2D texture, Rectangle rectangle) : this(texture, rectangle, Color.White)
    { }

    public Sprite2D(Texture2D texture, Vector2 location)
    {
        this.Texture = texture;
        this._bounds = Rectangle.Empty;
        this.Location = location;
        this.Color = Color.White;
    }

    public Sprite2D(Texture2D texture, Rectangle rectangle, Color color, float layer = 0)
    {
        this.Texture = texture;
        this._location = Vector2.Zero;
        this.Bounds = rectangle;
        this.Color = color;
        this.Layer = layer;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (Show)
        {
            Vector2 origin = Texture.Bounds.Center.ToVector2();
            Rectangle destRect = new Rectangle(_bounds.X + _bounds.Width / 2, _bounds.Y + _bounds.Height / 2, _bounds.Width, _bounds.Height);
            spriteBatch.Draw(Texture, destRect, null, Color, Angle, origin, SpriteEffects.None, Layer);
        }
    }

    public bool Contains(Vector2 pos)
    {
        if (Angle == 0)
            return Bounds.Contains(pos);
        Vector2 origin = _bounds.Center.ToVector2();
        Vector2 virtualPos = Vector2.Transform(pos - origin, Matrix.CreateRotationZ(-Angle)) + origin;

        if (_bounds.Contains(virtualPos))
            return true;
        return false;
    }

    public bool Contains(Point pos)
    {
        return Contains(pos.ToVector2());
    }

    public void Center(Rectangle boundaries) {
        Location = new Vector2((boundaries.Width / 2) - (Bounds.Width / 2) + boundaries.X, (boundaries.Height / 2) - (Bounds.Height / 2) + boundaries.Y);
    }
}