using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Glide.Tools;

namespace Glide.Scripts
{	
	public class TimedSpawner : Spawner 
	{
	    [Space(10)]
	    [Header("Spawn Timing")]
	    public float MinSpawnTime = 0.5f;
		public float MaxSpawnTime = 2f;

	    [Space(10)]
	    [Header("Position")]
	    public Vector3 MinPosition;
	    public Vector3 MaxPosition;

	    protected virtual void Start () 
		{
			_objectPooler = GetComponent<ObjectPooler> ();
			Invoke("TimeSpawn",MinSpawnTime);
		}

	    protected virtual void TimeSpawn () 
		{ 
	        Vector3 spawnPosition = transform.position;
	        spawnPosition.x += Random.Range(MinPosition.x, MaxPosition.x);
	        spawnPosition.y += Random.Range(MinPosition.y, MaxPosition.y);
	        spawnPosition.z += Random.Range(MinPosition.z, MaxPosition.z);
	        
	        Spawn (spawnPosition);
			
			float randomTime = Random.Range(MinSpawnTime,MaxSpawnTime);		
			Invoke("TimeSpawn",randomTime);
			
		}
	}
}