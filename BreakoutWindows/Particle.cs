using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BreakoutWindows.Entities;

namespace BreakoutWindows
{
	public class Particle : Entity
	{
		private double lifeTime;

		#region Constructors
		public Particle(Vector2 pos, double lt)
			: base(new Rectangle((int)pos.X, (int)pos.Y, 1, 1), new Texture2D(GameObjects.graphicsDevice, 1, 1))
		{
			this.lifeTime = lt;
			acceleration = new Vector2(0, 80);
		}
		public Particle(Vector2 pos, double lt, Vector2 speed)
			: base(new Rectangle((int)pos.X, (int)pos.Y, 3, 3), GameObjects.textureBall/*new Texture2D(GameObjects.graphicsDevice, 1, 1)*/)
		{
			this.lifeTime = lt;
			this.speed = speed;
			acceleration = new Vector2(0, 80);
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
	}
}
