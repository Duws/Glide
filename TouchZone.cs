using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Glide.Scripts
{	
	[RequireComponent(typeof(Rect))]
	[RequireComponent(typeof(CanvasGroup))]
	
	public class TouchZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
	    public enum TouchZoneActions { MainAction,Pause,Land, Accel, Start}
	    public TouchZoneActions TouchZoneBinding;
		public bool SendDownEvent = true;
		public bool SendUpEvent = true;
	    public bool SendPressedEvent = false;

	    protected bool _zonePressed = false;
	    protected string OnPointerDownAction;
	    protected string OnPointerUpAction;
	    protected string OnPointerPressedAction;

	    protected void Start ()
	    {
	        CanvasGroup canvasRenderer = GetComponent<CanvasGroup>();
	        canvasRenderer.alpha = 0f;
	        
	        switch( TouchZoneBinding)
	        {
	            case TouchZoneActions.MainAction:
	                OnPointerDownAction = "MainActionButtonDown";
	                OnPointerUpAction = "MainActionButtonUp";
	                OnPointerPressedAction = "MainActionButtonPressed";
	                break;
	            case TouchZoneActions.Pause:
	                OnPointerDownAction = "PauseButtonDown";
	                OnPointerUpAction = "PauseButtonUp";
	                OnPointerPressedAction = "PauseButtonPressed";
	                break;
	           case TouchZoneActions.Land:
	               OnPointerPressedAction = "LandPressed";
	               break;
	        }

	        if (!SendDownEvent) { OnPointerDownAction = null; }
	        if (!SendUpEvent) { OnPointerUpAction = null; }
	        if (!SendPressedEvent) { OnPointerPressedAction = null; }
		}

	    protected void Update()
	    {
	        if (_zonePressed)
	        {
	            OnPointerPressed();
	        }
	    }

	    public void OnPointerDown(PointerEventData data)
	    {
	        _zonePressed = true;
	        if (OnPointerDownAction!=null)
	            InputManager.Instance.SendMessage(OnPointerDownAction);
	    }

	    public void OnPointerUp(PointerEventData data)
	    {
	        _zonePressed = false;
	        if (OnPointerUpAction != null)
	            InputManager.Instance.SendMessage(OnPointerUpAction);
	    }

	    public void OnPointerPressed()
	    {
	        if (OnPointerPressedAction != null)
	            InputManager.Instance.SendMessage(OnPointerPressedAction);
	    }
	}
}
