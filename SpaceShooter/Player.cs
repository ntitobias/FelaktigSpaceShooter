using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{
    //Står på sid 59 i boken

    class Player
    {
        Texture2D gfx;
        Vector2 position;
        Vector2 speed;

        public Player(Texture2D texture, float X, float Y, float speedX, float speedY)
        {
            this.gfx = texture;
            this.position.X = X;
            this.position.Y = Y;

        }

        public void Update(GameWindow window)
        {
            //Tangentbordsstyrning
            KeyboardState keyboardState = Keyboard.GetState();

            //Förflyttning i x-led
            if (position.X <= window.ClientBounds.Width - gfx.Width && position.X >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                {
                    position.X += speed.X;
                }
                if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                {
                    position.X -= speed.X;
                }
            }
            //Förflyttning i y-led
            if (position.Y <= window.ClientBounds.Height - gfx.Height && position.Y >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                {
                    position.Y += speed.Y;
                }
                if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                {
                    position.Y -= speed.Y;
                }
            }

            //Flytta tillbaka rymdskeppet om det hamnar utanför bildskärmen
            if (position.X < 0) position.X = 0;
            if (position.X > window.ClientBounds.Width - gfx.Width)
                position.X = window.ClientBounds.Width - gfx.Width;
            if (position.Y < 0) position.Y = 0;
            if (position.Y > window.ClientBounds.Height - gfx.Height)
                position.Y = window.ClientBounds.Height - gfx.Height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gfx, position, Color.White);
        }

    }
}
