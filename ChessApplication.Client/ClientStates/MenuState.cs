using System;
using System.Collections.Generic;
using System.Text;
using ChessApplication.Client.Resources;
using ChessApplication.Client.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChessApplication.Client.ClientStates
{
    static class MenuState
    {
        static bool whiteAI = false;
        static bool blackAI = true;
        public static bool Active { get; private set; }
        static _UI UI;

        public static void InitializeUI()
        {
            UI = new _UI();
            UI.AddButton(Textures.Container["button"], new Point(100, 100), PlayButton);
            UI.AddButton(Textures.Container["button"], new Point(200, 100), ChangeSideButton);
        }
        static void PlayButton()
        {
            MenuState.End();
            GameState.Begin(whiteAI, blackAI);
        }
        static void ChangeSideButton()
        {
            whiteAI = !whiteAI;
            blackAI = !blackAI;
        }
        public static void Begin()
        {
            Active = true;
        }
        public static void End()
        {
            Active = false;
        }

        public static void Update()
        {
            UI.Update();
        }
        public static void Draw(SpriteBatch sb)
        {
            UI.Draw(sb);
        }
    }
}
