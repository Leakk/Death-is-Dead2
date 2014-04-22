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
    class scrolling
    {
        public Rectangle[] list;

        public scrolling(Rectangle[] rec)
        {
            list = rec;
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < list.Length; i++)
            {
                list[i].X =list[i].X-1;
            }
        }
    }
}
