using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch spriteBatch;
        int score;
        //Player
        public Texture2D playerTex;
        public Vector2 playerPos;
        public int playerSpeed = 10; //Player speed
        public Player player;
        //Enemies
        public Texture2D enemyTex;
        public Vector2 enemyPos;
        public float enemySpeedDown = 1f; //Enemy speed
        List<Enemy> enemiesList;
        Enemy enemy;
        Rectangle enemyRect;
        int enemyAmount = 10; //Amount of enemies
        //Bullets
        public Texture2D bulletTex;
        public Vector2 bulletPos;
        public int bulletSpeed = 10;
        List<Bullet> bulletList;
        Bullet bullet;
        private KeyboardState keystate;
        Rectangle bulletRect;

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
            //Loads the textures necessary and creates the lists required. 
            enemyAmount += 1;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTex = Content.Load<Texture2D>("Player");
            enemyTex = Content.Load<Texture2D>("alien02_sprites");
            bulletTex = Content.Load<Texture2D>("BulletPng");
            player = new Player(playerTex, playerPos, playerSpeed);
            enemiesList = new List<Enemy>();
            bulletList = new List<Bullet>();

            //Creating Enemies and their position in line, could make into a for-loop inside a for-loop, but this works for now (Hard coding). 
            for (int i = 0; i < enemyAmount -1; i++)
            {
                enemyPos.X += _graphics.PreferredBackBufferWidth / enemyAmount;
                Enemy b = new Enemy(enemyTex, enemyPos, enemySpeedDown);
                enemiesList.Add(b);
            }
            enemyPos.X = 0;
            for (int i = 0; i < enemyAmount - 1; i++)
            {
                enemyPos.Y = -50;
                enemyPos.X += _graphics.PreferredBackBufferWidth / enemyAmount;
                Enemy b = new Enemy(enemyTex, enemyPos, enemySpeedDown);
                enemiesList.Add(b);
            }
            enemyPos.X = 0;
            for (int i = 0; i < enemyAmount - 1; i++)
            {
                enemyPos.Y = -100;
                enemyPos.X += _graphics.PreferredBackBufferWidth / enemyAmount;
                Enemy e = new Enemy(enemyTex, enemyPos, enemySpeedDown);
                enemiesList.Add(e);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            Window.Title = ("Player Health: " + player.playerHealth + "   Score: " + score);
            //Creates a bullet when pressing "space"
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !keystate.IsKeyDown(Keys.Space))
            {
                Bullet b = new Bullet(bulletTex, player.pos, bulletSpeed);
                bulletList.Add(b);
                keystate = Keyboard.GetState();
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                keystate = Keyboard.GetState();
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // All player/enemy/bullet updates
            foreach (Enemy enemy in enemiesList)
            {
                enemy.Update(gameTime);
            }
            foreach (Bullet bullet in bulletList)
            {
                bullet.Update(gameTime);
            }
            player.Update(gameTime);
            base.Update(gameTime);
            
            //Bullet hits enemy reaction
            foreach (Bullet bullet in bulletList)
            {
                foreach (Enemy enemy in enemiesList)
                {
                    if (bullet.GetHitBox().Intersects(enemy.GetHitBox()))
                    {
                        score += 10;
                        enemiesList.Remove(enemy);
                        bullet.pos.Y = -100;
                        break;
                    }
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MidnightBlue);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            base.Draw(gameTime);
            //Checks if the player is alive, then draws out the player
            if (player.pIsAlive)
            {
                player.Draw(spriteBatch);
            }
            //Draws out the enemy incase it is still Alive
            foreach (Enemy enemy in enemiesList)
            {
                if (enemy.eIsAlive)
                {
                    enemy.Draw(spriteBatch);
                }
            }
            //Draws out the enemy incase it is still "Alive"
            foreach (Bullet bullet in bulletList)
            {
                if (bullet.isAlive)
                {
                    bullet.Draw(spriteBatch);
                }
            }
            spriteBatch.End();
        }
    }
}
