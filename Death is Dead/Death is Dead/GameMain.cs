using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Death_is_Dead
{
    class GameMain
    {
        //Fields

        private Player player;

        //Constructors

        public GameMain()
        {
            player = new Player(new Vector2(350, 0), Ressources.Player);
        }

        // Get & Set

        //Methods

        //Update & Draw

        public void Update()
        {
            player.Update(Keyboard.GetState());
        }

        public void Draw(SpriteBatch sb)
        {
            player.Draw(sb);
        }

    }
}
