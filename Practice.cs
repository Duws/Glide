using UnityEngine;
using System.Collections;

namespace Glide.Scripts
{
	public class Practice : MonoBehaviour
	{
		[Header("Scenes")]
		public string PracticeHardcore;
		public string PracticeClassic;
		public string BackGameModes;

		void start()
		{
			// check if app is newly installed
			if (!PlayerPrefs.HasKey ("PracticeMode"))
			{
				// if yes, set gamemode to 3
				PlayerPrefs.SetFloat ("PracticeMode", 2);
			}
		}

		public void practiceClassic()
		{
			PlayerPrefs.SetFloat ("PracticeMode", 0);
			LoadingSceneManager.LoadScene (PracticeClassic);
		}

		public void practiceHardcore()
		{
			PlayerPrefs.SetFloat ("PracticeMode", 1);
			LoadingSceneManager.LoadScene (PracticeHardcore);
		}

		public void backtogamemode()
		{
			LoadingSceneManager.LoadScene (BackGameModes);
		}
	}
}
