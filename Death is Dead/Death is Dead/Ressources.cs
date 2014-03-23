using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Death_is_Dead
{
    class Ressources
    {
        public static Texture2D Player;
        public static Texture2D PlayerFlip;
        public static Texture2D Tir;
        public static Texture2D TirFlip;
        public static Texture2D fond;
        public static Texture2D plateforme;
        public static Texture2D sol;
        public static Texture2D smoke;
        public static SoundEffect tir_son;
        public static SoundEffect button_sound;
        public static void Load(ContentManager content)
        {
            Player = content.Load<Texture2D>("sprite/player");
            PlayerFlip = content.Load<Texture2D>("sprite/playerFlip");
            Tir = content.Load<Texture2D>("sprite/tir");
            TirFlip = content.Load<Texture2D>("sprite/tirFlip");
            fond = content.Load<Texture2D>("sprite/cool");
            plateforme = content.Load<Texture2D>("sprite/plate");
            sol = content.Load<Texture2D>("sprite/sol");
            tir_son = content.Load<SoundEffect>("Sound_effects/tir_son");
            smoke = content.Load<Texture2D>("sprite/smoke");
            button_sound = content.Load<SoundEffect>("Sound_effects/Menu/button_sound");
           
        }
    }
}
