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
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        //Button that spawns three balls. No, this is the spaw functions that creates the balls
        public void spawn()
        {
            for (int i = 0; i < 3; i++)
            {
                Box bx = new Box();
                cnvGame.AddPhysicsUserControl(bx, 5, 0, 1);
            }
        }

        //actually this is the button that spawns three balls
        public void spw(object sender, RoutedEventArgs e)
        {
            spawn();
        }

        public void move(Vector2 location)
        {
            List<PhysicsSprite> physicsList = cnvGame.PhysicsObjects.Values.ToList();
            for (int i = 0; i < physicsList.Count; i++)
            {
                PhysicsSprite spr = physicsList[i];
                //TODO: get the location where the player touches
                Vector2 dir = spr.BodyObject.Position - location;
                dir = location;
                spr.BodyObject.ApplyLinearImpulse(dir);
                
            }
        }


        public void uTouched(object sender, RoutedEventArgs e)
        {
            float x = 1f;
            float y = 1f;
            //TODO: Somwhow get the players touch location
            move(new Vector2(x, y));
        }

        public void DeleteAll(object sender, RoutedEventArgs e)
        {
            List<PhysicsSprite> lista = cnvGame.PhysicsObjects.Values.ToList();
            for (int i = lista.Count - 1; i >= 0; i--)
            {
                // keep the ground and walls!
                PhysicsSprite spr = lista[i];

                if (spr.Name != "ground" && (spr.Name.StartsWith("ground") == false))
                    cnvGame.DeletePhysicsObject(spr.Name);
            }
        }
        
    }
}
