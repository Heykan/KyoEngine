using System;
using System.Collections.Generic;
using Kyo.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kyo.Manager;

public static class ScreenManager
{
    private static Stack<IScreen> _listOfScreens;
    private static IScreen _currentScreen;

    public static int Width { get; set; }
    public static int Height { get; set; }

    public static Vector2 Scale {
        get => new((float)Width / (8 * Constants.Case.X), (float)Height / (8 * Constants.Case.Y));
    }

    public static void Initialize(IScreen startScreen) {
        _listOfScreens = new();

        _currentScreen = startScreen;
        _listOfScreens.Push(_currentScreen);

        _currentScreen.Initialize();
    }

    public static void Push(IScreen screen) {
        _listOfScreens.Push(screen);
    }

    public static void Pop() {
        _listOfScreens.Pop();
        _currentScreen = _listOfScreens.Peek();
    }


    public static void ChangeSCreen() {
        if (_listOfScreens.Peek() != _currentScreen) {
            _currentScreen = _listOfScreens.Peek();

            _currentScreen.Initialize();
        }
    }

    public static void Update(GameTime gameTime) {
        _currentScreen?.Update(gameTime);
    }

    public static void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        _currentScreen?.Draw(gameTime, spriteBatch);
    }
}