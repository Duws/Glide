using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Glide.Scripts
{
	public class RestartManager : MonoBehaviour
	{
		public GameObject RestartButton;
		public int RestartLivesHardcore, RestartLivesClassic, checkmode;
		public Text RestartText;

		public string NextLevelName;

		static public RestartManager Instance { get { return _instance; } }
		static protected RestartManager _instance;

		public void Start()
		{
			checkmode = (int)PlayerPrefs.GetFloat ("GameModes");

			RestartLivesHardcore = (int)PlayerPrefs.GetFloat ("Lives_Hardcore");
			RestartLivesClassic = (int)PlayerPrefs.GetFloat ("Lives_Classic");

			if (checkmode == 0) 
			{
				if (RestartLivesClassic <= 0)
				{
					RestartButton.SetActive (false);
					RestartText.text = "No more lives";
				} 
				else
				{
					RestartButton.SetActive (true);
					RestartText.text = "";
				}
			}
			else if (checkmode == 1) 
			{
				if (RestartLivesHardcore <= 0)
				{
					RestartButton.SetActive (false);
					RestartText.text = "No more lives";
				} 
				else
				{
					RestartButton.SetActive (true);
					RestartText.text = "";
				}
			}


		}

		public void Update()
		{
			RestartLivesHardcore = (int)PlayerPrefs.GetFloat ("Lives_Hardcore");
			RestartLivesClassic = (int)PlayerPrefs.GetFloat ("Lives_Classic");
		}

		public void onClick()
		{
			if (checkmode == 0)
			{
				if (RestartLivesClassic <= 0)
				{
					RestartButton.SetActive (false);
					RestartText.text = "No more lives";
				}
				else 
				{
					RestartButton.SetActive (true);
					RestartText.text = "";

					RestartLivesClassic--;
					PlayerPrefs.SetFloat ("Lives_Classic", RestartLivesClassic);
				}
			} 
			else if (checkmode == 1)
			{
				if (RestartLivesHardcore <= 0)
				{
					RestartButton.SetActive (false);
					RestartText.text = "No more lives";
				} 
				else 
				{
					RestartButton.SetActive (true);
					RestartText.text = "";

					RestartLivesHardcore--;
					PlayerPrefs.SetFloat ("Lives_Hardcore", RestartLivesHardcore);
				}
			}
			//
		}
		//****
		public void PracticeRestart()
		{
			
		}
	}
}