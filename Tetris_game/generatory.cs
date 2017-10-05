/* Artur Dobrzanski */
#region using
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;
#endregion

#region Generatory
namespace BrickHole
{
    public class KratkaEkranowa
    {
        private bool czyKratkaJestZajeta = false;
        displayParametrs parametrKratki;

        public void zmienZajetosc()
        {
            if (czyKratkaJestZajeta == true)
                czyKratkaJestZajeta = false;
            else
                czyKratkaJestZajeta = true;
        }
        public bool sprawdzZajetosc()
        {
            return czyKratkaJestZajeta;
        }
        public void zwolnijkratke()
        {
            czyKratkaJestZajeta = false;
        }
        public displayParametrs zwracaParametryKratki()
        {
            return parametrKratki;
        }
        public KratkaEkranowa(displayParametrs inParametrKratki)
        {
            parametrKratki = inParametrKratki;
        }
    }

    public class GeneratorKlockow
    {
        #region zmienneKlasyGeneratora
        private displayParametrs parametryEkranu, parametryKratki;
        private Brick klocek;
        private KratkaEkranowa kratkaEkraowaObszarGry;
        private List<KratkaEkranowa> siatkaEkranowa = new List<KratkaEkranowa>();
        public List<Brick> Klocki = new List<Brick>();
        private objectParameters parametryObiektu;
        private Rectangle brickRectangle;
        private int iloscElementowOsX;
        private Random rand = new Random();
        private byte niebieski, czerwony, zielony, przezroczystosc;
        private Color kolor;
        #endregion

        public void Update(Gra gra)
        {
            aktualizujPolozenieKlockow(gra);
            if (gra.aktualnyStanGry != Gra.StanGry.KoniecGry)
            {
                if (gra.graDwuosobowa)
                    SprawdzCzyZniszczycKlocek2Player(gra);
                else
                    SprawdzCzyZniszczycKlocek(gra);
                SprawdzIGenerujKolejneKlocki(gra);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Brick sprite in Klocki)
            {
                if (sprite.rectangle.X >= parametryEkranu.minX && sprite.rectangle.Y > 0 - brickRectangle.Height)
                    sprite.Draw(spriteBatch);
            }
        }
        public void generuj(Gra gra)
        {
            generujSiatke();
            generujKlocki(gra);
        }
        public bool SprawdzKolizjeGraczy(Gra game)
        {
            Rectangle glowaGracza = game.postacGracz1.rectangle;
            if (game.graDwuosobowa)
            {
                Rectangle glowaGracza2 = game.postacGracz2.rectangle;
                glowaGracza2.Height -= 4;
                glowaGracza2.Width -= 4;
                glowaGracza2.X += 2;
                glowaGracza2.Y += 2;
                glowaGracza.Height -= 4;
                glowaGracza.Width -= 4;
                glowaGracza.X += 2;
                glowaGracza.Y += 2;
                if (glowaGracza2.Intersects(glowaGracza))
                    {
                        return true;
                    }
             }
                return false;         
        }
        public bool pozwolenieNaRuch(Gra game)
        {
            Rectangle glowaGracza = game.postacGracz1.rectangle, glowaGracza2 = game.postacGracz2.rectangle; ;
            glowaGracza.Height -= 2; glowaGracza2.Height -= 2;
            glowaGracza.Width -= 2; glowaGracza2.Width -= 2;
            glowaGracza.X += 1; glowaGracza2.X += 1;
            glowaGracza.Y += 1; glowaGracza2.Y += 1;
            if (glowaGracza2.Intersects(glowaGracza))
            {
                return false;
            }
            return true;
        }
        private void SprawdzIGenerujKolejneKlocki(Gra gra)
        {
            if (Klocki.Count() < iloscElementowOsX * iloscElementowOsX)
            {
                foreach (KratkaEkranowa kratka in siatkaEkranowa)
                    kratka.zwolnijkratke();
                generujKlocki(gra);
            }

        }
        private void aktualizujPolozenieKlockow(Gra game)
        {
            Rectangle tempRect = new Rectangle(0,0,0,0);
            for (int i = 0; i < Klocki.Count() - 5; i += 4)
            {
                for (byte px = 0; px < Klocki[i].fallSpeed; px++)
                {
                    bool brakKolizji = true;
                    for (int j = 0; j < i; j++)
                    {
                        tempRect = Klocki[j].rectangle;
                        tempRect.Y--; 
                        if (Klocki[i].CheckCollision(tempRect) || Klocki[i + 1].CheckCollision(tempRect) ||
                            Klocki[i + 2].CheckCollision(tempRect) || Klocki[i + 3].CheckCollision(tempRect))
                        {
                            brakKolizji = false;
                            break;
                        }
                    }

                    if (brakKolizji == true && Klocki[i].YPos < parametryEkranu.maxY - brickRectangle.Height)
                    {
                        Klocki[i].moveBrick1px(true);
                        Klocki[i + 1].moveBrick1px(true);
                        Klocki[i + 2].moveBrick1px(true);
                        Klocki[i + 3].moveBrick1px(true);
                        if (game.graDwuosobowa)
                            CzyGraczGinie2(game, i);
                        else
                            CzyGraczGinie(game, i);
                    }
                }
            }

        }
        private void analizaKolizji(Gra game)
        {
            for (int i = 0; i < Klocki.Count() - 5; i += 4)
            {
                bool brakKolizji = true;
                Rectangle tempRect;
                for (int j = 0; j < i; j++)
                {
                    tempRect = Klocki[j].rectangle;
                    
                }
            }
        }
        private void CzyGraczGinie(Gra game, int i)
        {
                Rectangle glowaGracza = game.postacGracz1.rectangle;
                glowaGracza.Height -= 4;
                glowaGracza.Width -= 4;
                glowaGracza.X += 2;
                glowaGracza.Y += 2;
                if (Klocki[i].rectangle.Intersects(glowaGracza) || Klocki[i + 1].rectangle.Intersects(glowaGracza) ||
                    Klocki[i + 2].rectangle.Intersects(glowaGracza) || Klocki[i + 3].rectangle.Intersects(glowaGracza))
                {
                    GameOver(game);
                }
            
        }
        private void CzyGraczGinie2(Gra game, int i)
        {
               Rectangle glowaGracza = game.postacGracz1.rectangle, glowaGracza2 = game.postacGracz2.rectangle; ;
               glowaGracza.Height -= 4; glowaGracza2.Height -= 4;
               glowaGracza.Width -= 4; glowaGracza2.Width -= 4;
               glowaGracza.X += 2; glowaGracza2.X += 2;
               glowaGracza.Y += 2; glowaGracza2.Y += 2;
               if (Klocki[i].rectangle.Intersects(glowaGracza) || Klocki[i + 1].rectangle.Intersects(glowaGracza) ||
                   Klocki[i + 2].rectangle.Intersects(glowaGracza) || Klocki[i + 3].rectangle.Intersects(glowaGracza))
               {
                   if (game.player1Win == false)
                        game.player2Win = true;
                   GameOver(game);
               }
               else if (Klocki[i].rectangle.Intersects(glowaGracza2) || Klocki[i + 1].rectangle.Intersects(glowaGracza2) ||
                       Klocki[i + 2].rectangle.Intersects(glowaGracza2) || Klocki[i + 3].rectangle.Intersects(glowaGracza2))
               {
                   if (game.player2Win == false)
                    game.player1Win = true;
                    GameOver(game);
               }
               
            
        }
        private void SprawdzCzyZniszczycKlocek(Gra game)
        {
            Rectangle glowaGracza = game.postacGracz1.rectangle;
            glowaGracza.Height -= 4;
            glowaGracza.Width -= 4;
            glowaGracza.X += 2;
            glowaGracza.Y += 2;
            if (game.graDwuosobowa == false)
            {
                for (int i = 0; i < Klocki.Count(); i += 4)
                {
                    if (Klocki[i].rectangle.Intersects(glowaGracza) || Klocki[i + 1].rectangle.Intersects(glowaGracza) ||
                        Klocki[i + 2].rectangle.Intersects(glowaGracza) || Klocki[i + 3].rectangle.Intersects(glowaGracza))
                    {
                        Klocki.Remove(Klocki[i]);
                        Klocki.Remove(Klocki[i]);
                        Klocki.Remove(Klocki[i]);
                        Klocki.Remove(Klocki[i]);
                        game.dodajPunkty(1);
                        for (int i2 = 0; i2 < Klocki.Count(); i2 += 4)
                        {
                            if (Klocki[i2].rectangle.Intersects(game.postacGracz1.rectangle) || Klocki[i2 + 1].rectangle.Intersects(game.postacGracz1.rectangle) ||
                               Klocki[i2 + 2].rectangle.Intersects(game.postacGracz1.rectangle) || Klocki[i2 + 3].rectangle.Intersects(game.postacGracz1.rectangle))
                            {
                                Klocki.Remove(Klocki[i2]);
                                Klocki.Remove(Klocki[i2]);
                                Klocki.Remove(Klocki[i2]);
                                Klocki.Remove(Klocki[i2]);
                                game.dodajPunkty(1);
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            else SprawdzCzyZniszczycKlocek2Player(game);
        }
        private void SprawdzCzyZniszczycKlocek2Player(Gra game)
        {
            Rectangle glowaGracza = game.postacGracz1.rectangle;
            Rectangle glowaGracza2 = game.postacGracz2.rectangle;
            glowaGracza.Height -= 4; glowaGracza2.Height -= 4;
            glowaGracza.Width -= 4; glowaGracza2.Width -= 4;
            glowaGracza.X += 2; glowaGracza2.X += 2;
            glowaGracza.Y += 2; glowaGracza2.Y += 2;
 
            for (int i = 0; i < Klocki.Count() - 5; i += 4)
            {
                if (Klocki[i].rectangle.Intersects(glowaGracza) || Klocki[i + 1].rectangle.Intersects(glowaGracza) ||
                    Klocki[i + 2].rectangle.Intersects(glowaGracza) || Klocki[i + 3].rectangle.Intersects(glowaGracza) )
                {
                    game.dodajPunkty(1);
                    Klocki.Remove(Klocki[i]);
                    Klocki.Remove(Klocki[i]);
                    Klocki.Remove(Klocki[i]);
                    Klocki.Remove(Klocki[i]);
                    for (int i2 = 0; i2 < Klocki.Count() - 5; i2 += 4)
                    {
                        if (Klocki[i2].rectangle.Intersects(glowaGracza) || Klocki[i2 + 1].rectangle.Intersects(glowaGracza) ||
                           Klocki[i2 + 2].rectangle.Intersects(glowaGracza) || Klocki[i2 + 3].rectangle.Intersects(glowaGracza))
                        {
                            game.dodajPunkty(1);
                            Klocki.Remove(Klocki[i2]);
                            Klocki.Remove(Klocki[i2]);
                            Klocki.Remove(Klocki[i2]);
                            Klocki.Remove(Klocki[i2]);
                            break;
                        }
                    }
                    break;
                 }
                else if (Klocki[i].rectangle.Intersects(glowaGracza2) || Klocki[i + 1].rectangle.Intersects(glowaGracza2) ||
                    Klocki[i + 2].rectangle.Intersects(glowaGracza2) || Klocki[i + 3].rectangle.Intersects(glowaGracza2))
                {
                    game.dodajPunkty(2);
                    Klocki.Remove(Klocki[i]);
                    Klocki.Remove(Klocki[i]);
                    Klocki.Remove(Klocki[i]);
                    Klocki.Remove(Klocki[i]);
                    for (int i2 = 0; i2 < Klocki.Count() - 5; i2 += 4)
                    {
                        if (Klocki[i2].rectangle.Intersects(glowaGracza2) || Klocki[i2 + 1].rectangle.Intersects(glowaGracza2) ||
                           Klocki[i2 + 2].rectangle.Intersects(glowaGracza2) || Klocki[i2 + 3].rectangle.Intersects(glowaGracza2))
                        {
                            game.dodajPunkty(2);
                            Klocki.Remove(Klocki[i2]);
                            Klocki.Remove(Klocki[i2]);
                            Klocki.Remove(Klocki[i2]);
                            Klocki.Remove(Klocki[i2]);
                            break;
                        }
                    }
                    break;
                }
            }
        }
        private void generujSiatke()
        {
            for (int numerRzedu = parametryEkranu.minY; numerRzedu > parametryEkranu.minY - 5 * brickRectangle.Height; numerRzedu -= brickRectangle.Height)
            {
                for (int numerElementuWrzedzie = parametryEkranu.minX; numerElementuWrzedzie < parametryEkranu.maxX; numerElementuWrzedzie += brickRectangle.Width)
                {
                    parametryKratki.minX = numerElementuWrzedzie;
                    parametryKratki.maxX = numerElementuWrzedzie + brickRectangle.Width;
                    parametryKratki.minY = numerRzedu;
                    parametryKratki.maxY = numerRzedu + brickRectangle.Height;
                    kratkaEkraowaObszarGry = new KratkaEkranowa(parametryKratki);
                    siatkaEkranowa.Add(kratkaEkraowaObszarGry);
                }
            }
        }
        private void generujKlocki(Gra gra)
        {
            for (int i = iloscElementowOsX ; i < siatkaEkranowa.Count(); i++)
            {
                byte random =(byte) rand.Next(0, 18);
                niebieski = (byte)rand.Next(0, 255);
                czerwony = (byte)rand.Next(0, 255);
                zielony = (byte)rand.Next(0, 255);
                przezroczystosc = (byte)rand.Next(230, 255);
                kolor = new Color(czerwony, zielony, niebieski, przezroczystosc);
                #region sprawdzanieRandILokowanieKlockow
                #region dlaRand0
                if ((i + iloscElementowOsX + 1) < siatkaEkranowa.Count())
                {
                    if (random == 0 && sprawdzPrzypadek0(i) == false)
                    {
                        ganerujFigure0(i, gra);
                    }
                }
                #endregion
                #region dlaRand1
                if ((i + 3) < siatkaEkranowa.Count())
                {
                    if (random == 1 && sprawdzPrzypadek1(i) == false)
                    {
                        ganerujFigure1(i, gra);
                    }
                }
                #endregion
                #region dlaRand2
                if ((i + 3 * iloscElementowOsX) < siatkaEkranowa.Count())
                {
                    if (random == 2 && sprawdzPrzypadek2(i) == false)
                    {
                        ganerujFigure2(i, gra);
                    }
                }
                #endregion
                #region dlaRand3
                if ((i + 2 * iloscElementowOsX) < siatkaEkranowa.Count())
                {
                    if (random == 3 && sprawdzPrzypadek3(i) == false)
                    {
                        ganerujFigure3(i, gra);
                    }
                }
                #endregion
                #region dlaRand4
                if ((i + 2 * iloscElementowOsX + 1) < siatkaEkranowa.Count())
                {
                    if (random == 4 && sprawdzPrzypadek4(i) == false)
                    {
                        ganerujFigure4(i, gra);
                    }
                }
                #endregion
                #region dlaRand5
                if ((i + iloscElementowOsX) < siatkaEkranowa.Count())
                {
                    if (random == 5 && sprawdzPrzypadek5(i) == false)
                    {
                        ganerujFigure5(i, gra);
                    }
                }
                #endregion
                #region dlaRand6
                if ((i + iloscElementowOsX + 2) < siatkaEkranowa.Count())
                {
                    if (random == 6 && sprawdzPrzypadek6(i) == false)
                    {
                        ganerujFigure6(i, gra);
                    }
                }
                #endregion
                #region dlaRand7
                if ((i + iloscElementowOsX + 2) < siatkaEkranowa.Count())
                {
                    if (random == 7 && sprawdzPrzypadek7(i) == false)
                    {
                        ganerujFigure7(i, gra);
                    }
                }
                #endregion
                #region dlaRand8
                if ((i + iloscElementowOsX) < siatkaEkranowa.Count())
                {
                    if (random == 8 && sprawdzPrzypadek8(i) == false)
                    {
                        ganerujFigure8(i, gra);
                    }
                }
                #endregion
                #region dlaRand9
                if ((i + iloscElementowOsX + 2) < siatkaEkranowa.Count())
                {
                    if (random == 9 && sprawdzPrzypadek9(i) == false)
                    {
                        ganerujFigure9(i, gra);
                    }
                }
                #endregion
                #region dlaRand10
                if ((i + iloscElementowOsX) < siatkaEkranowa.Count())
                {
                    if (random == 10 && sprawdzPrzypadek10(i) == false)
                    {
                        ganerujFigure10(i, gra);
                    }
                }
                #endregion
                #region dlaRand11
                if ((i + 2 * iloscElementowOsX + 1) < siatkaEkranowa.Count())
                {
                    if (random == 11 && sprawdzPrzypadek11(i) == false)
                    {
                        ganerujFigure11(i, gra);
                    }
                }
                #endregion
                #region dlaRand12
                if ((i + 2 * iloscElementowOsX + 1) < siatkaEkranowa.Count())
                {
                    if (random == 12 && sprawdzPrzypadek12(i) == false)
                    {
                        ganerujFigure12(i, gra);
                    }
                }
                #endregion
                #region dlaRand13
                if ((i + 2 * iloscElementowOsX) < siatkaEkranowa.Count())
                {
                    if (random == 13 && sprawdzPrzypadek13(i) == false)
                    {
                        ganerujFigure13(i, gra);
                    }
                }
                #endregion
                #region dlaRand14
                if ((i + 2 * iloscElementowOsX) < siatkaEkranowa.Count())
                {
                    if (random == 14 && sprawdzPrzypadek14(i) == false)
                    {
                        ganerujFigure14(i, gra);
                    }
                }
                #endregion
                #region dlaRand15
                if ((i + 2 * iloscElementowOsX) < siatkaEkranowa.Count())
                {
                    if (random == 15 && sprawdzPrzypadek15(i) == false)
                    {
                        ganerujFigure15(i, gra);
                    }
                }
                #endregion
                #region dlaRand16
                if ((i + 2 * iloscElementowOsX) < siatkaEkranowa.Count())
                {
                    if (random == 16 && sprawdzPrzypadek16(i) == false)
                    {
                        ganerujFigure16(i, gra);
                    }
                }
                #endregion
                #region dlaRand17
                if ((i + iloscElementowOsX + 1) < siatkaEkranowa.Count())
                {
                    if (random == 17 && sprawdzPrzypadek17(i) == false)
                    {
                        ganerujFigure17(i, gra);
                    }
                }
                #endregion
                #region dlaRand18
                if ((i + iloscElementowOsX) < siatkaEkranowa.Count())
                {
                    if (random == 18 && sprawdzPrzypadek18(i) == false)
                    {
                        ganerujFigure18(i, gra);
                    }
                }
                #endregion
                #endregion
            }
        }
        private void generujKlocek(Gra gra)
        {
            ladujParametryKlockow(gra, parametryKratki.minX, parametryKratki.minY);
            klocek = new Brick(gra.Content.Load<Texture2D>("grafika/brick"), parametryObiektu, parametryEkranu);
        }
        private void ladujParametryKlockow(Gra gra, int xInit, int yInit)
        {
            parametryObiektu.xInitial = xInit;
            parametryObiektu.yInitial = yInit;
            parametryObiektu.x = xInit;
            parametryObiektu.y = yInit;
            parametryObiektu.xSpeed = 0;
            parametryObiektu.ySpeed = (brickRectangle.Height / gra.szybkoscPokonaniaKlatkiPrzezKlocek);
            parametryObiektu.objectRectangle.Width = brickRectangle.Width;
            parametryObiektu.objectRectangle.Height = brickRectangle.Height;
        }
        private void GameOver(Gra game)
        {
            game.aktualnyStanGry = Gra.StanGry.KoniecGry;
        }

        #region funkcjeSprawdzajacePolozenie
        private bool sprawdzPrzypadek0(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i + 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX].sprawdzZajetosc() == false && siatkaEkranowa[i + iloscElementowOsX + 1].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        private bool sprawdzPrzypadek1(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i + 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + 2].sprawdzZajetosc() == false && siatkaEkranowa[i + 3].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        private bool sprawdzPrzypadek2(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i + iloscElementowOsX].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + 2 * iloscElementowOsX].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + 3 * iloscElementowOsX].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        private bool sprawdzPrzypadek3(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i + iloscElementowOsX].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX - 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + 2 * iloscElementowOsX - 1].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        private bool sprawdzPrzypadek4(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i + iloscElementowOsX].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX + 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + 2 * iloscElementowOsX + 1].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        private bool sprawdzPrzypadek5(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i - 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX - 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX - 2].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        private bool sprawdzPrzypadek6(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i + 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX + 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX + 2].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        private bool sprawdzPrzypadek7(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i + iloscElementowOsX].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX + 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX + 2].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        private bool sprawdzPrzypadek8(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i + iloscElementowOsX].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX - 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX - 2].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        private bool sprawdzPrzypadek9(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i + 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + 2].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX + 2].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        private bool sprawdzPrzypadek10(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i + 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + 2].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        private bool sprawdzPrzypadek11(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i + iloscElementowOsX].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + 2 * iloscElementowOsX].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + 2 * iloscElementowOsX + 1].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        private bool sprawdzPrzypadek12(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i + 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX + 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + 2 * iloscElementowOsX + 1].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        private bool sprawdzPrzypadek13(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i + 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + 2 * iloscElementowOsX].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        private bool sprawdzPrzypadek14(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i + iloscElementowOsX].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + 2 * iloscElementowOsX].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + 2 * iloscElementowOsX - 1].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        private bool sprawdzPrzypadek15(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i + iloscElementowOsX].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX + 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + 2 * iloscElementowOsX].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        private bool sprawdzPrzypadek16(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i + iloscElementowOsX].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX - 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + 2 * iloscElementowOsX].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        private bool sprawdzPrzypadek17(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i + iloscElementowOsX].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX - 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + iloscElementowOsX + 1].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        private bool sprawdzPrzypadek18(int i)
        {
            if (siatkaEkranowa[i].sprawdzZajetosc() == false && siatkaEkranowa[i + iloscElementowOsX].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i + 1].sprawdzZajetosc() == false &&
                         siatkaEkranowa[i - 1].sprawdzZajetosc() == false)
                return false;
            else return true;
        }
        #endregion
        #region funkcjeGenerujaceFigury

        private void ganerujFigure0(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.maxX < parametryEkranu.maxX)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        generacjaIKolor(gra, kolor, i + 1);
                    else if (j == 1)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX);
                    else
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX + 1);
                }

            }
        }
        private void ganerujFigure1(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.maxX < parametryEkranu.maxX - 3 * brickRectangle.Width)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 0; j < 3; j++)
                    generacjaIKolor(gra, kolor, i + j + 1);
            }
        }
        private void ganerujFigure2(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.maxX < parametryEkranu.maxX - brickRectangle.Width)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 1; j < 4; j++)
                    generacjaIKolor(gra, kolor, i + j * iloscElementowOsX);
            }
        }
        private void ganerujFigure3(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.minX > parametryEkranu.minX + brickRectangle.Width)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX);
                    else if (j == 1)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX - 1);
                    else
                        generacjaIKolor(gra, kolor, i + 2 * iloscElementowOsX - 1);
                }
            }
        }
        private void ganerujFigure4(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.maxX < parametryEkranu.maxX - brickRectangle.Width)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX);
                    else if (j == 1)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX + 1);
                    else
                        generacjaIKolor(gra, kolor, i + 2 * iloscElementowOsX + 1);
                }
            }
        }
        private void ganerujFigure5(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.minX > parametryEkranu.minX + 2 * brickRectangle.Width)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        generacjaIKolor(gra, kolor, i - 1);
                    else if (j == 1)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX - 1);
                    else
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX - 2);
                }
            }
        }
        private void ganerujFigure6(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.maxX < parametryEkranu.maxX - 2 * brickRectangle.Width)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        generacjaIKolor(gra, kolor, i + 1);
                    else if (j == 1)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX + 1);
                    else
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX + 2);
                }
            }
        }
        private void ganerujFigure7(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.maxX < parametryEkranu.maxX - 2 * brickRectangle.Width)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX);
                    else if (j == 1)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX + 1);
                    else
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX + 2);
                }
            }
        }
        private void ganerujFigure8(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.minX > parametryEkranu.minX + 2 * brickRectangle.Width)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX);
                    else if (j == 1)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX - 1);
                    else
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX - 2);
                }
            }
        }
        private void ganerujFigure9(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.maxX < parametryEkranu.maxX - 2 * brickRectangle.Width)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        generacjaIKolor(gra, kolor, i + 1);
                    else if (j == 1)
                        generacjaIKolor(gra, kolor, i + 2);
                    else
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX + 2);
                }
            }
        }
        private void ganerujFigure10(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.maxX < parametryEkranu.maxX - 2 * brickRectangle.Width)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        generacjaIKolor(gra, kolor, i + 1);
                    else if (j == 1)
                        generacjaIKolor(gra, kolor, i + 2);
                    else
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX);
                }
            }
        }
        private void ganerujFigure11(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.maxX < parametryEkranu.maxX - brickRectangle.Width)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX);
                    else if (j == 1)
                        generacjaIKolor(gra, kolor, i + 2 * iloscElementowOsX);
                    else
                        generacjaIKolor(gra, kolor, i + 2 * iloscElementowOsX + 1);
                }
            }
        }
        private void ganerujFigure12(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.maxX < parametryEkranu.maxX - brickRectangle.Width)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        generacjaIKolor(gra, kolor, i + 1);
                    else if (j == 1)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX + 1);
                    else
                        generacjaIKolor(gra, kolor, i + 2 * iloscElementowOsX + 1);
                }
            }
        }
        private void ganerujFigure13(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.maxX < parametryEkranu.maxX - brickRectangle.Width)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        generacjaIKolor(gra, kolor, i + 1);
                    else if (j == 1)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX);
                    else
                        generacjaIKolor(gra, kolor, i + 2 * iloscElementowOsX);
                }
            }
        }
        private void ganerujFigure14(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.minX > parametryEkranu.minX + brickRectangle.Width)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX);
                    else if (j == 1)
                        generacjaIKolor(gra, kolor, i + 2 * iloscElementowOsX);
                    else
                        generacjaIKolor(gra, kolor, i + 2 * iloscElementowOsX - 1);
                }
            }
        }
        private void ganerujFigure15(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.maxX < parametryEkranu.maxX - brickRectangle.Width)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX);
                    else if (j == 1)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX + 1);
                    else
                        generacjaIKolor(gra, kolor, i + 2 * iloscElementowOsX);
                }
            }
        }
        private void ganerujFigure16(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.minX > parametryEkranu.minX + brickRectangle.Width)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX);
                    else if (j == 1)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX - 1);
                    else
                        generacjaIKolor(gra, kolor, i + 2 * iloscElementowOsX);
                }
            }
        }
        private void ganerujFigure17(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.maxX < parametryEkranu.maxX - brickRectangle.Width && parametryKratki.minX > parametryEkranu.minX + brickRectangle.Width)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX);
                    else if (j == 1)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX - 1);
                    else
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX + 1);
                }
            }
        }
        private void ganerujFigure18(int i, Gra gra)
        {
            parametryKratki = siatkaEkranowa[i].zwracaParametryKratki();
            if (parametryKratki.maxX < parametryEkranu.maxX - brickRectangle.Width && parametryKratki.minX > parametryEkranu.minX + brickRectangle.Width)
            {
                generacjaIKolorFirst(gra, kolor, i);
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        generacjaIKolor(gra, kolor, i + iloscElementowOsX);
                    else if (j == 1)
                        generacjaIKolor(gra, kolor, i + 1);
                    else
                        generacjaIKolor(gra, kolor, i - 1);
                }
            }
        }

        #region funkcjePomocnicze
        private void generacjaIKolorFirst(Gra gra, Color color, int indeks)
        {
            generujKlocek(gra);
            klocek.ZmienKolorTekstury(color);
            Klocki.Add(klocek);
            siatkaEkranowa[indeks].zmienZajetosc();
        }
        private void generacjaIKolor(Gra gra, Color color, int indeks)
        {
            parametryKratki = siatkaEkranowa[indeks].zwracaParametryKratki();
            generujKlocek(gra);
            klocek.ZmienKolorTekstury(color);
            Klocki.Add(klocek);
            siatkaEkranowa[indeks].zmienZajetosc();
        }
        #endregion
        #endregion
        public GeneratorKlockow(displayParametrs inParametryEkranu, Rectangle inBrickRectangle, int inIloscElementowOsX)
        {
            parametryEkranu = inParametryEkranu;
            brickRectangle = inBrickRectangle;
            iloscElementowOsX = inIloscElementowOsX;
        }
    }
}
#endregion