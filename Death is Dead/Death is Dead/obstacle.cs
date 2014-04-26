using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Death_is_Dead
{
    class obstacle
    {
        public Rectangle rectangle;
        public Texture2D texture;

        public obstacle(Rectangle rect, Texture2D text)
        {
            rectangle = rect;
            texture = text;
        }
    }
}
