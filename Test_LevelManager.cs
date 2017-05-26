using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Glide.Tools;

namespace Glide.Scripts
{	
	public class Test_LevelManager : LevelManager
	{
		[Header("Backgrounds")]
		public List<GameObject> Backgrounds;

		protected override void Start()
		{
			RandomizeBackground();

			Speed = InitialSpeed;
			DistanceTraveled = 0;

			GUIManager.Instance.FaderOn(false,IntroFadeDuration);

	        InstantiateCharacters();

	        ManageControlScheme();

	        _savedPoints =GameManager.Instance.Points;
			_started = DateTime.UtcNow;
	        GameManager.Instance.SetStatus(GameManager.GameStatus.BeforeGameStart);
	        GameManager.Instance.SetPointsPerSecond(PointsPerSecond);

	        GameManager.Instance.SetLives(1);

			CurrentPlayableCharacters[0].GetComponent<Rigidbody2D>().isKinematic=true;

		}

		protected virtual void RandomizeBackground()
		{
			if (Backgrounds.Count!=0)
			{
				for (int i=0;i<Backgrounds.Count;i++)
				{
					Backgrounds[i].SetActive(false);
				}

				int randomBg = UnityEngine.Random.Range(0,Backgrounds.Count);
				Backgrounds[randomBg].SetActive(true);
			}
		}

	    public override void LevelStart()
	    {
			base.LevelStart();
			CurrentPlayableCharacters[0].GetComponent<Rigidbody2D>().isKinematic=false;
	    }

	    protected override void InstantiateCharacters()
	    {
			base.InstantiateCharacters();
	    }

	    public override void ResetLevel()
	    {
	        base.ResetLevel();
	    }

	    protected override void ManageControlScheme() 
	    {
	        base.ManageControlScheme();
	    }

	    public override void Update()
		{
			base.Update();
		}
				
		protected override void AllCharactersAreDead()
		{
			if ((GameManager.Instance.Status == GameManager.GameStatus.GameOver) || (GameManager.Instance.Status == GameManager.GameStatus.LifeLost))
			{
				return;
			}

	        if (LifeLostExplosion != null)
	        {
	            GameObject explosion = Instantiate(LifeLostExplosion);
	            explosion.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y,0) ;
	        }


            GUIManager.Instance.SetGameOverScreen(true);
            GameManager.Instance.SetStatus(GameManager.GameStatus.GameOver);
			EventManager.TriggerEvent("GameOver");
	        
	    }
	}
}
