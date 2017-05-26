using UnityEngine;
using System.Collections;

namespace Glide.Scripts
{
	public class CameraBehavior : MonoBehaviour
	{
	    [Header("Zoom level and position")]
		public Vector3 MinimumZoom;
		public float MinimumZoomOrthographic=3f;
		public Vector3 MaximumZoom;
		public float MaximumZoomOrthographic=5f;
	    [Space(10)]
	    [Header("Following the player")]
	    public bool FollowsPlayerX=false;
	    public bool FollowsPlayerY = true;
	    bool FollowsPlayerZ = false;
	    public float CameraSpeed;

	    [Space(10)]
	    [Header("Bounds")]
	    public bool CameraCanOnlyMoveWithinBounds = true; 
		public Vector2 BottomLeftBounds;
	    public Vector2 TopRightBounds;

	    protected Vector3 _initialPosition;
		protected float _initialSize;
		protected Camera _camera;
	    protected Vector3 _playerPosition;

	    protected Vector3 _currentZoomOffset;

	    protected float _xMin, _xMax, _yMin, _yMax;
	    protected Vector3 _newCameraPosition;

	    protected Vector2 _topRightBounds;
	    protected Vector2 _bottomLeftBounds;

	    protected Vector2 _boundsTopRight;
	    protected Vector2 _boundsBottomRight;
	    protected Vector2 _boundsBottomLeft;
	    protected Vector2 _boundsTopLeft;

	    protected float _shakeIntensity;
	    protected float _shakeDecay;
	    protected float _shakeDuration;

	    protected virtual void Start () 
		{
			Initialize();
		}
		
		protected virtual void Initialize()
		{
			_camera=GetComponent<Camera>();
			_initialPosition = transform.position;
			_initialSize=MinimumZoomOrthographic;		
	        _playerPosition = LevelManager.Instance.StartingPosition.transform.position;

	        _camera.orthographicSize = _initialSize;
			_camera.transform.position = MinimumZoom;        
	    }

	    protected virtual void Update()
		{
			if (GameManager.Instance!=null)
			{
				if (GameManager.Instance.Status==GameManager.GameStatus.Paused)
				{
					return;
				}
			}

	        if (LevelManager.Instance.CurrentPlayableCharacters.Count>0)
	        { 
	           _playerPosition = LevelManager.Instance.CurrentPlayableCharacters[0].transform.position;
	        }
	        if (!FollowsPlayerX) { _playerPosition.x = 0; }
	        if (!FollowsPlayerY) { _playerPosition.y = 0; }
	        if (!FollowsPlayerZ) { _playerPosition.z = 0; }

	        _camera.orthographicSize = MinimumZoomOrthographic + (MaximumZoomOrthographic-MinimumZoomOrthographic) * (LevelManager.Instance.Speed / LevelManager.Instance.MaximumSpeed);
			_currentZoomOffset = MinimumZoom + (MaximumZoom - MinimumZoom) * (LevelManager.Instance.Speed / LevelManager.Instance.MaximumSpeed);

	        GetLevelBounds();
			_newCameraPosition = Vector3.Lerp(transform.position, _currentZoomOffset + _playerPosition, CameraSpeed*Time.deltaTime);
	        
			float posX = _newCameraPosition.x;
			float posY = _newCameraPosition.y;
			float posZ = _newCameraPosition.z;
	        if (CameraCanOnlyMoveWithinBounds)
	        {
		        posX = Mathf.Clamp(_newCameraPosition.x, _xMin, _xMax);
	    	    posY = Mathf.Clamp(_newCameraPosition.y, _yMin, _yMax);
			}
			
	        Vector3 shakeFactorPosition = Vector3.zero;
	        if (_shakeDuration > 0)
	        {
	            shakeFactorPosition = Random.insideUnitSphere * _shakeIntensity * _shakeDuration;
	            _shakeDuration -= _shakeDecay * Time.deltaTime;
	        }
	        
	        _camera.transform.position = new Vector3(posX,posY,posZ) + shakeFactorPosition;
		}

		public virtual void Shake(Vector3 shakeParameters)
	    {
	        _shakeIntensity = shakeParameters.x;
	        _shakeDuration = shakeParameters.y;
	        _shakeDecay = shakeParameters.z;
	    }

	    public virtual void ResetCamera()
		{
			transform.position = _initialPosition;
			_camera.orthographicSize=_initialSize;
		}

	    protected virtual void GetLevelBounds()
	    {
	        float cameraHeight = Camera.main.orthographicSize * 2f;
	        float cameraWidth = cameraHeight * Camera.main.aspect;
	        _xMin = BottomLeftBounds.x + (cameraWidth / 2);
	        _xMax = TopRightBounds.x - (cameraWidth / 2);
	        _yMin = BottomLeftBounds.y + (cameraHeight / 2);
	        _yMax = TopRightBounds.y - (cameraHeight / 2);
	    }

	    protected virtual void OnDrawGizmosSelected()
	    {
	       // _boundsTopRight = new Vector2(TopRightBounds.x, TopRightBounds.y);
	       // _boundsBottomRight = new Vector2(TopRightBounds.x, BottomLeftBounds.y);
	       // _boundsBottomLeft = new Vector2(BottomLeftBounds.x, BottomLeftBounds.y);
	       // _boundsTopLeft = new Vector2(BottomLeftBounds.x, TopRightBounds.y);
	        
	      //  Gizmos.color = Color.blue;
	      //  Gizmos.DrawLine(_boundsTopRight, _boundsBottomRight);
	      //  Gizmos.DrawLine(_boundsBottomRight, _boundsBottomLeft);
	      //  Gizmos.DrawLine(_boundsBottomLeft, _boundsTopLeft);
	      //  Gizmos.DrawLine(_boundsTopLeft, _boundsTopRight);
	    }

	}
}