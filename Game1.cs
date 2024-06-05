using System;
using Kyo.Manager;
using Kyo.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Kyo;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        Window.ClientSizeChanged += Window_ClientSizeChanged;
    }

    protected override void Initialize()
    {
        ScreenManager.Width = 1024;
        ScreenManager.Height = 1024;

        _graphics.PreferredBackBufferWidth = ScreenManager.Width;
        _graphics.PreferredBackBufferHeight = ScreenManager.Height;
        _graphics.ApplyChanges();


        base.Initialize();
    }

    protected override async void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        await ContentService.Instance.LoadContent(Content, GraphicsDevice);

        ScreenManager.Initialize(new GameScreen());
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        InputManager.BeginUpdate();
        InputManager.Update(gameTime);
        ScreenManager.Update(gameTime);
        InputManager.EndUpdate();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        ScreenManager.Draw(gameTime, _spriteBatch);

        base.Draw(gameTime);
    }


    private void Window_ClientSizeChanged(object sender, EventArgs e)
    {
        ScreenManager.Width = Window.ClientBounds.Width;
        ScreenManager.Height = Window.ClientBounds.Height;
    }
}
