using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace FreezeEngine
{
    /// <summary>
    /// Responsible for handlind the user's input during the game's GameLoop.
    /// </summary>
    public static class Input
    {
        public static List<IGameObject> gameObjects = new List<IGameObject>();
        // Declare public property of Direction type with a private set, used
        // to indicate to the GameLoop what direction the player selected
        // through the input
        public static Direction Dir { get; private set; }
        // Declare public property of Direction type, used to indicate to the
        // game loop the last direction the player had selected
        public static Direction LastDir { get; private set; }
        // Declare private readonly variable of type
        // BlockingCollection<ConsoleKey> used to save the player's input, used 
        // to determine what direction the player chooses
        private static readonly BlockingCollection<ConsoleKey> inputCol
            = new BlockingCollection<ConsoleKey>();

        /// <summary>
        /// Sets Dir and LastDir to Direction.None values and takes everything
        /// saved in the inputCol BlockingCollection, effectively resetting any
        /// and all input given by the player up to that point.
        /// </summary>
        public static void ResetInput()
        {
            // Assign Direction.None value to Dir
            Dir = Direction.None;
            // Assign Direction.None value to LastDir
            LastDir = Direction.None;
            // for cycle that takes all ConsoleKeys stored in inputCol
            for (int i = 0; i < inputCol.Count; i++)
            {
                inputCol.Take();
            }
        }
        /// <summary>
        /// Processes the ConsoleKeys given to the inputCol BlockingCollection
        /// and assigns a direction to Dir based on which key was pressed,
        /// assigning the LastDir to Dir if not ConsoleKey can be taken from
        /// inputCol.
        /// </summary>
        public static void ProcessInput()
        {
            // if statement that checks if it is possible to take an element
            // from inputCol
            if (inputCol.TryTake(out ConsoleKey key))
            {
                // switch statement that assigns a Direction value to Dir
                // based on the ConsoleKey taken from inputCol
                switch (key)
                {
                    // In case key taken is W
                    case ConsoleKey.W:
                        // Assign Direction.Up to Dir
                        Dir = Direction.Up;
                        break;
                    // In case key taken is S
                    case ConsoleKey.S:
                        // Assign Direction.Down to Dir
                        Dir = Direction.Down;
                        break;
                    // In case key taken is A
                    case ConsoleKey.A:
                        // Assign Direction.Left to Dir
                        Dir = Direction.Left;
                        break;
                    // In case key taken is D
                    case ConsoleKey.D:
                        // Assign Direction.Right to Dir
                        Dir = Direction.Right;
                        break;
                }
            }
            // else statement that assigns LastDir's value to Dir if no
            // element can be taken from inputCol
            else
            {
                Dir = Direction.None;
            }
        }
        /// <summary>
        /// Public method to be ran in it's own thread, constantly accepting
        /// input from the player and adding the ConsoleKey to the inputCol
        /// BlockingCollection, to be processed by the ProcessInput method.
        /// </summary>
        public static void ReadKeys()
        {
            // Declare key variable of ConsoleKey type
            ConsoleKey key;

            // while loop that runs as long as the IsRunning property is true
            while (true)
            {
                // Assigns a ConsoleKey value, returned from 
                // Console.Readkey().Key to variable key
                key = Console.ReadKey(true).Key;
                // Adds key read to the inputCol BlockingCollection
                inputCol.Add(key);
            }
        }
    }
}
