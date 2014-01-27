using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BreakoutWindows.Entities;
using BreakoutWindows.HelperClasses;

namespace BreakoutWindows
{
	public class Particle : Entity
	{
		private double lifeTime;
		private double startingLifeTime;
		//private static Vector2 defaultSize = new Vector2(3, 3);

		#region Constructors
		public Particle(Vector2 pos, double lt)
			: base(new RectangleFloat((int)pos.X, (int)pos.Y, 1, 1), GameObjects.textureFireParticle)
		{
			this.lifeTime = lt;
			this.startingLifeTime = lt;
			acceleration = new Vector2(0, 80);
		}
		public Particle(Vector2 pos, double lt, Vector2 speed, Vector2 size)
			: base(new RectangleFloat((int)pos.X - size.X / 2, (int)pos.Y - size.Y / 2, size.X, size.Y), GameObjects.textureFireParticle/*new Texture2D(GameObjects.graphicsDevice, 1, 1)*/)
		{
			this.lifeTime = lt;
			this.speed = speed;
			this.startingLifeTime = lt;
			acceleration = new Vector2(0, 30);
			//Console.WriteLine("CREATED PARTICLE" + speed.X);
		}

		#endregion

		public override void update(GameTime gameTime)
		{
			lifeTime -= gameTime.ElapsedGameTime.TotalMilliseconds;
			if (lifeTime > 0)
			{
				//Console.WriteLine("Updated PARTICLE");
				base.update(gameTime);
			}
			else
			{
				//Console.WriteLine("KILLED PARTICLE" + speed.X);
				GameObjects.destroyed.Add(this);
			}
		}

		public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, boundingBox.Rectangle, new Color(255, (int)(255 * (startingLifeTime - lifeTime) / startingLifeTime), 0));
		}

	}
}
