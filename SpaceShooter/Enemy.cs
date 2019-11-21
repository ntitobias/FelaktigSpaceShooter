using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{
    abstract class Enemy : PhysicalObject
    { 
        public Enemy(Texture2D texture, float X, float Y, float speedX, float speedY)
            : base(texture, X, Y, speedX, speedY)
        {
        }

        public abstract void Update(GameWindow window);
    }

    class Mine : Enemy
    {
        public Mine(Texture2D texture, float X, float Y)
            : base(texture, X, Y, 16f, 1f)
        {
        }

        public override void Update(GameWindow window)
        {
            //Flytta på fienden
            position.X += speed.X;
            position.Y += speed.Y;
            //Kontrollera så att den inte åker utanför kanten
            if (position.X > window.ClientBounds.Width - gfx.Width || position.X < 0)
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

    class Tripod : Enemy   //7. Ändra arvet från PhysicalObject till Enemy för att få polymorfin att fungera.
    {
        public Tripod(Texture2D texture, float X, float Y) 
            : base(texture, X, Y, 0f, 3f) //5. Ändra från 30f till exempelvis 3f för vettig hastighet. Detta fel upptäcks dock inte förrän fel 7 är åtgärdat.
        {
        }
        public override void Update(GameWindow window) //7. Lägg till override
        {
            //Flytta på fienden
            position.Y += speed.Y;
            //Gör fienden inaktiv om den åker ut där nere
            if (position.Y > window.ClientBounds.Height)
            {
                IsAlive = false;
            }
        }
    }
}
