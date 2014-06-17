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
        public static float isPlayerNearby(Players player, Mob mob)
        {
            return Math.Abs(player.position.X - mob.position.X);
        }

        public static bool isPlateformNearby(Obstacle[] rect, Mob mob, bool flip)
        {
            foreach (Obstacle item in rect)
            {
                if (item.texture.Width == Ressources.plateforme.Width && item.rectangle.Y > mob.position.Y - 42 && Math.Abs(item.rectangle.Center.X - mob.position.X) < 100 + item.texture.Width / 2 && (flip ? item.rectangle.X < mob.HitboxB.rect_coll(rect).X : item.rectangle.X > mob.HitboxB.rect_coll(rect).X))
                    return true;
            }

            return false;
        }

        public static bool isGroundNearby(Obstacle[] rect, Mob mob, bool flip)
        {
            foreach (Obstacle item in rect)
            {
                if (flip ? item.rectangle.Center.X == mob.HitboxB.rect_coll(rect).Center.X - Ressources.sol.Width : item.rectangle.Center.X == mob.HitboxB.rect_coll(rect).Center.X + Ressources.sol.Width)
                    return true;
            }

            return false;
        }

        public static Tuple<int, bool> isMobBefore(Players player, Mob mob)
        {
            if (player.position.X < mob.position.X)
                return Tuple.Create(-1, true);
            else
                return Tuple.Create(1, false);
        }

        public static bool PlayerAbove(Players player, Mob mob)
        {
            return player.position.Y <= mob.position.Y;
        }
    }
}
