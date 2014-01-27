using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BreakoutWindows.HelperClasses;

namespace BreakoutWindows.Entities
{
	public class Paddle : Entity
	{
		private int paddleSpeed = 500;
		private float originalWidth;
		private int lastSizeUpgrade = 0;
		public Ball stickyBall;

		public Paddle(RectangleFloat box, Texture2D tex) 
			: base(box, tex)
		{
			originalWidth = box.Width;
			stickyBall = null;
		}

		public override void update(GameTime gameTime)
		{
			// Handle size upgrade
			if (lastSizeUpgrade != POWERUPS.sizeUpgrade)
			{
				X += Width / 2;
				Width = originalWidth + (int)(originalWidth * Math.Sqrt(POWERUPS.sizeUpgrade) / 2);
				X -= Width / 2;
				lastSizeUpgrade = POWERUPS.sizeUpgrade;
			}

			float initialX = X;

			// Handle input
			KeyboardState keyState = Keyboard.GetState();
			if (keyState.IsKeyDown(Keys.Left))
			{
				boundingBox.X -= (int)(paddleSpeed * gameTime.ElapsedGameTime.TotalSeconds);
				if (X < 0)
				{
					X = 0;
				}
			}
			if (keyState.IsKeyDown(Keys.Right))
			{
				boundingBox.X += (int)(paddleSpeed * gameTime.ElapsedGameTime.TotalSeconds);
				if (X > GameObjects.viewport.Width - boundingBox.Width)
				{
					X = GameObjects.viewport.Width - boundingBox.Width;
				}
			}
			if (keyState.IsKeyDown(Keys.Space))
			{
				fire();
			}

			if (stickyBall != null)
			{
				stickyBall.X += X - initialX;
			}

		}

		public void stick(Ball ball)
		{
			stickyBall = ball;
		}

		public void fire()
		{
			stickyBall = null;
		}
	}
}
