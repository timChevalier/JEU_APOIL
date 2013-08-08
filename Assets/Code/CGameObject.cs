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
		m_bIsVisible = false;
	}
	
	
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void SetVisible() 
	{
		m_bIsVisible = true;

	}

}
