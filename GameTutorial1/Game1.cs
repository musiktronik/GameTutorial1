#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace GameTutorial1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Add sprites
        private Texture2D background;
        private Texture2D shuttle;
        private Texture2D earth;
        private Texture2D arrow;
        private float angle = 0; //Rotation angle in radians

        //Blending
        private Texture2D blue;
        private Texture2D green;
        private Texture2D red;
        private float blueAngle = 0;
        private float greenAngle = 0;
        private float redAngle = 0;

        private float blueSpeed = 0.025f;
        private float greenSpeed = 0.017f;
        private float redSpeed = 0.022f;

        private float distance = 100;


        //Add variables for scoring
        private SpriteFont font;
        private int score = 0;

        //For animation
        private AnimatedSprite animatedSprite;

        //Keyboard input
        private KeyboardState oldKeyState;

        //Mouse input
        private MouseState oldMouseState;
        private int mouseX = 0;
        private int mouseY = 0;


        Keys[] keyArray;

        //Constructor
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
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
            background = Content.Load<Texture2D>("stars"); // change these names to the names of your images
            shuttle = Content.Load<Texture2D>("shuttle");  // if you are using your own images.
            earth = Content.Load<Texture2D>("earth");
            
            font = Content.Load<SpriteFont>("Score"); // Use the name of your sprite font file here.

            Texture2D texture = Content.Load<Texture2D>("SmileyWalk");
            animatedSprite = new AnimatedSprite(texture, 4, 4);

            arrow = Content.Load<Texture2D>("arrow"); // use the name of your texture here, if you are using your own

            blue = Content.Load<Texture2D>("blue");
            green = Content.Load<Texture2D>("green");
            red = Content.Load<Texture2D>("red");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState newKeyState = Keyboard.GetState();  // get the newest state
            KeyboardState keyState = Keyboard.GetState();  // get the newest state
            MouseState mouseState = Mouse.GetState();
            MouseState newMouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            score++;
            animatedSprite.Update(); //Animate sprite

            angle += 0.01f; //Rotate arrow

            blueAngle += blueSpeed;
            greenAngle += greenSpeed;
            redAngle += redSpeed;

            //Keyboard input
            if (newKeyState.IsKeyDown(Keys.Left))
            {
                // do something here
                angle -= 0.02f; //Rotate arrow
            }


            // handle the input
            if (oldKeyState.IsKeyUp(Keys.Right) && newKeyState.IsKeyDown(Keys.Right))
            {
                // do something here
                // this will only be called when the key if first pressed
                blueAngle += blueSpeed * 10;
            }
            oldKeyState = newKeyState;  // set the new state as the old state for next time

            //Check control keys
            if ((keyState.IsKeyDown(Keys.LeftControl) || keyState.IsKeyDown(Keys.RightControl)) && keyState.IsKeyDown(Keys.C))
            {
                // do something here for when Ctrl-C is pressed
                score = 0;
            }

            Keys[] pressedKeys = keyState.GetPressedKeys();
            keyArray = pressedKeys;

            //Mouse input
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                // Do whatever you want here
                redAngle += redSpeed * 10;
            }

            if (newMouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released)
            {
                // do something here
                greenAngle += greenSpeed * 10;
            }
            oldMouseState = newMouseState; // this reassigns the old state so that it is ready for next time

            //Get mouse position
            mouseX = mouseState.X;
            mouseY = mouseState.Y;


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(Color.Black);

            //Colored balls (blending)
            Vector2 bluePosition = new Vector2(
                            (float)Math.Cos(blueAngle) * distance,
                            (float)Math.Sin(blueAngle) * distance);
            Vector2 greenPosition = new Vector2(
                            (float)Math.Cos(greenAngle) * distance,
                            (float)Math.Sin(greenAngle) * distance);
            Vector2 redPosition = new Vector2(
                            (float)Math.Cos(redAngle) * distance,
                            (float)Math.Sin(redAngle) * distance);

            Vector2 center = new Vector2(300, 140);

            // TODO: Add your drawing code here
            //spriteBatch.Begin();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            
            //Sprites
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.Draw(earth, new Vector2(400, 240), Color.White);
            spriteBatch.Draw(shuttle, new Vector2(450, 240), Color.White);

            //Score
            spriteBatch.DrawString(font, "Score: " + score.ToString(), new Vector2(100, 100), Color.White);
            //spriteBatch.DrawString(font, "Keys: " + keyArray, new Vector2(100, 200), Color.White);

            spriteBatch.DrawString(font, "Mouse: (" + mouseX.ToString() +"," + mouseY.ToString() +")", new Vector2(100, 150), Color.Red);

            //Rotating arrow
            Vector2 location = new Vector2(400, 240);
            Rectangle sourceRectangle = new Rectangle(0, 0, arrow.Width, arrow.Height);
            //Vector2 origin = new Vector2(0, 0);
            //Vector2 origin = new Vector2(arrow.Width / 2, arrow.Height);
            Vector2 origin = new Vector2(arrow.Width / 2, arrow.Height / 2);

            spriteBatch.Draw(arrow, location, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);

            spriteBatch.Draw(blue, center + bluePosition, Color.White);
            spriteBatch.Draw(green, center + greenPosition, Color.White);
            spriteBatch.Draw(red, center + redPosition, Color.White);

            spriteBatch.End();

            //SmileyWalk
            animatedSprite.Draw(spriteBatch, new Vector2(400, 200));

            base.Draw(gameTime);
        }
    }
}
