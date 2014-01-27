#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using BreakoutWindows.Entities;
#endregion

namespace BreakoutWindows
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	/// 
	public static class GameObjects
	{
		public static Paddle paddle;
		public static List<Brick> bricks;
		public static List<Ball> balls;
		public static List<Powerup> powerups;
		public static List<Particle> particles;
		public static List<Entity> destroyed;

		public static Texture2D textureBall;
		public static Texture2D textureBrick;
		public static Texture2D texturePowerup;
		public static Viewport viewport;
		public static GraphicsDevice graphicsDevice;
	}

	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public Game1()
			: base()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			GameObjects.bricks = new List<Brick>();
			GameObjects.balls = new List<Ball>();
			GameObjects.powerups = new List<Powerup>();
			GameObjects.particles = new List<Particle>();
			GameObjects.destroyed = new List<Entity>();
			GameObjects.graphicsDevice = GraphicsDevice;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();

		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			GameObjects.textureBall = Content.Load<Texture2D>("ball");
			GameObjects.textureBrick = Content.Load<Texture2D>("paddle");
			
			//GameObjects.texturePowerup = Content.Load <Texture2D>("powerup");

			GameObjects.viewport = this.GraphicsDevice.Viewport;

			GameObjects.paddle = new Paddle(new Rectangle(this.GraphicsDevice.Viewport.Width / 2 - 40, 
												this.GraphicsDevice.Viewport.Height - 10, 
												80, 
												10), 
											Content.Load<Texture2D>("paddle"));

			GameObjects.balls.Add(new Ball(new Rectangle(GameObjects.paddle.boundingBox.X + GameObjects.paddle.boundingBox.Width / 2 - 5,
												GameObjects.paddle.boundingBox.Top - 10,
												20,
												20),
											Content.Load<Texture2D>("ball"),
											new Vector2(-300, -300)));

			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					GameObjects.bricks.Add(new Brick(new Rectangle(this.GraphicsDevice.Viewport.Width * x / 9,
														20 * y,
														this.GraphicsDevice.Viewport.Width / 9,
														20),
													GameObjects.textureBrick));
				}
			}
			
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();


			

			#region Update and handle Balls
			int ballNumber = GameObjects.balls.Count;
			for (int i = 0; i < ballNumber; i++)
			{
				Ball ball = GameObjects.balls[i];
				ball.update(gameTime);
			}
				// Collides with bricks
				/*for (int j = 0; j < bricks.Count; j++)
				{
					Brick brick = bricks[j];
					if (ball.collidesWith(brick))
					{
						
						if (ball.boundingBox.Left < brick.boundingBox.Left && ball.speed.X > 0 || 
							ball.boundingBox.Right > brick.boundingBox.Right && ball.speed.X < 0)
						{
							ball.speed.X = -ball.speed.X;
						}
						if (ball.boundingBox.Top < brick.boundingBox.Top && ball.speed.Y > 0 || 
							ball.boundingBox.Bottom > brick.boundingBox.Bottom && ball.speed.Y < 0)
						{
							ball.speed.Y = -ball.speed.Y;
						}
						bricks.RemoveAt(j);
						brick = null;
						j--;
					}
				}

				// Collides with other balls
				for (int j = i+1; j < ballNumber; j++)
				{
					Ball ball2 = balls[j];
					if (ball.collidesWith(ball2) && ball.Y != ball2.Y)
					{

						Vector2 collision = new Vector2(ball.X - ball2.X, ball.Y - ball2.Y);
						float distance = collision.Length();
						if (distance == 0)
						{
							collision = new Vector2(1, 0);
							distance = 1;
						}
						collision = new Vector2(collision.X / distance, collision.Y / distance);
						float aci = Vector2.Dot(ball.speed, collision);
						float bci = Vector2.Dot(ball2.speed, collision);

						ball.speed += (bci - aci) * collision;
						ball2.speed += (aci - bci) * collision;
					}
				}*/

			#endregion

			GameObjects.paddle.update(gameTime);

			foreach (Particle particle in GameObjects.particles)
				particle.update(gameTime);

			foreach (Powerup powerup in GameObjects.powerups)
				powerup.update(gameTime);
			
			POWERUPS.update(gameTime);

			// Remove all destroyed objects
			foreach (Entity entity in GameObjects.destroyed)
			{
				if (entity is Powerup)
				{
					GameObjects.powerups.Remove((Powerup)entity);
				}
				else if (entity is Brick)
				{
					GameObjects.bricks.Remove((Brick)entity);
				}
				else if (entity is Ball)
				{
					GameObjects.balls.Remove((Ball)entity);
				}
				else if (entity is Particle)
				{
					GameObjects.particles.Remove((Particle)entity);
				}
			}
			GameObjects.destroyed.Clear();



			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			GameObjects.paddle.draw(gameTime, spriteBatch);
			foreach (Ball ball in GameObjects.balls)
				ball.draw(gameTime, spriteBatch);
			foreach (Brick brick in GameObjects.bricks)
				brick.draw(gameTime, spriteBatch);
			foreach (Powerup powerup in GameObjects.powerups)
				powerup.draw(gameTime, spriteBatch);
			foreach (Particle particle in GameObjects.particles)
				particle.draw(gameTime, spriteBatch);

			spriteBatch.End();



			base.Draw(gameTime);
		}
	}
}
