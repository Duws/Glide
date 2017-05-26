using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Glide.Tools;
using UnityEngine.SceneManagement;

namespace Glide.Scripts
{	
	public class LoadingSceneManager : MonoBehaviour 
	{
		[Header("Binding")]
		public static string LoadingScreenSceneName="LoadingScreen";

		[Header("GameObjects")]
		public Text LoadingText;
		public CanvasGroup LoadingProgressBar;
		public CanvasGroup LoadingAnimation;
		public CanvasGroup LoadingCompleteAnimation;

		[Header("Time")]
		public float StartFadeDuration=0.2f;
		public float ProgressBarSpeed=2f;
		public float ExitFadeDuration=0.2f;
		public float LoadCompleteDelay=0.5f;

		protected AsyncOperation _asyncOperation;
		protected static string _sceneToLoad = "";
		protected float _fadeDuration = 0.5f;
		protected float _fillTarget=0f;

		public static void LoadScene(string sceneToLoad) 
		{		
			_sceneToLoad = sceneToLoad;					
			Application.backgroundLoadingPriority = ThreadPriority.High;
			if (LoadingScreenSceneName!=null)
			{
				SceneManager.LoadScene(LoadingScreenSceneName);
			}
		}
		
		protected virtual void Start() 
		{
			if (_sceneToLoad != "")
			{
				StartCoroutine(LoadAsynchronously());
			}
		}

		protected virtual void Update()
		{
			LoadingProgressBar.GetComponent<Image>().fillAmount = MMMaths.Approach(LoadingProgressBar.GetComponent<Image>().fillAmount,_fillTarget,Time.deltaTime*ProgressBarSpeed);
		}

		protected virtual IEnumerator LoadAsynchronously() 
		{
			// we setup our various visual elements
			LoadingSetup();

			// we start loading the scene
			_asyncOperation = SceneManager.LoadSceneAsync(_sceneToLoad,LoadSceneMode.Single );
			_asyncOperation.allowSceneActivation = false;

			// while the scene loads, we assign its progress to a target that we'll use to fill the progress bar smoothly
			while (_asyncOperation.progress < 0.9f) 
			{
				_fillTarget = _asyncOperation.progress;
				yield return null;
			}
			// when the load is close to the end (it'll never reach it), we set it to 100%
			_fillTarget = 1f;

			// we wait for the bar to be visually filled to continue
			while (LoadingProgressBar.GetComponent<Image>().fillAmount != _fillTarget)
			{
				yield return null;
			}

			// the load is now complete, we replace the bar with the complete animation
			LoadingComplete();
			yield return new WaitForSeconds(LoadCompleteDelay);

			// we fade to black
			GUIManager.Instance.FaderOn(true,ExitFadeDuration);
			yield return new WaitForSeconds(ExitFadeDuration);

			// we switch to the new scene
			_asyncOperation.allowSceneActivation = true;
		}

		protected virtual void LoadingSetup() 
		{
			GUIManager.Instance.Fader.gameObject.SetActive(true);
			GUIManager.Instance.Fader.GetComponent<Image>().color=new Color(0,0,0,1f);
			GUIManager.Instance.FaderOn(false,ExitFadeDuration);

			LoadingCompleteAnimation.alpha=0;
			LoadingProgressBar.GetComponent<Image>().fillAmount = 0f;
			LoadingText.text = "loading";

		}

		protected virtual void LoadingComplete() 
		{
			LoadingCompleteAnimation.gameObject.SetActive(true);
			StartCoroutine(MMFade.FadeCanvasGroup(LoadingProgressBar,0.1f,0f));
			StartCoroutine(MMFade.FadeCanvasGroup(LoadingAnimation,0.1f,0f));
			StartCoroutine(MMFade.FadeCanvasGroup(LoadingCompleteAnimation,0.1f,1f));

		}
	}
}