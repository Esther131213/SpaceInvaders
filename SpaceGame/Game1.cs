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
using Microsoft.VisualBasic.Logging;
using SharpDX.Direct3D9;

namespace SpaceGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch spriteBatch;
        //Player
        public Texture2D playerTex;
        public Vector2 playerPos;
        public int playerSpeed = 10; //Player speed
        public Player player;
        //Enemies
        public Texture2D enemyTex;
        public Vector2 enemyPos;
        public int enemySpeedDown = 2; //Enemy speed
        List<Enemy> enemiesList;
        Enemy enemy;
        int enemyAmount = 10; //Amount of enemies
        int enemyTotalRows = 2;
        int rows;

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
            enemyPos = new Vector2(0, 0);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            enemyAmount += 1;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTex = Content.Load<Texture2D>("Player");
            enemyTex = Content.Load<Texture2D>("alien02_sprites");
            player = new Player(playerTex, playerPos, playerSpeed);
            enemiesList = new List<Enemy>();

            //Creating Enemies and their position in line
            for (int i = 0; i < enemyAmount -1; i++)
            {
                enemyPos.X += _graphics.PreferredBackBufferWidth / enemyAmount;
                Enemy b = new Enemy(enemyTex, enemyPos, enemySpeedDown);
                enemiesList.Add(b);
            }
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            foreach (Enemy enemy in enemiesList)
            {
                enemy.Update(gameTime);
            }
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
            foreach (Enemy enemy in enemiesList)
            {
                enemy.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
