using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Glide.Tools;

namespace Glide.Scripts
{	
	public class LevelSelector : MonoBehaviour
	{
	    public string LevelName;
		
	    public virtual void GoToLevel()
	    {
	        LevelManager.Instance.GotoLevel(LevelName);
	    }

	    public virtual void RestartLevel()
	    {
			LevelManager.Instance.GotoLevel(SceneManager.GetActiveScene().name);
	    }

	    public virtual void Resume()
	    {
	        GameManager.Instance.UnPause();
	    }
	}
}
