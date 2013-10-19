using UnityEngine;
using System.Collections;

public class CGameObject : MonoBehaviour {
	
	/// <summary>
	/// Is the object visible ?
	/// @todo: Public or not ?
	/// </summary>
	bool m_bIsVisible;
	/// <summary>
	/// A marker for the objects in the layer "ForceDisplay"
	/// @todo : what does this layer mean ? Public or not ?
	/// </summary>
	bool m_bIsForceDisplay;
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		//gameObject.layer = LayerMask.NameToLayer("Scene");
		m_bIsVisible = false;
		if (gameObject.layer == LayerMask.NameToLayer("ForceDisplay"))
			m_bIsForceDisplay = true;
		else
			m_bIsForceDisplay = false;
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
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
	
	/// <summary>
	/// Sets the visibility of the object.
	/// </summary>
	public void SetVisible() 
	{
		m_bIsVisible = true;
	}

}
