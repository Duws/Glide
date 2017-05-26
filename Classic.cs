using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Glide.Scripts
{
	public class Classic : MonoBehaviour
	{
		static public Classic Instance { get { return _instance; } }
		static protected Classic _instance;

		[Header("Others")]
		//lives
		public int tut;
		public Text txtlives;
		public Text CheckLife;
		public Text time;
		float timer;
		int checktime;
		public int lives;
		// highscore
		public Text Highscore;

		[Header("Scenes")]
		public string GlideClassic;
		public string BackGameModes;

		void Start ()
		{
			// get highscore from playerprefs
			Highscore.text = "LASTED FOR\n" + (int)PlayerPrefs.GetFloat ("HighScore_Classic") + "\nSECONDS";

			tut = 0;
			
			// check if app is newly installed
			if (!PlayerPrefs.HasKey ("Lives_Classic"))
			{
				// if yes, set starting lives to 5
				PlayerPrefs.SetFloat ("Lives_Classic", 5);
			//	Debug.Log ("fresh " + (int)PlayerPrefs.GetFloat ("lives"));
			}

			//get lives from playerpref
			lives = (int)PlayerPrefs.GetFloat ("Lives_Classic");

			txtlives.text = "LIVES\n" + lives;

			// check if app is newly installed
			if (!PlayerPrefs.HasKey ("Timer_Classic"))
			{
				// if yes, set timer to 0
				PlayerPrefs.SetInt ("Timer_Classic", 0);
			}

			timer = (int)PlayerPrefs.GetInt("Timer_Classic");
			//Debug.Log ("Timer from Get_DB : " + timer);

		}

		//onclick
		public virtual void Checklife()
		{
			if (lives <= 0)
			{
				CheckLife.text = "LIVES DEPLETED";
			//	Debug.Log ("yaycheck");
			}
			else if (lives > 0)
			{
				CheckLife.text = "";
				lives--;
				PlayerPrefs.SetFloat ("Lives_Classic", lives);
				LoadingSceneManager.LoadScene (GlideClassic);
				//Debug.Log ("minus"+lives);
			}
		}   

		public void backtogamemode()
		{
			LoadingSceneManager.LoadScene (BackGameModes);
		}

		void Update()
		{
			if (Input.GetButtonDown ("MainAction")) {Checklife (); }
			if (lives <= 4)
			{
				timer += Time.deltaTime;
				checktime = (int)timer;
				// put to db incase scene is destroyed
				PlayerPrefs.SetInt ("Timer_Classic", checktime);
				//Debug.Log (checktime);

				txtlives.text = "LIVES\n" + lives + " of 5";

				time.text = "LIFE TIMER\n" + (int)timer + "s of 300s";
				//Debug.Log (timer);

				if (timer >= 300) {
					lives++;
					// clamp lives to not exceed more than 5
					int livesclamp = Mathf.Clamp (lives, 0, 5);
					PlayerPrefs.SetFloat ("Lives_Classic", livesclamp);
					CheckLife.text = "";
					timer = 0;
					checktime = (int)timer;
					PlayerPrefs.SetInt ("Timer_Classic", checktime);
				}
			} 
			else 
			{
				txtlives.text = "LIVES\n" + lives;
				time.text = "";
			}
				
			//Debug.Log (lives);
		}
	}
}
