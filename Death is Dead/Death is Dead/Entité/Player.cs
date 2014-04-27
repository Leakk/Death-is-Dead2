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
    class Player : Entity
    {
        private Projectile[] tirs;
        float position_Y_texture;

        Random rnd = new Random();
        private Smoke[] smoke;
        float badrandom;

        private Life Life;
        public int life;

        public Player(Vector2 position, Texture2D texture, int life)
            : base(position, texture, life, true)
        {
            tirs = new Projectile[10];
            smoke = new Smoke[20];
            position_Y_texture = Convert.ToInt32(position.Y);
            this.life = life;
            Life = new Life();
        }

        public Projectile[] Tirs
        {
            get { return tirs; }
            set { tirs = value; }
        }
        public Smoke[] Smoke
        {
            get { return smoke; }
            set { smoke = value; }
        }

        int latenceTir = 0;
        int latenceSmoke = 0;
        int k = 5;

        public void Update(KeyboardState keyboard, Obstacle[] rect)
        {
            if (k < 80)
            {
                if (k < 40)
                {
                    position_Y_texture = Convert.ToInt32(position.Y) - k * 0.25f;
                    k++;
                }
                else
                {
                    position_Y_texture = Convert.ToInt32(position.Y) + (k * 0.25f) - 20;
                    k++;
                }
            }
            else
                k = 0;

            if (keyboard.IsKeyDown(Keys.Space) && hasJumped)
            {
                hasJumped = false;
                this.velocity.Y = -12f;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                Collision next_b = (new Collision(new Rectangle(HitboxD.Rectangle.X + 8, HitboxD.Rectangle.Y, HitboxD.Rectangle.Width, HitboxD.Rectangle.Height)));
                if (next_b.is_coll(rect) == false)
                    velocity.X = 8;
                else
                {
                    velocity.X = 0;
                }

                hasFliped = false;

            }
            else if (keyboard.IsKeyDown(Keys.Q))
            {
                if (HitboxG.is_coll(rect) == false)
                    velocity.X = -8;
                else
                    velocity.X = 0;

                hasFliped = true;
            }
            else
                velocity.X = velocity.X / 1.15f;

            if (position.X < 0)
                position.X = 0;

            if (keyboard.IsKeyDown(Keys.P) && latenceTir == 0)
            {
                latenceTir = 20;
                for (int i = 0; i < tirs.Length; i++)
                {
                    if (tirs[i] == null)
                    {
                        tirs[i] = new Projectile(texture.Bounds.X, 10, this, hasFliped);
                        Ressources.tir_son.Play();
                        break;
                    }
                }
            }
            if (latenceTir > 0)
                latenceTir--;

            #region/*Smoke*/
            if ((HitboxB.is_coll(rect)) && latenceSmoke == 0)
            {
                latenceSmoke = 5;
                for (int i = 0; i < smoke.Length; i++)
                {
                    if (smoke[i] == null)
                    {
                        if (((HitboxD.is_coll(rect)) | HitboxG.is_coll(rect)))
                        {
                            smoke[i] = new Smoke(texture.Bounds.X, badrandom, this);
                        }
                        else
                        {
                            smoke[i] = new Smoke(texture.Bounds.X, (velocity.X / 6) + badrandom, this);
                        }
                        if (badrandom > 0)
                        {
                            badrandom = -0.05f * rnd.Next(4, 8);
                        }
                        else
                        {
                            badrandom = 0.05f * rnd.Next(4, 8);
                        }

                        break;
                    }
                }
            }
            if (latenceSmoke > 0)
                latenceSmoke--;
            #endregion

            base.Update(rect);
        }

        public void Draw(SpriteBatch sb)
        {
            Life.Udapte(life);
            Life.Draw(sb, 10, 10);

            if (hasFliped)
                sb.Draw(Ressources.PlayerFlip, new Vector2(position.X, position_Y_texture), Color.White);
            else
                sb.Draw(Ressources.Player, new Vector2(position.X, position_Y_texture), Color.White);

            sb.Draw(Ressources.plateforme, HitboxB.Rectangle, Color.Red);
            sb.Draw(Ressources.plateforme, HitboxD.Rectangle, Color.Red);
            sb.Draw(Ressources.plateforme, HitboxG.Rectangle, Color.Red);
            sb.Draw(Ressources.plateforme, HitboxH.Rectangle, Color.Red);
        }

    }
}
