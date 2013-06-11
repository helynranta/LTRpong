using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace BasketBall
{
    class Player
    {
        public void Initialize(Texture2D texture, Vector2 position)
        {
            PlayerTexture = texture;

            //Starting position of the player
            Position = position;

            //Players state
            Active = true;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayerTexture, Position, Color.White);
        }

        public void Move(Vector2 amount)
        {
            Position += amount;
        }
        public void Movement(Vector2 amount)
        {
            Position += amount;
        }


        //PLayers sprite
        public Texture2D PlayerTexture;

        //Position of the player
        public Vector2 Position;

        //Players state
        public bool Active;

        //PLayers width
        public int Width
        {
            get{ return PlayerTexture.Width;}
        }
        //players height
        public int Height
        {
            get{ return PlayerTexture.Height;}
        }
    }

}
