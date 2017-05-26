using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Glide.Scripts
{
	public class RandomBackground : MonoBehaviour 
	{
		[Header("Backgrounds")]
		public List<GameObject> Background;

		void Start () 
		{
			Randomizebg();
		}

		protected virtual void Randomizebg()
		{
			if (Background.Count!=0)
			{
				for (int i=0;i<Background.Count;i++)
				{
					Background[i].SetActive(false);
				}

				int randomBg = UnityEngine.Random.Range(0,Background.Count);
				Background[randomBg].SetActive(true);
			}
		}
	}
}
