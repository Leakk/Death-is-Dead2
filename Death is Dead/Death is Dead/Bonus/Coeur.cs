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
        public int x;
        public int y;
        public bool exist = false;
        public bool down = true;

        public void Udapte_coeur(int x, int y, ref Players p1, ref Players p2)
        {
            this.x = x;
            this.y = y;
            Rectangle rec = new Rectangle(x, y, 20, 20);
            Rectangle rec2 = new Rectangle((int)p1.position.X, (int)p1.position.Y, p1.texture.Width, p1.texture.Height);
            Rectangle rec3 = new Rectangle((int)p2.position.X, (int)p2.position.Y, p2.texture.Width, p2.texture.Height);

            if (exist && rec.Intersects(rec2))
            {
                if (p1.life <= 250)
                {
                    Ressources.son_ramassage_de_bonus.Play();
                    p1.life += 50;
                    exist = false;
                }
            }
            if (exist && rec.Intersects(rec3))
            {
                if (p2.life <= 250)
                {
                    Ressources.son_ramassage_de_bonus.Play();
                    p2.life += 50;
                    exist = false;
                }
            }

        }

        public void Draw(SpriteBatch sb)
        {
            if (exist)
            {
                if (colour.A >= 250) down = true;
                if (colour.A <= 50) down = false;
                if (down)
                {
                    colour.A -= 5;
                }
                else
                {
                    colour.A += 5;
                }

                Rectangle Rectcoeur = new Rectangle(x, y, 20, 22);
                sb.Draw(coeur, Rectcoeur, colour);
            }
        }
    }
}
