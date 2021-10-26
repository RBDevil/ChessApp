using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessApplication.Client.UI
{
    public class _UI
    {
        List<Button> buttons;
        public _UI()
        {
            buttons = new List<Button>();
        }

        public void AddButton(Texture2D texture, Point position, Button.ClickAction clickAction)
        {
            buttons.Add(new Button(texture, position, clickAction));
        }

        public void Update()
        {
            foreach (Button button in buttons)
            {
                button.Update(MouseManager.Position, MouseManager.LeftClick);
            }
        }
        public void Draw(SpriteBatch sb)
        {
            foreach (Button button in buttons)
            {
                button.Draw(sb);
            }
        }
    }
}
