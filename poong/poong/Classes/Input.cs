using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace poong
{
   public static class Input
    {
       public static List<GestureSample> Gestures;

       static Input()
       {
           Gestures = new List<GestureSample>();
       }

       public static void ProcessTouchInput(out Vector2 player1Velocity, out Vector2 player2Velocity)
       {
           Gestures.Clear();
           while (TouchPanel.IsGestureAvailable)
           {
               Gestures.Add(TouchPanel.ReadGesture());
           }
           player1Velocity = Vector2.Zero;
           player2Velocity = Vector2.Zero;

           foreach (GestureSample gestureSample in Gestures)
           {
               if (gestureSample.GestureType == GestureType.FreeDrag)
               {
                   // Left side of the screen == player1
                   if (gestureSample.Position.X >= 0 && gestureSample.Position.X <= Game1.ScreenWidth / 2)
                   {
                       player1Velocity.Y += gestureSample.Delta.Y;
                   }

                   //Right side for player2
                   if (gestureSample.Position.X >= Game1.ScreenWidth / 2 && gestureSample.Position.X <= Game1.ScreenWidth)
                   {
                       player2Velocity.Y += gestureSample.Delta.Y;
                   }
               }

            }
           
       }
    }
}
