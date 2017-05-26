using UnityEngine;
using System.Collections;
using System;
using Glide.Tools;

namespace Glide.Scripts
{	
	[RequireComponent (typeof (ObjectPooler))]
	public class Spawner : MonoBehaviour 
	{
		[Header("Size")]
		public Vector3 MinimumSize=new Vector3(1,1,1) ;
		public Vector3 MaximumSize=new Vector3(1,1,1) ;	
		public bool PreserveRatio=false;
		[Space(10)]	
		[Header("Rotation")]
		public Vector3 MinimumRotation ;
		public Vector3 MaximumRotation ;
		[Space(10)]	
		[Header("When can it spawn?")]
		public bool Spawning=true;
	    public bool OnlySpawnWhileGameInProgress = true;
	    public float InitialDelay=0f;

	    protected ObjectPooler _objectPooler;
	    protected float _startTime;

	    protected virtual void Awake()
	    {
	        _objectPooler = GetComponent<ObjectPooler>();
			_startTime=Time.time;
	    }
			
		public virtual GameObject Spawn(Vector3 spawnPosition,bool triggerObjectActivation=true)
		{
	        if (OnlySpawnWhileGameInProgress)
	        {
	            if (GameManager.Instance.Status!=GameManager.GameStatus.GameInProgress)
	            {
	                return null;
	            }
	        }
	        if ((Time.time - _startTime < InitialDelay) || (!Spawning))
	        {
	        	return null;
	        }

	        GameObject nextGameObject = _objectPooler.GetPooledGameObject();
			if (nextGameObject==null)	{ return null; }

			Vector3 newScale;
	        if (!PreserveRatio)
	        {
		        newScale = new Vector3 (UnityEngine.Random.Range (MinimumSize.x, MaximumSize.x), UnityEngine.Random.Range (MinimumSize.y, MaximumSize.y), UnityEngine.Random.Range (MinimumSize.z, MaximumSize.z));
			}
			else
			{
				newScale = Vector3.one * UnityEngine.Random.Range (MinimumSize.x, MaximumSize.x);
			}
			nextGameObject.transform.localScale = newScale;		
			
			if (nextGameObject.GetComponent<PoolableObject>()==null)
			{
				throw new Exception(gameObject.name+" is trying to spawn objects that don't have a PoolableObject component.");					
			}

			nextGameObject.transform.position =spawnPosition;
			
			nextGameObject.transform.eulerAngles = new Vector3 (
				UnityEngine.Random.Range (MinimumRotation.x, MaximumRotation.x), 
				UnityEngine.Random.Range (MinimumRotation.y, MaximumRotation.y), 
				UnityEngine.Random.Range (MinimumRotation.z, MaximumRotation.z)
				);

	        nextGameObject.gameObject.SetActive(true);

			if (triggerObjectActivation)
			{
				if (nextGameObject.GetComponent<PoolableObject>()!=null)
				{
					nextGameObject.GetComponent<PoolableObject>().TriggerOnSpawnComplete();
				}
			}

	        return (nextGameObject);
	    }		
	}
}