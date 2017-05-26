using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Glide.Tools;

namespace Glide.Scripts
{	
	public class GUIManager : MonoBehaviour 
	{
		public GameObject PauseScreen;	
	    public GameObject GameOverScreen;
		public Text PointsText, LevelText, CountdownText;
		public Image Fader;

		public Text instructions;
		int Lives_Hardcore, Lives_HardcoreClamp;
		int checkmode, practicemode;

		public Text previoushighscore;
		
		static public GUIManager Instance { get { return _instance; } }
		static protected GUIManager _instance;
		
		public void Awake()
		{
			_instance = this;
		}

		public virtual void Start()
		{
			checkmode = (int)PlayerPrefs.GetFloat ("GameModes");
			practicemode = (int)PlayerPrefs.GetFloat ("PracticeMode");
			//check what gamemode on
			if (checkmode == 0)
			{
				// check if app is newly installed
				if (!PlayerPrefs.HasKey ("HighScore_Classic"))
				{
					// if yes, set starting lives to 0
					PlayerPrefs.SetFloat ("HighScore_Classic", 0);
				}
				previoushighscore.text = "PREVIOUS HIGHSCORE\n" + "LASTED FOR\n" + (int)PlayerPrefs.GetFloat ("HighScore_Classic") + " SECONDS";
				//Debug.Log ("00000000000");
			}
			else if (checkmode == 1 || checkmode == 3)
			{
				// check if app is newly installed
				if (!PlayerPrefs.HasKey ("HighScore_Hardcore"))
				{
					// if yes, set starting lives to 0
					PlayerPrefs.SetFloat ("HighScore_Hardcore", 0);
				}
				//get lives from db
				Lives_Hardcore = (int)PlayerPrefs.GetFloat ("Lives_Hardcore");
				previoushighscore.text = "PREVIOUS HIGHSCORE\n" + "LASTED FOR\n" + (int)PlayerPrefs.GetFloat ("HighScore_Hardcore") + " SECONDS";
				//Debug.Log ("11111111111");
			}
		}

		public virtual void Initialize()
		{
			GameManager.Instance._checklanding = false;
			GameManager.Instance._landingtriggerenter = false;

			RefreshPoints();
	      //  InitializeLives();

	        if (CountdownText!=null)
	        {
				CountdownText.enabled=false;
			}
	    }

	    public virtual void InitializeLives()
	    {

	    	/*if (HeartsContainer==null)
	    		return;

	    	// we remove everything inside the HeartsContainer
	        foreach (Transform child in HeartsContainer.transform)
	        {
	            Destroy(child.gameObject);
	        }
			
	        int deadLives = GameManager.Instance.TotalLives - GameManager.Instance.CurrentLives;
	        // for each life in the total number of possible lives you can have
	        for (int i=0; i < GameManager.Instance.TotalLives; i++)
	        {
	        	// if the life is already lost, we display an empty heart
	            string resourceURL = "";
	            if (deadLives>0)
	            {
	                resourceURL = "GUI/GUIHeartEmpty";
	            }
	            else
	            {
	            	// if the life is still 'alive', we display a full heart
	                resourceURL = "GUI/GUIHeartFull";
	            }
	            // we instantiate the heart gameobject and position it
	            GameObject heart = (GameObject)Instantiate(Resources.Load(resourceURL));
	            heart.transform.SetParent(HeartsContainer.transform, false);
	            heart.GetComponent<RectTransform>().localPosition = new Vector3(HeartsContainer.GetComponent<RectTransform>().sizeDelta.x/2 - i * (heart.GetComponent<RectTransform>().sizeDelta.x * 75f ), 0, 0);
	            deadLives--;
	        }*/
	    }

	    public virtual void OnGameStart()
	    {
	    	
	    }
		
		public virtual void SetPause(bool state)
		{
			PauseScreen.SetActive(state);
	    }
		
		public virtual void SetCountdownActive(bool state)
		{
			if (CountdownText==null) { return; }
			CountdownText.enabled=state;
		}
		
		public virtual void SetCountdownText(string newText)
		{
			if (CountdownText==null) { return; }
			CountdownText.text=newText;
		}
		
		public virtual void SetGameOverScreen(bool state)
		{
			/// classic ************
			if (checkmode == 0) 
			{
				GameOverScreen.SetActive (state);
				Text gameOverScreenTextObject = GameOverScreen.transform.Find ("GameOverScreenText").GetComponent<Text> ();
				if (gameOverScreenTextObject != null) 
				{
					//Debug.Log ("_checklanding == true  ..... true : true");
					if ((int)PlayerPrefs.GetFloat ("HighScore_Classic") < GameManager.Instance.Points)
					{
						PlayerPrefs.SetFloat ("HighScore_Classic", GameManager.Instance.Points);
						gameOverScreenTextObject.text = "new highscore";
						instructions.text = "Lasted for\n" + (int)PlayerPrefs.GetFloat ("HighScore_Classic") + " Seconds";
					} 
					else 
					{
						//gameOverScreenTextObject.text = "Highscore: ";
						instructions.text = "Previous Highscore\n"+ "Lasted for " + (int)PlayerPrefs.GetFloat ("HighScore_Classic") + " Seconds";
					}
				}
			}
			/// hardcore *********
			else if (checkmode == 1 || checkmode == 3) 
			{
				if (GameManager.Instance._checklanding == true && GameManager.Instance._landingtriggerenter == true) 	
				{
					GameOverScreen.SetActive (state);
					Text gameOverScreenTextObject = GameOverScreen.transform.Find ("GameOverScreenText").GetComponent<Text> ();
					if (gameOverScreenTextObject != null) 
					{
						//Debug.Log ("_checklanding == true  ..... true : true");
						if ((int)PlayerPrefs.GetFloat ("HighScore_Hardcore") < GameManager.Instance.Points)
						{
							PlayerPrefs.SetFloat ("HighScore_Hardcore", GameManager.Instance.Points);
							gameOverScreenTextObject.text = "new highscore!";
							instructions.text = "Lasted for\n" + (int)PlayerPrefs.GetFloat ("HighScore_Hardcore") + " Seconds";
							// add lives if landing success
							Lives_Hardcore++;
							// clamp lives to not exceed more than 5
							Lives_HardcoreClamp = Mathf.Clamp (Lives_Hardcore, 0, 5);
							PlayerPrefs.SetFloat ("Lives_Hardcore", Lives_HardcoreClamp);
						} 
						else 
						{
							//gameOverScreenTextObject.text = "Highscore: ";
							instructions.text = "Previous Highscore\n"+ "Lasted for " + (int)PlayerPrefs.GetFloat ("HighScore_Hardcore") + " Seconds";
							// add lives if landing success even if it does not meet the highscore
							Lives_Hardcore++;
							// clamp lives to not exceed more than 5
							Lives_HardcoreClamp = Mathf.Clamp (Lives_Hardcore, 0, 5);
							PlayerPrefs.SetFloat ("Lives_Hardcore", Lives_HardcoreClamp);
						}
					}
				} 
				else if (GameManager.Instance._checklanding == true && GameManager.Instance._landingtriggerenter == false) 
				{
					GameOverScreen.SetActive (state);
					Text gameOverScreenTextObject = GameOverScreen.transform.Find ("GameOverScreenText").GetComponent<Text> ();
					if (gameOverScreenTextObject != null) 
					{
						//	Debug.Log ("_checklanding == false   .... true : false");
						gameOverScreenTextObject.text = "game over";
						instructions.text = "You Need to Land Properly";

					}
				}	
				else if (GameManager.Instance._checklanding == false && GameManager.Instance._landingtriggerenter == true) 
				{
					GameOverScreen.SetActive (state);
					Text gameOverScreenTextObject = GameOverScreen.transform.Find ("GameOverScreenText").GetComponent<Text> ();
					if (gameOverScreenTextObject != null) 
					{
						//	Debug.Log ("_checklanding == false   .... false : true");
						gameOverScreenTextObject.text = "game over";
						instructions.text = "You Need to Land Properly";

					}
				}	
				else if (GameManager.Instance._checklanding == false && GameManager.Instance._landingtriggerenter == false) 
				{
					GameOverScreen.SetActive (state);
					Text gameOverScreenTextObject = GameOverScreen.transform.Find ("GameOverScreenText").GetComponent<Text> ();
					if (gameOverScreenTextObject != null) 
					{
						//Debug.Log ("_checklanding == false   .... false : false");
						gameOverScreenTextObject.text = "game over";
						instructions.text = "You Need to Land Properly";
					}
				}	
			}
			/// practice classic mode ********
			else if (practicemode == 0)
			{
				GameOverScreen.SetActive (state);
				Text gameOverScreenTextObject = GameOverScreen.transform.Find ("GameOverScreenText").GetComponent<Text> ();
				if (gameOverScreenTextObject != null) 
				{
					instructions.text = "Lasted for\n" + GameManager.Instance.Points.ToString("0" + "\nSeconds");
				}
			}
			/// practice hard core*******
			else if (practicemode == 1) 
			{
				if (GameManager.Instance._checklanding == true && GameManager.Instance._landingtriggerenter == true) 	
				{
					GameOverScreen.SetActive (state);
					Text gameOverScreenTextObject = GameOverScreen.transform.Find ("GameOverScreenText").GetComponent<Text> ();
					if (gameOverScreenTextObject != null) 
					{
						gameOverScreenTextObject.text = "GAME OVER";
						instructions.text = "Lasted for\n" + GameManager.Instance.Points.ToString("0" + "\nSeconds");
					}
				} 
				else if (GameManager.Instance._checklanding == true && GameManager.Instance._landingtriggerenter == false) 
				{
					GameOverScreen.SetActive (state);
					Text gameOverScreenTextObject = GameOverScreen.transform.Find ("GameOverScreenText").GetComponent<Text> ();
					if (gameOverScreenTextObject != null) 
					{
						//	Debug.Log ("_checklanding == false   .... true : false");
						gameOverScreenTextObject.text = "game over";
						instructions.text = "You Need to Land Properly";

					}
				}	
				else if (GameManager.Instance._checklanding == false && GameManager.Instance._landingtriggerenter == true) 
				{
					GameOverScreen.SetActive (state);
					Text gameOverScreenTextObject = GameOverScreen.transform.Find ("GameOverScreenText").GetComponent<Text> ();
					if (gameOverScreenTextObject != null) 
					{
						//	Debug.Log ("_checklanding == false   .... false : true");
						gameOverScreenTextObject.text = "game over";
						instructions.text = "You Need to Land Properly";

					}
				}	
				else if (GameManager.Instance._checklanding == false && GameManager.Instance._landingtriggerenter == false) 
				{
					GameOverScreen.SetActive (state);
					Text gameOverScreenTextObject = GameOverScreen.transform.Find ("GameOverScreenText").GetComponent<Text> ();
					if (gameOverScreenTextObject != null) 
					{
						//Debug.Log ("_checklanding == false   .... false : false");
						gameOverScreenTextObject.text = "game over";
						instructions.text = "You Need to Land Properly";
					}
				}	
			}
			// ***********
		}

			
		public virtual void RefreshPoints()
		{
			if (PointsText==null)
				return;

			PointsText.text=GameManager.Instance.Points.ToString("0" + "\nSECONDS");	
		}
		
		public virtual void SetLevelName(string name)
		{
			if (LevelText==null)
				return;

			LevelText.text=name;		
		}
		
		public virtual void FaderOn(bool state,float duration)
		{
			if (Fader==null)
			{
				return;
			}
			Fader.gameObject.SetActive(true);
			if (state)
				StartCoroutine(MMFade.FadeImage(Fader,duration, new Color(0,0,0,1f)));
			else
				StartCoroutine(MMFade.FadeImage(Fader,duration, new Color(0,0,0,0f)));
		}		

		public virtual void FaderTo(Color newColor,float duration)
		{
			if (Fader==null)
			{
				return;
			}
			Fader.gameObject.SetActive(true);
			StartCoroutine(MMFade.FadeImage(Fader,duration, newColor));
		}		

		protected virtual void OnEnable()
		{
			EventManager.StartListening("GameStart",OnGameStart);
		}

		protected virtual void OnDisable()
		{
			EventManager.StopListening("GameStart",OnGameStart);
		}
	}
}