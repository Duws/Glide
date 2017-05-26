using UnityEngine;
using System.Collections;

namespace Glide.Scripts
{	
	public class KillsPlayerOnTouch : MonoBehaviour 
	{
	    protected virtual void OnTriggerEnter2D (Collider2D other)
		{
			TriggerEnter (other.gameObject);
		}
		
	    protected virtual void OnTriggerEnter (Collider other)
		{		
			TriggerEnter (other.gameObject);
		}	
		
		protected virtual void TriggerEnter(GameObject collidingObject)
		{		
			if (collidingObject.tag!="Player") { return; }	

			PlayableCharacter player = collidingObject.GetComponent<PlayableCharacter>();
			if (player==null) { return; }	

			LevelManager.Instance.KillCharacter(player);
		}
	}
}
