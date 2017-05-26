using UnityEngine;
using System.Collections;
using Glide.Tools;

namespace Glide.Scripts
{	
	public class PlayerConfig_Extension : PlayableCharacter 
	{
		public float FlyForce = 20f;
		public float MaximumVelocity = 5f;

		protected bool _boosting=false;

		protected override void Update ()
		{
			//ComputeDistanceToTheGround();
			UpdateAnimator ();		
			ResetPosition();
	        CheckDeathConditions ();

 			Fly();
		}

		protected virtual void FixedUpdate()
		{
			if(_rigidbodyInterface.Velocity.magnitude > MaximumVelocity)
	         {
					_rigidbodyInterface.Velocity = _rigidbodyInterface.Velocity.normalized * MaximumVelocity;
	         }
		}

		public override void MainActionStart()
		{
			_boosting=true;
		}

		public override void MainActionEnd()
		{
			_boosting=false;
		}

		public virtual void Fly()
		{
			if (GameManager.Instance._checklanding == true) 
			{
				// character land if landing button is pressed 
				FlyForce = 0f;
				_boosting = false;
				_rigidbodyInterface.AddForce (Vector3.down * FlyForce * Time.deltaTime);
			}
			else if (GameManager.Instance.accel == true && GameManager.Instance.checkbuttonaccel == true)
			{
				// if bool accel is true fire function
				// character jump if X azix acceleration is triggered more than -0.3f
				_rigidbodyInterface.AddForce(Vector3.up * 40 * Time.deltaTime );
			}
			else if (GameManager.Instance.accel == false && GameManager.Instance.checkbuttonaccel == true)
			{
				// if X azix acceleration is triggered more greater than -0.4f return to default
				_boosting = false;
				_rigidbodyInterface.AddForce(Vector3.down * 5 * Time.deltaTime );
			}
			else if (_boosting && GameManager.Instance.checkbuttonaccel == false)
			{
				// character jump if main action button is pressed
				_rigidbodyInterface.AddForce(Vector3.up * FlyForce * Time.deltaTime );

			}
		}
	}
}