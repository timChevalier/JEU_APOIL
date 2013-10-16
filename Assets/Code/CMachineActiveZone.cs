using UnityEngine;
using System.Collections;

public class CMachineActiveZone : MonoBehaviour {
	
	CMachine m_Machine;
	CGame m_Game;
	

	public void Init(CMachine obj){
		m_Game = GameObject.Find("_Game").GetComponent<CGame>();
		
		m_Machine = obj;
		
		//Debug.Log("Machine "+m_Machine.getGameObject().name);
		
		if(gameObject.GetComponent<Collider>() == null){
			Debug.LogError("Machine "+m_Machine.getGameObject().name+" have no active zone collider");
		}
	}
	
 	void OnTriggerStay(Collider other)
	{	
		if(other.gameObject ==  m_Game.getLevel().getPlayer().getGameObject() && Input.GetMouseButton(0))
		{
			m_Machine.Activate(m_Game.getLevel().getPlayer());
		}
	}
	
	
}
