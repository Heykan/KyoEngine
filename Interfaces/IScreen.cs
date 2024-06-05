using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kyo.Interfaces;

public interface IScreen {
    void Initialize();
    void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    void Update(GameTime gameTime);
}