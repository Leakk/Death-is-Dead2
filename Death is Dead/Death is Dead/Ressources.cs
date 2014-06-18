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

        public static Random random_number = new Random();

        public static Texture2D Player;
        public static Texture2D PlayerFlip;
        public static Texture2D Player2;
        public static Texture2D PlayerFlip2;
        public static Texture2D E2;
        public static Texture2D EFlip2;

        public static Texture2D Tir;
        public static Texture2D TirFlip;
        public static Texture2D fond0;
        static Texture2D fond;
        public static Texture2D Fond
        {
            get { return fond; }
        }
        public static Texture2D fond2;
        public static Texture2D fond3;
        public static Texture2D plateforme;
        public static Texture2D sol;
        public static Texture2D plateforme2;
        public static Texture2D sol2;
        public static Texture2D plateforme3;
        public static Texture2D sol3;
        public static Texture2D smoke;
        public static Texture2D particule;
        public static Texture2D lifebar;
        public static Texture2D Bonus_coeur;
        public static Texture2D Faux;
        public static Texture2D Faux_flipped;
        public static Texture2D Flag_final;
        public static SoundEffect tir_son;
        public static SoundEffect button_sound;
        public static SoundEffect Faux_sound;
        public static SoundEffect impact_tir_enemi;
        public static SoundEffect impact_tir_mur;
        public static SoundEffect impact_tir_player;
        public static SoundEffect son_ramassage_de_bonus;
        public static SoundEffect Faux_impact_on_ennemy;
        public static Song Game_over_song;
        public static Song Victory;
        public static Song lvl2_song;

        public static Texture2D boutton;
        public static Texture2D boutton_flecheD;
        public static Texture2D boutton_flecheG;
        public static SpriteFont font;

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
            fond0= content.Load<Texture2D>("sprite/cool");
            fond = content.Load<Texture2D>("sprite/cool");
            fond2 = content.Load<Texture2D>("sprite/Fond2");
            fond3 = content.Load<Texture2D>("sprite/Fond3");
            plateforme = content.Load<Texture2D>("sprite/plate");
            sol = content.Load<Texture2D>("sprite/sol");
            plateforme2 = content.Load<Texture2D>("sprite/plate2");
            sol2 = content.Load<Texture2D>("sprite/Sol2");
            plateforme3 = content.Load<Texture2D>("sprite/Plate3");
            sol3 = content.Load<Texture2D>("sprite/Sol3");
            smoke = content.Load<Texture2D>("sprite/smoke");
            particule = content.Load<Texture2D>("sprite/effet_swag");
            lifebar = content.Load<Texture2D>("sprite/Lifebar");
            Bonus_coeur = content.Load<Texture2D>("sprite/Bonus/coeur");
            Faux = content.Load<Texture2D>("sprite/Faux");
            Faux_flipped = content.Load<Texture2D>("sprite/Faux_flipped");
            Flag_final = content.Load<Texture2D>("sprite/Flag");
            #endregion

            #region /*Sound*/
            tir_son = content.Load<SoundEffect>("Sound_effects/tir_son");
            button_sound = content.Load<SoundEffect>("Sound_effects/Menu/button_sound");
            Faux_sound = content.Load<SoundEffect>("Sound_effects/Faux_sound");
            impact_tir_enemi = content.Load<SoundEffect>("Sound_effects/Game/impact_tir_enemi");
            impact_tir_mur = content.Load<SoundEffect>("Sound_effects/Game/impact_tir_mur");
            impact_tir_player = content.Load<SoundEffect>("Sound_effects/Game/impact_tir_joueur");
            son_ramassage_de_bonus = content.Load<SoundEffect>("Sound_effects/Game/son_ramassage_de_bonus");
            Faux_impact_on_ennemy = content.Load<SoundEffect>("Sound_effects/Game/Impact_faux_on_ennemy");
            Game_over_song = content.Load<Song>("Sound_effects/gameover");
            Victory = content.Load<Song>("Sound_effects/victoire");
            lvl2_song = content.Load<Song>("Sound_effects/Game/lvl2_song");
            #endregion

            font = content.Load<SpriteFont>("myfont");
            boutton_flecheD = content.Load<Texture2D>("Button_flecheD");
            boutton_flecheG = content.Load<Texture2D>("Button_flecheG");
            boutton = content.Load<Texture2D>("Button");
        }
    }
}
