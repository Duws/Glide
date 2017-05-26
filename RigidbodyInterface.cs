using UnityEngine;
using System.Collections;

namespace Glide.Tools
{	
	public class RigidbodyInterface : MonoBehaviour 
	{	
	    public Vector3 position
	    {
	        get
	        {
	            if (_rigidbody2D != null)
	            {
	                return _rigidbody2D.position;
	            }
	            if (_rigidbody != null)
	            {
	                return _rigidbody.position;
	            }
	            return Vector3.zero;
	        }
	        set { }
	    }

		protected virtual void Awake () 
		{
			_rigidbody2D=GetComponent<Rigidbody2D>();
			_rigidbody=GetComponent<Rigidbody>();
			
			if (_rigidbody2D != null) 
			{
				_mode="2D";
			}
			if (_rigidbody != null) 
			{
				_mode="3D";
			}
			if (_rigidbody==null && _rigidbody2D==null)
			{
				Debug.LogWarning("A RigidBodyInterface has been added to "+gameObject+" but there's no Rigidbody or Rigidbody2D on it.", gameObject);
			}
		}

		public Vector3 Velocity 
		{
			get 
			{ 
				if (_mode == "2D") 
				{
					return(_rigidbody2D.velocity);
				}
				else 
				{
					if (_mode == "3D") 
					{
						return(_rigidbody.velocity);
					}
					else
					{
						return new Vector3(0,0,0);
					}
				}
			}
			set 
			{
				if (_mode == "2D") {
					_rigidbody2D.velocity = value;
				}
				if (_mode == "3D") {
					_rigidbody.velocity = value;
				}
			}
		}

		protected string _mode;
		protected Rigidbody2D _rigidbody2D;
		protected Rigidbody _rigidbody;
		
		public virtual void AddForce(Vector3 force)
		{
			if (_mode == "2D") 
			{
				_rigidbody2D.AddForce(force,ForceMode2D.Impulse);
			}
			if (_mode == "3D")
			{
				_rigidbody.AddForce(force);
			}
		}

	    public virtual void MovePosition(Vector3 newPosition)
	    {
	        if (_mode == "2D")
	        {
	            _rigidbody2D.MovePosition(newPosition);
	        }
	        if (_mode == "3D")
	        {
	            _rigidbody.MovePosition(newPosition);
	        }
	    }
			
		public virtual void IsKinematic(bool status)
		{
			if (_mode == "2D") 
			{
				GetComponent<Rigidbody2D>().isKinematic=status;
			}
			if (_mode == "3D")
			{			
				GetComponent<Rigidbody>().isKinematic=status;
			}
		}
		
		public virtual void EnableBoxCollider(bool status)
		{
			if (_mode == "2D") 
			{
				GetComponent<Collider2D>().enabled=status;
			}
			if (_mode == "3D")
			{			
				GetComponent<Collider>().enabled=status;
			}
		}
	}
}
