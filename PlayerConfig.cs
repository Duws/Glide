using UnityEngine;
using System.Collections;
using Glide.Tools;

namespace Glide.Scripts
{	
	public class PlayerConfig : PlayerConfig_Extension 
	{
		public GameObject ActualRocket;
		public ParticleSystem ReactorLeft;
		public ParticleSystem ReactorRight;
	    public GameObject Explosion;
	    public Vector3 DeathShakeVector = new Vector3(2f,2f,2f);
		public AudioClip DeathSoundFx;

		protected float _horizontalRotationSpeed=5f;
		protected float _verticalRotationSpeed=2f;
		protected Vector3 _newAngle = Vector3.zero;
		protected float _horizontalRotationTarget;
		protected bool _barrelRolling=false;

		protected override void Start()
		{
			if (ActualRocket!=null)
			{
				_newAngle=ActualRocket.transform.localEulerAngles;
			}
			_animator = ActualRocket.GetComponent<Animator>();
		}

		protected override void Update()
		{
			base.Update();
	        HandleOrientation();
	        HandleReactors();
		}

		public override void MainActionStart()
		{
			if (GameManager.Instance.Status==GameManager.GameStatus.GameInProgress)
			{
				base.MainActionStart();
			}
			if (GameManager.Instance.Status==GameManager.GameStatus.GameOver)
			{

			}
			if (GameManager.Instance.Status==GameManager.GameStatus.BeforeGameStart) 
			{
				LevelManager.Instance.LevelStart();
				base.MainActionStart();

			}
		}

		protected virtual void HandleOrientation()
		{
			float climbingAdjuster = 0.8f;
			if (_rigidbodyInterface.Velocity.y > 0)
			{
				climbingAdjuster = 0.8f;
			}
			Vector3 target = new Vector3(transform.position.x + 5f,transform.position.y + (_rigidbodyInterface.Velocity.y/3f)*climbingAdjuster, transform.position.z);
			Debug.DrawLine(transform.position,target);
			Vector3 vectorToTarget = target - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _verticalRotationSpeed);

			_horizontalRotationTarget = 0f;

			_horizontalRotationTarget += _rigidbodyInterface.Velocity.y * 3f;

			// if we're barrelRolling, we apply a modifier to the rotation target
			/*if (_barrelRolling)
			{
				_horizontalRotationTarget += 360f;
			}*/

			_newAngle.x=Mathf.Lerp(_newAngle.x,_horizontalRotationTarget,Time.deltaTime * _horizontalRotationSpeed );
			ActualRocket.transform.localEulerAngles=_newAngle;

		}

		protected virtual void HandleReactors()
		{
			if (_boosting)
			{
				ReactorLeft.startSize=2f;
				ReactorRight.startSize=2f;

			}
			else if (GameManager.Instance._checklanding == true)
			{
				ReactorLeft.startSize=0.1f;
				ReactorRight.startSize=0.1f;
			}

			else
			{
				ReactorLeft.startSize=1f;	
				ReactorRight.startSize=1f;		
			}
		}

		protected virtual IEnumerator BarrelRoll(float delay)
		{
			float time=0f;
			float barrelRollDuration=2.3f;

			yield return new WaitForSeconds(delay);

			while (time < barrelRollDuration)
			{
				_barrelRolling=true;
				time += Time.deltaTime;
				yield return null;
			}
			_barrelRolling=false;
		}

		public override void Die()
		{
			GameObject explosion = (GameObject)Instantiate(Explosion);
	        explosion.transform.position = transform.position;

			if (SoundManager.Instance!=null && DeathSoundFx!=null)
			{
				SoundManager.Instance.PlaySound(DeathSoundFx,transform.position);	
			}

	        if (Camera.main.GetComponent<CameraBehavior>() != null)
	        {
				Camera.main.GetComponent<CameraBehavior>().Shake(DeathShakeVector);
	        }

			#if UNITY_ANDROID || UNITY_IPHONE
				Handheld.Vibrate();
			#endif
	        base.Die();
		}

		protected override void TriggerEnter(GameObject collidingObject)
		{
			if (!_barrelRolling)
			{
				StartCoroutine(BarrelRoll(1f));
			}
		}
	}
}
