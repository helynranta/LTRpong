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


//The game uses Physics Helper XAML for all collision detection and for overall physics
// http://physicshelperxaml.codeplex.com/
//The project has been created with the Getting Started tutorial from Physics Helper XAML webpage
// http://physicshelperxaml.codeplex.com/documentation
namespace GamePS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {   
            this.InitializeComponent();
            //As the game is  started, create a few obstacles to the screen
            //this.CreateObs();


        }

        Random random = new Random();

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        ////Creates balls to the canvas
        //public void spawn()
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        Box bx = new Box();
        //        cnvGame.AddPhysicsUserControl(bx, 5, 0, 1);
        //    }
        //}

        ////actually this is the button that spawns three balls
        //public void spw(object sender, RoutedEventArgs e)
        //{
        //    spawn();
        //}

        //public Vector2 dir = new Vector2(2, 1);
        //public float length;
        //public void move(Vector2 direction)
        //{
        //    List<PhysicsSprite> physicsList = cnvGame.PhysicsObjects.Values.ToList();
        //    for (int i = 0; i < physicsList.Count; i++)
        //    {
        //        PhysicsSprite spr = physicsList[i];
        //        //dir = direction of the impulse vector, END_POINT - START_POINT
        //        //END_POINT = user touch location, START_POINT = ball location
        //        dir = direction - spr.Position;
        //        length = dir.Length();
        //        // let's make a unit vector out of it
        //        dir.Normalize();
        //        //dir *= 0.7f;
        //        //Apply force to the balls in the direction that userd touched on the screen
        //        spr.BodyObject.ApplyLinearImpulse(dir);
                
                
        //    }
        //}

        //// RoutedEventArgs e
        //public void uTouched(object sender, TappedRoutedEventArgs e )
        //{
        //    //pt is the position where th euser touched on the canvas
        //    Point pt = e.GetPosition(cnvGame);
        //    // create a vector2 of the point, to move the objects in the pressed location
        //    Vector2 suunta;
        //    suunta.X = (float)pt.X;
        //    suunta.Y = (float)pt.Y;

        //    move(suunta);
        //}

        ////Deletes all physics objects except walls/ground / obstacles
        //public void DeleteAll(object sender, RoutedEventArgs e )
        //{
        //    List<PhysicsSprite> lista = cnvGame.PhysicsObjects.Values.ToList();
        //    for (int i = lista.Count - 1; i >= 0; i--)
        //    {
        //        // keep the ground and walls!
        //        PhysicsSprite spr = lista[i];

        //        if (spr.Name != "ground" && (spr.Name.StartsWith("ground") == false))
        //            cnvGame.DeletePhysicsObject(spr.Name);
        //    }

        //    CreateObs();
        //}

        ////Creates obstacles in randomized locations
        //public void CreateObs()
        //{
        //    int x, y;
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Obstacle obs = new Obstacle();
                
        //        x = random.Next(1,(int)(cnvGame.ActualWidth-obs.ActualWidth));
        //        y = random.Next(1, (int)(cnvGame.ActualHeight-obs.ActualHeight));
        //        cnvGame.AddPhysicsUserControl(obs, x, y);
        //    }
        //}

        public void start_game(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Stage2));
        }
      
    }
}
