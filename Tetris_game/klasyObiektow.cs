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
    #region struktury

    public struct displayParametrs
    {
        public int minX, maxX, minY, maxY;
    }
    public struct objectParameters
    {
        public int x, y, xSpeed, ySpeed, xInitial, yInitial;
        public Rectangle objectRectangle;
    }

    #endregion
    #region klasy dodatkowe gry

    public class BaseSprite
    {
        protected Texture2D spriteTexture;
        protected Rectangle spriteRectangle;
        protected Color kolorTekstury = Color.White;

        public void ZmienKolorTekstury(Color nowyKolor)
        {
            kolorTekstury = nowyKolor;
        }
        public void LoadTexture(Texture2D inSpriteTexture)
        {
            spriteTexture = inSpriteTexture;
        }
        public void SetRectangle(Rectangle inSpriteRectangle)
        {
            spriteRectangle = inSpriteRectangle;
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spriteRectangle, kolorTekstury);
        }
        public virtual void Update(Gra game, KeyboardState keyState)
        {
        }
        public BaseSprite(Texture2D inSpriteTexture, Rectangle inRectangle)
        {
            LoadTexture(inSpriteTexture);
            SetRectangle(inRectangle);
        }
    }
    public class ObiektRuchomy : BaseSprite
    {
        protected objectParameters parametryObiektu;
        protected displayParametrs rozmiarEkranu;
        public float YPos
        {
            get
            {
                return parametryObiektu.y;
            }
        }
        public int fallSpeed
        {
            get
            {
                return parametryObiektu.ySpeed;
            }
        }
        public Rectangle rectangle
        {
            get
            {
                return spriteRectangle;
            }
        }
        public virtual bool CheckCollision(Rectangle target)
        {
            return spriteRectangle.Intersects(target);
        }
        public void StartGame()
        {
            parametryObiektu.x = parametryObiektu.xInitial;
            parametryObiektu.y = parametryObiektu.yInitial;
            spriteRectangle.X = (int)parametryObiektu.x;
            spriteRectangle.Y = (int)parametryObiektu.y;
        }

        public ObiektRuchomy(Texture2D inSpriteTexture, objectParameters inParametryObiektu, displayParametrs inRozmiarEkranu)
            : base(inSpriteTexture, Rectangle.Empty)
        {
            parametryObiektu = inParametryObiektu;
            rozmiarEkranu = inRozmiarEkranu;
            spriteRectangle.Width = parametryObiektu.objectRectangle.Width;
            spriteRectangle.Height = parametryObiektu.objectRectangle.Height;
        }
    }
    public class Hero : ObiektRuchomy
    {
        public override void Update(Gra game, KeyboardState keyState)
        {
            if (game.aktualnyStanGry != Gra.StanGry.KoniecGry)
            {
                if (checkKey(game, keyState))
                {
                    game.aktualnyStanGry = Gra.StanGry.EkranTytulowy;
                }
                checkOutOfScreen();
                saveHeroPosition();
            }
            if (game.graDwuosobowa)
            {
                game.postacGracz2.Update2(game, keyState);
                PoprawKolizjeGraczy(game);
            }
        }
        private void Update2(Gra game, KeyboardState keyState)
        {
            if (game.aktualnyStanGry != Gra.StanGry.KoniecGry)
            {
                if (checkKeyPlayer2(game, keyState))
                {
                    game.aktualnyStanGry = Gra.StanGry.EkranTytulowy;
                }
                checkOutOfScreen();
                saveHeroPosition();
            }
        }
        public void death(Gra game)
        {
            spriteRectangle.Y += (int)parametryObiektu.ySpeed;
            spriteRectangle.Height -= (int)parametryObiektu.ySpeed;
        }
        private bool checkKey(Gra game, KeyboardState keyState)
        {
            byte pxKolizja = 2;
            if (keyState.IsKeyDown(Keys.P))
            {
                return true;
            }
            if (keyState.IsKeyDown(Keys.Left))
            {
                for (byte i = 0; i < parametryObiektu.xSpeed; i++)
                {
                    if (game.generatorKlockow.SprawdzKolizjeGraczy(game) == false)
                        parametryObiektu.x--;
                    else if (keyState.IsKeyUp(Keys.A) && game.generatorKlockow.pozwolenieNaRuch(game))
                            game.postacGracz2.parametryObiektu.x -= pxKolizja;
                }
            }
            if (keyState.IsKeyDown(Keys.Right))
            {
                for (byte i = 0; i < parametryObiektu.xSpeed; i++)
                {
                    if (game.generatorKlockow.SprawdzKolizjeGraczy(game) == false)
                        parametryObiektu.x++;
                    else if (keyState.IsKeyUp(Keys.D) && game.generatorKlockow.pozwolenieNaRuch(game))
                            game.postacGracz2.parametryObiektu.x += pxKolizja;
                }
            }
            if (keyState.IsKeyDown(Keys.Up))
            {
                for (byte i = 0; i < parametryObiektu.xSpeed; i++)
                {
                    if (game.generatorKlockow.SprawdzKolizjeGraczy(game) == false)
                        parametryObiektu.y--;
                    else if (keyState.IsKeyUp(Keys.W) && game.generatorKlockow.pozwolenieNaRuch(game)) 
                            game.postacGracz2.parametryObiektu.y -= pxKolizja;
                }
            }
            if (keyState.IsKeyDown(Keys.Down))
            {
                for (byte i = 0; i < parametryObiektu.xSpeed; i++)
                {
                    if (game.generatorKlockow.SprawdzKolizjeGraczy(game) == false)
                        parametryObiektu.y++;
                    else if (keyState.IsKeyUp(Keys.S) && game.generatorKlockow.pozwolenieNaRuch(game)) 
                            game.postacGracz2.parametryObiektu.y += pxKolizja;
                }
            }

            return false;
        }
        private bool checkKeyPlayer2(Gra game, KeyboardState keyState)
        {
            byte pxKolizja = 2;
            if (keyState.IsKeyDown(Keys.P))
            {
                return true;
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                for (byte i = 0; i < parametryObiektu.xSpeed; i++)
                {
                    if (game.generatorKlockow.SprawdzKolizjeGraczy(game) == false)
                        parametryObiektu.x--;
                    else if (keyState.IsKeyUp(Keys.Left) && game.generatorKlockow.pozwolenieNaRuch(game)) 
                            game.postacGracz1.parametryObiektu.x -= pxKolizja;
                }
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                for (byte i = 0; i < parametryObiektu.xSpeed; i++)
                {
                    if (game.generatorKlockow.SprawdzKolizjeGraczy(game) == false)
                        parametryObiektu.x++;
                    else if (keyState.IsKeyUp(Keys.Right) && game.generatorKlockow.pozwolenieNaRuch(game)) 
                            game.postacGracz1.parametryObiektu.x += pxKolizja;
                }
            }
            if (keyState.IsKeyDown(Keys.W))
            {
                for (byte i = 0; i < parametryObiektu.xSpeed; i++)
                {
                    if (game.generatorKlockow.SprawdzKolizjeGraczy(game) == false)
                        parametryObiektu.y--;
                    else if (keyState.IsKeyUp(Keys.Up) && game.generatorKlockow.pozwolenieNaRuch(game)) 
                            game.postacGracz1.parametryObiektu.y -= pxKolizja;
                }
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                for (byte i = 0; i < parametryObiektu.xSpeed; i++)
                {
                    if (game.generatorKlockow.SprawdzKolizjeGraczy(game) == false)
                        parametryObiektu.y++;
                    else if (keyState.IsKeyUp(Keys.Down) && game.generatorKlockow.pozwolenieNaRuch(game)) 
                            game.postacGracz1.parametryObiektu.y += pxKolizja;
                }
            }
            return false;
        }
        private void PoprawKolizjeGraczy(Gra game)
        {
            if (game.generatorKlockow.SprawdzKolizjeGraczy(game))
            {
                if (game.postacGracz1.parametryObiektu.y < game.postacGracz2.parametryObiektu.y)
                {
                    if (game.postacGracz2.parametryObiektu.y - game.postacGracz1.parametryObiektu.y > game.postacGracz1.parametryObiektu.objectRectangle.Width)
                    {
                        game.postacGracz1.parametryObiektu.y -= 2;
                        game.postacGracz2.parametryObiektu.y += 2;
                    }
                    else if (game.postacGracz1.parametryObiektu.x < game.postacGracz2.parametryObiektu.x)
                    {
                        game.postacGracz1.parametryObiektu.x -= 2;
                        game.postacGracz2.parametryObiektu.x += 2;
                    }
                    else
                    {
                        game.postacGracz1.parametryObiektu.x += 2;
                        game.postacGracz2.parametryObiektu.x -= 2;
                    }
                }
                else
                {
                    if (game.postacGracz1.parametryObiektu.y - game.postacGracz2.parametryObiektu.y > game.postacGracz2.parametryObiektu.objectRectangle.Width)
                    {
                        game.postacGracz2.parametryObiektu.y -= 2;
                        game.postacGracz1.parametryObiektu.y += 2;
                    }
                    else if (game.postacGracz2.parametryObiektu.x < game.postacGracz1.parametryObiektu.x)
                    {
                        game.postacGracz2.parametryObiektu.x -= 2;
                        game.postacGracz1.parametryObiektu.x += 2;
                    }
                    else
                    {
                        game.postacGracz2.parametryObiektu.x++;
                        game.postacGracz1.parametryObiektu.x--;
                    }
                }
            }
               
        }
        private void checkOutOfScreen()
        {
            if (parametryObiektu.x < rozmiarEkranu.minX)
                parametryObiektu.x = rozmiarEkranu.minX;
            if (parametryObiektu.x + spriteRectangle.Width >= rozmiarEkranu.maxX)
                parametryObiektu.x = rozmiarEkranu.maxX - spriteRectangle.Width;
            if (parametryObiektu.y < rozmiarEkranu.minY)
                parametryObiektu.y = rozmiarEkranu.minY;
            if (parametryObiektu.y + spriteRectangle.Height >= rozmiarEkranu.maxY)
                parametryObiektu.y = rozmiarEkranu.maxY - spriteRectangle.Height;
        }
        private void saveHeroPosition()
        {
            spriteRectangle.X = (int)parametryObiektu.x;
            spriteRectangle.Y = (int)parametryObiektu.y;
        }

        public Hero(Texture2D inSpriteTexture, objectParameters inParametryObiektu, displayParametrs inRozmiarEkranu)
            : base(inSpriteTexture, inParametryObiektu, inRozmiarEkranu)
        {
        }
    }
    public class Brick : ObiektRuchomy
    {
        public override void Update(Gra game, KeyboardState keyState)
        {
            checkOutOfScreen();
            saveBrickPosition();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spriteRectangle, kolorTekstury);
        }
        public void moveBrick(bool kierunek)
        {
            if (kierunek == true)
                parametryObiektu.y += parametryObiektu.ySpeed;
            else parametryObiektu.y -= parametryObiektu.ySpeed;
            saveBrickPosition();
        }
        public void moveBrick1px(bool kierunek)
        {
            if (kierunek == true)
                parametryObiektu.y += 1;
            else parametryObiektu.y -= 1;
            saveBrickPosition();
        }
        private void checkOutOfScreen()
        {
            if (parametryObiektu.x < rozmiarEkranu.minX)
                parametryObiektu.x = rozmiarEkranu.minX;
            if (parametryObiektu.x + spriteRectangle.Width >= rozmiarEkranu.maxX)
                parametryObiektu.x = rozmiarEkranu.maxX - spriteRectangle.Width;
            if (parametryObiektu.y + spriteRectangle.Height >= rozmiarEkranu.maxY)
                parametryObiektu.y = rozmiarEkranu.maxY - spriteRectangle.Height;
        }
        private void saveBrickPosition()
        {
            spriteRectangle.X = (int)parametryObiektu.x;
            spriteRectangle.Y = (int)parametryObiektu.y;
        }

        public Brick(Texture2D inSpriteTexture, objectParameters inParametryObiektu, displayParametrs inRozmiarEkranu)
            : base(inSpriteTexture, inParametryObiektu, inRozmiarEkranu)
        {
        }
    }

    #endregion
}