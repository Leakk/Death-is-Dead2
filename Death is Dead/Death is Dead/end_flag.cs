using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Death_is_Dead
{
    class end_flag
    {
        Texture2D flag = Ressources.Flag_final;
        Color colour = new Color(255, 255, 255, 255);
        int x;
        int y;
        public bool Win = false;

        public end_flag(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Udapte(Players p1,  Players p2)
        {
            this.x -= 1;
           
            Rectangle rec = new Rectangle(x, y, 70, 160);
            Rectangle rec2 = new Rectangle((int)p1.position.X, (int)p1.position.Y, p1.texture.Width, p1.texture.Height);
            Rectangle rec3 = new Rectangle((int)p2.position.X, (int)p2.position.Y, p2.texture.Width, p2.texture.Height);

            if (rec.Intersects(rec2))
            {
                Win = true;
            }
            if (rec.Intersects(rec3))
            {
                Win = true;
            }

        }

        public void Draw(SpriteBatch sb)
        {

            Rectangle Rect_flag = new Rectangle(x, y, 70, 160);
            sb.Draw(flag, Rect_flag, colour);

        }
    }
}
 