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
        private const int worldHeight = 30;
        private const int TileWidth = 16;
        private const int TileHeight = 16;


        private KeyboardState keyboardState;
        public ContentManager contentManager { get; }

        #region Only used by the maze generation algorithm
        public bool visited { get; set; }
        public Dictionary<CardinalPoint, Room> exits { get; set; } = new Dictionary<CardinalPoint, Room>();
        public Vector2 position { get; }
        #endregion

        public Room(Vector2 position, int dungeonNumber, Player player, ContentManager contentManager)
        {
            this.position = position;
            this.player = player;
            enemies = new List<Enemy>();
            this.contentManager = contentManager;

            for (int i = 0; i < DungeonUtilities.GetNumberOfEnemies(dungeonNumber); i++)
            {
                enemies.Add(new Enemy(GenerateRandomStartingPosition(), this));
            }

            GenerateRoomFloor();
        }

        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            HandleMovement(gameTime);
            WallCollision(player.GetHitbox().ToRectangle());
            player.Update(gameTime);
            //Debug.WriteLine("Le joueur a: " + player.health + " Points de vie");
            

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

            DrawRoomFloor(spriteBatch);
        }

        private static Vector2 GenerateRandomStartingPosition()
        {
            int x = Utilities.Utilities.GetRandomNumber(20, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width);
            int y = Utilities.Utilities.GetRandomNumber(20, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

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


        private void GenerateRoomFloor()
        {
            tiles = new Rectangle[worldWidth][];

            //Setting tiles with Rectangle
            for (int i = 0; i < worldWidth; i++)
            {
                tiles[i] = new Rectangle[worldHeight];
                for (int k = 0; k < worldHeight; k++)
                {
                    tiles[i][k] = new Rectangle(i * TileWidth, k * TileHeight, TileWidth, TileHeight);
                }
            }
        }
        private void DrawRoomFloor(SpriteBatch sp)
        {
            //Draw sprites to Rectangle. Checking if it's a wall or the floor.
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[0].Length; j++)
                {
                    if (j == 0)
                    {
                        if (i == 0)
                        {
                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tile01"), tiles[i][j], tiles[0][0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        }
                        else if (i == tiles.Length - 1)
                        {
                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tile03"), tiles[i][j], tiles[0][0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        }
                        else
                        {
                            //sp.DrawRectangle(new RectangleF(tiles[i][j].X, tiles[i][j].Y, tiles[i][j].Width, tiles[i][j].Height), Color.Red);
                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tile02"), tiles[i][j], tiles[0][0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        }
                    }
                    else if (i == 0)
                    {
                        if (j == tiles[0].Length - 1)
                        {
                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tileBL"), tiles[i][j], tiles[0][0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        }
                        else
                        {
                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tile04"), tiles[i][j], tiles[0][0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        }
                    }
                    else if (i == tiles.Length - 1)
                    {
                        if (j == tiles[0].Length - 1)
                        {
                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tileBR"), tiles[i][j], tiles[0][0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        }
                        else
                        {
                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tile05"), tiles[i][j], tiles[0][0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        }
                    }
                    else if (j == tiles[0].Length - 1)
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

        private void WallCollision(Rectangle _player)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[0].Length; j++)
                {
                    if (tiles[i][j].Intersects(_player)) {
                        if (i == 0)
                        {
                            player.position.X += 1f;
                        }
                        else if (i == tiles.Length - 1)
                        {
                            player.position.X -= 1f;
                        }
                        else if (j == 0)
                        {
                            player.position.Y += 1f;
                        }
                        else if (j == tiles[0].Length - 1)
                        {
                            player.position.Y -= 1f;
                        }
                    }
                }
            }
        }

    }
}
