using UnityEngine;
using System.Collections;

/// <summary>
/// ???
/// </summary>
public class CCollision2D : MonoBehaviour {
	/*
	public bool m_bCollision_fixe = false;
	bool m_bIsOverlapping;
	Vector3 m_NormalSurface;
	
	void SetOverlapping(bool bState)
	{
		m_bIsOverlapping = bState;
	}
	
	
	// Use this for initialization
	void Start () {
		m_bIsOverlapping = false;
		m_NormalSurface = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!m_bCollision_fixe)
		{
			if(m_bIsOverlapping)
			{
				CGame game = GameObject.Find("_Game").GetComponent<CGame>();
				transform.position -= game.m_fSpeedPlayer * m_NormalSurface.normalized * Time.deltaTime;
				Debug.DrawLine(transform.position, transform.position + 100*m_NormalSurface);
			}
		}
		
		
	}
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void OnCollisionEnter(Collision collision) 
	{  
		if (!collision.collider.CompareTag("sol"))
		{
			GameObject objCurrent = gameObject;
			GameObject objOther = collision.collider.gameObject;		
			
			Vector3 u = new Vector3(1, 0, 0);
			Vector3 v = new Vector3(0, 1, 0);
			float fAngle = transform.rotation.eulerAngles.z * 3.14159f /180f;
			Vector3 N = Mathf.Cos(fAngle) * u + Mathf.Sin(fAngle) * v;
			Vector3 T = -Mathf.Sin(fAngle) * u + Mathf.Cos(fAngle) * v;
			
			Vector3 posCurrentObject = objCurrent.transform.position;
			Vector3 posOtherObject = objOther.transform.position;
			Vector3 deltaPos = posOtherObject - posCurrentObject;
			
			m_NormalSurface = Vector3.zero;
			if(Vector3.Dot(deltaPos, N) > -objCurrent.transform.localScale.x/2f + objOther.transform.localScale.x/2f
				&& Vector3.Dot(deltaPos, N) < 0)
				m_NormalSurface += -N;
			if(Vector3.Dot(deltaPos, N) < objCurrent.transform.localScale.x/2f + objOther.transform.localScale.x/2f
				&& Vector3.Dot(deltaPos, N) > 0)
				m_NormalSurface += N;
			if(Vector3.Dot(deltaPos, T) > -objCurrent.transform.localScale.z/2f + objOther.transform.localScale.y/2f
				&& Vector3.Dot(deltaPos, T) < 0)
				m_NormalSurface += -T;
			if(Vector3.Dot(deltaPos, T) < objCurrent.transform.localScale.z/2f + objOther.transform.localScale.y/2f
				&& Vector3.Dot(deltaPos, T) > 0)
				m_NormalSurface += T;
			
			SetOverlapping(true);
			
			
		}
	}
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void OnCollisionExit(Collision collision) 
	{  	
		if (!collision.collider.CompareTag("sol"))
		{
			SetOverlapping(false);
		}
	}
*/
}
