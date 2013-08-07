using UnityEngine;
using System.Collections;

public class CGameObject : MonoBehaviour {
	
	bool m_bIsVisible;
	
	// Use this for initialization
	void Start () {
		gameObject.layer = LayerMask.NameToLayer("Scene");
		m_bIsVisible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(m_bIsVisible)
			gameObject.layer = LayerMask.NameToLayer("ForceDisplay");
		else {
			gameObject.layer = LayerMask.NameToLayer("Scene");
		}
	}
	
	public void SetVisible() 
	{
		m_bIsVisible = true;
	}


}
