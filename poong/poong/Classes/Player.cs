using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace poong
{
    public class Player : GameObject
    {
        public int score;

        public override void Move(Vector2 amount)
        {
            base.Move(amount);
            // Keep the player on the screen
            if (Position.Y <= 0)
                Position.Y = 0;
            if (Position.Y + Texture.Height >= Game1.ScreenHeight)
                Position.Y = Game1.ScreenHeight - Texture.Height;
        }
    }
}
