using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        Texture2D ship_texture; //rymdskeppets grafik
        Vector2 ship_vector; //rymdskeppets position
        Vector2 ship_speed; //rymdskeppets hastighet


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
            //skeppets startposition
            ship_vector = new Vector2(380, 400);

            //skeppets starthastighet
            ship_speed = new Vector2(1.5f, 1.5f);

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
            ship_texture = this.Content.Load<Texture2D>("Sprites/ship");

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

            //Tangentbordsstyrning
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                ship_vector.X += ship_speed.X;
            }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                ship_vector.X -= ship_speed.X;
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                ship_vector.Y += ship_speed.Y;
            }
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                ship_vector.Y -= ship_speed.Y;
            }

            /*
            //skeppets förflyttning
            ship_vector.X += ship_speed.X;

            //förhindra skeppet att åka utanför sidkanterna
            if (ship_vector.X < 0 || ship_vector.X >Window.ClientBounds.Width - ship_texture.Width)
            {
                ship_speed.X = ship_speed.X * -1;
            }

            ship_vector.Y += ship_speed.Y;
            //förhindra skeppet att åka utanför över- och underkanterna
            if (ship_vector.Y < 0 || ship_vector.Y > Window.ClientBounds.Height-ship_texture.Height)
            {
                ship_speed.Y = ship_speed.Y * -1;
            }
            */
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

            spriteBatch.Draw(ship_texture, ship_vector, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
