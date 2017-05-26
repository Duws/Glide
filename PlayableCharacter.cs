using UnityEngine;
using System.Collections;
using Glide.Tools;

namespace Glide.Scripts
{	
	public class PlayableCharacter : MonoBehaviour 
	{
	    public bool UseDefaultMecanim=true;	
		public bool ShouldResetPosition = true;
		public float ResetPositionSpeed = 0.5f;	
		//public float DistanceToTheGround {get;protected set;}
		
		protected Vector3 _initialPosition;
		protected bool _grounded;
		protected RigidbodyInterface _rigidbodyInterface;
		protected Animator _animator;
	    protected float _distanceToTheGroundRaycastLength=50f;
		protected float _groundDistanceTolerance=0.05f;
		protected LayerMask _collisionMaskSave;
		
		protected virtual void Awake () 
		{
			Initialize();
		}

		protected virtual void Start()
		{

		}
		
		protected virtual void Initialize()
		{
			_rigidbodyInterface = GetComponent<RigidbodyInterface> ();		
			_animator = GetComponent<Animator>();
			
			if (_rigidbodyInterface == null)
			{
				return;
			}
		}
		
		public virtual void SetInitialPosition(Vector3 initialPosition)
		{
			_initialPosition=initialPosition;	
		}
		
		protected virtual void Update ()
	    {   
	        UpdateAnimator ();

	        ResetPosition();

			CheckDeathConditions ();

			//ComputeDistanceToTheGround();
		}

		/*
		/// <summary>
		/// Determines the distance between the Jumper and the ground
		/// </summary>
		protected virtual void ComputeDistanceToTheGround()
		{
			// we cast a ray to the bottom to check if we're above ground and determine the distance
			Vector2 raycastOrigin = transform.position;	
			RaycastHit2D raycast = MMDebug.RayCast(raycastOrigin,Vector2.down,_distanceToTheGroundRaycastLength,1<<LayerMask.NameToLayer("Ground"),true,Color.gray,true);
			if (raycast)
			{
				DistanceToTheGround = raycast.distance;
	        }
			if (DistanceToTheGround-GetPlayableCharacterBounds().extents.y<_groundDistanceTolerance)
	        {
	        	_grounded=true;
	        }
	        else
	        {
	        	_grounded=false;
	        }
		}*/

		protected virtual void CheckDeathConditions()
		{
			if (LevelManager.Instance.CheckDeathCondition(GetPlayableCharacterBounds()))
			{
				LevelManager.Instance.KillCharacter(this);
			}
		}

		protected virtual Bounds GetPlayableCharacterBounds()
		{
			if (GetComponent<Collider>()!=null)
			{	
				return GetComponent<Collider>().bounds;				
			}

			if (GetComponent<Collider2D>()!=null)
			{	
				return GetComponent<Collider2D>().bounds;
			}

			return GetComponent<Renderer>().bounds;
		} 
		
		protected virtual void UpdateAnimator()
		{
	        if (_animator== null)
	        { return;  }
	
	        if (UseDefaultMecanim)
	        {
				UpdateAllMecanimAnimators();
	        }
	    }
	    
	    protected virtual void UpdateAllMecanimAnimators()
	    {		
			MMAnimator.UpdateAnimatorBool(_animator,"Grounded",_grounded);
			MMAnimator.UpdateAnimatorFloat(_animator, "VerticalSpeed", _rigidbodyInterface.Velocity.y);
	    }

	    protected virtual void ResetPosition()
	    {
	        if (ShouldResetPosition)
	        {
	            if (_grounded)
	            { 
	                _rigidbodyInterface.Velocity = new Vector3((_initialPosition.x - transform.position.x) * (ResetPositionSpeed), _rigidbodyInterface.Velocity.y, _rigidbodyInterface.Velocity.z);
	            }
	        }
	    }

	    public virtual void Disable()
		{
	        gameObject.SetActive(false);
	    }   

	    public virtual void Die()
		{
			Destroy(gameObject);
		}

		public virtual void DisableCollisions()
		{
			_rigidbodyInterface.EnableBoxCollider(false);
		}

		public virtual void EnableCollisions()
		{
			_rigidbodyInterface.EnableBoxCollider(true);
		}
		
		public virtual void MainActionStart() {	}
	    public virtual void MainActionEnd() { }
	    public virtual void MainActionOngoing() { }
	    
		public virtual void DownStart() { }
		public virtual void DownEnd() { }
	    public virtual void DownOngoing() { }

		public virtual void UpStart() { }
		public virtual void UpEnd() { }
	    public virtual void UpOngoing() { }

		public virtual void LeftStart() { }
		public virtual void LeftEnd() { }
	    public virtual void LeftOngoing() { }

		public virtual void RightStart() { }
		public virtual void RightEnd() { }
	    public virtual void RightOngoing() { }		
	    
		protected virtual void OnCollisionEnter2D (Collision2D collidingObject)
		{
			CollisionEnter (collidingObject.collider.gameObject);
		}

		protected virtual void OnCollisionExit2D (Collision2D collidingObject)
		{
			CollisionExit (collidingObject.collider.gameObject);
		}

		protected virtual void OnCollisionEnter (Collision collidingObject)
		{		
			CollisionEnter (collidingObject.collider.gameObject);
		}

	    protected virtual void OnCollisionExit (Collision collidingObject)
		{		
			CollisionExit (collidingObject.collider.gameObject);
		}

		protected virtual void OnTriggerEnter2D (Collider2D collidingObject)
		{
			TriggerEnter (collidingObject.gameObject);
		}
		
		protected virtual void OnTriggerExit2D (Collider2D collidingObject)
		{
			TriggerExit (collidingObject.gameObject);
		}
		
		protected virtual void OnTriggerEnter (Collider collidingObject)
		{
			TriggerEnter (collidingObject.gameObject);
		}
		
		protected virtual void OnTriggerExit (Collider collidingObject)
		{
			TriggerExit (collidingObject.gameObject);
		}
		
		protected virtual void CollisionEnter(GameObject collidingObject)
		{
			
		}
		
		protected virtual void CollisionExit (GameObject collidingObject)
		{
			
		}
		
		protected virtual void TriggerEnter(GameObject collidingObject)
		{
			
		}
		
		protected virtual void TriggerExit (GameObject collidingObject)
		{
			
		}
	}
}