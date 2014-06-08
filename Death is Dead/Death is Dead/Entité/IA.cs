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
    class IA
    {
        public static bool isPlayerNearby(Players player, Mob mob)
        {
            return Math.Abs(player.position.X - mob.position.X) <= 800;
        }

        public static bool isPlayerTooNearby(Players player, Mob mob)
        {
            return Math.Abs(player.position.X - mob.position.X) <= 50;
        }

        public static Tuple<int, bool> isMobBefore(Players player, Mob mob)
        {
            if (player.position.X < mob.position.X)
                return Tuple.Create(-1, true);
            else
                return Tuple.Create(1, false);
        }
    }
}
