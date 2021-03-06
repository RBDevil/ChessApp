using ChessApplication.Client.ClientStates;
using ChessApplication.Client.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ChessApplication.Client
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 107 * 8;
            _graphics.PreferredBackBufferHeight = 107 * 8;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Textures.LoadTextures(Content);
            Sounds.LoadSounds(Content);

            MenuState.InitializeUI();
            MenuState.Begin();
            
        }

        protected override void Update(GameTime gameTime)
        {
            UI.MouseManager.Update();
            
            if(MenuState.Active)
            {
                MenuState.Update();
            }
            if(GameState.Active)
            {
                GameState.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if (MenuState.Active)
            {
                MenuState.Draw(_spriteBatch);
            }
            if (GameState.Active)
            {
                GameState.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
