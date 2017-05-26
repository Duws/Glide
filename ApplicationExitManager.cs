using UnityEngine;
using System.Collections;

namespace Glide.Scripts																				
{
	public class ApplicationExitManager : MonoBehaviour {

		void OnApplicationQuit()
		{
			// save current system time as string in playerprefs
			PlayerPrefs.SetString("DateTime", System.DateTime.Now.ToBinary().ToString());
			//Debug.Log ("saving this date to prefs : " + System.DateTime.Now);

			//set game mode back to default 4
			PlayerPrefs.SetFloat ("GameModes", 4);
			//setpractice mode to default 2
			PlayerPrefs.SetFloat ("PracticeMode", 2);
		}
	}
}
