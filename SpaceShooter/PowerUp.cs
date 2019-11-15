using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    class GoldCoin:PhysicalObject
    {
        double timeToDie; //Hur länge guldmyntet lever kvar.

        public GoldCoin(Texture2D texture, float X, float Y, GameTime gameTime)
            : base(texture, X, Y, 0, 2f)
        {
            timeToDie = gameTime.TotalGameTime.TotalMilliseconds + 2000;
        }

        public void Update(GameTime gameTime)
        {
            //Döda guldmyntet om det är för gammalt
            if (timeToDie < gameTime.TotalGameTime.TotalMilliseconds)
                IsAlive = false;
        }
    }
}
