using UnityEngine;
using UnityEngine.UI;

namespace Glide.Scripts
{
	public class ScreenManager : MonoBehaviour
	{
		void Update ()
		{
			Screen.sleepTimeout = (int)0f;
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}

	}
}
