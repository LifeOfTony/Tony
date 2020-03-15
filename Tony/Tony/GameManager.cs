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


        public static Texture2D lightMask;
        public static Effect effect1;
        RenderTarget2D lightsTarget;
        RenderTarget2D mainTarget;

        Vector2 lightPosition;

        //A list holding the tileset textures.
        private List<Texture2D> tileset;

        //A SpriteFont for displaying text.
        private SpriteFont font;

        //The current level number.
        private int level;

        private int levels;

        private float countDuration = 2f;
        private float currentTime = 0f;


        //The text to be displayed.
        public static string textOutput;

        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            tileset = new List<Texture2D>();
            textOutput = "";
            level = 0;
            levels = 2;
            

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



            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            

            lightMask = Content.Load<Texture2D>("lightMask");
            effect1 = Content.Load<Effect>("lighteffect");


            // Loads the SpriteFont 'textFont' from Content.
            font = Content.Load<SpriteFont>("textFont");



            // Creates a new ItemReader for the Items.xml file.
            ItemReader itemList = new ItemReader(@"Content\Items.xml");

            Controller.Initialize(Content);

            // Creates a new LevelReader for the testmap.xml file. 
            LevelReader currentLevel = new LevelReader(@"Content\Levels\TestMapNew.tmx", Content, level);

            int mapWidth = currentLevel.width;
            int mapHeight = currentLevel.height;
            int tileWidth = currentLevel.tileWidth;
            int tileHeight = currentLevel.tileHeight;
            lightsTarget = new RenderTarget2D(
            GraphicsDevice, mapWidth * tileWidth, mapHeight * tileHeight);
            mainTarget = new RenderTarget2D(
            GraphicsDevice, mapWidth * tileWidth, mapHeight * tileHeight);

            Level newLevel = currentLevel.GetLevel();
            
            ObjectManager.Instance.CurrentLevel = newLevel;
            ObjectManager.Instance.AddLevel(newLevel);
            Pathfinder.CreateGrid(mapWidth, mapHeight, tileWidth, tileHeight);
            newLevel.setPaths();

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || Controller.exit == true)
                Exit();

            ClearText(gameTime);

            Controller.Update(gameTime);

            
            // Poll for current keyboard state
            KeyboardState state = Keyboard.GetState();

            // Calls any Player methods based on the Keyboard state.
            Player player = ObjectManager.Instance.CurrentLevel.Player;
            if (state.IsKeyDown(Keys.A)) player.move("A");
            if (state.IsKeyDown(Keys.W)) player.move("W");
            if (state.IsKeyDown(Keys.S)) player.move("S");
            if (state.IsKeyDown(Keys.D)) player.move("D");
            if (state.IsKeyDown(Keys.E)) player.interact();

            SaveItem saveI = new SaveItem();
            if (state.IsKeyDown(Keys.L)) saveI.save();
            if (state.IsKeyDown(Keys.P)) saveI.read();

            ObjectManager.Instance.MentalDecay(gameTime);

            foreach(Npc npc in ObjectManager.Instance.CurrentLevel.Npcs)
            {
                npc.Move();
            }
            

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

                float scale = 0.04f * ObjectManager.Instance.MentalState;
                if (scale < 1)
                {
                    scale = 1f;
                }

                float maskRadius = lightMask.Width / 2 * scale;
                Vector2 playerLocation = ObjectManager.Instance.CurrentLevel.Player.getPosition();

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
                foreach (Drawable drawable in ObjectManager.Instance.CurrentLevel.Drawables)
                    drawable.Draw(spriteBatch);

                // Ends the spriteBatch.
                spriteBatch.End();
            }



            //Blend RenderTargets.
            {
                GraphicsDevice.SetRenderTarget(null);
                GraphicsDevice.Clear(Color.Black);

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

                effect1.Parameters["lightMask"].SetValue(lightsTarget);
                effect1.CurrentTechnique.Passes[0].Apply();
                Vector2 screenCentre = new Vector2(GraphicsDevice.PresentationParameters.BackBufferWidth / 2, GraphicsDevice.PresentationParameters.BackBufferHeight / 2);
                Vector2 levelOffset = new Vector2(screenCentre.X - (mainTarget.Width/2), screenCentre.Y - (mainTarget.Height/2));
                spriteBatch.Draw(mainTarget, levelOffset, Color.White);
                spriteBatch.End();
            }

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            // Draws some text based on the textOutput variable.
            string text = "" + ObjectManager.Instance.MentalState;
            spriteBatch.DrawString(font, text , new Vector2(200, 200), Color.White);

            // Draws some text based on the textOutput variable.
            spriteBatch.DrawString(font, textOutput, new Vector2(750, 300), Color.White);

            spriteBatch.End();

            
            Controller.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
