using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Glide.Tools;

namespace Glide.Scripts
{	
	public class Instructions : MonoBehaviour 
	{
		public Text InstructionsText;
		public Image InstructionsPanel;
		public int Duration;
		public float FadeDuration;
		
		protected virtual void Start () 
		{
			if (LevelManager.Instance!=null)
			{
				if (LevelManager.Instance.InstructionsText!="")
				{
					InstructionsText.text = LevelManager.Instance.InstructionsText;
					Invoke ("Disappear",Duration);
				}
				else
				{
					DestroyInstructions();		
				}
			}
			else
			{
				DestroyInstructions();
			}		
		}
		
		protected virtual void Disappear () 
		{
			Color newColor=new Color(0,0,0,0);
			StartCoroutine(MMFade.FadeImage(InstructionsPanel, FadeDuration,newColor));
			StartCoroutine(MMFade.FadeText(InstructionsText,FadeDuration,newColor));
			Invoke ("DestroyInstructions",FadeDuration);
		}
		
		protected virtual void DestroyInstructions()
		{
			Destroy(gameObject);
		}
	}
}