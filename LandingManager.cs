using UnityEngine;
using System.Collections;
using Glide.Tools;

namespace Glide.Scripts
{
	public class LandingManager : MonoBehaviour
	{
		static public LandingManager Instance { get { return _instance; } }
		static protected LandingManager _instance;

		public void OnTriggerEnter2D (Collider2D other)
		{

			GameManager.Instance._landingtriggerenter = true;

			if (GameManager.Instance._checklanding == true && GameManager.Instance._landingtriggerenter == true)
			{
				//Debug.Log ("OnTriggerEnter2D true");
				GUIManager.Instance.SetGameOverScreen (true);
			    GameManager.Instance.SetTimeScale (0.0f);

				//	PlayableCharacter player = Rigidbody2D.GetComponent<PlayableCharacter>();
			} 
			else if (GameManager.Instance._checklanding == false && GameManager.Instance._landingtriggerenter == true)
			{
				//Debug.Log ("OnTriggerEnter2D false");
				GUIManager.Instance.SetGameOverScreen (true);
				//	GameManager.Instance.SetTimeScale (0.0f);
			}

			else if (GameManager.Instance._checklanding == false && GameManager.Instance._landingtriggerenter == false)
			{
				//Debug.Log ("OnTriggerEnter2D false :false");
				GUIManager.Instance.SetGameOverScreen (true);
				//	GameManager.Instance.SetTimeScale (0.0f);
			}

		}

	}

 }


