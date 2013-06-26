using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace poong
{
    public class Ball : GameObject
    {
        public Vector2 Velocity;
        public Random random;

        public Ball()
        {
            random = new Random();
        }

        public void Launch(float speed)
        {
            Position = new Vector2(Game1.ScreenWidth / 2 - Texture.Width / 2, Game1.ScreenHeight / 2 - Texture.Height / 2);
            //Set the ball randomli +- 60 Degrees to right
            float rotation = (float)(Math.PI / 2 + (random.NextDouble() * (Math.PI / 1.5f) - Math.PI / 3));
            Velocity.X = (float)Math.Sin(rotation);
            Velocity.Y = (float)Math.Cos(rotation);

            //50-50 change left and right

            if (random.Next(2) == 1)
            {
                Velocity.X *= -1; // launches to the left
            }

            Velocity *= speed;
        }

        //Collision detection for top & bottom
        public void CheckWallCollision()
        {
            //Goes off the top, change Y-direction to opposite
            if (Position.Y < 0)
            {
                Position.Y = 0;
                Velocity.Y *= -1;
            }

            //Goes off the bottom 
            if (Position.Y + Texture.Height > Game1.ScreenHeight)
            {
                Position.Y = Game1.ScreenHeight - Texture.Height;
                Velocity.Y *= -1;
            }
        }

        public override void Move(Vector2 amount)
        {
            base.Move(amount);
            CheckWallCollision();
        }
    }
}
