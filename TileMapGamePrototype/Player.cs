/* Artur Dobrzanski */ 
//using System;
//using System.Collections.Generic;
//using System.Linq;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;

namespace Krasnal
{
    public class Player : SpritePodstawowy
    {
        protected float minDisplayX, maxDisplayX, minDisplayY, maxDisplayY;
        protected int keyCode = 0, wybor = 0, poprzedniWybor = 0;
        protected int TIMER = 0;
        Rectangle pozycja = new Rectangle(0, 0, 100, 100);//(PozycjaX, PozycjaY, wielkośćKlatek, wielkośćKlatek);
        protected bool zajetoscKey = false;

        public void StartGame(float inMinDisplayX, float inMaxDisplayX, float inMinDisplayY, float inMaxDisplayY)
        {
            minDisplayX = inMinDisplayX;
            minDisplayY = inMinDisplayY;
            maxDisplayX = inMaxDisplayX;
            maxDisplayY = inMaxDisplayY;

            float displayWidth = maxDisplayX - minDisplayX;
            spriteRectangle.Width = Silnik.rozmiarKaflka / 2;
            float aspectRatio =
                    (float)spriteTexture.Width / spriteTexture.Height;
            spriteRectangle.Height = Silnik.rozmiarKaflka - 15;
            spriteRectangle.X = (int)Silnik.displayWidth / 2;
            spriteRectangle.Y = (int)Silnik.displayHeight / 2;
        }

        private void upTime()
        {
            if (TIMER < 7)
                TIMER++;
            else TIMER = 0;
        }
        public void animacja(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, pozycja);
        }

        public virtual void Update(Game1 game, int key)
        {

            if (key == 1 || key == 5 || key == 7)
            {
                if (keyCode == 0) keyCode = 1;
                poprzedniWybor = 3;
            }
            else if (key == 2 || key == 6 || key == 8)
            {
                if (keyCode == 0) keyCode = 2;
                poprzedniWybor = 2;
            }
            else if (key == 3)
            {
                if (keyCode == 0)
                {
                    poprzedniWybor = 1;
                    keyCode = 3;
                }
            }
            else if (key == 4)
            {
                if (keyCode == 0)
                {
                    keyCode = 4;
                    poprzedniWybor = 0;
                }
            }

            switch (keyCode)
            {
                case -1:
                    keyCode = -1;
                    break;
                case 1: //lewo
                    upTime();
                    if (TIMER == 0)
                    {
                        wybor = 5;
                        zajetoscKey = false;
                        keyCode = -1;
                    }
                    break;
                case 2: //prawo
                    upTime();
                    if (TIMER == 0)
                    {
                        wybor = 7;
                        zajetoscKey = false;
                        keyCode = -1;
                    }
                    break;
                case 3: //gora
                    upTime();
                    if (TIMER == 0)
                    {
                        wybor = 3;
                        zajetoscKey = false;
                        keyCode = -1;
                    }
                    break;
                case 4: //dol
                    upTime();
                    if (TIMER == 0)
                    {
                        wybor = 1;
                        zajetoscKey = false;
                        keyCode = -1;
                    }
                    break;
            }
            if (zajetoscKey == false)
            {
                switch (wybor)
                {
                    case 0:
                        if (poprzedniWybor == 0) pozycja = new Rectangle(0, 100, 35, 100);
                        else if (poprzedniWybor == 1) pozycja = new Rectangle(0, 100, 35, 100);
                        else if (poprzedniWybor == 2) pozycja = new Rectangle(0, 100, 35, 100);
                        else if (poprzedniWybor == 3) pozycja = new Rectangle(0, 0, 35, 100);
                        break;
                    case 1: //ruch w dol
                        //pozycja = new Rectangle(100, 0, 30, 100);
                        upTime();
                        if (TIMER == 0)
                            wybor = 2;
                        break;
                    case 2:
                        //pozycja = new Rectangle(200, 0, 30, 100);
                        upTime();
                        if (TIMER == 0)
                        {
                            keyCode = 0;
                            wybor = 0;
                        }
                        break;
                    case 3: //ruch w gore
                        //pozycja = new Rectangle(100, 100, 30, 100);
                        upTime();
                        if (TIMER == 0)
                            wybor = 4;
                        break;
                    case 4:
                        //pozycja = new Rectangle(200, 100, 30, 100);
                        upTime();
                        if (TIMER == 0)
                        {
                            keyCode = 0;
                            wybor = 0;
                        }
                        break;
                    case 5: //ruch w lewo
                        pozycja = new Rectangle(100, 0, 35, 100);
                        upTime();
                        if (TIMER == 0)
                            wybor = 6;
                        break;
                    case 6:
                        pozycja = new Rectangle(200, 0, 35, 100);
                        upTime();
                        if (TIMER == 0)
                        {
                            keyCode = 0;
                            wybor = 0;
                        }
                        break;
                    case 7: //ruch w prawo
                        pozycja = new Rectangle(100, 100, 35, 100);
                        upTime();
                        if (TIMER == 0)
                            wybor = 8;
                        break;
                    case 8:
                        pozycja = new Rectangle(200, 100, 35, 100);
                        upTime();
                        if (TIMER == 0)
                        {
                            keyCode = 0;
                            wybor = 0;
                        }
                        break;
                    default:
                        pozycja = new Rectangle(0, 0, 35, 100);
                        keyCode = 0;
                        wybor = 0;
                        break;
                }
            }
        }

        public void ruchWLewo()
        {
            spriteRectangle.X -= StanGracza.predkoscGracza;
        }

        public void ruchWPrawo()
        {
            spriteRectangle.X += StanGracza.predkoscGracza;
        }

        public void ruchWGore()
        {
            spriteRectangle.Y -= StanGracza.predkoscGracza;
        }

        public void ruchWDol()
        {
           spriteRectangle.Y += StanGracza.predkoscGracza;
        }

        public void wysrodkujGracza()
        {
            spriteRectangle.X = (int)Silnik.displayWidth / 2;
            spriteRectangle.Y = (int)Silnik.displayHeight / 2;
        }

    }
}
