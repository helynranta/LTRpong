using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

//This game has been made by following ertay shashko's blog http://ertayshashko.wordpress.com/2013/01/02/developing-2d-games-for-windows-8-using-monogame-part-fourpolishing/
//all the calsses etc except the particle engine which is from another tutorial (link in particle class)
//In case my commentation is unsufficient, go to the blog for more detailed explanation
//

namespace poong
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
        const int PADDLE_OFFSET = 70; // offset from the edges
        const float BALL_START_SPEED = 8f;
        const float SPIN = 2.5f;
        Player player1;
        Player player2;
        Ball ball;
        SpriteFont fonty;
        Texture2D middleTexture;  // Centerline
        ParticleEngine particleEngine; //Particle engine for the ball
        SoundEffectInstance music;
        


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            TouchPanel.EnabledGestures = GestureType.FreeDrag;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ScreenHeight = GraphicsDevice.Viewport.Height;
            ScreenWidth = GraphicsDevice.Viewport.Width;

            player1 = new Player();
            player2 = new Player();
            ball = new Ball();
            

            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //Load player textures
            player1.Texture = Content.Load<Texture2D>("Paddle");
            player2.Texture = Content.Load<Texture2D>("Paddle");

            //Set players positions
            player1.Position = new Vector2(PADDLE_OFFSET, ScreenHeight / 2 - player1.Texture.Height / 2);
            player2.Position = new Vector2(ScreenWidth - player2.Texture.Width - PADDLE_OFFSET, ScreenHeight / 2);

            //Load ball texture 
            ball.Texture = Content.Load<Texture2D>("Ball");
            ball.Launch(BALL_START_SPEED);

            //Load font
            fonty = Content.Load<SpriteFont>("Fonty");

            //Load middleline sprite
            middleTexture = Content.Load<Texture2D>("Middle");

            //ParticleEngines textures, from http://rbwhitaker.wikidot.com/2d-particle-engine-4
            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(Content.Load<Texture2D>("circle"));
            textures.Add(Content.Load<Texture2D>("star"));
            textures.Add(Content.Load<Texture2D>("diamond"));
            particleEngine = new ParticleEngine(textures, new Vector2(400, 240));
            ////////////////////////////////////////////////////////////////
            SoundManager.LoadSounds(Content);
            music = SoundManager.GameMusic.CreateInstance();
            music.IsLooped = true;
            music.Play();
            


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
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            // Keeping track of the screen changes
            ScreenHeight = GraphicsDevice.Viewport.Height;
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ball.Move(ball.Velocity);

            Vector2 player1TouchVelocity, player2TouchVelocity;
            Input.ProcessTouchInput(out player1TouchVelocity, out player2TouchVelocity);
            player1.Move(player1TouchVelocity);
            player2.Move(player2TouchVelocity);

            //NORMALIZE TOUCH VECTORS
            if (player1TouchVelocity.Y != 0)
            {
                player1TouchVelocity.Normalize();
            }
            if (player2TouchVelocity.Y != 0)
            {
                player2TouchVelocity.Normalize();
            }

            

            //Check ball paddle collision
            if (GameObject.CheckPaddleBallCollision(player1, ball))
            {
                ball.Velocity.X = Math.Abs(ball.Velocity.X);
                //add spin
                ball.Velocity += player1TouchVelocity * SPIN;
                //play sound
                SoundManager.PaddleBallCollisionSound.Play();
            }
            if (GameObject.CheckPaddleBallCollision(player2, ball))
            {
                ball.Velocity.X = -Math.Abs(ball.Velocity.X);
                //Add spin to the ball
                ball.Velocity += player2TouchVelocity * SPIN;
                //play sound
                SoundManager.PaddleBallCollisionSound.Play();
            }


            //Set the ball to the middle of the screen if either player scores
            if (ball.Position.X + ball.Texture.Width < 0)
            {
                ball.Launch(BALL_START_SPEED);
                player2.score++;
            }

            if (ball.Position.X > ScreenWidth)
            {
                ball.Launch(BALL_START_SPEED);
                player1.score++;
            }

            //Update the balls particles, location = balls´' location
            particleEngine.EmitterLocation = new Vector2(ball.Position.X + (ball.Texture.Width / 2), ball.Position.Y + (ball.Texture.Height / 2));
            particleEngine.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(middleTexture, new Rectangle(ScreenWidth / 2 - middleTexture.Width / 2, 0, middleTexture.Width, ScreenHeight), null, Color.White);
            _spriteBatch.DrawString(fonty, player1.score + "         " + player2.score, new Vector2(ScreenWidth / 2 - fonty.MeasureString(player1.score + "         " + player2.score).X / 2, 0), Color.BlueViolet);
            player1.Draw(_spriteBatch);
            player2.Draw(_spriteBatch);
            ball.Draw(_spriteBatch);
            particleEngine.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
