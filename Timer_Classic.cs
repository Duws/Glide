using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Glide.Scripts
{
	public class Timer_Classic : MonoBehaviour
	{
		static public Timer_Classic Instance { get { return _instance; } }
		static protected Timer_Classic _instance;

		float timer;
		int checktime;
		int lives;

		void Start ()
		{
			//get lives from playerpref
			lives = (int)PlayerPrefs.GetFloat ("Lives_Classic");
			//Debug.Log ("lives from timer manager: " + lives);

			timer = (int)PlayerPrefs.GetInt("Timer_Classic");
			//Debug.Log ("timer from timer manager: " + timer);

		}

		void Update()
		{
			if (lives <= 4)
			{
				timer += Time.deltaTime;
				checktime = (int)timer;
				//Debug.Log (checktime);

				// put to db incase scene is destroyed
				PlayerPrefs.SetInt ("Timer_Classic", checktime);
				//Debug.Log ("check time from timer manager" + checktime);

				if (timer >= 300) {
					lives++;
					// clamp lives to not exceed more than 5
					int livesclamp = Mathf.Clamp (lives, 0, 5);
					//Debug.Log ("livesclamp from timer manager: " + livesclamp);
					PlayerPrefs.SetFloat ("Lives_Classic", livesclamp);
					timer = 0;
					checktime = (int)timer;
					PlayerPrefs.SetInt ("Timer_Classic", checktime);
				}
			} 
		}
	}
}
