using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{
    class Enemy: PhysicalObject
    {

        public Enemy(Texture2D texture, float X, float Y)
            : base(texture, X, Y, 6f, 0.3f)
        {
        }


        public void Update(GameWindow window)
        {
            //Flytta på fienden
            position.X += speed.X;
            position.Y += speed.Y;
            //Kontrollera så att den inte åker utanför kanten
            if (position.X > window.ClientBounds.Width - gfx.Width|| position.X < 0)
            {
                speed.X *= -1;
            }
            //Gör fienden inaktiv om den åker ut där nere
            if (position.Y > window.ClientBounds.Height)
            {
                IsAlive = false;
            }
        }


    }
}
