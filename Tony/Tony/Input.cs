﻿using System;
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
        private static int range = 1;
         

        public static void CheckInputs()
        {
            if( Keyboard.GetState().IsKeyDown(Keys.A))
            {
                ObjectManager.Instance.CurrentLevel.Player.move("A");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                ObjectManager.Instance.CurrentLevel.Player.move("W");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                ObjectManager.Instance.CurrentLevel.Player.move("S");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                ObjectManager.Instance.CurrentLevel.Player.move("D");
            }



            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                foreach (GameObject i in ObjectManager.Instance.CurrentLevel.Objects.Where(i => i is InteractableObject))
                {
                    InteractableObject currentObject;
                    if (i is Npc)
                    {
                        Npc currentNpc = (Npc)i;
                        if (!currentNpc.getActor())
                        {
                            currentObject = (Npc)i;
                            InteractDetection(currentObject);
                        }


                    }
                    else
                    {
                        currentObject = (InteractableObject)i;
                        InteractDetection(currentObject);
                    }

                }
            }
        }


        private static void InteractDetection(InteractableObject currentObject)
        {

            //conditions of interaction.
            //if met and interaction is triggered.
            if (Detector.IsTouchingBottom(currentObject.getPosition(), currentObject.getSize(),
                ObjectManager.Instance.CurrentLevel.Player.getPosition(), ObjectManager.Instance.CurrentLevel.Player.getSize(), range)

                || Detector.IsTouchingTop(currentObject.getPosition(), currentObject.getSize(),
                ObjectManager.Instance.CurrentLevel.Player.getPosition(), ObjectManager.Instance.CurrentLevel.Player.getSize(), range)

                || Detector.IsTouchingLeft(currentObject.getPosition(), currentObject.getSize(),
                ObjectManager.Instance.CurrentLevel.Player.getPosition(), ObjectManager.Instance.CurrentLevel.Player.getSize(), range)

                || Detector.IsTouchingRight(currentObject.getPosition(), currentObject.getSize(),
                ObjectManager.Instance.CurrentLevel.Player.getPosition(), ObjectManager.Instance.CurrentLevel.Player.getSize(), range))
            {
                currentObject.Interact();
            }

        }




    }
}
