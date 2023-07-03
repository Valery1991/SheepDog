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
	public class Defines
	{
		public static int HowManyObjects = 13;
		public 	const string dir = "/Application/Data/Sprites/";
		public static Random gRandNum = new Random ();
		
		public Defines ()
		{
		}
				
		public static bool DidTheseTwoHit (Objects Ob1, Objects Ob2)
		{
			
			int OB1Width = Ob1.msprite.TextureInfo.Texture.Width;
			int OB2Width = Ob2.msprite.TextureInfo.Texture.Width;
				
			int OB1Height = Ob1.msprite.TextureInfo.Texture.Height;
			int OB2Height = Ob2.msprite.TextureInfo.Texture.Height;
			
		
			if (Ob1.position.X - (OB1Width / 2) > Ob2.position.X + (OB2Width / 2))
				return false; 
			if (Ob1.position.X + (OB1Width / 2) < Ob2.position.X - (OB2Width / 2))
				return false;
			if (Ob1.position.Y - OB1Height / 2 > Ob2.position.Y + OB2Height / 2)
				return false;
			if (Ob1.position.Y + OB1Height / 2 < Ob2.position.Y - OB2Height / 2)
				return false;

			Console.WriteLine ("A Hit, by George!!!");
			
			return true;			
			
		}
		
		public static float RANDOM_RANGE (float range)
		{
			return ((float)gRandNum.NextDouble () * range);
		}
	
		public static int INTRANDOM_RANGE (int range)
		{
			return (int)(gRandNum.NextDouble () * range);
		}
	
		public static float DEGREES_TO_RADIANS (float degrees)
		{
			return (degrees / 180.0f * (float)System.Math.PI);
		}

		public static float RADIANS_TO_DEGREES (float radians)
		{
			return (radians * 180.0f / (float)System.Math.PI);
		}
		
		
	}
	
}

// This code is from the original Arkanoid project