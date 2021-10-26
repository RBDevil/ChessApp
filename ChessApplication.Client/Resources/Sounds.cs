using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessApplication.Client.Resources
{
    public static class Sounds
    {
        public static Dictionary<string, SoundEffect> Container;

        public static void LoadSounds(ContentManager Content)
        {
            Container = new Dictionary<string, SoundEffect>();

            Container.Add("buttonClick", Content.Load<SoundEffect>("buttonClick"));
        }
    }
}
