using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FarseerPhysics.Dynamics.Contacts;
using Spritehand.FarseerHelper;
using Microsoft.Xna.Framework;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GamePS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Stage2 : Page
    {
        int player_x, player_y;
        public int scores = 3;
        public bool Hitmonkey = false;
        DateTime _dtLastFpsUpdate;
        DateTime MankeeTime;  //monkeys movement time
        DateTime spawntime; // spawn time for rocks
        PhysicsSprite monkey;
        PhysicsSprite player;
        public Stage2()
        {
            this.InitializeComponent();
            cnvGame1.Collision += new PhysicsCanvas.CollisionHandler(cnvGame_Collision);
            cnvGame1.TimerLoop += new PhysicsCanvas.TimerLoopHandler(cnvGame_TimerLoop);
            //CreateObs();
            
        }

        public void init()
        {
            PhysicsSprite monkey_temp = cnvGame1.PhysicsObjects["mankee"];
            PhysicsSprite player_temp = cnvGame1.PhysicsObjects["ball1"];
            monkey = monkey_temp;
            player = player_temp;
            
        }

        void cnvGame_TimerLoop(object source)
        {
            player_y = (int)ball1.Position.Y;
            player_x = (int)ball1.Position.X;
            // this event is fired for EACH Timer tick of the simulation.

            //Monkey moves every 2 seconds
            if ((DateTime.Now - MankeeTime).TotalMilliseconds > 2000)
            {
                
                mankee_move();
                
                MankeeTime = DateTime.Now;
            }
            //Display text time
            if ((DateTime.Now - _dtLastFpsUpdate).TotalMilliseconds > 50)
            {
                scorecount.Text = String.Format("Score: {0} {1},{2}", scores, player_x, player_y);
                _dtLastFpsUpdate = DateTime.Now;
            }

            //Spawn the rocks
            if ((DateTime.Now - spawntime).TotalMilliseconds > 3500)
            {
                spawn();
                spawntime = DateTime.Now;
            }
            
        }

        public void PlayerScores()
        {
            
            //this.Frame.Navigate(typeof(GameOver));
            scores++;
        }

        //To check if player hit mankee, any collectables, goal
        void cnvGame_Collision(PhysicsSprite sprite1, PhysicsSprite sprite2, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            // this event is fired for each sprite to sprite collision
            // taking into account CollisionGroup.
            if (sprite1.Name == "mankee" && sprite2.Name == "ball1")
            {
                Hitmonkey = true;
            }

            if (sprite1.Name == "ball1" && sprite2.Name == "mouth")
            {
                PlayerScores();
            }

            //if the player hits an obstacle, remove it
            if (sprite1.Name == "ball1" && sprite2.Name.StartsWith("obstacle"))
            {
                sprite2.IsStatic = false;
                cnvGame1.DeletePhysicsObject(sprite2.Name);
            }
            
            //if hit the floor
            if (sprite1.Name.StartsWith("rocks") && sprite2.Name == "ground")
            {
                cnvGame1.DeletePhysicsObject(sprite1.Name);
                scores--;
            }

            //if goes off the screen, scores
            if (sprite1.Name.StartsWith("rocks") && sprite2.Name.StartsWith("destroy"))
            {
                cnvGame1.DeletePhysicsObject(sprite1.Name);
                PlayerScores();
                
            }
            
        }
        

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        Random random = new Random();

        //Creates balls to the canvas
        public void spawn()
        {
            int x = random.Next(0, 1300);
            int y = random.Next(-100, -10);
            Box bx = new Box();
            cnvGame1.AddPhysicsUserControl(bx, x, y, 1);
            
        }

        //actually this is the button that spawns three balls
        public void spw(object sender, RoutedEventArgs e)
        {
            spawn();
        }

        public Vector2 dir = new Vector2(2, 1);
        public float length;
        public void move(Vector2 direction)
        {
            List<PhysicsSprite> physicsList = cnvGame1.PhysicsObjects.Values.ToList();
            for (int i = 0; i < physicsList.Count; i++)
            {
                PhysicsSprite spr = physicsList[i];
                //dir = direction of the impulse vector, END_POINT - START_POINT
                //END_POINT = user touch location, START_POINT = ball location
                dir = direction - spr.Position;
                length = dir.Length();
                // let's make a unit vector out of it
                dir.Normalize();
                //dir *= 0.7f;
                //Apply force to the balls in the direction that userd touched on the screen
                if (spr.Name == "ball1" || spr.Name == "ball2")
                    spr.BodyObject.ApplyLinearImpulse(dir);


            }
        }

        //Moves the monkey == mankee in random direction
        public void mankee_move()
        {
            
            //direction.Normalize();
            List<PhysicsSprite> physicsList = cnvGame1.PhysicsObjects.Values.ToList();
            for (int i = 0; i < physicsList.Count; i++)
            {
                PhysicsSprite spr = physicsList[i];
                if (spr.Name == "mankee" || spr.Name.StartsWith("mankee"))
                {
                    Vector2 pos;
                    pos.X = player_x;
                    pos.Y = player_y;
                    Vector2 direction = Vector2.Normalize(pos - spr.Position);
                    spr.BodyObject.ApplyLinearImpulse(direction);
                }
            }
        }

        // RoutedEventArgs e
        public void uTouched(object sender, TappedRoutedEventArgs e)
        {
            //pt is the position where th euser touched on the canvas
            Point pt = e.GetPosition(cnvGame1);
            // create a vector2 of the point, to move the objects in the pressed location
            Vector2 direction;
            direction.X = (float)pt.X;
            direction.Y = (float)pt.Y;
            
            
            move(direction);
        }

        //Deletes all physics objects except walls/ground / obstacles
        public void DeleteAll(object sender, RoutedEventArgs e)
        {
            List<PhysicsSprite> lista = cnvGame1.PhysicsObjects.Values.ToList();
            for (int i = lista.Count - 1; i >= 0; i--)
            {
                // keep the ground and walls!
                PhysicsSprite spr = lista[i];

                if (spr.Name != "ground" && (spr.Name.StartsWith("ground") == false))
                    cnvGame1.DeletePhysicsObject(spr.Name);
            }

            CreateObs();
        }

        //Creates obstacles in randomized locations
        public void CreateObs()
        {
            int x, y;
            for (int i = 0; i < 10; i++)
            {
                Obstacle obs = new Obstacle();

                x = random.Next(50, (int)(cnvGame1.ActualWidth - (obs.ActualWidth + right_wall.ActualWidth)));
                y = random.Next(50, (int)(cnvGame1.ActualHeight - (obs.ActualHeight + ground.ActualHeight)));
                cnvGame1.AddPhysicsUserControl(obs, x, y);
            }
        }

        public void go_back(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        public void AAA(object sender, RoutedEventArgs e)
        {
            //set_collision_groups();
            init();
            CreateObs();
        }

        //public void set_collision_groups()
        //{
        //    PhysicsSprite spr = cnvGame1.PhysicsObjects["ball1"];
        //    spr.BodyObject.CollisionCategories = FarseerPhysics.Dynamics.Category.Cat2;
        //    spr.BodyObject.CollidesWith = FarseerPhysics.Dynamics.Category.Cat2;
        //}

    }

}
