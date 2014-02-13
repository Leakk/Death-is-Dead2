using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;


namespace Death_is_Dead
{
    class Physics_Engine
    {

        //Fields

        private float gravity;
        private int spritesize;
        private const float i = 1.5f;
        private const float j = 0;

        //Constructors

        public Physics_Engine(float gravity, int spritesize)
        {
            this.gravity = gravity;
            this.spritesize = spritesize;
        }

        //Get & Set

        public float Gravity
        {
            get { return gravity; }
            set { gravity = value; }
        }

        public int Spritesize
        {
            get { return spritesize; }
            set { spritesize = value; }
        }

        //Methods

        public Vector2 apply_gravity(Vector2 velocity)
        {
            Vector2 final_velocity = new Vector2(velocity.X, velocity.Y);
            final_velocity.Y = velocity.Y + i * gravity;
            return (final_velocity);
        }

        public bool touched_floor(Vector2 position)
        {
            return (position.Y >= 600 - spritesize);
        }


        //Update & Draw

    }
}
