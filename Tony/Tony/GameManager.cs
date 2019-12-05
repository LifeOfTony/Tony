using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;

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
        int mapHeight;
        int mapWidth;



        //A list holding the tileset textures.
        private List<Texture2D> tileset;

        //A SpriteFont for displaying text.
        private SpriteFont font;

        //The current level number.
        private int level;

        private double mentalState;

        //The text to be displayed.
        public static string textOutput;
        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            tileset = new List<Texture2D>();
            textOutput = "";
            level = 0;
            mentalState = 100;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //These four lines set up the screen to fit the users monitor.
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
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
            var pp = GraphicsDevice.PresentationParameters;
            lightsTarget = new RenderTarget2D(
            GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
            mainTarget = new RenderTarget2D(
            GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);





            // Loads the SpriteFont 'textFont' from Content.
            font = Content.Load<SpriteFont>("textFont");

            // Creates a new ItemReader for the Items.xml file.
            ItemReader itemList = new ItemReader(@"Content\Items.xml");

            // Creates a new LevelReader for the testmap.xml file. 
            LevelReader currentLevel = new LevelReader(@"Content\newtestmap.tmx");

            mapWidth = currentLevel.width * currentLevel.tileWidth;
            mapHeight = currentLevel.height * currentLevel.tileHeight;
            

            // Creates all of the textures from the tileset.
            foreach(string currentTexture in currentLevel.tileset)
            {
                this.tileset.Add(Content.Load<Texture2D>(currentTexture));
            }

            int tileLayerNum = currentLevel.layers.Count;

            #region Tile creation

            // Creates all instances of Tile objects from the tileNumbers list.
            for (int i = 0; i < tileLayerNum; i++)
            {
                string layer = currentLevel.layers[i];
                List<string[]> currentLayer = currentLevel.tileSplitter(layer);
                for (int y = 0; y < currentLayer.Count; y++)
                {
                    string[] currentRow = currentLayer[y];
                    for (int x = 0; x < currentRow.Length; x++)
                    {
                        int textureNumber = Int32.Parse(currentRow[x]);
                        if (textureNumber == 0) break;
                        Vector2 position = new Vector2(x * currentLevel.tileWidth, y * currentLevel.tileHeight);
                        Vector2 size = new Vector2(currentLevel.tileWidth, currentLevel.tileHeight);
                        Sprite currentTile = new Sprite(position, size, i, tileset[textureNumber - 1]);
                        ObjectManager.addObject(currentTile);
                    }
                }

            }

            #endregion


            #region Object creation



            // Loops through the collider list to make colliders.
            foreach (XElement objectData in currentLevel.colliders)
            {
                // Data taken from the object element.
                Vector2 position = new Vector2(Int32.Parse(objectData.Attribute("x").Value), Int32.Parse(objectData.Attribute("y").Value));
                Vector2 size = new Vector2(Int32.Parse(objectData.Attribute("width").Value), Int32.Parse(objectData.Attribute("height").Value));

                // creates a new collider.
                Collider currentCollider = new Collider(position, size);
                ObjectManager.addObject(currentCollider);
            }

            // Loops through the interactors list to make interacable objects.
            foreach (XElement objectData in currentLevel.interactors)
            {
                // Data taken from the object element.
                Vector2 position = new Vector2(Int32.Parse(objectData.Attribute("x").Value), Int32.Parse(objectData.Attribute("y").Value));
                Vector2 size = new Vector2(Int32.Parse(objectData.Attribute("width").Value), Int32.Parse(objectData.Attribute("height").Value));
                string requires = "none";
                string gives= "none";

                // Uses the property element of the objectData to assign requires and gives.
                IEnumerable<XElement> properties = objectData.Element("properties").Elements();
                foreach (XElement property in properties)
                {
                    if (property.Attribute("name").Value == "Requires") requires = property.Attribute("value").Value;
                    if (property.Attribute("name").Value == "Gives") gives = property.Attribute("value").Value;
                }

                // Creates a standard InteractableObject and associated Sprite.
                if(objectData.Attribute("type").Value == "Interactable")
                {

                    InteractableObject currentObject = new InteractableObject(position, size, requires, gives, 1, tileset[Int32.Parse(objectData.Attribute("gid").Value) - 1]);
                    ObjectManager.addObject(currentObject);
                }


            }

            XElement playerData = currentLevel.player;
            {
                Vector2 position = new Vector2(Int32.Parse(playerData.Attribute("x").Value), Int32.Parse(playerData.Attribute("y").Value));
                Vector2 size = new Vector2(Int32.Parse(playerData.Attribute("width").Value), Int32.Parse(playerData.Attribute("height").Value));
                Player player = new Player(position, size, 1, tileset[Int32.Parse(playerData.Attribute("gid").Value) - 1], 1);
                ObjectManager.addObject(player);
            }

            #endregion



        }




        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

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



            // Clears the displayed text.
            textOutput = "";

            // Poll for current keyboard state
            KeyboardState state = Keyboard.GetState();

            // Finds the Player object from the Objects list.
            foreach(GameObject p in ObjectManager.Objects)
            {
                if(p is Player)
                {
                    // Calls any Player methods based on the Keyboard state.
                    Player player = (Player)p;
                    if (state.IsKeyDown(Keys.A)) player.move("A");
                    if (state.IsKeyDown(Keys.W)) player.move("W");
                    if (state.IsKeyDown(Keys.S)) player.move("S");
                    if (state.IsKeyDown(Keys.D)) player.move("D");
                    if (state.IsKeyDown(Keys.E)) player.interact();
                }
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

                float scale = 4f;

                float maskRadius = lightMask.Width / 2 * scale;
                Vector2 playerLocation = ObjectManager.player.getPosition();

                lightPosition = new Vector2(playerLocation.X - maskRadius, playerLocation.X - maskRadius);

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
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

                // Draws some text based on the textOutput variable.
                spriteBatch.DrawString(font, textOutput, new Vector2(750, 200), Color.White);

                // Draws all Drawable objects.
                foreach (Drawable drawable in ObjectManager.Drawables)
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
                spriteBatch.Draw(mainTarget, Vector2.Zero, Color.White);
                spriteBatch.End();
            }
            

            base.Draw(gameTime);
        }
    }
}
