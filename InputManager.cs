using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Glide.Tools;

namespace Glide.Scripts
{	
	public class InputManager : MonoBehaviour
	{
		static public InputManager Instance { get { return _instance; } }
		static protected InputManager _instance;
		protected RigidbodyInterface rigidbodyInterface;
		protected Animator animator;
		public float Force = 20f;
		public int get_lives;

		public GameObject Takeoff;
		public AudioClip SoundFx;

		public void Awake()
		{
			_instance = this;
		}

	    protected virtual void Update()
		{
	        HandleKeyboard();         
	    }

	    protected virtual void HandleKeyboard()
		{
			if (Input.GetButtonDown("Pause")) { PauseButtonDown(); }
			if (Input.GetButtonUp("Pause")) { PauseButtonUp(); }
			if (Input.GetButton("Pause")) { PauseButtonPressed(); }

			if (Input.GetButtonDown("MainAction")) { MainActionButtonDown(); }
			if (Input.GetButtonUp("MainAction")) { MainActionButtonUp(); }
			if (Input.GetButton("MainAction")) { MainActionButtonPressed(); }

			if (Input.GetButton ("Land")) { LandPressed (); } 
	    }
		
		/// PAUSE BUTTON ----------------------------------------------------------------------------------------------------------------
		public virtual void PauseButtonDown() { GameManager.Instance.Pause(); }
		/// <summary>
		/// Triggered once when the pause button is released
		/// </summary>
		public virtual void PauseButtonUp() {}
		/// <summary>
		/// Triggered while the pause button is being pressed
		/// </summary>
		public virtual void PauseButtonPressed() {}


		/// MAIN ACTION BUTTON ----------------------------------------------------------------------------------------------------------------
	    public virtual void MainActionButtonDown()
	    {
			GameManager.Instance.landanimator = 2;
			GameManager.Instance.checkbuttonaccel = false;
	        if (LevelManager.Instance.ControlScheme==LevelManager.Controls.SingleButton)
	        {
	            if (GameManager.Instance.Status == GameManager.GameStatus.GameOver)
	            {
	                LevelManager.Instance.GameOverAction();
	                return;
	            }
	            if (GameManager.Instance.Status == GameManager.GameStatus.LifeLost)
	            {
	                LevelManager.Instance.LifeLostAction();
	                return;
	            }
	        }

			for (int i = 0; i < LevelManager.Instance.CurrentPlayableCharacters.Count; ++i)
			{
				LevelManager.Instance.CurrentPlayableCharacters[i].MainActionStart();
				TakeOffFX();
			//	Debug.Log ("HEYAdown");
			}
		}
	
	    public virtual void MainActionButtonUp()
	    {   
			GameManager.Instance.landanimator = 1;
			for (int i = 0; i < LevelManager.Instance.CurrentPlayableCharacters.Count; ++i)
	        {
				LevelManager.Instance.CurrentPlayableCharacters[i].MainActionEnd();
			//	Debug.Log ("HEYoup" );
	        }
		}
		
		public virtual void MainActionButtonPressed() 
		{
			GameManager.Instance.checkbuttonaccel = false;
				for (int i = 0; i < LevelManager.Instance.CurrentPlayableCharacters.Count; ++i)
				{
					LevelManager.Instance.CurrentPlayableCharacters[i].MainActionOngoing();
				//	Debug.Log ("HEYApress" );
				}
		}

		/// Land BUTTON ----------------------------------------------------------------------------------------------------------------
	    public virtual void LandPressed()
	    {
			GameManager.Instance.landanimator = 3;
			GameManager.Instance._checklanding = true;
			//PlayableCharacter._rigidbodyInterface.AddForce(Vector3.down * FlyForce * Time.deltaTime );
			//Debug.Log ("LandPressed"+ GameManager.Instance.check);
		}


		/// TakeOff FX function ---------------------------------------------------------------------------------------------------------
		public void TakeOffFX()
		{
			GameObject offFx = (GameObject)Instantiate(Takeoff);
			offFx.transform.position = transform.position;

			// if we have a sound manager and if we've specified a song to play when this object is picked
			if (SoundManager.Instance!=null && SoundFx!=null)
			{
				// we play that sound once
				SoundManager.Instance.PlaySound(SoundFx,transform.position);	
			}
		}

	}
}