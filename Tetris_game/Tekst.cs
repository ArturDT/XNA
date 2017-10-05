/* Artur Dobrzanski */
#region using
//using System;
//using System.Collections.Generic;
//using System.Linq;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;
#endregion
namespace BrickHole
{
    public class KlawiszePisanie
    {
        // aktualnie wciśnięte klawisze
        public void Pisz(KeyboardState keyState)
        {
            Keys[] pressedKeys;
            Keys[] oldKeys = new Keys[0];
            string messageString = "";
            pressedKeys = keyState.GetPressedKeys();

            // przeszukuje kolejno wszystkie aktualnie wciśnięte klawisze
            for (int i = 0; i < pressedKeys.Length; i++)
            {
                // ustawia flagę określającą, że nie znaleziono klawisza
                bool foundIt = false;

                // przeszukuje kolejno wszystkie poprzednio wciśnięte klawisze
                for (int j = 0; j < oldKeys.Length; j++)
                {
                    if (pressedKeys[i] == oldKeys[j])
                    {
                        // znaleziono klawisz w tablicy wcześniej wciśniętych klawiszy
                        foundIt = true;
                    }
                }
                if (foundIt == false)
                {
                    // skoro sterowanie trafiło w to miejsce, nie znaleziono klawisza w tablicy poprzednio wciśniętych klawiszy,
                    // należy zdekodować wartość klawisza, aby wygenerować odpowiedni komunikat
                    string keyString = ""; // ten łańcuch początkowo jest pusty
                    switch (pressedKeys[i])
                    {
                        case Keys.D0:
                            keyString = "0";
                            break;
                        case Keys.D1:
                            keyString = "1";
                            break;
                        case Keys.D2:
                            keyString = "2";
                            break;
                        case Keys.D3:
                            keyString = "3";
                            break;
                        case Keys.D4:
                            keyString = "4";
                            break;
                        case Keys.D5:
                            keyString = "5";
                            break;
                        case Keys.D6:
                            keyString = "6";
                            break;
                        case Keys.D7:
                            keyString = "7";
                            break;
                        case Keys.D8:
                            keyString = "8";
                            break;
                        case Keys.D9:
                            keyString = "9";
                            break;
                        case Keys.A:
                            keyString = "A";
                            break;
                        case Keys.B:
                            keyString = "B";
                            break;
                        case Keys.C:
                            keyString = "C";
                            break;
                        case Keys.D:
                            keyString = "D";
                            break;
                        case Keys.E:
                            keyString = "E";
                            break;
                        case Keys.F:
                            keyString = "F";
                            break;
                        case Keys.G:
                            keyString = "G";
                            break;
                        case Keys.H:
                            keyString = "H";
                            break;
                        case Keys.I:
                            keyString = "I";
                            break;
                        case Keys.J:
                            keyString = "J";
                            break;
                        case Keys.K:
                            keyString = "K";
                            break;
                        case Keys.L:
                            keyString = "L";
                            break;
                        case Keys.M:
                            keyString = "M";
                            break;
                        case Keys.N:
                            keyString = "N";
                            break;
                        case Keys.O:
                            keyString = "O";
                            break;
                        case Keys.P:
                            keyString = "P";
                            break;
                        case Keys.Q:
                            keyString = "Q";
                            break;
                        case Keys.R:
                            keyString = "R";
                            break;
                        case Keys.S:
                            keyString = "S";
                            break;
                        case Keys.T:
                            keyString = "T";
                            break;
                        case Keys.U:
                            keyString = "U";
                            break;
                        case Keys.W:
                            keyString = "W";
                            break;
                        case Keys.V:
                            keyString = "V";
                            break;
                        case Keys.X:
                            keyString = "X";
                            break;
                        case Keys.Y:
                            keyString = "Y";
                            break;
                        case Keys.Z:
                            keyString = "Z";
                            break;
                        case Keys.Space:
                            keyString = " ";
                            break;
                        case Keys.OemPeriod:
                            keyString = ".";
                            break;
                        case Keys.Enter:
                            keyString = "\n";
                            break;

                    }

                    if (keyState.IsKeyUp(Keys.LeftShift) && keyState.IsKeyUp(Keys.RightShift))
                    {
                        keyString = keyString.ToLower();
                    }

                    if (pressedKeys[i] == Keys.Back)
                    {
                        if (messageString.Length > 0)
                        {
                            messageString = messageString.Remove(messageString.Length - 1, 1);
                        }
                    }

                    messageString = messageString + keyString;
                }
            }

            // zapamiętuje klawisze na potrzeby następnego wywołania
            oldKeys = pressedKeys;
        }
    }
}