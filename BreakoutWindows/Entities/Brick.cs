using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BreakoutWindows.Entities
{
	public class Brick : Entity
	{
		public int health = 1;
		private int powerupChance = 0;
		public Brick(Rectangle box, Texture2D tex)
			: base(box,tex)
		{

		}

		public bool Damage(int dmg)
		{
			health -= dmg;
			if (health <= 0)
			{
				GameObjects.destroyed.Add(this);
				spawnPowerup();
				explode();
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
				GameObjects.powerups.Add(new Powerup(	new Rectangle((int)Center.X - 10, (int)Center.Y - 10, 20, 20),
														GameObjects.textureBrick
														));
			}
		}
		private void explode()
		{
			float speed = 100;
			int particleAmount = 20;
			float degPerParticle = (float)Math.PI * 2.0f / (float)particleAmount;
			Console.WriteLine("Deg per particle " + degPerParticle);
			float currentDeg = 0;

			for (int i = 0; i < particleAmount; i++)
			{
				GameObjects.particles.Add(new Particle(Center, 1000, new Vector2(speed * (float)Math.Cos(currentDeg), speed * (float)Math.Sin(currentDeg))));
				currentDeg += degPerParticle;
				Console.WriteLine("current X " + (speed * (float)Math.Cos(currentDeg)));
			}
		}
	}
}
