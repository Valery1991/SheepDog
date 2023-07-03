using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Services;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
	 
namespace SheepDog
{
	public class Sheep : Objects
	{
		public bool InsidePen = false;
		public float DirectionX = (Defines.gRandNum.Next (0, 4) - 2.0f) / 5;
		public float DirectionY = (Defines.gRandNum.Next (0, 4) - 2.0f) / 5;
		public int SheepInPen = 0;
		public int PenSize = 50;
		
		public Sheep (String fname)
		{			
			texture_info = new TextureInfo (new Texture2D (Defines.dir + fname, false));
			Graphics = texture_info.Texture;
			msprite = new SpriteUV (texture_info);
			msprite.Quad = trs;
			msprite.Quad.S = texture_info.TextureSizef; 
			position = AppMain.scene.Camera.CalcBounds ().Center;
			msprite.CenterSprite ();			
		}
		
		override public void Update (bool Pause)
		{			
			msprite.Quad.S = texture_info.TextureSizef * scale; 
			msprite.Quad.R = Vector2.Rotation (Defines.DEGREES_TO_RADIANS (rotation.Y));		
			
			msprite.Position = position;
			msprite.CenterSprite ();
			
			if (InsidePen == false) 
			{
				int Proximity = 50;
					
				if (Dog.IsBarking)
					Proximity = 150;
				
			if (FMath.Sqrt ((AppMain.Dog.position.X - position.X) * (AppMain.Dog.position.X - position.X) + (AppMain.Dog.position.Y - position.Y) * (AppMain.Dog.position.Y - position.Y)) < Proximity) {
						
					if (position.X < AppMain.Dog.position.X) 
					{
						position.X -= 15;
						if (DirectionX > 0)		
							DirectionX = -DirectionX;
					} 
					else 
					{
						position.X += 15;
						if (DirectionX < 0)		
							DirectionX = -DirectionX;
					}
					if (position.Y < AppMain.Dog.position.Y) 
					{
						position.Y -= 15;
						if (DirectionY < 0)		
							DirectionY = -DirectionY;
					} 
					else 
					{
						position.Y += 15;
						if (DirectionY > 0)		
							DirectionY = -DirectionY;
					}
				}
			}

			if (InsidePen == false) 
			
			{			
				position.X += DirectionX;
				position.Y += DirectionY;
				if (position.X < 0) 
				{
					position.X = 0;
					DirectionX = -DirectionX;
				}
				if (position.X + msprite.TextureInfo.Texture.Width > AppMain.width) 
				{
					position.X = AppMain.width - msprite.TextureInfo.Texture.Width;
					DirectionX = -DirectionX;
				}
				if (position.Y < 0) 
				{
					position.Y = 0;
					DirectionY = -DirectionY;
				}
				if (position.Y + msprite.TextureInfo.Texture.Height > AppMain.height) 
				{
					position.Y = AppMain.height - msprite.TextureInfo.Texture.Height;
					DirectionY = -DirectionY;
				}
						
						
				if (position.X < PenSize) 
				{
					if (position.Y < PenSize || position.Y > AppMain.height - PenSize) 
					{
						SheepInPen++;
						InsidePen = true;
						AppMain.Score += 1;
						
					}
				}
				if (position.X > AppMain.width - PenSize) 
				{
					if (position.Y < PenSize || position.Y > AppMain.height - PenSize) 
					{
						SheepInPen++;
						
						InsidePen = true;
						AppMain.Score += 1;
						
					}
				}
			}
			
			base.Update (Pause);
			
		}
	}
}

// As said in AppMain, I worked together with Myk on this class. Had a lot of trouble with the collision etc
