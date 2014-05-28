using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Death_is_Dead
{
    class Ressources
    {
        public static Texture2D Player;
        public static Texture2D PlayerFlip;
        public static Texture2D Player2;
        public static Texture2D PlayerFlip2;
        public static Texture2D E2;
        public static Texture2D EFlip2;

        public static Texture2D Tir;
        public static Texture2D TirFlip;
        public static Texture2D fond;
        public static Texture2D plateforme;
        public static Texture2D sol;
        public static Texture2D smoke;
        public static Texture2D particule;
        public static Texture2D lifebar;
        public static Texture2D Bonus_coeur;
        public static SoundEffect tir_son;
        public static SoundEffect button_sound;
        public static void Load(ContentManager content)
        {
            #region /*Sprite*/
            E2 = content.Load<Texture2D>("sprite/E2");
            EFlip2 = content.Load<Texture2D>("sprite/EFlip");
            Player = content.Load<Texture2D>("sprite/player");
            PlayerFlip = content.Load<Texture2D>("sprite/playerFlip");
            Player2 = content.Load<Texture2D>("sprite/14");
            PlayerFlip2 = content.Load<Texture2D>("sprite/15");
            Tir = content.Load<Texture2D>("sprite/tir");
            TirFlip = content.Load<Texture2D>("sprite/tirFlip");
            fond = content.Load<Texture2D>("sprite/cool");
            plateforme = content.Load<Texture2D>("sprite/plate");
            sol = content.Load<Texture2D>("sprite/sol");
            smoke = content.Load<Texture2D>("sprite/smoke");
            particule = content.Load<Texture2D>("sprite/effet_swag");
            lifebar = content.Load<Texture2D>("sprite/Lifebar");
            Bonus_coeur = content.Load<Texture2D>("sprite/Bonus/coeur");
            #endregion

            #region /*Sound*/
            tir_son = content.Load<SoundEffect>("Sound_effects/tir_son");
            button_sound = content.Load<SoundEffect>("Sound_effects/Menu/button_sound");
            #endregion
        }
    }
}
