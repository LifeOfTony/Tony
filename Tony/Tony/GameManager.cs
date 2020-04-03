using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
// using GeonBit UI elements
using GeonBit.UI;
using GeonBit.UI.Entities;


namespace Tony
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameManager : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public float screenWidth;
        public float screenHeight;
        private Camera camera;

        public static Texture2D lightMask;
        public static Effect effect1;
        RenderTarget2D lightsTarget;
        RenderTarget2D mainTarget;

        bool changeScreen;

        Vector2 lightPosition;

        //A list holding the tileset textures.
        private List<Texture2D> tileset;





        private float countDuration = 2f;
        private float currentTime = 0f;


        //The text to be displayed.
        public static string textOutput;

        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            tileset = new List<Texture2D>();
            changeScreen = false;

            

        }

 

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {


            UserInterface.Initialize(Content, BuiltinThemes.editor);

            //These four lines set up the screen to fit the users monitor.
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            //Create a Camera Object (ScreenWidth, ScreenHeight, Zoom Level)
            camera = new Camera(screenWidth, screenHeight, 1.5f);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            lightMask = Content.Load<Texture2D>("lightMask");
            effect1 = Content.Load<Effect>("lighteffect");

            // Creates a new ItemReader for the Items.xml file.
            ItemReader itemList = new ItemReader(@"Content\Items.xml");

            Controller.Initialize(Content);


            

            lightsTarget = new RenderTarget2D(
            GraphicsDevice,graphics.PreferredBackBufferWidth,graphics.PreferredBackBufferHeight);
            mainTarget = new RenderTarget2D(
            GraphicsDevice,graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            TextureManager.LoadTextures(Content);

            ObjectManager.SetLevels(Content);

        }




        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }


        public void ClearText(GameTime gameTime)
        {
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds; //Time passed since last Update() 

            if (currentTime >= countDuration)
            {
                currentTime -= countDuration;
                // Clears the displayed text.
                textOutput = "";
            }
        }





        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Controller.exit == true)
                Exit();

            ClearText(gameTime);

            Controller.Update(gameTime);

            
            // Poll for current keyboard state
            KeyboardState state = Keyboard.GetState();

            Player player = ObjectManager.currentLevel.Player;
            //update camera
            camera.follow(player);
            Input.CheckInputs();

            if(Input.isFullScreen != graphics.IsFullScreen)
            {
                graphics.ToggleFullScreen();
                graphics.ApplyChanges();
            }



            SaveNLoad saveI = new SaveNLoad();

            ObjectManager.Update(gameTime);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //Create lightsTarget RenderTarget.
            {

                float scale = 0.04f * ObjectManager.mentalState;
                if (scale < 1)
                {
                    scale = 1f;
                }

                float maskRadius = lightMask.Width / 2 * scale;
                Vector2 playerLocation = ObjectManager.currentLevel.Player.getPosition();

                lightPosition = new Vector2(playerLocation.X - maskRadius, playerLocation.Y - maskRadius);

                GraphicsDevice.SetRenderTarget(lightsTarget);
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
                spriteBatch.Draw(lightMask, lightPosition, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                spriteBatch.End();
            }

            //Create mainTarget RenderTarget.
            {
                GraphicsDevice.SetRenderTarget(mainTarget);
                GraphicsDevice.Clear(Color.Transparent);
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

                
                // Draws all Drawable objects.
                foreach (Drawable drawable in ObjectManager.currentLevel.Drawables)
                    drawable.Draw(spriteBatch);


                foreach(GameObject i in ObjectManager.currentLevel.Objects)
                {
                    if (i is Interactable && !(i is Event))
                    {
                        if (Detector.isTouching(i.getPosition(), i.getSize(), ObjectManager.currentLevel.Player.getPosition(), ObjectManager.currentLevel.Player.getSize(), 1))
                        {
                            Sprite speechSprite = new Sprite(new Vector2(i.getPosition().X + i.getSize().X, i.getPosition().Y - i.getSize().Y), new Vector2(16, 16), TextureManager.speechBubble, 0.4f);
                            speechSprite.Draw(spriteBatch);
                        }
                    }
                   
                }


                // Ends the spriteBatch.
                spriteBatch.End();
            }



            //Blend RenderTargets.
            {
                GraphicsDevice.SetRenderTarget(null);
                GraphicsDevice.Clear(Color.Black);

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: camera.Transform);

                effect1.Parameters["lightMask"].SetValue(lightsTarget);
                effect1.CurrentTechnique.Passes[0].Apply();
                Vector2 screenCentre = new Vector2(GraphicsDevice.PresentationParameters.BackBufferWidth / 2, GraphicsDevice.PresentationParameters.BackBufferHeight / 2);
                Vector2 levelOffset = new Vector2(screenCentre.X - (mainTarget.Width/2), screenCentre.Y - (mainTarget.Height/2));
                spriteBatch.Draw(mainTarget, levelOffset, Color.White);
                spriteBatch.End();
            }

            Controller.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
