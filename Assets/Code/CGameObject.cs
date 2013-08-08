using UnityEngine;
using System.Collections;

public class CGameObject : MonoBehaviour {
	
	bool m_bIsVisible;
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void Start () {
		gameObject.layer = LayerMask.NameToLayer("Scene");
		m_bIsVisible = false;
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void Update () {
		if(m_bIsVisible)
		{
			gameObject.layer = LayerMask.NameToLayer("ForceDisplay");
		}
		else {
			gameObject.layer = LayerMask.NameToLayer("Scene");
		}
	}
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void OnCollisionStay(Collision collision) 
	{  
		if (collision.collider.CompareTag("Player"))
		{
			Vector3 posObjet = transform.position;
			CGame game = GameObject.Find("_Game").GetComponent<CGame>();
			CPlayer player = game.getLevel().getPlayer();
			player.InAObject(posObjet);
		}
	}
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void OnCollisionExit(Collision collision) 
	{  
		if (collision.collider.CompareTag("Player"))
		{
			CGame game = GameObject.Find("_Game").GetComponent<CGame>();
			CPlayer player = game.getLevel().getPlayer();
			player.getOutOfObject();
		}
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void SetVisible() 
	{
		m_bIsVisible = true;
	}


}
