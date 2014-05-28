using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Death_is_Dead
{
    class Projectile
    {
        private int size;
        private bool is_collided;
        private Vector2 velocity;
        public Vector2 position;
        public Texture2D texture;
        private particule_tir[] effet_swag;
        private Entity tireur;
        int latenceParticule = 0;
        private bool hasFliped;

        public Projectile(int size, int speed, Entity tireur, bool hasFliped)
        {
            effet_swag = new particule_tir[5];
            this.size = size;
            if (tireur.hasFliped)
            {
                this.velocity = new Vector2(-speed, 0);
                this.texture = Ressources.TirFlip;
            }
            else
            {
                this.velocity = new Vector2(speed, 0);
                this.texture = Ressources.Tir;
            }
            this.hasFliped = hasFliped;
            this.tireur = tireur;
            this.is_collided = false;
            this.position = tireur.position;
        }

        public void Update()
        {
            this.position += this.velocity;
            if (latenceParticule == 0)
            {
                latenceParticule = 3;
                for (int i = 0; i < effet_swag.Length; i++)
                {
                    if (effet_swag[i] == null)
                    {
                        {
                            position.Y -= 17;
                            effet_swag[i] = new particule_tir(20, -2, position);
                            position.Y += 17;
                        }

                        break;
                    }
                    else
                    {
                        if (effet_swag[i].Alpha <= 10)
                        {
                            effet_swag[i] = null;
                        }
                    }
                }
            }
            for (int i = 0; i < effet_swag.Length; i++)
            {
                if (effet_swag[i] !=null)
                effet_swag[i].Update();
            }
            latenceParticule--;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);

            for (int i =0;i<effet_swag.Length;i++)
            {
                if (effet_swag[i] != null)
                {
                    effet_swag[i].Draw(sb);
                } 
            }

        }
    }
}
