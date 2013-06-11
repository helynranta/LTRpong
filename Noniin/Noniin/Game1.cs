using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using BasketBall;
using System;
using System.Collections.Generic;

namespace BasketBall
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        public static int ScreenWidth;
        public static int ScreenHeight;
        public int maxX;
        public int maxY;
        public float strength;
        public bool wasPressed;
        public float time;
        public float last_time;
        public bool isTouching;
   
        //Represents the player
        Player player = new Player();
        Vector2 direction;
        Vector2 tiilidirection;
        //Player tiili = new Player();
        List<Player> esteet = new List<Player>();



        //Mouse states for tracking mouse clicks
        MouseState CurrentMouseState;
        MouseState PreviousMouseState;
        Random random;

        //Keyboard test
        KeyboardState state;



        //Playersw movement speed
        float playerMoveSpeed;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            // TODO: Add your initialization logic here
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;
            maxX = GraphicsDevice.Viewport.Width;
            maxY = GraphicsDevice.Viewport.Height;
            player.Position = new Vector2(4f, ScreenHeight - player.Height);


            //Make the mouse visible
            IsMouseVisible = true;


            //Enable freedag gesture
            TouchPanel.EnabledGestures = GestureType.FreeDrag;

            //Setting the players move speed
            playerMoveSpeed = 8.0f;
            strength = 0;
            wasPressed = false;
            time = 5.0f;
            isTouching = false;

            tiilidirection = new Vector2(0, 2);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //Load player resources
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,
                GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            Vector2 tiiliPos = new Vector2(200, 50);

            player.Initialize(Content.Load<Texture2D>("basketball"), playerPosition);
           // tiili.Initialize(Content.Load<Texture2D>("hand"), tiiliPos);


            random = new Random();
            int x = 0;
            int y = 0;
            for (int i = 0; i < 7; i++)
            {
                x = random.Next((50 + 3 * i), 900);
                y = random.Next(1, 650);

                tiiliPos.X = x;
                tiiliPos.Y = y;
                Player j = new Player();
                j.Initialize(Content.Load<Texture2D>("hand"), tiiliPos);
                esteet.Add(j);
            }
      

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

       

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            //Screen adjust
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;
            UpdatePlayer(gameTime);
            base.Update(gameTime);    
        }

        //Updatin the player
        private void UpdatePlayer(GameTime gametime)
        {

            TouchCollection touches = TouchPanel.GetState();
            foreach (var touch in touches)
            {
                isTouching = true;
                Vector2 posDelta = touch.Position - player.Position;
                strength = posDelta.Length() * 0.1f;
                posDelta.Normalize();
                posDelta = posDelta * strength;
                direction = posDelta;
                wasPressed = true;
                if (touch.State == TouchLocationState.Released)
                    isTouching = false;
            }
           // direction.Normalize();
            

            //Windows 8 Touch gestures for MonoGame

            //while (TouchPanel.IsGestureAvailable)
            //{

            //    GestureSample gesture = TouchPanel.ReadGesture();

            //    if (gesture.GestureType == GestureType.FreeDrag)
            //    {
            //        player.Position += gesture.Delta;
            //    }
            //}


            //Moving with mouse
            
            CurrentMouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(CurrentMouseState.X, CurrentMouseState.Y);

            if (CurrentMouseState.LeftButton == ButtonState.Pressed)
            {
                
                Vector2 posDelta = mousePosition - player.Position;
                strength = posDelta.Length() * 0.01f;
                posDelta.Normalize();
                posDelta = posDelta * strength;
                direction = posDelta;
                wasPressed = true;
            }

            if (CurrentMouseState.LeftButton == ButtonState.Released  && wasPressed)
                player.Move(direction);

            if (player.Position.Y < (ScreenHeight - player.Height))
                direction.Y += 0.5f;

            //Hoping that the player/ball will stay on the screen
            if (player.Position.Y < -4)
                player.Position.Y = 1;
            if (player.Position.Y > ScreenHeight+10)
                player.Position.Y = ScreenHeight - 1;
            if (player.Position.X < -4)
                player.Position.X = 1;
            if (player.Position.X > ScreenWidth)
                player.Position.X = ScreenWidth - 1;


            // Limiting the space where player can touch the ball
            //if (isTouching == true && player.Position.X > ScreenWidth / 2)
            //    player.Position.X = (ScreenWidth / 2 -3) ;

            //if (isTouching == true && player.Position.Y < ScreenHeight / 2)
            //    player.Position.Y = (ScreenHeight / 2 - 3);

            //Slowing the horizontal speed of the ball
            if (isTouching == false && direction.X > 0)
            {
                direction.X -= 0.1f;
            }

            //slowing the vertical speed of the ball
            if (isTouching == false && direction.Y > 0)
            {
                direction.Y -= 0.08f;
            }


            // If the speed is low enough, let's stop moving
            if (Math.Abs(direction.X) < 0.4)
                direction.X = 0;
            if (Math.Abs(direction.Y) < 0.4)
                direction.Y = 0;


            //Limiting the max velocity of the player
            if (Math.Abs(direction.X) > 20)
            {
                if (direction.X < 0)
                    direction.X = -20;
                else
                    direction.X = 20;
            }
            if (Math.Abs(direction.Y) > 20)
            {
                if (direction.Y < 0)
                    direction.Y = -20f;
                else
                    direction.Y = 20;
            }
          
            // Checking that the player doesn't get OoutOfBounds
            if (player.Position.Y > (ScreenHeight - player.Height) || player.Position.Y < 2)
            {
                direction.Y *= -1f;
            }
            if (player.Position.X > (ScreenWidth - player.Width) || player.Position.X < 2)
            {
                direction.X *= -1f;
            }


            //Moving the hands / obstacles
            foreach (Player este in esteet)
            {
                este.Move(tiilidirection);
            }

            //Check if the ball hits the obstacles, then bounce off
            foreach (Player este in esteet)
            {
                Vector2 distance;
                distance = este.Position - player.Position;
                if (distance.Length() < 80f)
                {

                    direction.X *= -1.0005f;
                    direction.Y *= -1.0005f;
                }

            }
            foreach (Player este in esteet)
            {
                if (este.Position.Y > (ScreenHeight - este.Height) || este.Position.Y < 0)
                {
                    tiilidirection.Y *= -1f;
                }
            }

            //keeping track of time
            time += (float)gametime.ElapsedGameTime.TotalSeconds;

            //if (player.Position.X >= ScreenWidth || player.Position.X < -(2 * player.Width))
            //{
            //    player.Position.X = ScreenWidth / 2;
            //}
            //if (player.Position.Y >= ScreenHeight || player.Position.Y < 0)
            //{
            //    player.Position.Y = ScreenHeight / 2;
            //}


        }

       

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LawnGreen);

            // TODO: Add your drawing code here
            //Drawing the sprite / Ball
            _spriteBatch.Begin();
            player.Draw(_spriteBatch);
           // tiili.Draw(_spriteBatch);
            foreach (Player este in esteet)
            {
                este.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
