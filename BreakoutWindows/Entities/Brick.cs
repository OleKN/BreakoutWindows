using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BreakoutWindows.HelperClasses;
using BreakoutWindows;

namespace BreakoutWindows.Entities
{
	public class Brick : Entity
	{
		public int health = 1;
		private int powerupChance = 30;
		public Brick(RectangleFloat box, Texture2D tex)
			: base(box,tex)
		{

		}

		public bool Damage(int dmg, bool explode = true)
		{
			health -= dmg;
			if (health <= 0 && !GameObjects.destroyed.Contains(this))
			{
				GameObjects.destroyed.Add(this);
				spawnPowerup();
				if (explode)
					Effects.explode(this);
				return true;
			}
			else
			{
				return false;
			}
		}

		private void spawnPowerup()
		{
			Random random = new Random();
			if (random.Next(1, 101) <= powerupChance)
			{
				GameObjects.powerups.Add(new Powerup(	new RectangleFloat((int)Center.X - 10, (int)Center.Y - 10, 20, 20),
														GameObjects.textureBrick
														));
			}
		}
		/*
		private void explode()
		{
			float maxSpeed = 100;
			int particleAmount = 1000;
			int lifeTime = 500;
			float degPerParticle = (float)Math.PI * 2.0f / (float)particleAmount;
			float currentDeg = degPerParticle;

			for (int i = 0; i < particleAmount; i++)
			{
				Random random = new Random((int)(currentDeg * 1000 * Center.X));
				Random random2 = new Random((int)(i * random.Next(1000) * Center.Y));

				GameObjects.particles.Add(new Particle(new Vector2((float)random.NextDouble() * (boundingBox.Right - boundingBox.Left) + boundingBox.Left,
																	(float)random.NextDouble() * (boundingBox.Bottom - boundingBox.Top) + boundingBox.Top), 
														lifeTime * (0.5 * random.NextDouble()) + 0.5 * lifeTime, 
														new Vector2(	(float)random.NextDouble() * maxSpeed * (float)Math.Cos(currentDeg), 
																		(float)random2.NextDouble() * maxSpeed * (float)Math.Sin(currentDeg))));
				currentDeg += degPerParticle;
			}
		}*/
	}
}
