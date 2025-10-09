using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;


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
        public float enemySpeed = 1f; //Enemy speed
        Enemy[,] enemieArray;
        Enemy enemy;
        int enemyAmount = 6; //Amount of enemies per row
        int enemyRowAmount = 5; //Amount of enemy rows
        //Bullets
        public Texture2D bulletTex;
        public Vector2 bulletPos;
        public int bulletSpeed = 10;
        List<Bullet> bulletList;
        Bullet bullet;
        //GameState
        //GameOver
        bool GameOver = false;
        Texture2D gameOverText;
        Vector2 gameOverPos;
        Random rngPos = new Random();

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
            //GameState Textures
            //Game Over
            gameOverText = Content.Load<Texture2D>("game_over-2");
            gameOverPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, 300);

            //Game Running
            //Loads the textures necessary and creates the lists required. 
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTex = Content.Load<Texture2D>("Player");
            enemyTex = Content.Load<Texture2D>("alien02_sprites");
            bulletTex = Content.Load<Texture2D>("BulletPng");
            player = new Player(playerTex, playerPos, playerSpeed, this);
            enemieArray = new Enemy[enemyAmount, enemyRowAmount];
            bulletList = new List<Bullet>();
            //Creating Enemies and their position. (Also creates the seoerate lines of enemies)

            for (int r = 0; r < enemyRowAmount; r++)
            {
                for (int i = 0; i < enemyAmount; i++)
                {
                    enemyPos.Y = r * -50;
                    int enemyFrame = 500 / (enemyAmount + 1);
                    enemyPos.X = enemyFrame + i * enemyFrame;
                    enemieArray[i,r] = new Enemy(enemyTex,enemyPos, enemySpeed);
                    continue;
                }

                enemyPos.X = 0;
            }
        }

        public enum GameState
        {
            startScreen = 1,
            play = 2,
            gameOver = 3
        }

        GameState gameState = GameState.startScreen;

        public void CreateBullet()
        {
            Bullet b = new Bullet(bulletTex, player.pos, bulletSpeed);
            bulletList.Add(b);
        }

        protected override void Update(GameTime gameTime)
        {
            if (player.pIsAlive)
            Window.Title = ("Welcome to Space Invaders!  Player Health: " + player.playerHealth + "   Score: " + score);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // All player/enemy/bullet updates

            //Player takes damage if enemy reaches floor
            foreach (Enemy enemy in enemieArray)
            {
                if (enemy.pos.Y > 890 && enemy.eIsAlive)
                {
                    player.takeDamage();
                    enemy.eIsAlive = false;
                    break;
                }

                enemy.Update(gameTime);
            }

            foreach (Bullet bullet in bulletList)
            {
                bullet.Update(gameTime);
            }
            
            //Bullet hits enemy reaction
            foreach (Bullet bullet in bulletList)
            {
                foreach (Enemy enemy in enemieArray)
                {
                    if (bullet.GetHitBox().Intersects(enemy.GetHitBox()))
                    {
                        score += 10;
                        enemy.eIsAlive = false;
                        enemy.eHitBox.X = 3000;
                        bullet.pos.Y = -100;
                        break;
                    }
                }
            }

            player.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MidnightBlue);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            base.Draw(gameTime);

            if (GameOver == false)
            {
                if (player.playerHealth > 0)
                {
                    //Checks if the player is alive, then draws out the player
                    if (player.pIsAlive = true)
                    {
                        player.Draw(spriteBatch);
                    }
                    //Draws out the enemy incase it is still Alive
                    foreach (Enemy enemy in enemieArray)
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
                }
                else
                {
                    player.pIsAlive = false;
                    foreach (Enemy enemy in enemieArray)
                    {
                        enemy.eIsAlive = false;
                    }
                    GameOver = true;
                }
            }
            if (GameOver)
            {
                spriteBatch.Draw(gameOverText, gameOverPos, null, Microsoft.Xna.Framework.Color.White, 0, new Vector2(gameOverText.Width / 2, gameOverText.Height / 2), 1, SpriteEffects.None, 1);
            }

            spriteBatch.End();
        }
    }
}
