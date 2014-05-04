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
        Color colour = new Color(0, 0, 0, 255);
        Rectangle rectangleLife;
        private int hp;
        private float factor;


        public Life()
        {
            this.texture = Ressources.lifebar;
        }

        public void Udapte(int Life)
        {
            if (Life <= 100)
            {
                hp = Life;
                colour.G = (byte)(55 + (2 * Life));
                colour.R = (byte)(255 - (2 * Life));
            }
            else
            {
                hp = Life;
                colour.G = (byte)(55 + (2 * 100));
                colour.R = (byte)(255 - (2 * 100));
            }


        }
        public void Draw(SpriteBatch sb, int pos_x, int pos_y, float Factor_lenght, int thickness)
        {
            rectangleLife = new Rectangle(pos_x, pos_y, (int)(Factor_lenght*(float)hp), thickness);
            sb.Draw(texture, rectangleLife, colour);
        }


    }
}
