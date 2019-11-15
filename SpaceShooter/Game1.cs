using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceShooter
{
    /// <summary>
    /// 
    /// Tobias version av bokens exempel.
    /// 
    /// Höstterminen 2019
    /// 
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //mina variabler
        Player player;
        PrintText printText;
        List<Enemy> enemies;
        List<GoldCoin> goldCoins;
        Texture2D goldCoinSprite;
        int enemiesCount = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            goldCoins = new List<GoldCoin>();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            player = new Player(this.Content.Load<Texture2D>("Sprites/ship"), 
                380, 400, 2.5f, 4.5f, this.Content.Load<Texture2D>("Sprites/bullet"));
            printText = new PrintText(Content.Load<SpriteFont>("myFont"));
            goldCoinSprite = Content.Load<Texture2D>("coin");

            //Skapa fiender
            enemies = new List<Enemy>();

            CreateEnemies();
        }

        private void CreateEnemies()
        {
            enemiesCount++;
            Random random = new Random();
            Texture2D tmpSprite = this.Content.Load<Texture2D>("Sprites/mine");
            for (int i = 0; i < enemiesCount; i++)
            {
                int rndX = random.Next(0, Window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, Window.ClientBounds.Height / 2);

                Mine temp = new Mine(tmpSprite, rndX, -rndY);
                enemies.Add(temp);
            }

            /*
            //Tripoder
            tmpSprite = this.Content.Load<Texture2D>("Sprites/tripod");
            for (int i = 0; i < enemiesCount; i++)
            {
                int rndX = random.Next(0, Window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, Window.ClientBounds.Height / 2);

                Tripod temp = new Tripod(tmpSprite, rndX, -rndY);
                enemies.Add(temp);
            }
            */
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update(Window, gameTime);

            //Gå igenom alla fiender
            foreach (Enemy e in enemies.ToList())
            {
                //Kontrollera om fienden kolliderar med ett skott
                foreach(Bullet b in player.Bullets)
                {
                    if (e.CheckCollision(b))
                    {
                        e.IsAlive = false;  //Döda fienden
                        b.IsAlive = false;  //Döda skottet
                        player.Points++;    //Ge poäng till spelaren
                    }
                }

                if (e.IsAlive)  //Kontrollera om fienden lever
                {   //Kontrollera kollision med spelaren
                    if (e.CheckCollision(player))
                        Exit();
                    e.Update(Window);   //Flytta på den
                }
                else //Ta bort fienden för den är död
                    enemies.Remove(e);
            }

            //Guldmynten ska uppstå slumpmässigt, en chans på 200.
            Random random = new Random();
            int newCoin = random.Next(1, 2000);
            if(newCoin == 1) // ok, nytt guldmynt ska uppstå
            {
                // Var ska guldmyntet uppstå?
                int rndX = random.Next(0, Window.ClientBounds.Width - goldCoinSprite.Width);
                int rndY = random.Next(0, Window.ClientBounds.Height - goldCoinSprite.Height);
                // Lägg till myntet i listan
                goldCoins.Add(new GoldCoin(goldCoinSprite, rndX, rndY, gameTime));
            }

            //Gå igenom listan med existerande guldmynt
            foreach(GoldCoin gc in goldCoins.ToList())
            {
                if (gc.IsAlive)
                {
                    //Kollar om guldmyntet har blivit för gammalt för att leva vidare
                    gc.Update(gameTime);

                    //Kontrollera om de kolliderat med spelaren
                    if (gc.CheckCollision(player))
                    {
                        //Ta bort myntet vid kollision
                        goldCoins.Remove(gc);
                        //Ge spelaren poäng
                        player.Points+=100;
                    }
                }
                else
                    //Ta bort guldmyntet för det är dött
                    goldCoins.Remove(gc);
            }

            // Skapa nya fiender ifall alla är döda
            if (enemies.Count < 1)
            {
                CreateEnemies();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();


            //Rita spelaren
            player.Draw(spriteBatch);

            //Rita fiendena
            foreach(Enemy e in enemies)
                e.Draw(spriteBatch);

            // Rita texter
            // För att kunna skriva svenska tecken (åäöÅÄÖ) behöver man ändra lite i
            // XML-filen som hänger ihop med din font. Leta efter taggen <END>.
            // Ändra där värdet till exempelvis 255.
            printText.Print("Poäng:" + player.Points, spriteBatch, 0, 0);
            printText.Print("Antal fiender:" + enemies.Count, spriteBatch, 0, 20);

            spriteBatch.End();

            //Rita guldmynt
            foreach (GoldCoin gc in goldCoins)
                gc.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
