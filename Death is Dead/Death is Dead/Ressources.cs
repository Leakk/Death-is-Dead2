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
        public static Texture2D Tir;
        public static Texture2D fond;
        public static Texture2D plateforme;
        public static void Load(ContentManager content)
        {
            Player = content.Load<Texture2D>("sprite/player");
            Tir = content.Load<Texture2D>("sprite/tir");
            fond = content.Load<Texture2D>("sprite/cool");
            plateforme = content.Load<Texture2D>("sprite/plate");
        }
    }
}
