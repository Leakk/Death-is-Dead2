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
            #region/*Faux*/
            if (!hasFliped)
            {
                Faux_damageBox_ground.X = (int)position.X + 30;
                Faux_damageBox_ground.Y = (int)position.Y - 10;
                Faux_damageBox_ground.Width = 90;
                Faux_damageBox_ground.Height = 75;

                Faux_damageBox_air.X = (int)position.X + 15;
                Faux_damageBox_air.Y = (int)position.Y + 68;
                Faux_damageBox_air.Width = 85;
                Faux_damageBox_air.Height = 40;

            }
            else
            {
                Faux_damageBox_ground.X = (int)position.X - 60;
                Faux_damageBox_ground.Y = (int)position.Y - 10;
                Faux_damageBox_ground.Width = 90;
                Faux_damageBox_ground.Height = 75;

                Faux_damageBox_air.X = (int)position.X - 42;
                Faux_damageBox_air.Y = (int)position.Y + 68;
                Faux_damageBox_air.Width = 85;
                Faux_damageBox_air.Height = 40;
            }

            if (keyboard.IsKeyDown(Keys.P) && (CurrentWeaponIsFaux) && (latence_Faux == 0) && (!attackFaux_animation_ground) && (!attackFaux_animation_air))
            {
                if (HitboxB.is_coll(rect)) attackFaux_animation_ground = true;
                else attackFaux_animation_air = true;
            }
            #region /* attaque sur le sol */
            if (attackFaux_animation_ground)
            {
                latence_Faux = 30;

                if (attackFaux_animation1 < 4) /* la faux descend */
                {
                    Faux_damageBox_ground_isActivate = true;
                    Faux_rotation += 0.3f;
                    attackFaux_animation1 += 1;
                }
                else
                {
                    if (attackFaux_animation2 < 10) /* la faux remonte */
                    {
                        Faux_damageBox_ground_isActivate = false;
                        Faux_rotation -= 0.12f;
                        attackFaux_animation2 += 1;
                    }
                    else                          /* on remet les variables comme il faut pour une prochaine relecture */
                    {
                        attackFaux_animation1 = 0;
                        attackFaux_animation2 = 0;
                        attackFaux_animation_ground = false; /* animation terminé */
                    }
                }

            }
            #endregion
            #region /* attaque en l'air */
            if (attackFaux_animation_air)
            {
                latence_Faux = 25;

                if (attackFaux_animation1 < 6) /* la faux descend */
                {
                    Faux_damageBox_ground_isActivate = true;
                    Faux_damageBox_air_isActivate = true;
                    Faux_rotation += 0.4f;
                    attackFaux_animation1 += 1;
                }
                else
                {
                    if (attackFaux_animation2 < 16) /* la faux remonte */
                    {
                        Faux_damageBox_ground_isActivate = false;
                        Faux_damageBox_air_isActivate = false;
                        Faux_rotation -= 0.15f;
                        attackFaux_animation2 += 1;
                    }
                    else                          /* on remet les variables comme il faut pour une prochaine relecture */
                    {
                        attackFaux_animation1 = 0;
                        attackFaux_animation2 = 0;
                        attackFaux_animation_air = false; /* animation terminé */
                    }
                }

            }
            #endregion
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
