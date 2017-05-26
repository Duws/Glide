using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Glide.Tools;

namespace Glide.Scripts
{	
	public class DistanceSpawner : Spawner 
	{
		[Header("Gap between objects")]
		public Vector3 MinimumGap = new Vector3(1,1,1);
		public Vector3 MaximumGap = new Vector3(1,1,1);
		[Space(10)]	
		[Header("Y Clamp")]
		public float MinimumYClamp;
		public float MaximumYClamp;
		[Header("Z Clamp")]
		public float MinimumZClamp;
		public float MaximumZClamp;
		[Space(10)]
		[Header("Spawn angle")]
		public bool SpawnRotatedToDirection=true;

	    protected Transform _lastSpawnedTransform;
		protected float _nextSpawnDistance;

	    protected virtual void Start () 
		{
			_objectPooler = GetComponent<ObjectPooler> ();	
		}

	    protected virtual void Update () 
		{
			CheckSpawn();
		}

		protected virtual void CheckSpawn()
		{
	        if (OnlySpawnWhileGameInProgress)
	        {
	            if (GameManager.Instance.Status != GameManager.GameStatus.GameInProgress)
	            {
	                _lastSpawnedTransform = null;
	                return ;
	            }
	        }

	        if ((_lastSpawnedTransform== null) || (!_lastSpawnedTransform.gameObject.activeInHierarchy))
	        {
				DistanceSpawn(transform.position + MMMaths.RandomVector3(MinimumGap,MaximumGap));	
	            return;
	        }

			if (transform.InverseTransformPoint(_lastSpawnedTransform.position).x < -_nextSpawnDistance )
			{
				Vector3 spawnPosition = transform.position;		
				DistanceSpawn(spawnPosition);	
			}
		}
		
		protected virtual void DistanceSpawn(Vector3 spawnPosition)
		{
			GameObject spawnedObject = Spawn(spawnPosition,false);

			if (spawnedObject==null)
			{
				_lastSpawnedTransform=null;
				_nextSpawnDistance = UnityEngine.Random.Range(MinimumGap.x, MaximumGap.x) ;
				return;
			}

			if (spawnedObject.GetComponent<PoolableObject>()==null)
			{
				throw new Exception(gameObject.name+" is trying to spawn objects that don't have a PoolableObject component.");					
			}

			if (SpawnRotatedToDirection)
			{
				spawnedObject.transform.rotation *= transform.rotation;
			}
			
			if (spawnedObject.GetComponent<MovingObject>()!=null)
			{
				spawnedObject.GetComponent<MovingObject>().Direction=transform.rotation*Vector3.left;
			}

			if (_lastSpawnedTransform!=null)
			{
				spawnedObject.transform.position = transform.position;

				float xDistanceToLastSpawnedObject = transform.InverseTransformPoint(_lastSpawnedTransform.position).x;

				spawnedObject.transform.position += transform.rotation
													* Vector3.right
													* (xDistanceToLastSpawnedObject 
													+ _lastSpawnedTransform.GetComponent<PoolableObject>().Size.x/2 
													+ spawnedObject.GetComponent<PoolableObject>().Size.x/2) ;

				spawnedObject.transform.position += (transform.rotation * ClampedPosition(MMMaths.RandomVector3(MinimumGap,MaximumGap)/2));

				if (spawnedObject.GetComponent<MovingObject>()!=null)
				{
					spawnedObject.GetComponent<MovingObject>().Move();
				}
			}

			spawnedObject.GetComponent<PoolableObject>().TriggerOnSpawnComplete();

			_nextSpawnDistance = spawnedObject.GetComponent<PoolableObject>().Size.x/2 ;
			_lastSpawnedTransform = spawnedObject.transform;
			
		}

		protected virtual Vector3 ClampedPosition(Vector3 vectorToClamp)
		{
			vectorToClamp.y = Mathf.Clamp (vectorToClamp.y, MinimumYClamp, MaximumYClamp);
			vectorToClamp.z = Mathf.Clamp (vectorToClamp.z, MinimumZClamp, MaximumZClamp);
			return vectorToClamp;
		}

	    protected virtual void OnDrawGizmosSelected()
	    {
	        DrawClamps();

			GUIStyle style = new GUIStyle();

			Gizmos.matrix = transform.localToWorldMatrix;

	        Gizmos.color = Color.yellow;
	        Gizmos.DrawWireCube(Vector3.zero, MinimumGap);

	        Gizmos.color = Color.red;
			Gizmos.DrawWireCube(Vector3.zero, MaximumGap);

			Gizmos.matrix=Matrix4x4.identity;

			if (MinimumGap!=Vector3.zero)
			{
		        style.normal.textColor = Color.yellow;		 
				Vector3 labelPosition = transform.position + (Mathf.Abs(MinimumGap.y/2)+1) * Vector3.up + Vector3.left;
				labelPosition = MMMaths.RotatePointAroundPivot(labelPosition,transform.position,transform.rotation.eulerAngles);
				#if UNITY_EDITOR
					UnityEditor.Handles.Label(labelPosition, "Minimum Gap", style);
				#endif
			}

			if (MaximumGap!=Vector3.zero)
			{
				style.normal.textColor = Color.red;		 
				Vector3 labelPosition = transform.position + (-Mathf.Abs(MaximumGap.y/2)+1) * Vector3.up + Vector3.left;
				labelPosition = MMMaths.RotatePointAroundPivot(labelPosition,transform.position,transform.rotation.eulerAngles);
				#if UNITY_EDITOR
					UnityEditor.Handles.Label(labelPosition, "Maximum Gap", style);
				#endif
			}
			MMDebug.GizmosDrawArrow(transform.position,transform.rotation*Vector3.left*10,Color.green);
	    }

	    protected virtual void DrawClamps()
	    {
			GUIStyle style = new GUIStyle();
			if (MinimumYClamp!=MaximumYClamp)
			{
				style.normal.textColor = Color.cyan;	
				Vector3 labelPosition = transform.position + (Mathf.Abs(MaximumYClamp)+1) * Vector3.up + Vector3.left;
				labelPosition = MMMaths.RotatePointAroundPivot(labelPosition,transform.position,transform.rotation.eulerAngles);	 
				#if UNITY_EDITOR
				UnityEditor.Handles.Label(labelPosition, "Clamp", style);
				#endif
			}

			float xMinus5 = transform.position.x - 5;
			float xPlus5 = transform.position.x + 5;

			float minimumYClamp = MinimumYClamp + transform.position.y;
			float maximumYClamp = MaximumYClamp + transform.position.y;
			float minimumZClamp = MinimumZClamp + transform.position.z;
			float maximumZClamp = MaximumZClamp + transform.position.z;

			Gizmos.color = Color.cyan;

			Gizmos.DrawLine(MMMaths.RotatePointAroundPivot(new Vector3(xMinus5, minimumYClamp, minimumZClamp),transform.position,transform.rotation.eulerAngles),
				MMMaths.RotatePointAroundPivot(new Vector3(xPlus5, minimumYClamp, minimumZClamp),transform.position,transform.rotation.eulerAngles));

			Gizmos.DrawLine(MMMaths.RotatePointAroundPivot(new Vector3(xMinus5, maximumYClamp, minimumZClamp),transform.position,transform.rotation.eulerAngles),
				MMMaths.RotatePointAroundPivot(new Vector3(xPlus5, maximumYClamp, minimumZClamp),transform.position,transform.rotation.eulerAngles));

			Gizmos.DrawLine(MMMaths.RotatePointAroundPivot(new Vector3(xMinus5, minimumYClamp, maximumZClamp),transform.position,transform.rotation.eulerAngles),
				MMMaths.RotatePointAroundPivot(new Vector3(xPlus5, minimumYClamp, maximumZClamp),transform.position,transform.rotation.eulerAngles));

			Gizmos.DrawLine(MMMaths.RotatePointAroundPivot(new Vector3(xMinus5, maximumYClamp, maximumZClamp),transform.position,transform.rotation.eulerAngles),
				MMMaths.RotatePointAroundPivot(new Vector3(xPlus5, maximumYClamp, maximumZClamp),transform.position,transform.rotation.eulerAngles));

			Gizmos.DrawLine(MMMaths.RotatePointAroundPivot(new Vector3(xMinus5, maximumYClamp, minimumZClamp),transform.position,transform.rotation.eulerAngles),
				MMMaths.RotatePointAroundPivot(new Vector3(xPlus5, maximumYClamp, maximumZClamp),transform.position,transform.rotation.eulerAngles));

			Gizmos.DrawLine(MMMaths.RotatePointAroundPivot(new Vector3(xMinus5, maximumYClamp, maximumZClamp),transform.position,transform.rotation.eulerAngles),
				MMMaths.RotatePointAroundPivot(new Vector3(xPlus5, maximumYClamp, minimumZClamp),transform.position,transform.rotation.eulerAngles));

			Gizmos.DrawLine(MMMaths.RotatePointAroundPivot(new Vector3(xMinus5, minimumYClamp, minimumZClamp),transform.position,transform.rotation.eulerAngles),
				MMMaths.RotatePointAroundPivot(new Vector3(xPlus5, minimumYClamp, maximumZClamp),transform.position,transform.rotation.eulerAngles));

			Gizmos.DrawLine(MMMaths.RotatePointAroundPivot(new Vector3(xMinus5, minimumYClamp, maximumZClamp),transform.position,transform.rotation.eulerAngles),
				MMMaths.RotatePointAroundPivot(new Vector3(xPlus5, minimumYClamp, minimumZClamp),transform.position,transform.rotation.eulerAngles));

			Gizmos.DrawLine(MMMaths.RotatePointAroundPivot(new Vector3(xPlus5, maximumYClamp, minimumZClamp),transform.position,transform.rotation.eulerAngles),
				MMMaths.RotatePointAroundPivot(new Vector3(xPlus5, maximumYClamp, maximumZClamp),transform.position,transform.rotation.eulerAngles));

			Gizmos.DrawLine(MMMaths.RotatePointAroundPivot(new Vector3(xMinus5, maximumYClamp, maximumZClamp),transform.position,transform.rotation.eulerAngles),
				MMMaths.RotatePointAroundPivot(new Vector3(xMinus5, maximumYClamp, minimumZClamp),transform.position,transform.rotation.eulerAngles));

			Gizmos.DrawLine(MMMaths.RotatePointAroundPivot(new Vector3(xPlus5, minimumYClamp, minimumZClamp),transform.position,transform.rotation.eulerAngles),
				MMMaths.RotatePointAroundPivot(new Vector3(xPlus5, minimumYClamp, maximumZClamp),transform.position,transform.rotation.eulerAngles));

			Gizmos.DrawLine(MMMaths.RotatePointAroundPivot(new Vector3(xMinus5, minimumYClamp, maximumZClamp),transform.position,transform.rotation.eulerAngles),
				MMMaths.RotatePointAroundPivot(new Vector3(xMinus5, minimumYClamp, minimumZClamp),transform.position,transform.rotation.eulerAngles));
	    }		
	}
}