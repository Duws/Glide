using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Glide.Tools
{	
	public class MultipleObjectPooler : ObjectPooler
	{
		public GameObject[] GameObjectsToPool;	
		public int[] PoolSize;
		public bool PoolCanExpand = true;

	    protected GameObject _waitingPool;
		protected List<GameObject> _pooledGameObjects;
	    
	    protected override void FillObjectPool()
	    {
	        _waitingPool = new GameObject("[MultipleObjectPooler] " + this.name);
			_pooledGameObjects = new List<GameObject>();

	        int i = 0;
			foreach (GameObject pooledGameObject in GameObjectsToPool)
	        {
				if (i > PoolSize.Length) { return; }

	            for (int j = 0; j < PoolSize[i]; j++)
	            {
					AddOneObjectToThePool(pooledGameObject);
				}
				i++;
	        }
	    }
	    
		protected virtual GameObject AddOneObjectToThePool(GameObject typeOfObject)
		{
			GameObject newGameObject = (GameObject)Instantiate(typeOfObject);
			newGameObject.gameObject.SetActive(false);
			newGameObject.transform.parent = _waitingPool.transform;
			newGameObject.name=typeOfObject.name+"-"+_pooledGameObjects.Count;
			_pooledGameObjects.Add(newGameObject);	
			return newGameObject;
		}
		
		public override GameObject GetPooledGameObject()
		{
			int randomIndex = Random.Range(0, _pooledGameObjects.Count);
			int preventOverflowCounter=0;
					
			while ( (_pooledGameObjects[randomIndex].gameObject.activeInHierarchy) && (preventOverflowCounter<_pooledGameObjects.Count) )
			{
				randomIndex = Random.Range(0, _pooledGameObjects.Count);
				preventOverflowCounter++;
			}
			
			if (_pooledGameObjects[randomIndex].gameObject.activeInHierarchy)
			{	
				if (PoolCanExpand)
				{
					randomIndex = Random.Range(0, GameObjectsToPool.Length);
					return AddOneObjectToThePool(GameObjectsToPool[randomIndex]);						 	
				}
				else
				{
					return null;
				}
			}
			else
			{			
				return _pooledGameObjects[randomIndex];   
			}
			
			 
		}
		
		public virtual GameObject GetPooledGameObjectOfType(string type)
	    {
			GameObject correspondingGameObject=null;

			for (int i = 0; i < _pooledGameObjects.Count; i++)
	        {
				if (_pooledGameObjects[i].name == type)
	            {
					if (!_pooledGameObjects[i].gameObject.activeInHierarchy)
	                {
						return _pooledGameObjects[i];
					}
					else
					{
						correspondingGameObject = _pooledGameObjects[i];
					}
	            }            
	        }
			
	        if (PoolCanExpand && correspondingGameObject!=null)
	        {
				GameObject newGameObject = (GameObject)Instantiate(correspondingGameObject);
	            _pooledGameObjects.Add(newGameObject);
	            return newGameObject;
	        }

	        return null;
	    }
	}
}