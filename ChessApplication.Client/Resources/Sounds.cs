using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessApplication.Client.Resources
{
    public static class Sounds
    {
        public static Dictionary<string, SoundEffectInstance> Container;

        public static void LoadSounds(ContentManager Content)
        {
            Container = new Dictionary<string, SoundEffectInstance>();

            Container.Add("buttonClick", Content.Load<SoundEffect>("buttonClick").CreateInstance());
            Container.Add("pieceMovement", Content.Load<SoundEffect>("pieceMovement").CreateInstance());
            Container["pieceMovement"].Volume = 0.3f;
        }
    }
}
