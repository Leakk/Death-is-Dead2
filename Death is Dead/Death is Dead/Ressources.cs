using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Death_is_Dead
{
    class Ressources
    {
        public static Texture2D Player;
        public static void Load(ContentManager content)
        {
            Player = content.Load<Texture2D>("sprite/player");
        }
    }
}
