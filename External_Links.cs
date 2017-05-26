using UnityEngine;
using System.Collections;

namespace Glide.Scripts
{
	public class External_Links : MonoBehaviour 
	{
		public virtual void OnClick()
		{
			// Direct to Play Store, Open Write Up App
			Application.OpenURL ("https://play.google.com/store/apps/details?id=com.writer.thesis.writeup");
		}

		public virtual void Rate()
		{
			// Direct to Play store, rate the game
			Application.OpenURL ("https://play.google.com/store/apps/details?id=com.Writers.Glide");
		}
	}
}
