using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Glide.Scripts
{	
	public class GameManager : MonoBehaviour
	{			
		public int TotalLives = 1;
	    public int CurrentLives { get; protected set;  }
		public float Points { get; protected set; }
		public float TimeScale;
	    public enum GameStatus { BeforeGameStart, GameInProgress, Paused, GameOver, LifeLost };
	    public GameStatus Status{ get; protected set; }

		public bool _checklanding = false;
		public bool _landingtriggerenter = false;
		public bool accel = false;
		public bool checkbuttonaccel = false;
		public int landanimator = 0;

	    public delegate void GameManagerInspectorRedraw();
	    public event GameManagerInspectorRedraw GameManagerInspectorNeedRedraw;

	    protected float _savedTimeScale;
	    protected IEnumerator _scoreCoroutine;
	    protected float _pointsPerSecond;
	    protected GameStatus _statusBeforePause;
	    
		static public GameManager Instance { get { return _instance; } }
		static protected GameManager _instance;
		
		public void Awake()
		{
			_instance = this;        
	    }

	    protected virtual void Start()
		{
	        CurrentLives = TotalLives;
	        _savedTimeScale = TimeScale;
	        Time.timeScale = TimeScale;
	        if (GUIManager.Instance!=null)
	        {
				GUIManager.Instance.Initialize();
	        }
	    }

	    public virtual void SetPointsPerSecond(float newPointsPerSecond)
	    {
	        _pointsPerSecond = newPointsPerSecond;
	    }
		
		public virtual void SetStatus(GameStatus newStatus)
		{
			Status=newStatus;
	        if (GameManagerInspectorNeedRedraw != null) { GameManagerInspectorNeedRedraw(); }
	    }
				
		public virtual void Reset()
		{
			Points = 0;
			TimeScale = 1f;
			GameManager.Instance.SetStatus(GameStatus.GameInProgress);
			EventManager.TriggerEvent("GameStart");
			GUIManager.Instance.RefreshPoints ();
		}

	    public virtual void AutoIncrementScore(bool status)
	    {
	        if (status)
	        {
	            StartCoroutine(IncrementScore());
	        }
	        else
	        {
	            StopCoroutine(IncrementScore());
	        }
	    }

	    protected virtual IEnumerator IncrementScore()
		{
	        while (true)
	        {
	            if ((GameManager.Instance.Status == GameStatus.GameInProgress) && (_pointsPerSecond!=0) )
	            {
	                AddPoints(_pointsPerSecond / 100);
	            }
	            yield return new WaitForSeconds(0.01f);
	        }
	    }
		
		public virtual void AddPoints(float pointsToAdd)
		{
			Points += pointsToAdd;
			if (GUIManager.Instance!=null)
			{
				GUIManager.Instance.RefreshPoints ();
			}
		}
		
		public virtual void SetPoints(float points)
		{
			Points = points;
			if (GUIManager.Instance!=null)
			{
				GUIManager.Instance.RefreshPoints ();
			}
		}

	    public virtual void SetLives(int lives)
	    {
	        CurrentLives = lives;
			if (GUIManager.Instance!=null)
			{
		        GUIManager.Instance.InitializeLives();
			}
	    }
	    
	    public virtual void LoseLives(int lives)
	    {
			CurrentLives -= lives;
			if (GUIManager.Instance!=null)
			{
		        GUIManager.Instance.InitializeLives();
			}
	    }

	    public virtual void SetTimeScale(float newTimeScale)
		{
			_savedTimeScale = Time.timeScale;
			Time.timeScale = newTimeScale;
		}
		
		public virtual void ResetTimeScale()
		{
			Time.timeScale = _savedTimeScale;
		}
		
		public virtual void Pause()
		{
			if (Time.timeScale>0.0f)
			{
				Instance.SetTimeScale(0.0f);
				_statusBeforePause=Instance.Status;
				Instance.SetStatus(GameStatus.Paused);
				EventManager.TriggerEvent("Pause");
				if (GUIManager.Instance!=null)
				{
					GUIManager.Instance.SetPause(true);
				}
			}
			else
			{
	            UnPause();	
			}		
		}

	    public virtual void UnPause()
	    {
	        Instance.ResetTimeScale();
			Instance.SetStatus(_statusBeforePause);
			if (GUIManager.Instance!=null)
			{
		        GUIManager.Instance.SetPause(false);
			}
	    }
	}
}