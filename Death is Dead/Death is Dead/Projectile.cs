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
        private Texture2D texture;
        private Player tireur;

        public Projectile(int size, int speed, Player tireur)
        {
            this.size = size;
            this.velocity = new Vector2(speed, 0);
            this.tireur = tireur;
            this.is_collided = false;
            this.position = tireur.position;
            this.texture = Ressources.Tir;
        }

        public void Update()
        {
            this.position += this.velocity;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }
    }
}
