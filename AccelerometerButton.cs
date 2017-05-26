using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Glide.Scripts																				
{
	public class AccelerometerButton : MonoBehaviour
	{
		public Text Accelerometer;
		public int accelerometertriggered;

		static public AccelerometerButton Instance { get { return _instance; } }
		static protected AccelerometerButton _instance;

		public void Start()
		{
			// check if app has empty accelerometer event
			if (!PlayerPrefs.HasKey ("Accelerometer"))
			{
				// if none lets turn off the accelerometer
				PlayerPrefs.SetFloat ("Accelerometer", 0);
			//	Debug.Log ("none" + (int)PlayerPrefs.GetFloat ("Accelerometer"));
			}

			accelerometertriggered = (int)PlayerPrefs.GetFloat ("Accelerometer");

			if (accelerometertriggered == 0) 
			{
				Accelerometer.text = "off";
			}
			else
			{
				Accelerometer.text = "on";
			}
		}

		public void onClick()
		{
			if (accelerometertriggered == 0) 
			{
				accelerometertriggered = 1;
				PlayerPrefs.SetFloat ("Accelerometer", accelerometertriggered);
				Accelerometer.text = "on";
			//	Debug.Log ("accelerometertriggered = true;");
			}
			else
			{
				accelerometertriggered = 0;
				PlayerPrefs.SetFloat ("Accelerometer", accelerometertriggered);
				Accelerometer.text = "off";
			//	Debug.Log ("accelerometertriggered = false;");
			}
		}
	}
}
