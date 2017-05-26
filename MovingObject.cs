using UnityEngine;
using System.Collections;
using Glide.Tools;

namespace Glide.Scripts
{	
	public class MovingObject : MonoBehaviour 
	{
		public float Speed=0;
	    public float Acceleration = 0;

	    public Vector3 Direction = Vector3.left;
	    
	    protected Vector3 _movement;
	    protected float _initialSpeed;

	    protected virtual void Awake () 
		{
	        _initialSpeed = Speed;
	    }

	    protected virtual void OnEnable()
		{
	        Speed = _initialSpeed;
	    }
		
		// On update(), we move the object based on the level's speed and the object's speed, and apply acceleration
		protected virtual void Update ()
		{
	    	Move();
	    }

	    public virtual void Move()
	    {
			if (LevelManager.Instance==null)
			{
				_movement = Direction * (Speed / 10) * Time.deltaTime;
			}
			else
			{
				_movement = Direction * (Speed / 10) * LevelManager.Instance.Speed * Time.deltaTime;
			}
			transform.Translate(_movement,Space.World);
			// We apply the acceleration to increase the speed
			Speed += Acceleration * Time.deltaTime;
	    }
	}
}