using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Tony
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameManager : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private List<Texture2D> tileset;
        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.tileset = new List<Texture2D>();
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
            LevelReader currentLevel = new LevelReader(@"C:\Users\Jiynto\Source\Repos\charlieheslington\Tony\Tony\Tony\Content\testmap.tmx");
            foreach(string currentTexture in currentLevel.tileset)
            {
                this.tileset.Add(Content.Load<Texture2D>(currentTexture));
            }

            for(int y = 0; y < currentLevel.tileNumbers.Count; y++)
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

            foreach(XElement objectdata in currentLevel.objectElements)
            {
                IEnumerable<XElement> properties = objectdata.Element("properties").Elements();
                Vector2 position = new Vector2(Int32.Parse(objectdata.Attribute("x").Value), Int32.Parse(objectdata.Attribute("y").Value));
                Vector2 size = new Vector2(Int32.Parse(objectdata.Attribute("width").Value), Int32.Parse(objectdata.Attribute("height").Value));

                foreach( XElement property in properties)
                {
                    if (property.Attribute("name").Value == "Drawable")
                    {
                        Sprite currentObject = new Sprite(position, size, 0, new Vector2(0), 1, tileset[Int32.Parse(objectdata.Attribute("gid").Value) - 1]);
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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            foreach (Drawable drawable in ObjectManager.Objects)
                drawable.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
