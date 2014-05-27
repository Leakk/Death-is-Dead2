using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Death_is_Dead.Bonus
{
    class Coeur
    {
        Texture2D coeur = Ressources.Bonus_coeur;
        Color colour = new Color(255, 255, 255, 255);
        int x;
        int y;
        public bool exist = false;

      /*  public void coeur()
        {
            this.coeur = Ressources.Bonus_coeur;
        }*/

        public void Udapte_coeur(int x, int y,ref Player p1)
        {
            this.x = x;
            this.y = y;

            if (exist && (p1.position.X < x) && (p1.position.X + p1.texture.Width > x) && (p1.position.Y < y) && (p1.position.Y + p1.texture.Height > y))
            {
                p1.life += 50;
                exist = false;
            }

        }

        public void Draw(SpriteBatch sb,int x,int y)
        {
            if (exist)
            {
                Rectangle Rectcoeur = new Rectangle(x, y, 20, 22);
                sb.Draw(coeur, Rectcoeur, colour);
            }
        }
    }
}
