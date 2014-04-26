using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Death_is_Dead
{
    class Collision
    {
        Rectangle rectangle;

        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }
        public Collision(Rectangle rect)
        {
            this.rectangle = rect;
        }


        public bool is_coll( obstacle[] liste)
        {
            foreach (obstacle item in liste)
            {
                if (rectangle.Intersects(item.rectangle) == true)
                    return true;
            }
            return false;
        }
        public Rectangle rect_coll( obstacle[] liste)
        {
            foreach (obstacle item in liste)
            {
                if (rectangle.Intersects(item.rectangle) == true)
                    return item.rectangle;
            }
            return new Rectangle(0,0,0,0);
        }

    }
}
