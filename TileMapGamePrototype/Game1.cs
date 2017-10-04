/* Artur Dobrzanski */ 
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;


namespace Krasnal
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Kursor kursor;
        public Player gracz;
        public StanGracza stanGracza;
        public MapaOperacje mapa;
        float minDisplayX, maxDisplayX, minDisplayY, maxDisplayY;
        float overScanPercentage = 10.0f;
        public static int ileDodacDoX, ileDodacDoY;
        Plecak plecak;
        #region tekst

        public static string wiadomosc, wiadomosc2, wiadomosc3, wiadomosc4;

        private void loadFont()
        {
            Silnik.font = Content.Load<SpriteFont>("SpriteFont1");
        }

        void drawText(string text, Color textColor, float x, float y)
        {
            Vector2 textVector = new Vector2(x, y);
            spriteBatch.DrawString(Silnik.font, text, textVector, textColor);
        }
        void drawAllText()
        {
            drawText(wiadomosc, Color.Black, 0, maxDisplayY);
            drawText(wiadomosc2, Color.Blue, minDisplayX, maxDisplayY - 100);
            drawText(wiadomosc3, Color.Blue, minDisplayX, maxDisplayY - 150);
            drawText(wiadomosc4, Color.Blue, minDisplayX, maxDisplayY - 200);
        }
        #endregion
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Silnik.displayWidth;
            graphics.PreferredBackBufferHeight = Silnik.displayHeight;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            wiadomosc = wiadomosc2 = wiadomosc3 = wiadomosc4 = "";
            MapaObiekt mapaBuf = new MapaObiekt();
            setScreenSizes();
            ileDodacDoX = 0;
            ileDodacDoY = 0;
            gracz = new Player();
            mapa = new MapaOperacje();
            plecak = new Plecak();
            kursor = new Kursor();
            stanGracza = new StanGracza();
            mapa.wczytajMape(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            gracz.LoadTexture(Content.Load<Texture2D>("grafika/hero"));
            mapa.kafelka.LoadTexture(Content.Load<Texture2D>("grafika/kafelki"));
            mapa.kafelka.LoadTexture2(Content.Load<Texture2D>("grafika/obiektyMapa"));
            mapa.itemek.LoadTexture(Content.Load<Texture2D>("grafika/plecak"));
            gracz.StartGame(minDisplayX, maxDisplayX, minDisplayY, maxDisplayY);
            loadFont();
            kursor.LoadTexture(Content.Load<Texture2D>("grafika/kursor"));
            plecak.LoadTexture(Content.Load<Texture2D>("grafika/plecak"));
            stanGracza.LoadTexture(Content.Load<Texture2D>("grafika/stanyGracza"));

        }

        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
                this.Exit();
            int keyCode = checkKeys(keyState);
            kursor.pozycjaKursora = new Vector2(mouseState.X, mouseState.Y);
            kursor.update(this, mouseState);
            gracz.Update(this, keyCode);
            mapa.Update(gracz, keyCode);
            plecak.update();
            stanGracza.update();
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            mapa.rysujMape(spriteBatch, ileDodacDoX, ileDodacDoY);
            gracz.animacja(spriteBatch);
            drawAllText();
            plecak.rysujPlecak(spriteBatch);
            kursor.rysujKursor(spriteBatch);
            stanGracza.rysujStany(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }


        private void setScreenSizes()
        {
            Silnik.displayWidth = graphics.GraphicsDevice.Viewport.Width;
            Silnik.displayHeight = graphics.GraphicsDevice.Viewport.Height;
            float xOverscanMargin = getPercentage(overScanPercentage, Silnik.displayWidth) / 2.0f;
            float yOverscanMargin = getPercentage(overScanPercentage, Silnik.displayHeight) / 2.0f;

            minDisplayX = xOverscanMargin;
            minDisplayY = yOverscanMargin;

            maxDisplayX = Silnik.displayWidth - xOverscanMargin;
            maxDisplayY = Silnik.displayHeight - yOverscanMargin;
        }
        float getPercentage(float percentage, float inputValue)
        {
            return (inputValue * percentage) / 100;
        }
        private int checkKeys(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Left) && keyState.IsKeyDown(Keys.Up)) return 5;
            else if (keyState.IsKeyDown(Keys.Left) && keyState.IsKeyDown(Keys.Down)) return 7;
            else if (keyState.IsKeyDown(Keys.Right) && keyState.IsKeyDown(Keys.Up)) return 6;
            else if (keyState.IsKeyDown(Keys.Right) && keyState.IsKeyDown(Keys.Down)) return 8;             //keyCode:
            else if (keyState.IsKeyDown(Keys.Left)) return 1;                                               //1 - lewo
            else if (keyState.IsKeyDown(Keys.Right)) return 2;                                              //2 - prawo
            else if (keyState.IsKeyDown(Keys.Up)) return 3;                                                 //3 - gora
            else if (keyState.IsKeyDown(Keys.Down)) return 4;                                               //4 - dol
            //5 - gora+lewo - skos
            //6 - gora+prawo - skos
            //7 - dol+lewo - skos
            //8 - dol+prawo - skos
            return 0;

        }
    }
}
