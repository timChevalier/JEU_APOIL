using UnityEngine;
using System.Collections;

public class CGameObject : MonoBehaviour {
	
	bool m_bIsVisible;
	bool m_bIsForceDisplay;
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void Start () {
		//gameObject.layer = LayerMask.NameToLayer("Scene");
		m_bIsVisible = false;
		if (gameObject.layer == LayerMask.NameToLayer("ForceDisplay"))
			m_bIsForceDisplay = true;
		else
			m_bIsForceDisplay = false;
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void Update () 
	{
		if(!m_bIsForceDisplay)
		{
			if(m_bIsVisible)
			{
				gameObject.layer = LayerMask.NameToLayer("ForceDisplay");
			}
			else 
			{
				gameObject.layer = LayerMask.NameToLayer("Scene");
			}	
			m_bIsVisible = false;
		}
		
		if(gameObject.tag.Equals("player"))
		{
			gameObject.layer = LayerMask.NameToLayer("ForceDisplay");
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
