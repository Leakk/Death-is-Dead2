using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Death_is_Dead
{
    [Serializable]
    class Obstacle
    {
        public Rectangle rectangle;
        [NonSerialized]
        public Texture2D texture;
        public uint[] tab;

        public Obstacle(Rectangle rect, Texture2D text)
        {
            rectangle = rect;
            texture = text;
            tab = new uint[texture.Height * texture.Width];
            text.GetData<uint>(tab);
        }
        public void maj(ContentManager co)
        {
            Texture2D x = co.Load<Texture2D>("sprite/sol");
            texture = new Texture2D(x.GraphicsDevice, tab.Length / 65, 65);
            texture.SetData<uint>(tab);
        }
    }
}
