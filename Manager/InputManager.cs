using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Kyo.Manager;

public static class InputManager 
{
    public static event Action MouseLeftButtonPressed;

    private static MouseState _mouseState;
    private static MouseState _oldMouseState;

    public static Point MousePosition {get; private set;}
    public static bool LeftButtonPressed {get; private set;} = false;

    public static void BeginUpdate()
    {
        _oldMouseState = Mouse.GetState();
        LeftButtonPressed = false;
    }

    public static void EndUpdate()
    {
        _mouseState = _oldMouseState;
    }

    public static void Update(GameTime gameTime)
    {
        MousePosition = _mouseState.Position;
        if (_mouseState.LeftButton == ButtonState.Released && _oldMouseState.LeftButton == ButtonState.Pressed)
        {
            LeftButtonPressed = true;
            if (MouseLeftButtonPressed != null)
                MouseLeftButtonPressed();
        }
    }
}