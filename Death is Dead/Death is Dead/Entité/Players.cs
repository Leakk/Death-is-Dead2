﻿using System;
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
    [Serializable]
    class Players : Entity
    {
        [NonSerialized]
        public float position_Y_texture;

        [NonSerialized]
        int k = 5;
        [NonSerialized]
        public Texture2D Faux = Ressources.Faux;
        [NonSerialized]
        public Vector2 faux_pos = new Vector2(0, 0);
        [NonSerialized]
        public Vector2 tmp = new Vector2(0, 300);
        [NonSerialized]
        public float Faux_rotation;
        public bool CurrentWeaponIsFaux = false;   /* ça par contre sa doit etre SERIALIZED */
        [NonSerialized]
        public bool attackFaux_animation_ground = false;
        [NonSerialized]
        public bool attackFaux_animation_air = false;
        [NonSerialized]
        public int attackFaux_animation1 = 0;
        [NonSerialized]
        public int attackFaux_animation2 = 0;
        [NonSerialized]
        public int latence_Faux;
        [NonSerialized]
        public Rectangle Faux_damageBox_ground = new Rectangle();
        [NonSerialized]
        public Rectangle Faux_damageBox_air = new Rectangle();
        [NonSerialized]
        public bool Faux_damageBox_ground_isActivate = false;
        [NonSerialized]
        public bool Faux_damageBox_air_isActivate = false;


        public Players(Vector2 position, Texture2D texture, int life,Boolean faux)
            : base(position, texture, life, true)
        {
            Tirs = new Projectile[10];
            position_Y_texture = position.Y;
            this.life = life;
            Life = new Life();
            CurrentWeaponIsFaux = faux;
        }


        public void Update2(Obstacle[] rect, KeyboardState keyboard)
        {
            #region /*Flottement*/
            if (k < 80)
            {
                if (k < 40)
                {
                    position_Y_texture = position.Y - k * 0.25f;
                    k++;
                }
                else
                {
                    position_Y_texture = position.Y + (k * 0.25f) - 20;
                    k++;
                }
            }
            else
                k = 0;
            #endregion      
           

            if (position.X < 0)
                position.X = 0;
            if (position.X > 800 - texture.Width)
                position.X = 800 - texture.Width;
            base.Update(rect);
            Life.Udapte(life);

            if (position.X <= 4 && HitboxD.is_coll(rect) || life <= 0 || position.Y > 600)
                dead = true;
            else
                dead = false;


        }


    }
}
