using UnityEngine;
using System.Collections;

namespace Glide.Scripts
{
	public class AccelerometerManager : MonoBehaviour 	
	{	
		void Update()
		{
			// get x-axis acceleration value
			float _xaccel = Input.acceleration.x;

			int accelerometertriggered = (int)PlayerPrefs.GetFloat ("Accelerometer");

			if (_xaccel < -0.1f) 
			{
				// checks if button accelerometer is on/off
				if (accelerometertriggered == 1) 
				{
					// if x acceleration is less than -0.1f set bool accel to true
					GameManager.Instance.accel = true;
					GameManager.Instance.checkbuttonaccel = true;

					for (int i = 0; i < LevelManager.Instance.CurrentPlayableCharacters.Count; ++i)
					{
						LevelManager.Instance.CurrentPlayableCharacters[i].MainActionStart();
						//	Debug.Log ("HEYAdown");
					}
					//Debug.Log ("up    " + _xaccel);
				}
			}

			else if (_xaccel > -0.2f)
			{
				// else set bool accel to false
				GameManager.Instance.accel = false;
				//Debug.Log ("down    " + _xaccel);
			}
		}

	}
}
