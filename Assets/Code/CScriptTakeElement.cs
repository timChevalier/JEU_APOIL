using UnityEngine;
using System.Collections;

public class CScriptTakeElement : MonoBehaviour 
{
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
		if(CApoilInput.DropObject)
		{
			m_Game.getLevel().getPlayer().DropElement();
		}
	}
	
	void OnTriggerStay(Collider other)
	{	
		// ramasser un objet
		if(other.gameObject == m_Game.getLevel().getPlayer().getGameObject() && CApoilInput.PickUpObject)	
		{
			m_Game.getLevel().getPlayer().PickUpObject(m_TakeElement);
		}
		
	}
	
	public void SetTakeElement(CTakeElement obj)
	{
		m_TakeElement = obj;
	}
}
