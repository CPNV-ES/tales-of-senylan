using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using TalesOfSenylan.Models.Characters;
using TalesOfSenylan.Models.Utilities;

namespace TalesOfSenylan.Models.Dungeon
{
    public class Room
    {
        private const int worldWidth = 50;
        private const int worldHeight = 30;
        private const int TileWidth = 16;
        private const int TileHeight = 16;

        private static Rectangle[][] tiles;


        private KeyboardState keyboardState;

        public Room(Vector2 position, Dungeon dungeon, Player player, ContentManager contentManager)
        {
            this.position = position;
            this.player = player;
            this.dungeon = dungeon;
            enemies = new List<Enemy>();
            this.contentManager = contentManager;

            for (var i = 0; i < DungeonUtilities.GetNumberOfEnemies(dungeon.dungeonNumber); i++)
                enemies.Add(new Enemy(GenerateRandomStartingPosition(), this));

            GenerateRoomFloor();
        }

        private Player player { get; }
        public List<Enemy> enemies { get; set; }
        public ContentManager contentManager { get; }
        private Dungeon dungeon;

        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            HandleMovement(gameTime);
            HandleWallCollision(player.GetHitbox().ToRectangle());
            player.Update(gameTime);

            // Debug.WriteLine("Le joueur a: " + player.health + " Points de vie");
            CheckRoomChange();

            //ToList() to make a copy of the list and remove an item safely from the original list
            foreach (var enemy in enemies.ToList())
            {
                if (player.IsCollided(enemy.GetHitbox()) &&
                    (keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.K)))
                {
                    enemy.health -= GetDamagesInflicted(gameTime);

                    if (enemy.health <= 0) enemies.Remove(enemy);
                }
                HandleWallCollisionEnemy(enemy);
                enemy.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            player.Draw(gameTime, spriteBatch);

            foreach (var enemy in enemies) enemy.Draw(gameTime, spriteBatch);

            DrawRoomFloor(spriteBatch);
        }

        private void CheckRoomChange()
        {
            CardinalPoint? nullableExitCardinalPoint = DungeonUtilities.GetExitFromPosition(player.position);
            if (nullableExitCardinalPoint != null)
            {
                CardinalPoint exitCardinalPoint = (CardinalPoint) nullableExitCardinalPoint;
                dungeon.ChangeRoom(exits[exitCardinalPoint], exitCardinalPoint.Opposite());
            }
        }

        private static Vector2 GenerateRandomStartingPosition()
        {
            var x = Utilities.Utilities.GetRandomNumber(20, Constants.GameWidth);
            var y = Utilities.Utilities.GetRandomNumber(20, Constants.GameHeight);

            return new Vector2(x, y);
        }

        //player Attack Handling
        private int GetDamagesInflicted(GameTime gameTime)
        {
            return player.GetDamagePoints(gameTime);
        }

        //player Movement Handling
        private void HandleMovement(GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                player.position.Y -= player.speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                player.position.Y += player.speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                player.position.X -= player.speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                player.position.X += player.speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
        }


        private void GenerateRoomFloor()
        {
            tiles = new Rectangle[worldWidth][];

            //Setting tiles with Rectangle
            for (var i = 0; i < worldWidth; i++)
            {
                tiles[i] = new Rectangle[worldHeight];
                
                for (var k = 0; k < worldHeight; k++)
                    tiles[i][k] = new Rectangle(i * TileWidth, k * TileHeight, TileWidth, TileHeight);
            }
        }

        private void DrawRoomFloor(SpriteBatch sp)
        {
            var north = false;
            var south = false;
            var west = false;
            var east = false;

            foreach (CardinalPoint c in exits.Keys)
            {
                if (c == CardinalPoint.NORTH) north = true;
                else if (c == CardinalPoint.SOUTH) south = true;
                else if (c == CardinalPoint.WEST) west = true;
                else if (c == CardinalPoint.EAST) east = true;
                }


            //Draw sprites to Rectangle. Checking if it's a wall or the floor.
            for (var i = 0; i < tiles.Length; i++)
                for (var j = 0; j < tiles[0].Length; j++)
                    if (j == 0)
                    {
                        if (i == 0)
                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tile01"), tiles[i][j], tiles[0][0],
                                Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        else if (i == tiles.Length - 1)
                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tile03"), tiles[i][j], tiles[0][0],
                                Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        else
                            //sp.DrawRectangle(new RectangleF(tiles[i][j].X, tiles[i][j].Y, tiles[i][j].Width, tiles[i][j].Height), Color.Red);
                            if (north)
                            {
                                if (i == tiles.Length / 2 || i == tiles.Length / 2 - 3 || i == tiles.Length / 2 - 2 || i == tiles.Length / 2 - 1 || i == tiles.Length / 2 + 1 || i == tiles.Length / 2 + 2)
                                {
                                    // Draw nothing
                                    tiles[i][j].Y = -30;
                                    tiles[i][j].Height = 0;
                                }
                                else
                                {
                                    sp.Draw(contentManager.Load<Texture2D>("Tiles/tile02"), tiles[i][j], tiles[0][0],
                                            Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                                }
                            }
                            else
                            {
                                //Reset position since we move it to make an exit.
                                if (tiles[i][j].Y != 0)
                                {
                                    tiles[i][j].Height = TileHeight;
                                    tiles[i][j].Y = 0;
                                }
                                sp.Draw(contentManager.Load<Texture2D>("Tiles/tile02"), tiles[i][j], tiles[0][0],
                                                Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                            }
                    }
                    else if (i == 0)
                    {
                        if (j == tiles[0].Length - 1)
                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tileBL"), tiles[i][j], tiles[0][0],
                                Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        else
                            if (west)
                            {
                                if (j == tiles[0].Length / 2 || j == tiles[0].Length / 2 - 3 || j == tiles[0].Length / 2 - 2 || j == tiles[0].Length / 2 - 1 || j == tiles[0].Length / 2 + 1 || j == tiles[0].Length / 2 + 2)
                                {
                                    // Draw nothing
                                    tiles[i][j].X = -30;
                                    tiles[i][j].Width = 0;
                                }
                                else
                                {
                                    sp.Draw(contentManager.Load<Texture2D>("Tiles/tile04"), tiles[i][j], tiles[0][0],
                                        Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                                }
                            }
                            else
                            {
                                if(tiles[i][j].X != 0)
                                {
                                    tiles[i][j].X = 0;
                                    tiles[i][j].Width = TileWidth;
                                }
                                
                                sp.Draw(contentManager.Load<Texture2D>("Tiles/tile04"), tiles[i][j], tiles[0][0],
                                    Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                            }
                    }
                    else if (i == tiles.Length - 1)
                    {
                        if (j == tiles[0].Length - 1)
                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tileBR"), tiles[i][j], tiles[0][0],
                                Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        else
                            if (east)
                            {
                                if (j == tiles[0].Length / 2 || j == tiles[0].Length / 2 - 3 || j == tiles[0].Length / 2 - 2 || j == tiles[0].Length / 2 - 1 || j == tiles[0].Length / 2 + 1 || j == tiles[0].Length / 2 + 2)
                                {
                                    // Draw nothing
                                    tiles[i][j].X += 30;
                                    tiles[i][j].Width = 0;
                                }
                                else
                                {
                                    sp.Draw(contentManager.Load<Texture2D>("Tiles/tile05"), tiles[i][j], tiles[0][0],
                                            Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                                }
                            }
                            else
                            {
                                if (tiles[i][j].X != tiles[i][tiles[0].Length -1].X)
                                {
                                    tiles[i][j].Width = TileWidth;
                                    tiles[i][j].X = tiles[i][tiles[0].Length - 1].X;
                                }

                                
                                sp.Draw(contentManager.Load<Texture2D>("Tiles/tile05"), tiles[i][j], tiles[0][0],
                                        Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                            }
                    }
                    else if (j == tiles[0].Length - 1)
                    {
                        if (south)
                        {
                            if (i == tiles.Length / 2 || i == tiles.Length / 2 - 3 || i == tiles.Length / 2 - 2 || i == tiles.Length / 2 - 1 || i == tiles.Length / 2 + 1 || i == tiles.Length / 2 + 2)
                            {
                                // Draw nothing
                                tiles[i][j].Height = 0;
                                tiles[i][j].Y += 30;
                                sp.DrawRectangle(new RectangleF(tiles[i][j].X, tiles[i][j].Y, tiles[i][j].Width, tiles[i][j].Height), Color.Red);
                            }
                            else
                            {
                                sp.Draw(contentManager.Load<Texture2D>("Tiles/tile06"), tiles[i][j], tiles[0][0], Color.White,0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                            }
                        }
                        else
                        {

                            if(tiles[i][j].Y != tiles[tiles.Length - 1][j].Y)
                            {
                                tiles[i][j].Height = TileHeight;
                                tiles[i][j].Y = tiles[tiles.Length - 1][j].Y;
                            }

                            sp.Draw(contentManager.Load<Texture2D>("Tiles/tile06"), tiles[i][j], tiles[0][0], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                        }  
                    }
                    else
                    {
                        //sp.DrawRectangle(new RectangleF(tiles[i][j].X, tiles[i][j].Y, tiles[i][j].Width, tiles[i][j].Height), Color.Red);
                        sp.Draw(contentManager.Load<Texture2D>("Tiles/tile07"), tiles[i][j], tiles[0][0], Color.White,
                            0.0f, Vector2.Zero, SpriteEffects.None, 0.3f);
                    }
        }

        private void HandleWallCollision(Rectangle _player)
        {
            for (var i = 0; i < tiles.Length; i++)
            for (var j = 0; j < tiles[0].Length; j++)
                if (tiles[i][j].Intersects(_player))
                {
                    if (i == 0)
                        player.position.X += 1f;
                    else if (i == tiles.Length - 1)
                        player.position.X -= 1f;
                    else if (j == 0)
                        player.position.Y += 1f;
                    else if (j == tiles[0].Length - 1) player.position.Y -= 1f;
                }
        }

        private void HandleWallCollisionEnemy(Enemy enemy)
        {
            for (var i = 0; i < tiles.Length; i++)
                for (var j = 0; j < tiles[0].Length; j++)
                    if (tiles[i][j].Intersects(enemy.GetHitbox().ToRectangle()))
                    {
                        if (i == 0)
                            enemy.position.X += 1f;
                        else if (i == tiles.Length - 1)
                            enemy.position.X -= 1f;
                        else if (j == 0)
                            enemy.position.Y += 1f;
                        else if (j == tiles[0].Length - 1) enemy.position.Y -= 1f;
                    }
        }

        #region Only used by the maze generation algorithm

        public bool visited { get; set; }
        public Dictionary<CardinalPoint, Room> exits { get; set; } = new Dictionary<CardinalPoint, Room>();
        public Vector2 position { get; }

        #endregion
    }
}