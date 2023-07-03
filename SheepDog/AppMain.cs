using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Audio;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace SheepDog
{
	public class AppMain
	{
		public static Scene scene;
		public static Dog Dog;
		public static Sheep Sheep;
		public static Objects SheepPen;
		public static Objects Plant;
		public static Objects Floor;
		public static GamePadData gamepadData;
		public static int height;
		public static int width;
		public static int AmountSheep = 25;
		public static int AmountPlant = 15;
		public static int AmountFloor = 60;
		public static int Score;
		public static Label ScoreTracker;
		public static Label BarkTracker;
		public static Label RestartGame;
		public static List<Objects> TheObjectsList = new List<Objects> ();
		public static float positionplantX = 0;
		public static float positionplantY = 0;
		public static float positionfloorX = 0;
		public static float positionfloorY = 0;
		public static BgmPlayer bgmp;
		
		  public static void Music ()
  		{
  			Bgm bgm = new Bgm ("/Application/Data/Sound/Dogsong.mp3");
  			bgmp = bgm.CreatePlayer ();
   			bgmp.Loop = true;
   			bgmp.Play (); 	
 		 }
	
		public static void Main (string[] args)
		{
			Director.Initialize ();
			
			scene = new Scene ();
			scene.Camera.SetViewFromViewport ();
			width = Director.Instance.GL.Context.GetViewport ().Width;
			height = Director.Instance.GL.Context.GetViewport ().Height;
			Director.Instance.RunWithScene (scene, true);
			Director.Instance.GL.Context.SetClearColor (112, 200, 160, 255);
			
			bool gameOver = false;
			
			Music ();
			
			Creation ();
			
			while (!gameOver) 
			{
				if (Score <= 24)
				{
					Director.Instance.Update ();
					foreach (Objects OB in TheObjectsList) 
					{
						OB.Update (false);
					}
			
					// Note to self: D on keyboard
					if (Input2.GamePad0.Circle.Press == true)
						gameOver = true;
				
					Director.Instance.Render ();
					Director.Instance.GL.Context.SwapBuffers ();
					Director.Instance.PostSwap ();		
				
					ScoreTracker.Text = "Sheep Saved:" + Score.ToString () + "/25";
					ScoreTracker.Draw ();
					ScoreTracker.Color = Colors.Black;
					ScoreTracker.Scale = new Vector2 (2, 2);
				
					BarkTracker.Text = "Barks:" + Dog.Barks.ToString ();
					BarkTracker.Draw ();
					BarkTracker.Color = Colors.Black;
					BarkTracker.Scale = new Vector2 (2, 2);		
				} 
				else 
				{			
					Director.Instance.Update ();
					gamepadData = GamePad.GetData (0);
				
					foreach (Objects OB in TheObjectsList) 
					{				
						OB.Update (true);					
					}
					if (Input2.GamePad0.Circle.Press == true)
						gameOver = true;
    
					Director.Instance.Render ();
					Director.Instance.GL.Context.SwapBuffers ();
					Director.Instance.PostSwap ();
				
					BarkTracker.Text = "Amount of Barks:" + Dog.Barks.ToString ();
					BarkTracker.Draw ();
					BarkTracker.Color = Colors.White;
					BarkTracker.Scale = new Vector2 (2, 2);
					BarkTracker.Position = new Vector2 (300, 300);
				
					ScoreTracker.Text = "Sheep Saved:" + Score.ToString () + "/25";
					ScoreTracker.Draw ();
					ScoreTracker.Color = Colors.White;
					ScoreTracker.Scale = new Vector2 (2, 2);
					ScoreTracker.Position = new Vector2 (300, 280);
									
					RestartGame.Text = "Press Triangle (W) to restart, Circle (D) to quit :)";
					RestartGame.Draw ();
					RestartGame.Color = Colors.White;
					RestartGame.Scale = new Vector2 (2, 2);
					RestartGame.Position = new Vector2 (200, 260);
				
					// Note to self: W on keyboard
					if ((gamepadData.Buttons & GamePadButtons.Triangle) == GamePadButtons.Triangle) 
					{			
						foreach (Objects OB in TheObjectsList) {
							scene.RemoveChild (Sheep.msprite, true);
							scene.RemoveChild (Dog.msprite, true);
							scene.RemoveChild (ScoreTracker, true);
							scene.RemoveChild (RestartGame, true);
							scene.RemoveChild (BarkTracker, true);
							scene.RemoveChild (OB.msprite, true);
							scene.RemoveChild (SheepPen.msprite, true);	
						} 
					
						Score = 0;
						Dog.Barks = 0;
						Sheep.SheepInPen = 0;
					
						Creation ();
					
					}
				}
			}	
		}
		
		public static void Creation ()
		{	
			// Floor.. doing this the hard way because the way I originally planned didnt work. So inefficient but it works I guess
			for (int i = 0; i < AmountFloor; i++) 
			{
				Floor = new Objects ("Floor.png");
				scene.AddChild (Floor.msprite);
				Floor.position.Y = Defines.gRandNum.Next (0 + Floor.msprite.TextureInfo.Texture.Height, height - Floor.msprite.TextureInfo.Texture.Height);
				Floor.position.X = Defines.gRandNum.Next (0 + Floor.msprite.TextureInfo.Texture.Height, width - Floor.msprite.TextureInfo.Texture.Height);
				TheObjectsList.Add (Floor);	
			}
			
			// Plants.. Yay scenery.
			for (int i = 0; i < AmountPlant; i++) 
			{
				Plant = new Objects ("Plant.png");
				scene.AddChild (Plant.msprite);
				Plant.position.Y = Defines.gRandNum.Next (0 + Plant.msprite.TextureInfo.Texture.Height, height - Plant.msprite.TextureInfo.Texture.Height);
				Plant.position.X = Defines.gRandNum.Next (0 + Plant.msprite.TextureInfo.Texture.Height, width - Plant.msprite.TextureInfo.Texture.Height);
				TheObjectsList.Add (Plant);	
			}
						
			// This is the dog. Sprite is from Pokemon, copyright Nintendo
			Dog = new Dog ("growlithe.png");
			scene.AddChild (Dog.msprite);
			Dog.position.Y = height / 2;
			Dog.position.X = width / 2;
			TheObjectsList.Add (Dog);
									
			// Sheep pen creation
			SheepPen = new Objects ("Pen2.png");
			scene.AddChild (SheepPen.msprite);
			SheepPen.position.Y = SheepPen.msprite.TextureInfo.Texture.Height / 2;
			SheepPen.position.X = width - SheepPen.msprite.TextureInfo.Texture.Width / 2;
			TheObjectsList.Add (SheepPen);
			
			SheepPen = new Objects ("Pen2.png");
			scene.AddChild (SheepPen.msprite);
			SheepPen.position.Y = height - SheepPen.msprite.TextureInfo.Texture.Height / 2;
			SheepPen.position.X = SheepPen.msprite.TextureInfo.Texture.Width / 2;
			TheObjectsList.Add (SheepPen);
			
			SheepPen = new Objects ("Pen2.png");
			scene.AddChild (SheepPen.msprite);
			SheepPen.position.Y = height - SheepPen.msprite.TextureInfo.Texture.Height / 2;
			SheepPen.position.X = width - SheepPen.msprite.TextureInfo.Texture.Width / 2;
			TheObjectsList.Add (SheepPen);
			
			SheepPen = new Objects ("Pen2.png");
			scene.AddChild (SheepPen.msprite);
			SheepPen.position.Y = (0 + SheepPen.msprite.TextureInfo.Texture.Height / 2);
			SheepPen.position.X = (0 + SheepPen.msprite.TextureInfo.Texture.Width / 2);
			TheObjectsList.Add (SheepPen);

			// And here are the sheep! Yay. Sprite is from Pokemon, copyright Nintendo
			for (int i = 0; i < AmountSheep; i++) 
			{			
				Sheep = new Sheep ("mareep.png");
				scene.AddChild (Sheep.msprite);
				Sheep.position.Y = Defines.gRandNum.Next (0 + Sheep.msprite.TextureInfo.Texture.Height, height - Sheep.msprite.TextureInfo.Texture.Height);
				Sheep.position.X = Defines.gRandNum.Next (0 + Sheep.msprite.TextureInfo.Texture.Height, width - Sheep.msprite.TextureInfo.Texture.Height);
				TheObjectsList.Add (Sheep);			
			}
			
			// Score.. because everybody loves scores.
			ScoreTracker = new Label ("Sheep Saved:" + Score);
			scene.AddChild (ScoreTracker);
			ScoreTracker.Position = new Vector2 (100, 500);
			
			BarkTracker = new Label ("Barks:" + Dog.Barks);
			scene.AddChild (BarkTracker);
			BarkTracker.Position = new Vector2 (100, 520);
			
			RestartGame = new Label ("Press Cross (S) to bark, Circle (D) to quit");
			scene.AddChild (RestartGame);
			RestartGame.Position = new Vector2 (100, 480);

		}		
	}
}

		
