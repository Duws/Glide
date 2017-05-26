using UnityEngine;
using System.Collections;

namespace Glide.Scripts
{
	public class GameModes : MonoBehaviour 
	{
		public string ClassicMode, HardcoreMode, PracticeMode, gotoGameMode;

		void start()
		{
			// check if app is newly installed
			if (!PlayerPrefs.HasKey ("GameModes"))
			{
				// if yes, set gamemode to 4
				PlayerPrefs.SetFloat ("GameModes", 4);
			}
		}

		public virtual void classic()
		{
			PlayerPrefs.SetFloat ("GameModes", 0);
			LoadingSceneManager.LoadScene (ClassicMode);
		//	Debug.Log ("classic");
		}

		public virtual void hardcore()
		{
			PlayerPrefs.SetFloat ("GameModes", 1);
			LoadingSceneManager.LoadScene (HardcoreMode);
			//Debug.Log ("hardcore");
		}

		public virtual void practice()
		{
			PlayerPrefs.SetFloat ("GameModes", 2);
			LoadingSceneManager.LoadScene (PracticeMode);
		}

		public virtual void gotooldscene()
		{
			PlayerPrefs.SetFloat ("GameModes", 3);
			LoadingSceneManager.LoadScene (gotoGameMode);
		}
	}
}
