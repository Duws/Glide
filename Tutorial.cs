using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Glide.Scripts
{
	public class Tutorial : MonoBehaviour
	{
		public string NextLevelName;
		public List<GameObject> tut;
		int ini = 0;

		void Start() 
		{
			tut[0].SetActive(true);
			tut[1].SetActive(false);
			tut[2].SetActive(false);
			tut[3].SetActive(false);

			ini = 0;
			//Debug.Log (ini);
		}

		public void OnClick() 
		{
			ini++;
			//Debug.Log (ini);

			if (ini == 1)
			{
				tut[0].SetActive(false);
				tut[1].SetActive(true);
			}
			else if (ini == 2) 
			{
				tut[1].SetActive(false);
				tut[2].SetActive(true);
			}
			else if (ini == 3) 
			{
				tut[2].SetActive(false);
				tut[3].SetActive(true);
			}
			else if (ini == 4)
			{
					LoadingSceneManager.LoadScene (NextLevelName);
					ini = 0;
			}
		}
	}
}
