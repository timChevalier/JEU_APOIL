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
		if(other.gameObject == m_Game.getLevel().getPlayer().getGameObject() && Input.GetMouseButton(0))	
		{
			m_Game.getLevel().getPlayer().PickUpObject(m_TakeElement);
		}
		
	}
	
	public void SetTakeElement(CTakeElement obj)
	{
		m_TakeElement = obj;
	}
}
