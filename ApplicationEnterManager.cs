using System;
using UnityEngine;
using System.Collections;

namespace Glide.Scripts
{
	public class ApplicationEnterManager : MonoBehaviour
	{
		static public ApplicationEnterManager Instance { get { return _instance; } }
		static protected ApplicationEnterManager _instance;

		//holder
		int Lives_Hardcore, Lives_Classic;
		//from db
		int Lives_HardcoreDB, Lives_ClassicDB;
		//clamp lives
		int HardcoreLivesClamp, ClassicLivesClamp;
		//date storage
		long temp;

		DateTime currentDate;
		DateTime oldDate;

		void Start ()
		{
			// get lives from db
			Lives_HardcoreDB = (int)PlayerPrefs.GetFloat ("Lives_Hardcore");
			Lives_ClassicDB = (int)PlayerPrefs.GetFloat ("Lives_Classic");
			//Debug.Log (livesfromdb);

			// store current time when game starts
			currentDate = System.DateTime.Now;

			// check if app has empty saved time
			if (!PlayerPrefs.HasKey ("DateTime"))
			{
				//Debug.Log ("STARTING DATE: " + PlayerPrefs.GetString ("DateTime"));
				PlayerPrefs.SetString ("DateTime", System.DateTime.Now.ToBinary ().ToString ());
				// grab the date from playerprefs as a long
				temp = Convert.ToInt64(PlayerPrefs.GetString("DateTime"));
				//Debug.Log ("temp: " + temp);
			}
			else
			{
				// grab the old date from playerprefs as a long
				temp = Convert.ToInt64(PlayerPrefs.GetString("DateTime"));
			}

			// convert the old time from binary to a date time variable
			DateTime oldDate = DateTime.FromBinary(temp);
			//Debug.Log ("oldDate : " + oldDate);

			// use the subtract method and store the result as a timespan variable
			TimeSpan difference = currentDate.Subtract(oldDate);
			//Debug.Log ("Difference : " + difference);

			// convert timespan to seconds
			double secondsaway = difference.TotalSeconds;
			//Debug.Log ("Seconds Away : " + secondsaway);

			if (secondsaway >= 1500)
			{
				//add lives
				Lives_Hardcore = Lives_HardcoreDB + 5;
				Lives_Classic = Lives_ClassicDB + 5;
				//hardcore lives clamp
				HardcoreLivesClamp = Mathf.Clamp (Lives_Hardcore, 0, 5);
				PlayerPrefs.SetFloat ("Lives_Hardcore", HardcoreLivesClamp);
				//classic lives clamp
				ClassicLivesClamp = Mathf.Clamp (Lives_Classic, 0, 5);
				PlayerPrefs.SetFloat ("Lives_Classic", ClassicLivesClamp);
			} 

			else if (secondsaway >= 1200 && secondsaway <= 1499)
			{	
				//add lives
				Lives_Hardcore = Lives_HardcoreDB + 4;
				Lives_Classic = Lives_ClassicDB + 4;
				//hardcore lives clamp
				HardcoreLivesClamp = Mathf.Clamp (Lives_Hardcore, 0, 5);
				PlayerPrefs.SetFloat ("Lives_Hardcore", HardcoreLivesClamp);
				//classic lives clamp
				ClassicLivesClamp = Mathf.Clamp (Lives_Classic, 0, 5);
				PlayerPrefs.SetFloat ("Lives_Classic", ClassicLivesClamp);
			}

			else if (secondsaway >= 900 && secondsaway <= 1199)
			{	
				//add lives
				Lives_Hardcore = Lives_HardcoreDB + 3;
				Lives_Classic = Lives_ClassicDB + 3;
				//hardcore lives clamp
				HardcoreLivesClamp = Mathf.Clamp (Lives_Hardcore, 0, 5);
				PlayerPrefs.SetFloat ("Lives_Hardcore", HardcoreLivesClamp);
				//classic lives clamp
				ClassicLivesClamp = Mathf.Clamp (Lives_Classic, 0, 5);
				PlayerPrefs.SetFloat ("Lives_Classic", ClassicLivesClamp);
			}

			else if (secondsaway >= 600 && secondsaway <= 899)
			{	
				//add lives
				Lives_Hardcore = Lives_HardcoreDB + 2;
				Lives_Classic = Lives_ClassicDB + 2;
				//hardcore lives clamp
				HardcoreLivesClamp = Mathf.Clamp (Lives_Hardcore, 0, 5);
				PlayerPrefs.SetFloat ("Lives_Hardcore", HardcoreLivesClamp);
				//classic lives clamp
				ClassicLivesClamp = Mathf.Clamp (Lives_Classic, 0, 5);
				PlayerPrefs.SetFloat ("Lives_Classic", ClassicLivesClamp);
			}

			else if (secondsaway >= 300 && secondsaway <= 599)
			{	
				//add lives
				Lives_Hardcore = Lives_HardcoreDB + 1;
				Lives_Classic = Lives_ClassicDB + 1;
				//hardcore lives clamp
				HardcoreLivesClamp = Mathf.Clamp (Lives_Hardcore, 0, 5);
				PlayerPrefs.SetFloat ("Lives_Hardcore", HardcoreLivesClamp);
				//classic lives clamp
				ClassicLivesClamp = Mathf.Clamp (Lives_Classic, 0, 5);
				PlayerPrefs.SetFloat ("Lives_Classic", ClassicLivesClamp);
			}

		}

	}
}
