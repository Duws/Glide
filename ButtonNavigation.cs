using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Glide.Scripts
{	
	public class ButtonNavigation : MonoBehaviour
	{
	    public List<Button> ButtonList;
	    public float KeyboardSpeed = 0.3f;

	    public enum AxisChoices { Vertical,Horizontal }
	    public AxisChoices Axis;
	    public bool AutoPressButtonOnFocus=false;
	    protected int _selectedIndex = 0;
	    protected float _timeCounter=0f;
	    protected bool _buttonPressRequired = true;

	    protected virtual int MenuSelection(List<Button>  buttonList, int currentIndex, string direction)
	    {
	        if (direction == "lower")
	        {
	            if (currentIndex == buttonList.Count - 1)
	            {
	                currentIndex = 0;
	            }
	            else
	            {
	                currentIndex += 1;
	            }
			}
			if (direction == "higher")
			{
				if (currentIndex == 0)
				{
					currentIndex = buttonList.Count - 1;
				}
				else
				{
					currentIndex -= 1;
				}
			}
	        return currentIndex;
	    }

	    protected virtual void Update()
	    {
	        if (Input.GetAxisRaw(Axis.ToString()) <0)
	        {
	            MoveMenu("lower");
	        }

	        if (Input.GetAxisRaw(Axis.ToString()) > 0)
	        {
	            MoveMenu("higher");
	        }
	    }
		
	    public virtual void MoveMenu(string direction)
	    {
	        if (Time.realtimeSinceStartup - _timeCounter > KeyboardSpeed)
	        {
	            _timeCounter = Time.realtimeSinceStartup;
	            _selectedIndex = MenuSelection(ButtonList, _selectedIndex, direction);
	            _buttonPressRequired = true;
	        }
	    }
	    
		/*
	    protected virtual void OnGUI()
	    {

	        GUI.FocusControl(ButtonList[_selectedIndex].name);

	        EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
	        eventSystem.SetSelectedGameObject(ButtonList[_selectedIndex].gameObject, new BaseEventData(eventSystem));

	        if (AutoPressButtonOnFocus && _buttonPressRequired)
	        {
	            BaseEventData tempEventData = new PointerEventData(eventSystem);
	            ExecuteEvents.Execute(ButtonList[_selectedIndex].gameObject, tempEventData, ExecuteEvents.submitHandler);
	            _buttonPressRequired = false;
	        }
	    }*/
	}
}
