using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Death_is_Dead
{
    class Life
    {
        Texture2D texture;
        Color colour = new Color(0, 0,0, 255);
        Rectangle rectangleLife;


      public Life()
        {
            this.texture = Ressources.lifebar;
        }

        public void Udapte(int Life)
        {
            colour.G = (byte)(55+(2 * Life));
            colour.R = (byte)(255-(2 * Life));
            rectangleLife = new Rectangle(10, 10, Life, 15);
           

        }
        public void Draw(SpriteBatch sb)

        { 
       
            sb.Draw(texture, rectangleLife, colour);
        }


    }
}
