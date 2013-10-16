using UnityEngine;
using System.Collections;

public class CScriptTakeElement : MonoBehaviour {
	
	CGame m_Game;
	CTakeElement m_TakeElement;
	
	// Use this for initialization
	void Start () 
	{
		m_Game = GameObject.Find("_Game").GetComponent<CGame>();
		m_Game.getLevel().CreateElement<CTakeElement>(gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnTriggerStay(Collider other)
	{	
		bool bInputTakeElement = m_Game.IsPadXBoxMod() ? Input.GetKey(KeyCode.JoystickButton0) : Input.GetMouseButton(0);
		if(other.gameObject == m_Game.getLevel().getPlayer().getGameObject() && bInputTakeElement)	
		{
			m_Game.getLevel().getPlayer().PickUpObject(m_TakeElement);
		}
		
	}
	
	public void SetTakeElement(CTakeElement obj)
	{
		m_TakeElement = obj;
	}
}
