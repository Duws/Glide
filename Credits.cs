using UnityEngine;
using UnityEngine.UI;
using Glide.Tools;

namespace Glide.Scripts
{
	public class Credits : MonoBehaviour
	{	
		public string MainMenu, gotoCredits, RequestNiGab;

		public virtual void OnClick()
		{
			LoadingSceneManager.LoadScene (gotoCredits);
		}

		public void BacktoMenu()
		{
			LoadingSceneManager.LoadScene (MainMenu);
		}

		public void OldStart()
		{
			LoadingSceneManager.LoadScene (RequestNiGab);
		}

	}
}
