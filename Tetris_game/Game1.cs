/* Artur Dobrzanski */
#region using
//using System;
using System.Collections.Generic;
//using System.Linq;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;
#endregion

#region klasa glowna gry

namespace BrickHole
{

    public class Gra : Microsoft.Xna.Framework.Game
    {
        #region zmienne Œwiat gry
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public EkranTytulowy ekranTytulowy;
        public BaseSprite tloObszarInterfejsu, tloObszarGry, gornyPasekNaKlocki;
        public AktualizacjaPunktacji aktualizacjaPunktacji;
        public Opcje opcje;
        public Hero postacGracz1, postacGracz2;
        public GeneratorKlockow generatorKlockow;
        public displayParametrs parametryEkranu;
        public objectParameters parametryObiektu;
        public List<BaseSprite> GameSprites = new List<BaseSprite>();
        public Rectangle brickRectangle;
        public int iloscElementowOsX;
        SpriteFont font;
        public bool graDwuosobowa,player1Win = false, player2Win = false;
        public enum StanGry { EkranTytulowy, GraUruchomiona, TablicaWyników, Opcje, KoniecGry }
        public StanGry aktualnyStanGry = StanGry.EkranTytulowy;
        public int iloscPktGracz1, iloscPktGracz2;
        public byte szybkoscGracz1, szybkoscPokonaniaKlatkiPrzezKlocek;

        #endregion
        #region g³ówne funkcje

        public Gra()
        {
            graphics = new GraphicsDeviceManager(this);
            setScreenSize();
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            iloscElementowOsX = 20;
            iloscPktGracz1 = 0; 
            szybkoscGracz1 = 5;//5
            iloscPktGracz2 = 0;
            szybkoscPokonaniaKlatkiPrzezKlocek = 15;//15
            base.Initialize();
        }
        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("SpriteFont1");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ladujParametry();
            tloObszarInterfejsu = new BaseSprite(Content.Load<Texture2D>("grafika/tloInterfejs"), new Rectangle(0, 0,
                                    (int) graphics.GraphicsDevice.Viewport.Width, (int) graphics.GraphicsDevice.Viewport.Height));
            GameSprites.Add(tloObszarInterfejsu);
            tloObszarGry = new BaseSprite(Content.Load<Texture2D>("grafika/tloObszarGry"), new Rectangle((int) parametryEkranu.minX, 
                          (int) parametryEkranu.minY, (int) (parametryEkranu.maxX - parametryEkranu.minX), (int) (parametryEkranu.maxY - parametryEkranu.minY)));
            GameSprites.Add(tloObszarGry);   
            ekranTytulowy = new EkranTytulowy(Content.Load<Texture2D>("grafika/EkranTytulowy"),new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), font );
            tloObszarInterfejsu = new BaseSprite(Content.Load<Texture2D>("grafika/tloInterfejs"), new Rectangle((int)parametryEkranu.minX, 0,
                                   (int)parametryEkranu.maxX - (int)parametryEkranu.minX, (int)parametryEkranu.minY));
            aktualizacjaPunktacji = new AktualizacjaPunktacji(new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), font);
            opcje = new Opcje(Content.Load<Texture2D>("grafika/EkranTytulowy"), new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), font);
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            switch (aktualnyStanGry)
            {
                case StanGry.EkranTytulowy:
                    ekranTytulowy.Update(this, keyState);
                    break;
                    case StanGry.GraUruchomiona:
                    UpdateGraUruchomiona(keyState);
                    break;
                case StanGry.KoniecGry:
                    ekranTytulowy.GameOver(this, keyState);
                    UpdateGraUruchomiona(keyState);
                    break;
                case StanGry.Opcje:
                    opcje.Update(this, keyState);
                    break;
                case StanGry.TablicaWyników:
                    break;
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            switch (aktualnyStanGry)
            {
                case StanGry.EkranTytulowy:
                    ekranTytulowy.Draw(spriteBatch);
                    break;
                case StanGry.GraUruchomiona:
                    RysujGraUruchomiona();
                    break;
                case StanGry.KoniecGry:
                    RysujGraUruchomiona();
                    ekranTytulowy.GameOverDraw(graDwuosobowa, player1Win, player2Win, spriteBatch);
                    break;
                case StanGry.Opcje:
                    opcje.Draw(this, spriteBatch);
                    break;
                case StanGry.TablicaWyników:
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion
        #region funkcje klasy BrickHole
      
        public void newGame()
        {
            player1Win = false;
            player2Win = false;
            if (graDwuosobowa)
            {
                ladujParametryHeroDla2Graczy(1);
                postacGracz1 = new Hero(Content.Load<Texture2D>("grafika/Hero"), parametryObiektu, parametryEkranu);
                ladujParametryHeroDla2Graczy(2);
                postacGracz2 = new Hero(Content.Load<Texture2D>("grafika/Hero2"), parametryObiektu, parametryEkranu);
                postacGracz2.StartGame();
            }
            else
            {
                ladujParametryHero();
                postacGracz1 = new Hero(Content.Load<Texture2D>("grafika/Hero"), parametryObiektu, parametryEkranu);     
            }
            generatorKlockow = new GeneratorKlockow(parametryEkranu, brickRectangle, iloscElementowOsX);
            generatorKlockow.generuj(this);
            postacGracz1.StartGame();
            iloscPktGracz1 = 0;
            iloscPktGracz2 = 0;
        }
        public void dodajPunkty(int nrGracza)
        {
            if (nrGracza == 1)
                iloscPktGracz1++;
            else iloscPktGracz2++;
        }
        private void setScreenSize()
        {
          //  graphics.ToggleFullScreen();
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 650;
        }
        private void ladujParametry()
        {
            ladujParametryEkranu();
            setBrickRectangle();
        }
        private void ladujParametryEkranu()
        {
            parametryEkranu.maxX = obliczParametrEkranu(80, true);
            parametryEkranu.minX = obliczParametrEkranu(20, true);
            parametryEkranu.maxY = obliczParametrEkranu(95, false);
            parametryEkranu.minY = obliczParametrEkranu(5, false);
        }
        private void ladujParametryHero()
        {
            parametryObiektu.objectRectangle.Width = brickRectangle.Width - (brickRectangle.Width / 2);
            parametryObiektu.objectRectangle.Height = brickRectangle.Height - (brickRectangle.Height / 4);
            parametryObiektu.xInitial = obliczParametrEkranu(50, true); 
            parametryObiektu.yInitial = parametryEkranu.maxY;
            parametryObiektu.x = parametryObiektu.xInitial;
            parametryObiektu.y = parametryObiektu.yInitial;
            parametryObiektu.xSpeed = szybkoscGracz1;
            parametryObiektu.ySpeed = szybkoscGracz1;
        }
        private void ladujParametryHeroDla2Graczy(int nrGracza)
        {
            if (nrGracza == 1)
            {
                parametryObiektu.xInitial = parametryEkranu.maxX;
                parametryObiektu.yInitial = parametryEkranu.maxY;
            }
            else
            {
                parametryObiektu.xInitial = parametryEkranu.minX;
                parametryObiektu.yInitial = parametryEkranu.maxY;
            }
            parametryObiektu.objectRectangle.Width = brickRectangle.Width - (brickRectangle.Width / 2);
            parametryObiektu.objectRectangle.Height = brickRectangle.Height - (brickRectangle.Height / 4);
            parametryObiektu.xSpeed = szybkoscGracz1;
            parametryObiektu.ySpeed = szybkoscGracz1;
        }
        private float getPercentage(float percentage, float inputValue)
        {
            return (inputValue * percentage) / 100;
        }
        private void setBrickRectangle()
        {
            brickRectangle.Width = (((parametryEkranu.maxX - parametryEkranu.minX) / iloscElementowOsX));
            brickRectangle.Height = brickRectangle.Width;
            parametryObiektu.objectRectangle.Width = brickRectangle.Width;
            parametryObiektu.objectRectangle.Height = brickRectangle.Height;
        }
        private int obliczParametrEkranu(int procent, bool os)
        {
            if (os == true)
                return (int)(getPercentage(procent, graphics.GraphicsDevice.Viewport.Width)
                    - getPercentage(procent, graphics.GraphicsDevice.Viewport.Width) % iloscElementowOsX);
            else return (int)(getPercentage(procent, graphics.GraphicsDevice.Viewport.Height) -
                getPercentage(procent, graphics.GraphicsDevice.Viewport.Width) % iloscElementowOsX);
        }
        private void RysujGraUruchomiona()
        {
            foreach (BaseSprite sprite in GameSprites)
                sprite.Draw(spriteBatch);
            generatorKlockow.Draw(spriteBatch);
            tloObszarInterfejsu.Draw(spriteBatch);
            aktualizacjaPunktacji.DrawPunktow(this, spriteBatch);
            if (graDwuosobowa)
                postacGracz2.Draw(spriteBatch);
            postacGracz1.Draw(spriteBatch);
        }
        private void UpdateGraUruchomiona(KeyboardState keyState)
        {
            foreach (BaseSprite sprite in GameSprites)
                sprite.Update(this, keyState);
            foreach (Brick sprite in generatorKlockow.Klocki)
            {
                sprite.Update(this, keyState);
            }
            generatorKlockow.Update(this);
            postacGracz1.Update(this, keyState);
            
        }
        #endregion        
    }
}
#endregion