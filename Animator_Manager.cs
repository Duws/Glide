using UnityEngine;
using System.Collections;
using Glide.Tools;

namespace Glide.Scripts
{	
	public class Animator_Manager : MonoBehaviour 
	{
		Animator anim;

		void Start () 
		{
			anim = GetComponent<Animator> ();
		}

		void Update () 
		{
			//1 is idle
			//2 is thrust
			//3 or 4 is land
			if (GameManager.Instance.landanimator == 2)
			{
				anim.SetInteger ("Animate", 2);
			} 
			else if (GameManager.Instance.landanimator == 1)
			{
				anim.SetInteger ("Animate", 1);
			}
			else if (GameManager.Instance.landanimator == 3)
			{
				anim.SetInteger ("Animate", 3);
			}


		}
	}
}
