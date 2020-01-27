using System;

namespace FreezeEngine
{
    public static class Renderer
    {
        // Creates a variable of DoubleBuffer for when rendering
        public static DoubleBuffer<char> Db = new DoubleBuffer<char>(30, 30);
        public static void Render()
        {
            // Swap the current frame for the next frame of the DoubleBuffer
            Db.Swap();

            // Update objects displayed
            // Loop throught the doublebuffers YDim, starting at 0
            for (int y = 0; y < Db.YDim; y++)
            {
                // Loop throught the doublebuffers XDim, starting at 0
                for (int x = 0; x < Db.XDim; x++)
                {
                    Console.Write(Db[x, y]);
                }
                Console.Write('\n');
            }
            // Sets CursorPosition to 0 in x and 0 in y
            Console.SetCursorPosition(0, 0);
            // Clears the doublebuffer
            Db.Clear();
        }
    }
}
