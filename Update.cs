using System;
using System.Collections.Generic;
using System.Threading;

namespace FreezeEngine
{
    public class Update
    {
        // Creates a variable to store all objects
        private readonly List<IGameObject> gameObjects;
        // Creates the thread for the InputSystem variable
        private readonly Thread keyReader;

        /// <summary>
        /// Constructor of the class GameLoop initializing all the variables
        /// created above
        /// </summary>
        public Update()
        {
            // Creates a new Thread and gives it a method of InputSystem
            keyReader = new Thread(Input.ReadKeys)
            {
                // Assign a name to the Input Thread
                Name = "InputThread"
            };
            // hides the cursor
            Console.CursorVisible = false;
            gameObjects = Input.gameObjects;
            Loop();
        }
        /// <summary>
        /// The main loop of the game
        /// </summary>
        private void Loop()
        {
            // Starts the input thread creates
            keyReader.Start();
            // Runs and executes the code while running is true
            while (true)
            {
                // creates a long with the value of the ticks at the moment
                long start = DateTime.Now.Ticks;
                // Processes the input
                Input.ProcessInput();
                // Runs the Update method
                UpdateObjects();
                if (true)
                {
                    // Runs the Render method
                    Renderer.Render();
                }
                //Thread.Sleep(Math.Abs(
                //    (int)(start / 20000)
                //    + 20
                //    - (int)(DateTime.Now.Ticks / 20000)));
            }
        }
        private void UpdateObjects()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
            }
        }
    }
}



