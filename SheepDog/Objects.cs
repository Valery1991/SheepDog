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
	public class Objects
	{		
		public float Xpos;
		public float Ypos;
		public TextureInfo texture_info;
		public Texture2D Graphics;
		public Vector2	position;
		public Vector2	rotation;
		public Vector2	velocity;
		public float scale = 1.0f;
		public float speed = 5;
		public float Direction = 0.0f;
		public SpriteUV msprite;
		public TRS trs = new TRS ()
				{
		    	T = new Vector2 (0.0f, 0.0f),
		    	R = Vector2.Rotation (
			     Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Deg2Rad (90.0f)
			                     ),		    			
			
				S = new Vector2 (64.0f, 64.0f)		
	
				};
	
		public  Objects (String fname)
		{
			//Console.WriteLine ("using string based Object");	
		
			texture_info = new TextureInfo (new Texture2D (Defines.dir + fname, false));	
		
			Graphics = texture_info.Texture;
			
			msprite = new SpriteUV (texture_info);

			msprite.Quad = trs;

			msprite.Quad.S = texture_info.TextureSizef; 

			position = AppMain.scene.Camera.CalcBounds ().Center;
			msprite.CenterSprite ();
		
		}

		public  Objects (Texture2D tex)
		{
			
			//Console.WriteLine ("using Texture based Object");	
			texture_info = new TextureInfo (tex);	
			Graphics = texture_info.Texture;
			msprite = new SpriteUV (texture_info);
			msprite.Quad = trs;
			msprite.Quad.S = texture_info.TextureSizef; 
			position = AppMain.scene.Camera.CalcBounds ().Center;
			msprite.CenterSprite ();
			
		
		}
	
		public Objects ()
		{
				
		}

		virtual public void Update (bool Pause)
		{
			msprite.Quad.S = texture_info.TextureSizef * scale; 
			msprite.Quad.R = Vector2.Rotation (Defines.DEGREES_TO_RADIANS (rotation.Y));		

			msprite.Position = position;
			msprite.CenterSprite ();

		}
	}
}

// This code is from the original Arkanoid project