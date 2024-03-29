﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BreakoutWindows.HelperClasses;

namespace BreakoutWindows.Entities
{
	public class Entity
	{
		// IMPLEMENT FLOAT RECTANGLE
		public RectangleFloat boundingBox;
		protected Texture2D texture;
		public Vector2 speed;
		public Vector2 acceleration;

		#region Constructors
		public Entity(RectangleFloat box, Texture2D tex, Vector2 s)
		{
			this.boundingBox = box;
			this.texture = tex;
			this.speed = s;
			this.acceleration = new Vector2(0, 0);
		}

		public Entity(RectangleFloat box, Texture2D tex){
			this.boundingBox = box;
			this.texture = tex;
			this.speed = new Vector2(0, 0);
			this.acceleration = new Vector2(0, 0);
		}
		#endregion

		#region Getters and setters
		public float X
		{
			get { return boundingBox.X; }
			set { boundingBox.X = value; }
		}
		public float Y
		{
			get { return boundingBox.Y; }
			set { boundingBox.Y = value; }
		}
		public float Width
		{
			get { return boundingBox.Width; }
			set { boundingBox.Width = value; }
		}
		public float Height
		{
			get { return boundingBox.Height; }
			set { boundingBox.Height = value; }
		}
		public Vector2 Center
		{
			get { return new Vector2(X + Width / 2, Y + Height / 2); }
		}
		#endregion

		public virtual void update(GameTime gameTime)
		{
			speed.X += acceleration.X * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
			speed.Y += acceleration.Y * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;

			boundingBox.X += (speed.X * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000);
			boundingBox.Y += (speed.Y * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000);
		}

		public virtual void draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, boundingBox.Rectangle, Color.White);
		}

		public bool collidesWith(Entity other)
		{
			if (boundingBox.Intersects(other.boundingBox))
			{
				return true;
			} 
			else 
			{
				return false;
			}
		}

	}
}
