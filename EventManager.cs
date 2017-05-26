using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace Glide.Scripts
{	
	public static class EventManager 
	{
	    private static Dictionary <string, UnityEvent> eventDictionary;

	    private static void Init ()
	    {
	        if (eventDictionary == null)
	        {
	            eventDictionary = new Dictionary<string, UnityEvent>();
	        }
	    }

	    public static void StartListening (string eventName, UnityAction listener)
	    {
	    	Init();

	        UnityEvent thisEvent = null;
	        if (eventDictionary.TryGetValue (eventName, out thisEvent))
	        {
	            thisEvent.AddListener (listener);
	        } 
	        else
	        {
	            thisEvent = new UnityEvent ();
	            thisEvent.AddListener (listener);
	            eventDictionary.Add (eventName, thisEvent);
	        }
	    }

	    public static void StopListening (string eventName, UnityAction listener)
	    {
			Init();

	        UnityEvent thisEvent = null;
	        if (eventDictionary.TryGetValue (eventName, out thisEvent))
	        {
	            thisEvent.RemoveListener (listener);
	        }
	    }

	    public static void TriggerEvent (string eventName)
	    {
			Init();

	        UnityEvent thisEvent = null;
	        if (eventDictionary.TryGetValue (eventName, out thisEvent))
	        {
	            thisEvent.Invoke ();
	        }
	    }
		
	}
}