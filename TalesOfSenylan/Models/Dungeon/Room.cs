using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TalesOfSenylan.Models.Characters;
using TalesOfSenylan.Models.Utilities;

namespace TalesOfSenylan.Models.Dungeon
{
    public class Room
    {
        private Player player { get; set; }
        public List<Enemy> enemies { get; set; }

        private static Rectangle[][] tiles;
        private const int worldWidth = 50;
        private const int worldDepth = 50;
        private const int TileWidth = 16;
        private const int TileHeight = 16;


        private KeyboardState keyboardState;
        public ContentManager contentManager { get; }

        public Room(int dungeonNumber, Player player, ContentManager contentManager)
        {
            this.player = player;
            enemies = new List<Enemy>();
            this.contentManager = contentManager;

            for (int i = 0; i < DungeonUtilities.GetNumberOfEnemies(dungeonNumber); i++)
            {
                enemies.Add(new Enemy(GenerateRandomStartingPosition(), this));
            }
        }

        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            HandleMovement(gameTime);
            player.Update(gameTime);
            Debug.WriteLine("Le joueur a: " + player.health + " Points de vie");

            //ToList() to make a copy of the list and remove an item safely from the original list
            foreach (Enemy enemy in enemies.ToList())
            {
                if (player.IsCollided(enemy.GetHitbox()) && (keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.K)))
                {
                    enemy.health -= HandleAttack(gameTime);

                    if (enemy.health <= 0)
                    {
                        enemies.Remove(enemy);
                    }
                }
                enemy.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            player.Draw(gameTime, spriteBatch);

            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(gameTime, spriteBatch);
            }

            GenerateRoomFloor(spriteBatch);
        }

        private static Vector2 GenerateRandomStartingPosition()
        {
            // Todo: position shouldn't be between 20 and 200 => make it respect dungeon bounds.
            int x = Utilities.Utilities.GetRandomNumber(20, 200);
            int y = Utilities.Utilities.GetRandomNumber(20, 200);

            return new Vector2(x, y);
        }

        //player Attack Handling
        private int HandleAttack(GameTime gameTime)
        {
            return player.GetDamagePoints(gameTime);
        }

        //player Movement Handling
        private void HandleMovement(GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                player.position.Y -= player.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                player.position.Y += player.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                player.position.X -= player.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                player.position.X += player.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void GenerateRoomFloor(SpriteBatch sp)
        {
            tiles = new Rectangle[worldWidth][];
            for (int l = 0; l < worldWidth; l++)
            {
                tiles[l] = new Rectangle[worldDepth];
            }

            for (int i = 0; i < worldWidth; i++)
            {
                for (int k = 0; k < worldDepth; k++)
                {
                    tiles[i][k] = new Rectangle(i * TileWidth, k * TileHeight, TileWidth, TileHeight);
                }
            }

            for (int i = 0; i < tiles.Length; i++)
                for (int j = 0; j < tiles.Length; j++)
                {
                    if (j == 0)
                    {
                        if (i == 0)
                        {
                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tile01"), tiles[i][j], tiles[0][0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        }
                        else if (i == tiles.Length-1)
                        {
                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tile03"), tiles[i][j], tiles[0][0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        }
                        else
                        {
                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tile02"), tiles[i][j], tiles[0][0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        }
                    }
                    else if (i == 0)
                    {
                        if (j == tiles.Length - 1)
                        {
                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tileBL"), tiles[i][j], tiles[0][0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        }
                        else
                        {
                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tile04"), tiles[i][j], tiles[0][0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        }
                    }
                    else if(i == tiles.Length - 1)
                    {
                        if (j == tiles.Length - 1)
                        {
                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tileBR"), tiles[i][j], tiles[0][0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        }
                        else
                        {
                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tile05"), tiles[i][j], tiles[0][0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        }
                    }
                    else if(j == tiles.Length - 1)
                    {
                        sp.Draw(contentManager.Load<Texture2D>("Tiles/tile06"), tiles[i][j], tiles[0][0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);

                    }
                    else
                    {
                        //sp.DrawRectangle(new RectangleF(tiles[i][j].X, tiles[i][j].Y, tiles[i][j].Width, tiles[i][j].Height), Color.Red);
                        sp.Draw(contentManager.Load<Texture2D>("Tiles/tile07"), tiles[i][j], tiles[0][0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                    }
                }
        }
    }
}
