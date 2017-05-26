using UnityEngine;
using System.Collections;
using Glide.Tools;

namespace Glide.Scripts
{	
	[RequireComponent (typeof (PoolableObject))]
	
	public class OutOfBoundsRecycle : MonoBehaviour 
	{
		public float DestroyDistanceBehindBounds=5f;

	    protected virtual void Update () 
		{
			if (LevelManager.Instance.CheckRecycleCondition(GetComponent<PoolableObject>().GetBounds(),DestroyDistanceBehindBounds))
			{
				GetComponent<PoolableObject>().Destroy();
			}
		}
	}
}