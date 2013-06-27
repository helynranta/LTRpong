using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace poong
{
    public static class SoundManager
    {
        public static SoundEffect BallWallCollisionSound;
        public static SoundEffect PaddleBallCollisionSound;
        public static SoundEffect GameMusic;

        public static void LoadSounds(ContentManager Content)
        {
            BallWallCollisionSound = Content.Load<SoundEffect>("BallWallCollision");
            PaddleBallCollisionSound = Content.Load<SoundEffect>("PaddleBallCollision");
            GameMusic = Content.Load<SoundEffect>("gamemusic");
        }
    }
}
