using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BreakoutWindows.Entities
{
	public class Ball : Entity
	{
		private Rectangle initialRect;
		private Vector2 initialSpeed;
		public int damage = 1;

		#region Constructors
		public Ball(Rectangle box, Texture2D tex)
			: base(box, tex)
		{
			this.initialRect = box;
		}
		public Ball(Rectangle box, Texture2D tex, Vector2 speed)
			: base(box, tex, speed)
		{
			this.initialRect = box;
			this.initialSpeed = speed;
		}
		#endregion

		public override void update(GameTime gameTime)
		{
			base.update(gameTime);

			handleWallCollision();

			if (collidesWith(GameObjects.paddle)){
				handlePaddleCollison(GameObjects.paddle);
			}

			foreach (Brick brick in GameObjects.bricks)
			{
				if (collidesWith(brick))
					if (handleBrickCollision(brick)) {
						//CARRY ON IF PENETRATION IS ACTIVE
					}
			}


			for (int i = GameObjects.balls.IndexOf(this) + 1; i < GameObjects.balls.Count; i++)
			{
				if (radiallyCollidesWith(GameObjects.balls[i]))
				{
					handleBallCollision(GameObjects.balls[i]);
				}
			}



		}

		public bool radiallyCollidesWith(Ball other)
		{
			Vector2 distance = new Vector2(other.X - X, other.Y - Y);
			Vector2 relativeSpeed = new Vector2(other.speed.X - speed.X, other.speed.Y - speed.Y);

			if (relativeSpeed.X * distance.X + relativeSpeed.Y * distance.Y >= 0) 
			{
				return false;
			} 
			else if (distance.Length() < Width / 2 + other.Width)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		#region Collision handlers
		private void handleWallCollision()
		{
			if (speed.X < 0 && boundingBox.X < 0 || speed.X > 0 && boundingBox.Right > GameObjects.viewport.Width)
			{
				speed.X = -speed.X;
			}
			if (speed.Y < 0 && boundingBox.Y < 0 /*|| speed.Y > 0 && boundingBox.Bottom > viewport.Height*/)
			{
				speed.Y = -speed.Y;
			}
			else if (speed.Y > 0 && boundingBox.Top > GameObjects.viewport.Height)
			{
				if (GameObjects.balls.Count == 1) { 
					speed = initialSpeed;
					boundingBox = initialRect;
				}
				else
				{
					GameObjects.destroyed.Add(this);
				}
			}
		}

		private bool handleBrickCollision(Brick brick)
		{
			bool destroyed = brick.Damage(damage);

			if (boundingBox.Left < brick.boundingBox.Left && speed.X > 0 ||
				boundingBox.Right > brick.boundingBox.Right && speed.X < 0)
			{
				speed.X = -speed.X;
			}
			if (boundingBox.Top < brick.boundingBox.Top && speed.Y > 0 ||
				boundingBox.Bottom > brick.boundingBox.Bottom && speed.Y < 0)
			{
				speed.Y = -speed.Y;
			}
			return destroyed;
		}

		private void handleBallCollision(Ball ball)
		{
			Vector2 collision = new Vector2(X - ball.X, Y - ball.Y);
			float distance = collision.Length();
			if (distance == 0)
			{
				collision = new Vector2(1, 0);
				distance = 1;
			}
			collision = new Vector2(collision.X / distance, collision.Y / distance);
			float aci = Vector2.Dot(speed, collision);
			float bci = Vector2.Dot(ball.speed, collision);

			speed += (bci - aci) * collision;
			ball.speed += (aci - bci) * collision;
		}

		private void handlePaddleCollison(Paddle paddle)
		{
			Vector2 distance = new Vector2(Center.X - paddle.Center.X, Center.Y - paddle.Center.Y - paddle.Width / 4);
			float length = distance.Length();
			distance.X = distance.X / length * speed.Length();
			distance.Y = distance.Y / length * speed.Length();
			speed = distance;
		}
		#endregion

	}
}
