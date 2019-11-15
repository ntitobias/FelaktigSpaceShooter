using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace SpaceShooter
{
    class Player:PhysicalObject
    {
        int points = 0;

        List<Bullet> bullets;
        Texture2D bulletGfx;
        double timeSinceLastBullet;

        public Player(Texture2D gfx, float X, float Y, float speedX, float speedY, Texture2D bulletGfx)
            :base(gfx, X, Y, speedX, speedY)
        {
            bullets = new List<Bullet>();
            this.bulletGfx = bulletGfx;
            timeSinceLastBullet = 0;
        }

        public int Points { get { return points; } set { points = value; } }
        public List<Bullet> Bullets { get { return bullets; } }

        public void Update(GameWindow window, GameTime gameTime)
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

            //Spelaren vill skjuta
            if (keyboardState.IsKeyDown(Keys.P))
            {
                //Kontrollera om spelaren får skjuta
                if (gameTime.TotalGameTime.TotalMilliseconds > 
                    timeSinceLastBullet + 200)
                {
                    //Skapa skottet
                    Bullet temp = new Bullet(bulletGfx, 
                        position.X + gfx.Width / 2, position.Y);
                    bullets.Add(temp);
                    //Sätt timeSinceLastBullet till detta ögonblick
                    timeSinceLastBullet = gameTime.TotalGameTime.Milliseconds;
                }
            }

            //Flytta skotten
            foreach(Bullet b in bullets.ToList())
            {
                b.Update();

                if (!b.IsAlive)
                {
                    bullets.Remove(b);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gfx, position, Color.White);
            foreach (Bullet b in bullets)
                b.Draw(spriteBatch);
        }
    }

    class Bullet : PhysicalObject
    {
        public Bullet(Texture2D gfx, float X, float Y) 
            : base(gfx, X, Y, 0, 3f)
        {
        }

        public void Update()
        {
            position.Y += speed.Y;
            if (position.Y < 0)
                IsAlive = false;
        }
    }
}
