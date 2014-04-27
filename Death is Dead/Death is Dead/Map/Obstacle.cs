using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Death_is_Dead
{
    class Obstacle
    {
        public Rectangle rectangle;
        public Texture2D texture;

        public Obstacle(Rectangle rect, Texture2D text)
        {
            rectangle = rect;
            texture = text;
        }
    }
}
