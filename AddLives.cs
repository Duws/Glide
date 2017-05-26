using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Glide.Scripts																				
{
	public class AddLives : MonoBehaviour
	{
		public Text writers;
		int Lives_Classic, Timer_Classic, CheckTime_Classic;
		int Lives_Hardcore, Timer_Hardcore, CheckTime_Hardcore;

		void Start ()
		{
			//classic
			Lives_Classic = (int)PlayerPrefs.GetFloat ("Lives_Classic");
			Timer_Classic = (int)PlayerPrefs.GetInt("Timer_Classic");
			//hardcore
			Lives_Hardcore = (int)PlayerPrefs.GetFloat ("Lives_Hardcore");
			Timer_Hardcore = (int)PlayerPrefs.GetInt("Timer_Hardcore");
		}

		public void onClick()
		{
			if (Lives_Classic <= 4 || Lives_Hardcore <= 4) 
			{
				Timer_Classic = 0;
				Timer_Hardcore = 0;
				CheckTime_Classic = (int)Timer_Classic;
				CheckTime_Hardcore = (int)Timer_Hardcore;

				PlayerPrefs.SetInt ("Timer_Classic", CheckTime_Classic);
				PlayerPrefs.SetInt ("Timer_Hardcore", CheckTime_Hardcore);

				Lives_Classic = 5;
				Lives_Hardcore = 5;
				int LivesClamp_Classic = Mathf.Clamp (Lives_Classic, 0, 5);
				int LivesClamp_Hardcore = Mathf.Clamp (Lives_Hardcore, 0, 5);
				PlayerPrefs.SetFloat ("Lives_Classic", LivesClamp_Classic);
				PlayerPrefs.SetFloat ("Lives_Hardcore", LivesClamp_Hardcore);
			} 
			else 
			{
				writers.text = "LIVES = 5";
			}
		}
	}
}
