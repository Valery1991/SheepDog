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
	public class Dog : Objects
	{
		
		public static GamePadData gamepadData;
		public static int Barks;
		public static bool IsBarking = false;
		
		public Dog (String fname)
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
			
			int dogspeed = 5;
			
			// Thanks to Marco Jonkers (2nd year programmer) for helping me with the controls
			
			gamepadData = GamePad.GetData (0);
			
			if ((gamepadData.Buttons & GamePadButtons.Left) == GamePadButtons.Left & position.X > 0 + (AppMain.Dog.msprite.TextureInfo.Texture.Width / 2)) {
				
				position.X -= dogspeed;
				
			}
				
			if ((gamepadData.Buttons & GamePadButtons.Right) == GamePadButtons.Right & position.X < AppMain.width - (AppMain.Dog.msprite.TextureInfo.Texture.Width / 2)) {
					
				position.X += dogspeed;
				
			}
			
			if ((gamepadData.Buttons & GamePadButtons.Up) == GamePadButtons.Up & position.Y < AppMain.height - (AppMain.Dog.msprite.TextureInfo.Texture.Height / 2)) {
			
				position.Y += dogspeed;
			
			}
			
			if ((gamepadData.Buttons & GamePadButtons.Down) == GamePadButtons.Down & position.Y > 0 + (AppMain.Dog.msprite.TextureInfo.Texture.Height / 2)) {
			
				position.Y -= dogspeed;
			
			}
			
			// Note to self: S on keyboard
			if ((gamepadData.Buttons & GamePadButtons.Cross) == GamePadButtons.Cross) {
				IsBarking = true;
				Barks++;
				;
			} else
				IsBarking = false;
			
			base.Update (Pause);
			
		}
	}
}

