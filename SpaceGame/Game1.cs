using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Drawing;
using SharpDX.MediaFoundation;

namespace SpaceGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch spriteBatch;
        public Texture2D playerTex;
        public Vector2 playerPos;
        public int playerSpeed;
        public Player player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges(); 
            playerPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, 800);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            playerTex = Content.Load<Texture2D>("Player");
            player = new Player(playerTex, playerPos, playerSpeed);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            player.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.MidnightBlue);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            base.Draw(gameTime);
            player.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
