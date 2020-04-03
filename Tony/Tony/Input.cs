using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tony
{
    public static class Input
    {

        private static bool interactDown = false;

        public static void CheckInputs()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.W) 
                || Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    ObjectManager.currentLevel.Player.move("A");
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    ObjectManager.currentLevel.Player.move("W");
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    ObjectManager.currentLevel.Player.move("S");
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    ObjectManager.currentLevel.Player.move("D");
                }
            }
            else
            {
                ObjectManager.currentLevel.Player.move(null);
            }



            
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if (Controller.gameState == Controller.GameState.playing)
                {
                    Controller.gameState = Controller.GameState.paused;
                }
                
            }



            if (Keyboard.GetState().IsKeyDown(Keys.E) && interactDown == false)
            {
                interactDown = true;
                foreach (GameObject i in ObjectManager.currentLevel.Objects.Where(i => i is InteractableObject))
                {
                    InteractableObject currentObject;
                    if (i is Npc)
                    {
                        Npc currentNpc = (Npc)i;
                        if (!currentNpc.getActor())
                        {
                            if (InteractDetection(currentNpc, 1)) currentNpc.Interact();
                        }


                    }
                    else
                    {
                        currentObject = (InteractableObject)i;
                        if (InteractDetection(currentObject, 1)) currentObject.Interact();
                    }

                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.E)) interactDown = false;

        }


        public static bool InteractDetection(GameObject currentObject, int range)
        {

            //conditions of interaction.
            //if met and interaction is triggered.
            if (Detector.IsTouchingBottom(currentObject.getPosition(), currentObject.getSize(),
                ObjectManager.currentLevel.Player.getPosition(), ObjectManager.currentLevel.Player.getSize(), range)

                || Detector.IsTouchingTop(currentObject.getPosition(), currentObject.getSize(),
                ObjectManager.currentLevel.Player.getPosition(), ObjectManager.currentLevel.Player.getSize(), range)

                || Detector.IsTouchingLeft(currentObject.getPosition(), currentObject.getSize(),
                ObjectManager.currentLevel.Player.getPosition(), ObjectManager.currentLevel.Player.getSize(), range)

                || Detector.IsTouchingRight(currentObject.getPosition(), currentObject.getSize(),
                ObjectManager.currentLevel.Player.getPosition(), ObjectManager.currentLevel.Player.getSize(), range))
            {
                return true;
            }
            return false;

        }




    }
}
