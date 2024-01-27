using System;
using Kyo.Core;
using Kyo.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Kyo;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Board _board;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        Window.ClientSizeChanged += Window_ClientSizeChanged;
    }

    protected override void Initialize()
    {
        ScreenManager.Width = 800;
        ScreenManager.Height = 800;

        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 800;
        _graphics.ApplyChanges();

        _board = new Board();

        base.Initialize();
    }

    protected override async void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        await _board.Load(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        InputManager.BeginUpdate();
        InputManager.Update(gameTime);
        _board.Update(gameTime);

        InputManager.EndUpdate();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        _board.Draw(gameTime, _spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }


    private void Window_ClientSizeChanged(object sender, EventArgs e)
    {
        ScreenManager.Width = Window.ClientBounds.Width;
        ScreenManager.Height = Window.ClientBounds.Height;
    }
}
