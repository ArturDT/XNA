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
    public class Napisy 
    {
        protected Rectangle spriteRectangle;
        protected SpriteFont font;
        protected KeyboardState oldKey;

        public Napisy(Rectangle inRectangle, SpriteFont inFont)
        {
            spriteRectangle = inRectangle;
            font = inFont;
        }
    }
    public class Menu: Napisy
    {
        protected Texture2D spriteTexture;

        public void UpdateOldKey(KeyboardState inOldKey)
        {
            oldKey = inOldKey;
        }

        public Menu(Texture2D inSpriteTexture, Rectangle inRectangle, SpriteFont inFont)
            : base(inRectangle, inFont)
        {
            spriteRectangle = inRectangle;
            font = inFont;
        }
    }
    public class EkranTytulowy : Menu
    {   
        private enum wyborMenu { startOnePlayer, startTwoPlayer, options, highscores, about, exit }
        private wyborMenu wybranaOpcjaMenu = wyborMenu.startOnePlayer;
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spriteRectangle, Color.White);
            if (wybranaOpcjaMenu == wyborMenu.startOnePlayer)
                spriteBatch.DrawString(font, "One Player", new Vector2(spriteRectangle.Width / 2 - (spriteRectangle.Width / 10), (spriteRectangle.Height / 7)), Color.Blue);
            else spriteBatch.DrawString(font, "One Player", new Vector2(spriteRectangle.Width / 2 - (spriteRectangle.Width / 10), (spriteRectangle.Height / 7)), Color.Red);
            if (wybranaOpcjaMenu == wyborMenu.startTwoPlayer)
                spriteBatch.DrawString(font, "Two Players", new Vector2(spriteRectangle.Width / 2 - (spriteRectangle.Width / 10), (spriteRectangle.Height / 7)*2), Color.Blue);
            else spriteBatch.DrawString(font, "Two Players", new Vector2(spriteRectangle.Width / 2 - (spriteRectangle.Width / 10), (spriteRectangle.Height / 7) * 2), Color.Red);
            if (wybranaOpcjaMenu == wyborMenu.options)
                spriteBatch.DrawString(font, "Options", new Vector2(spriteRectangle.Width / 2 - (spriteRectangle.Width / 12), (spriteRectangle.Height / 7) * 3), Color.Blue);
            else spriteBatch.DrawString(font, "Options", new Vector2(spriteRectangle.Width / 2 - (spriteRectangle.Width / 12), (spriteRectangle.Height / 7) * 3), Color.Red);
            if (wybranaOpcjaMenu == wyborMenu.highscores)
                spriteBatch.DrawString(font, "HighScores", new Vector2(spriteRectangle.Width / 2 - (spriteRectangle.Width / 10), (spriteRectangle.Height / 7) * 4), Color.Blue);
            else spriteBatch.DrawString(font, "HighScores", new Vector2(spriteRectangle.Width / 2 - (spriteRectangle.Width / 10), (spriteRectangle.Height / 7) * 4), Color.Red);
            if (wybranaOpcjaMenu == wyborMenu.about)
                spriteBatch.DrawString(font, "About", new Vector2(spriteRectangle.Width / 2 - (spriteRectangle.Width / 15), (spriteRectangle.Height / 7) * 5), Color.Blue);
            else spriteBatch.DrawString(font, "About", new Vector2(spriteRectangle.Width / 2 - (spriteRectangle.Width / 15), (spriteRectangle.Height / 7) * 5), Color.Red);
            if (wybranaOpcjaMenu == wyborMenu.exit)
                spriteBatch.DrawString(font, "Exit", new Vector2(spriteRectangle.Width / 2 - (spriteRectangle.Width / 20), (spriteRectangle.Height / 7) * 6), Color.Blue);
            else spriteBatch.DrawString(font, "Exit", new Vector2(spriteRectangle.Width / 2 - (spriteRectangle.Width / 20), (spriteRectangle.Height / 7) * 6), Color.Red);
        }
        public void Update(Gra game, KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Escape) && oldKey.IsKeyUp(Keys.Escape))
                game.Exit();
            switch (wybranaOpcjaMenu)
            {
                case wyborMenu.startOnePlayer:
                    if (keyState.IsKeyDown(Keys.Up) && oldKey.IsKeyUp(Keys.Up))
                        wybranaOpcjaMenu = wyborMenu.exit;
                    if (keyState.IsKeyDown(Keys.Down) && oldKey.IsKeyUp(Keys.Down))
                        wybranaOpcjaMenu = wyborMenu.startTwoPlayer;
                    if (keyState.IsKeyDown(Keys.Enter) && oldKey.IsKeyUp(Keys.Enter))
                    {
                        game.graDwuosobowa = false;
                        game.aktualnyStanGry = Gra.StanGry.GraUruchomiona;
                        game.newGame();
                    }
                    break;
                case wyborMenu.startTwoPlayer:
                    if (keyState.IsKeyDown(Keys.Up) && oldKey.IsKeyUp(Keys.Up))
                        wybranaOpcjaMenu = wyborMenu.startOnePlayer;
                    if (keyState.IsKeyDown(Keys.Down) && oldKey.IsKeyUp(Keys.Down))
                        wybranaOpcjaMenu = wyborMenu.options;
                    if (keyState.IsKeyDown(Keys.Enter) && oldKey.IsKeyUp(Keys.Enter))
                    {
                        game.graDwuosobowa = true;
                        game.aktualnyStanGry = Gra.StanGry.GraUruchomiona;
                        game.newGame();
                    }
                    break;
                case wyborMenu.options:
                    if (keyState.IsKeyDown(Keys.Up) && oldKey.IsKeyUp(Keys.Up))
                        wybranaOpcjaMenu = wyborMenu.startTwoPlayer;
                    if (keyState.IsKeyDown(Keys.Down) && oldKey.IsKeyUp(Keys.Down))
                        wybranaOpcjaMenu = wyborMenu.highscores;
                    if (keyState.IsKeyDown(Keys.Enter) && oldKey.IsKeyUp(Keys.Enter))
                    {
                        game.opcje.UpdateOldKey(keyState);
                        game.aktualnyStanGry = Gra.StanGry.Opcje;
                    }
                    break;
                case wyborMenu.highscores:
                    if (keyState.IsKeyDown(Keys.Up) && oldKey.IsKeyUp(Keys.Up))
                        wybranaOpcjaMenu = wyborMenu.options;
                    if (keyState.IsKeyDown(Keys.Down) && oldKey.IsKeyUp(Keys.Down))
                        wybranaOpcjaMenu = wyborMenu.about;
                    break;
                case wyborMenu.about:
                    if (keyState.IsKeyDown(Keys.Up) && oldKey.IsKeyUp(Keys.Up))
                        wybranaOpcjaMenu = wyborMenu.highscores;
                    if (keyState.IsKeyDown(Keys.Down) && oldKey.IsKeyUp(Keys.Down))
                        wybranaOpcjaMenu = wyborMenu.exit;
                    break;
                case wyborMenu.exit:
                    if (keyState.IsKeyDown(Keys.Up) && oldKey.IsKeyUp(Keys.Up))
                        wybranaOpcjaMenu = wyborMenu.about;
                    if (keyState.IsKeyDown(Keys.Down) && oldKey.IsKeyUp(Keys.Down))
                        wybranaOpcjaMenu = wyborMenu.startOnePlayer;
                    if (keyState.IsKeyDown(Keys.Enter))
                        game.Exit();
                    break;
            }
            oldKey = keyState;
        }
        public void GameOver(Gra game, KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Enter))
                game.aktualnyStanGry = Gra.StanGry.EkranTytulowy;
            if (game.player1Win)
                game.postacGracz2.death(game);
            else 
                game.postacGracz1.death(game);
        }
        public void GameOverDraw(bool graDwuosobowa, bool player1Win, bool player2Win, SpriteBatch spriteBatch)
        {
            if (graDwuosobowa)
            {
                if (player1Win)
                    spriteBatch.DrawString(font, "Player 1 Win", new Vector2(spriteRectangle.Width - (spriteRectangle.Width / 5), (spriteRectangle.Height / 6)), Color.Blue);
                else if (player2Win)
                    spriteBatch.DrawString(font, "Player 2 Win", new Vector2(0, (spriteRectangle.Height / 6)), Color.Red);
            }
            else
                spriteBatch.DrawString(font, "Game Over", new Vector2(spriteRectangle.Width - (spriteRectangle.Width / 5), (spriteRectangle.Height / 6)), Color.Red);
        }

        public EkranTytulowy(Texture2D inSpriteTexture, Rectangle inRectangle, SpriteFont inFont)
            : base(inSpriteTexture, inRectangle, inFont)
        {
            spriteTexture = inSpriteTexture;
            spriteRectangle = inRectangle;
            font = inFont;
        }

    }
    public class Opcje : Menu
    {
        private enum wyborOpcji { powrot }
        private wyborOpcji wybranaOpcjaOpcje = wyborOpcji.powrot;

        public void Draw(Gra game, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spriteRectangle, Color.White);
            if (wybranaOpcjaOpcje == wyborOpcji.powrot)
                spriteBatch.DrawString(font, "Back", new Vector2(spriteRectangle.Width / 2 - (spriteRectangle.Width / 20), (spriteRectangle.Height / 7) * 5), Color.Blue);
            else spriteBatch.DrawString(font, "Back", new Vector2(spriteRectangle.Width / 2 - (spriteRectangle.Width / 20), (spriteRectangle.Height / 7) * 5), Color.Red);
        }
        public void Update(Gra game, KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Escape) && oldKey.IsKeyUp(Keys.Escape))
                game.aktualnyStanGry = Gra.StanGry.EkranTytulowy;
            
            switch (wybranaOpcjaOpcje)
            {
                case wyborOpcji.powrot:
                    if (keyState.IsKeyDown(Keys.Up) && oldKey.IsKeyUp(Keys.Up))
                        wybranaOpcjaOpcje = wyborOpcji.powrot;
                    if (keyState.IsKeyDown(Keys.Down) && oldKey.IsKeyUp(Keys.Down))
                        wybranaOpcjaOpcje = wyborOpcji.powrot;
                    if (keyState.IsKeyDown(Keys.Enter) && oldKey.IsKeyUp(Keys.Enter))
                    {
                        game.aktualnyStanGry = Gra.StanGry.EkranTytulowy;
                        game.opcje.UpdateOldKey(keyState);
                    }
                    break;
            }
            oldKey = keyState;     
        }

        public Opcje(Texture2D inSpriteTexture, Rectangle inRectangle, SpriteFont inFont)
            : base(inSpriteTexture, inRectangle, inFont)
        {
            spriteTexture = inSpriteTexture;
            spriteRectangle = inRectangle;
            font = inFont;
        }
    }

    public class AktualizacjaPunktacji: Napisy
    {
        public void DrawPunktow(Gra game, SpriteBatch spriteBatch)
        {
            if (game.graDwuosobowa == false)
            {
                spriteBatch.DrawString(font, "Score:", new Vector2(spriteRectangle.Width - (spriteRectangle.Width / 5), (spriteRectangle.Height / 4)), Color.Blue);
                spriteBatch.DrawString(font, game.iloscPktGracz1.ToString(), new Vector2(spriteRectangle.Width - (spriteRectangle.Width / 5), (spriteRectangle.Height / 3)), Color.Blue);
            }
            else
            {
                spriteBatch.DrawString(font, "Score Player 1:", new Vector2(spriteRectangle.Width - (spriteRectangle.Width / 5), (spriteRectangle.Height / 4)), Color.Blue);
                spriteBatch.DrawString(font, game.iloscPktGracz1.ToString(), new Vector2(spriteRectangle.Width - (spriteRectangle.Width / 5), (spriteRectangle.Height / 3)), Color.Blue);

                spriteBatch.DrawString(font, "Score Player 2:", new Vector2(0, (spriteRectangle.Height / 4)), Color.Red);
                spriteBatch.DrawString(font, game.iloscPktGracz2.ToString(), new Vector2(0, (spriteRectangle.Height / 3)), Color.Red);
            }
        }
        public AktualizacjaPunktacji(Rectangle inRectangle, SpriteFont inFont) : base(inRectangle, inFont)
        {
            spriteRectangle = inRectangle;
            font = inFont;
        }
    }
}