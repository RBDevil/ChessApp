using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessApplication.UI
{
    class Button
    {
        public delegate void ClickAction();
        ClickAction clickAction;
        Texture2D texture;
        Vector2 position;
        public Button(Texture2D texture, Vector2 position, ClickAction clickAction)
        {
            this.position = position;
            this.clickAction = clickAction;
            this.texture = texture;
        }
        public void Update(Vector2 mousePosition, bool click)
        {
            //if click and hovered
            //clickAction?.Invoke();
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }
    }
}
