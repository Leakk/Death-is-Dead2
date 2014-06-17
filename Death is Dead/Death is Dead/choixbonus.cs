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
    class choixbonus : Mob
    {
        public choixbonus(Vector2 position, int choix)
            : base(position, Ressources.E2, 0)
        {
            if (choix == 1)
            {
                coeur.exist = true;
                coeur.x = (int)position.X;
                coeur.y = (int)position.Y;
            }
            else
            {
                Bonusfaux.exist = true;
                Bonusfaux.x = (int)position.X;
                Bonusfaux.y = (int)position.Y;
            }
            activer_bonus = false;
            type = choix;
            dead = true;

        }
        public void Update(Obstacle[] rect, Players player, Players p2)
        {
            base.Update(rect, player, p2);
        }
        public void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}
