using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemManager : MonoBehaviour {

	[SerializeField] private EventSystem _eventSystem;

	public void TurnOff() {
		_eventSystem.enabled = false;
	}
	
	public void TurnOn() {
		_eventSystem.enabled = true;
	}
}
