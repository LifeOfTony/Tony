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

        //A list holding the tileset textures.
        private List<Texture2D> tileset;

        //A SpriteFont for displaying text.
        private SpriteFont font;

        //The text to be displayed.
        public static string textOutput;
        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.tileset = new List<Texture2D>();
            textOutput = "";

            
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

            // Loads the SpriteFont 'textFont' from Content.
            font = Content.Load<SpriteFont>("textFont");

            // Creates a new ItemReader for the Items.xml file.
            ItemReader itemList = new ItemReader(@"Content\Items.xml");

            // Creates a new LevelReader for the testmap.xml file. 
            LevelReader currentLevel = new LevelReader(@"Content\testmap.xml");

            // Creates all of the textures from the tileset.
            foreach(string currentTexture in currentLevel.tileset)
            {
                this.tileset.Add(Content.Load<Texture2D>(currentTexture));
            }

            // Creates all instances of Tile objects from the tileNumbers list.
            for (int y = 0; y < currentLevel.tileNumbers.Count; y++)
            {
                string[] currentRow = currentLevel.tileNumbers[y];
                for(int x = 0; x < currentRow.Length; x++)
                {
                    Vector2 position = new Vector2(x * currentLevel.tileWidth, y * currentLevel.tileHeight);
                    Vector2 size = new Vector2(currentLevel.tileWidth, currentLevel.tileHeight);
                    Tile currentTile = new Tile(position, size, tileset[Int32.Parse(currentRow[x]) - 1]);
                    ObjectManager.addObject(currentTile);
                }
            }

            // Loops through all the Elements held by the objectElements list.
            foreach(XElement objectdata in currentLevel.objectElements)
            {
                // Data taken from the object element.
                bool collidable = bool.Parse(objectdata.Attribute("collidable").Value);
                Vector2 position = new Vector2(Int32.Parse(objectdata.Attribute("x").Value), Int32.Parse(objectdata.Attribute("y").Value));
                Vector2 size = new Vector2(Int32.Parse(objectdata.Attribute("width").Value), Int32.Parse(objectdata.Attribute("height").Value));

                // Creates a list of property elements.
                IEnumerable<XElement> properties = objectdata.Element("properties").Elements();
                
                // Creates the GameObject based on its properties.
                foreach( XElement property in properties)
                {
                    // Creates a Drawable object.
                    if (property.Attribute("name").Value == "Drawable")
                    {
                        Sprite currentObject = new Sprite(position, size, collidable, 1, tileset[Int32.Parse(objectdata.Attribute("gid").Value) - 1]);
                        ObjectManager.addObject(currentObject);
                    }

                    // Creates a Player object.
                    if(property.Attribute("name").Value == "Player")
                    {
                        Player player = new Player(position, size, collidable, 1, 1, tileset[Int32.Parse(objectdata.Attribute("gid").Value) - 1]);
                        ObjectManager.addObject(player);
                    }

                    // Creates an Interactable object.
                    if (property.Attribute("name").Value == "Interactable")
                    {
                        string requirement = property.Element("requirement").Attribute("value").Value;
                        string gives = property.Element("gives").Attribute("value").Value;
                        InteractableObject currentObject = new InteractableObject(position, size, collidable, requirement, gives);
                        ObjectManager.addObject(currentObject);
                    }
 
                }
            }

            


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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Begins the spriteBatch.
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            // Draws some text based on the textOutput variable.
            spriteBatch.DrawString(font, textOutput, new Vector2(750, 200), Color.Black);

            // Draws all Drawable objects.
            foreach (Drawable drawable in ObjectManager.Drawables)
                drawable.Draw(spriteBatch);

            // Ends the spriteBatch.
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
