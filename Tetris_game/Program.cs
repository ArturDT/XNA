/* Artur Dobrzanski */
using System;

namespace BrickHole
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Gra game = new Gra())
            {
                game.Run();
            }
        }
    }
#endif
}

