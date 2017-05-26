using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Glide.Scripts																				
{
	public class AudioHandler : MonoBehaviour
	{
		public Text mute;
		int AudioStat;

		public void Start()
		{
			// check if app has empty audio setting
			if (!PlayerPrefs.HasKey ("AudioSet"))
			{
				// if none lets turn on the volume by default
				PlayerPrefs.SetFloat ("AudioSet", 1);
				//Debug.Log ("none" + (int)PlayerPrefs.GetFloat ("AudioSet"));
			}
			
			AudioStat = (int)PlayerPrefs.GetFloat ("AudioSet");
			
			
			// 1 == volume on
			if (AudioStat == 1) 
			{
				AudioListener.pause = false;
				AudioListener.volume = 1;
				mute.text = "mute";
			}
			else
			{
				AudioListener.pause = true;
				AudioListener.volume = 0;
				mute.text = "unmute";
			}
			
		}

		public void onClick()
		{
			if (AudioStat == 1) 
			{
				AudioStat = 0;
				AudioListener.pause = true;
				AudioListener.volume = 0;
				PlayerPrefs.SetFloat ("AudioSet", AudioStat);
				mute.text = "unmute";
				//Debug.Log ("audio.mute = true;");
			}
			else
			{
				AudioStat = 1;
				AudioListener.pause = false;
				AudioListener.volume = 1;
				PlayerPrefs.SetFloat ("AudioSet", AudioStat);
				mute.text = "mute";
				//Debug.Log ("audio.mute = false;");
			}
		}
	}
}
