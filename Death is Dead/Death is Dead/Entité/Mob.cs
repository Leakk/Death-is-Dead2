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
    class Mob : Entity
    {
        int latenceTir = 0;

        public Mob(Vector2 position, Texture2D texture, int life)
            : base(position, texture, life, true)
        {
            this.life = life;
            Life = new Life();
        }

        public void Update(Obstacle[] rect,Player player)
        {
            
                base.Update(rect);
                //position.X += 1;
                Life.Udapte(life);

                if (life <= 0)
                {
                    HitboxB = new Collision(new Rectangle(0, 0, 0, 0));
                    HitboxD = new Collision(new Rectangle(0, 0, 0, 0));
                    HitboxG = new Collision(new Rectangle(0, 0, 0, 0));
                    HitboxH = new Collision(new Rectangle(0, 0, 0, 0));
                    dead = true;
                }

            if (position.X - player.position.X < 800 && position.X - player.position.X > -800)
            {
                if (HitboxD.is_coll(rect) || HitboxG.is_coll(rect))
                    velocity.Y = -6f;

                if (position.X < player.position.X)
                {
                    velocity.X = 2;
                    hasFliped = false;
                }
                if (position.X > player.position.X)
                {
                    velocity.X = -2;
                    hasFliped = true;
                }

                if (position.Y == player.position.Y && latenceTir == 0 && !dead)
                {
                    latenceTir = 40;
                    for (int i = 0; i < Tirs.Length; i++)
                    {
                        if (Tirs[i] == null)
                        {
                            Tirs[i] = new Projectile(texture.Bounds.X, 10, this, hasFliped);
                            Ressources.tir_son.Play();
                            break;
                        }
                    }
                }
                if (latenceTir > 0)
                    latenceTir--;

            }
        }

        public void Draw(SpriteBatch sb)
        {
            Life.Draw(sb, (int)position.X, (int)position.Y - 20,0.5f,5);

            if (hasFliped)
                sb.Draw(Ressources.PlayerFlip, new Vector2(position.X, position.Y), Color.White);
            else
                sb.Draw(Ressources.Player, new Vector2(position.X, position.Y), Color.White);

            //sb.Draw(Ressources.plateforme, HitboxB.Rectangle, Color.Red);
            //sb.Draw(Ressources.plateforme, HitboxD.Rectangle, Color.Red);
            //sb.Draw(Ressources.plateforme, HitboxG.Rectangle, Color.Red);
            //sb.Draw(Ressources.plateforme, HitboxH.Rectangle, Color.Red);
        }
    }
}
