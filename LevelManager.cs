using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Glide.Tools;

namespace Glide.Scripts
{	
	public class LevelManager : MonoBehaviour
	{
		static public LevelManager Instance { get { return _instance; } }
		static protected LevelManager _instance;
		protected virtual void Awake()
		{
			_instance=this;			
		}
		public enum Controls { SingleButton, LeftRight }

	    public float Speed { get; protected set; }	
		public float DistanceTraveled { get; protected set; }

		[Header("Prefabs")]
		public GameObject StartingPosition;
		public List<PlayableCharacter> PlayableCharacters;
		public List<PlayableCharacter> CurrentPlayableCharacters { get; set; }
		public float RunningTime { get; protected set; }
	    public float PointsPerSecond = 20;
		[Multiline]
	    public String InstructionsText;


	    [Space(10)]
		[Header("Level Bounds")]
		public Bounds RecycleBounds;

	    [Space(10)]
		[Header("Death Zone")]
		public Bounds DeathBounds;
			
		[Space(10)]
		[Header("Speed")]
		public float InitialSpeed = 10f;
		public float MaximumSpeed = 50f;
		public float SpeedAcceleration=1f;
		
		[Space(10)]
		[Header("Intro and Outro durations")]
		public float IntroFadeDuration=1f;
		public float OutroFadeDuration=1f;
		
		/*
		[Space(10)]
		[Header("Start")]
		/// the duration (in seconds) of the initial countdown
		public int StartCountdown;
		/// the text displayed at the end of the countdown
		public string StartText;*/

	    [Space(10)]
	    [Header("Mobile Controls")]
	    public Controls ControlScheme;

	    [Space(10)]
	    [Header("Life Lost")]
	    public GameObject LifeLostExplosion;

	    // protected stuff
	    protected DateTime _started;
		protected float _savedPoints;	
		protected float _recycleX;
		protected Bounds _tmpRecycleBounds;
			
		protected virtual void Start()
		{
	        Speed = InitialSpeed;
	        DistanceTraveled = 0;

	        InstantiateCharacters();

	        ManageControlScheme();

	        _savedPoints =GameManager.Instance.Points;
			_started = DateTime.UtcNow;
	        GameManager.Instance.SetStatus(GameManager.GameStatus.BeforeGameStart);
	        GameManager.Instance.SetPointsPerSecond(PointsPerSecond);

	        if (GUIManager.Instance != null) 
	        { 
				GUIManager.Instance.SetLevelName(SceneManager.GetActiveScene().name);		
				GUIManager.Instance.FaderOn(false,IntroFadeDuration);
			}

	        PrepareStart();
		}
		
		protected virtual void PrepareStart()
		{	
/*		
			//if we're supposed to show a countdown we schedule it, otherwise we just start the level
			if (StartCountdown>0)
			{
				GameManager.Instance.SetStatus(GameManager.GameStatus.BeforeGameStart);
	            StartCoroutine(PrepareStartCountdown());	
			}	
			else
			{*/
	            LevelStart();
	        //}	
		}
		
		/*
		protected virtual IEnumerator PrepareStartCountdown()
		{
			int countdown = StartCountdown;		
			GUIManager.Instance.SetCountdownActive(true);
			
			// while the countdown is active, we display the current value, and wait for a second and show the next
			while (countdown > 0)
			{
				if (GUIManager.Instance.CountdownText!=null)
				{
					GUIManager.Instance.SetCountdownText(countdown.ToString());
				}
				countdown--;
				yield return new WaitForSeconds(1f);
			}
			
			// when the countdown reaches 0, and if we have a start message, we display it
			if ((countdown==0) && (StartText!=""))
			{
				GUIManager.Instance.SetCountdownText(StartText);
				yield return new WaitForSeconds(1f);
			}
			
			// we turn the countdown inactive, and start the level
			GUIManager.Instance.SetCountdownActive(false);
	        LevelStart();
	    }*/

	    public virtual void LevelStart()
	    {
	        GameManager.Instance.SetStatus(GameManager.GameStatus.GameInProgress);
	        GameManager.Instance.AutoIncrementScore(true);
			EventManager.TriggerEvent("GameStart");
	    }

	  /*  protected virtual void InstantiateCharacters()
	    {
			CurrentPlayableCharacters = new List<PlayableCharacter>();
	        /// we go through the list of playable characters and instantiate them while adding them to the list we'll use from any class to access the
	        /// currently playable characters

	        // for each character in the PlayableCharacters list
	        for (int i = 0; i < PlayableCharacters.Count; i++)
	        {
	        	// we instantiate the corresponding prefab
	            PlayableCharacter instance = (PlayableCharacter)Instantiate(PlayableCharacters[i]);            
	            // we position it based on the StartingPosition point
				instance.transform.position = new Vector2(StartingPosition.transform.position.x + i * DistanceBetweenCharacters, StartingPosition.transform.position.y);
				// we set manually its initial position
				instance.SetInitialPosition(instance.transform.position);
				// we feed it to the game manager
	            CurrentPlayableCharacters.Add(instance);
	        }
	        EventManager.TriggerEvent("PlayableCharactersInstantiated");
	    }*/

			///////////////////
			/// 
			protected virtual void InstantiateCharacters()
			{
				CurrentPlayableCharacters = new List<PlayableCharacter>();

					if (PlayableCharacters.Count!=0)
					{
						for (int i=0;i<PlayableCharacters.Count;i++)
						{
							PlayableCharacters[i].gameObject.SetActive(false);
						}

						// get random character
						int randomCha = UnityEngine.Random.Range(0,PlayableCharacters.Count);
						PlayableCharacters[randomCha].gameObject.SetActive(true);

						// instance chosen character then feed it to the game
						PlayableCharacter instance = (PlayableCharacter)Instantiate(PlayableCharacters[randomCha]);
						instance.transform.position = new Vector2(StartingPosition.transform.position.x, StartingPosition.transform.position.y);
						instance.SetInitialPosition(instance.transform.position);
						CurrentPlayableCharacters.Add(instance);
					}
			}
				
	    public virtual void ResetLevel()
	    {
	        InstantiateCharacters();
	        PrepareStart();
	    }

	    protected virtual void ManageControlScheme() 
	    {
	        String buttonPath = "";
	        switch (ControlScheme)
	        {
	            case Controls.SingleButton:
	                buttonPath = "Canvas/MainActionButton";
	                if (GUIManager.Instance == null) { return; }
	                if (GUIManager.Instance.transform.Find(buttonPath) == null) { return; }
	                GUIManager.Instance.transform.Find(buttonPath).gameObject.SetActive(true);
	                break;
					/*
	            case Controls.LeftRight:
					buttonPath = "Canvas/LeftRight";
	                if (GUIManager.Instance == null) { return; }
	                if (GUIManager.Instance.transform.Find(buttonPath) == null) { return; }
	                GUIManager.Instance.transform.Find(buttonPath).gameObject.SetActive(true);
	                break;*/
	        }

	    }

	    public virtual void Update()
		{
			_savedPoints = GameManager.Instance.Points;
			_started = DateTime.UtcNow;

			// we increment the total distance traveled so far
			DistanceTraveled = DistanceTraveled + Speed * Time.fixedDeltaTime;
			
			// if we can still accelerate, we apply the level's speed acceleration
			if (Speed<MaximumSpeed)
			{
				Speed += SpeedAcceleration * Time.deltaTime;
			}

			RunningTime+=Time.deltaTime;

		
		}
		
		public virtual void SetSpeed(float newSpeed)
		{
			Speed = newSpeed;
		}
		
		public virtual void AddSpeed(float speedAdded)
		{
			Speed += speedAdded;
		}

		public virtual void TemporarilyMultiplySpeed(float factor, float duration)
		{
			StartCoroutine(TemporarilyMultiplySpeedCoroutine(factor, duration));
		}

		protected virtual IEnumerator TemporarilyMultiplySpeedCoroutine(float factor, float duration)
		{
			float saveSpeed=Speed;
			Speed = Speed * factor;
			yield return new WaitForSeconds(1f);
			Speed = saveSpeed;
		}

		public virtual bool CheckRecycleCondition(Bounds objectBounds,float destroyDistance)
		{
			_tmpRecycleBounds = RecycleBounds;
			_tmpRecycleBounds.extents+=Vector3.one * destroyDistance;

			if (objectBounds.Intersects(_tmpRecycleBounds)) 
			{
				return false;
			} 
			else 
			{
				return true;
			}
		}

		public virtual bool CheckDeathCondition(Bounds objectBounds)
		{
			if (objectBounds.Intersects(DeathBounds)) 
			{
				return false;
			} 
			else 
			{
				return true;
			}
		}

		public virtual void GotoLevel(string levelName)
		{		
			GUIManager.Instance.FaderOn(true,OutroFadeDuration);
			StartCoroutine(GotoLevelCo(levelName));
		}
		
		protected virtual IEnumerator GotoLevelCo(string levelName)
		{
	        if (Time.timeScale > 0.0f)
	        {
	            yield return new WaitForSeconds(OutroFadeDuration);
	        }
	        GameManager.Instance.UnPause();

	        if (string.IsNullOrEmpty(levelName))
			{
				LoadingSceneManager.LoadScene("StartScreen");
			}
			else
			{
				LoadingSceneManager.LoadScene(levelName);
			}
			
		}
		
		public virtual void GameOverAction()
		{
	    	GameManager.Instance.UnPause();
			GotoLevel(SceneManager.GetActiveScene().name);
		}

	    public virtual void LifeLostAction()
	    {   
	        ResetLevel();
	    }
		
		public virtual void KillCharacter(PlayableCharacter player)
		{
			StartCoroutine(KillCharacterCo(player));
		}
		
		protected virtual IEnumerator KillCharacterCo(PlayableCharacter player)
		{
			LevelManager.Instance.CurrentPlayableCharacters.Remove(player);
			player.Die();
			yield return new WaitForSeconds(0f);
	        	        
			if (LevelManager.Instance.CurrentPlayableCharacters.Count==0)
			{
				AllCharactersAreDead();
			}
					
		}
		
		protected virtual void AllCharactersAreDead()
		{
	        if (LifeLostExplosion != null)
	        {
	            GameObject explosion = Instantiate(LifeLostExplosion);
	            explosion.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y,0) ;
	        }

	        GameManager.Instance.SetStatus(GameManager.GameStatus.LifeLost);
	        EventManager.TriggerEvent("LifeLost");
	        _started = DateTime.UtcNow;
	        GameManager.Instance.SetPoints(_savedPoints);
	        GameManager.Instance.LoseLives(1);

	        if (GameManager.Instance.CurrentLives<=0)
			{
	            GUIManager.Instance.SetGameOverScreen(true);
	            GameManager.Instance.SetStatus(GameManager.GameStatus.GameOver);
				EventManager.TriggerEvent("GameOver");
	        }
	    }

	    protected virtual void OnEnable()
	    {

	    }

	    protected virtual void OnDisable()
	    {
	    	
	    }
	}
}
