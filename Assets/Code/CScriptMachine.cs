using UnityEngine;
using System.Collections;

public class CScriptMachine : MonoBehaviour {
	
	CGame m_Game;
	CMachine m_Machine;
	
	// Use this for initialization
	void Start() 
	{
		m_Game = GameObject.Find("_Game").GetComponent<CGame>();
		m_Game.getLevel().CreateElement<CMachine>(gameObject);
	}
	
	// Update is called once per frame
	void Update() 
	{
	
	}
	
	void OnTriggerStay(Collider other)
	{	

		
	}
	
	public void SetMachine(CMachine obj)
	{
		m_Machine = obj;
	}
}
