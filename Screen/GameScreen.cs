using Kyo.Interfaces;
using Kyo.Manager;
using Kyo.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kyo.Screen;

public class GameScreen : IScreen
{
    private Board board;

    public void Initialize()
    {
        board = new Board();
    }

    public void Update(GameTime gameTime)
    {
        board.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        board.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }
}