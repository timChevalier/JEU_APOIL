using UnityEngine;
using System.Collections;

public class CPorte : MonoBehaviour 
{	
	public GameObject m_objCamera;
	public GameObject m_PieceEnter;
	public GameObject m_PieceExit;
	public Vector2 m_Direction;
	bool m_bGoodWay;
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void Start () 
	{
		m_objCamera = GameObject.Find("Cameras");
		m_bGoodWay = true;
	}
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void Update () {
	
	}
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void OnTriggerEnter(Collider other)
	{
		CGame game = GameObject.Find("_Game").GetComponent<CGame>();
		if (other.gameObject == game.getLevel().getPlayer().getGameObject())
		{
			if(Vector2.Dot(game.getLevel().getPlayer().getDirectionDeplacement(), m_Direction) > 0)
				m_bGoodWay = true;
			else
				m_bGoodWay = false;
		}
	}
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void OnTriggerExit(Collider other)
	{
		CGame game = GameObject.Find("_Game").GetComponent<CGame>();
		if (other.gameObject == game.getLevel().getPlayer().getGameObject())
		{
			Vector3 pos = m_objCamera.transform.position;
			float fDeltaPos = (m_PieceExit.transform.position - m_PieceEnter.transform.position).x;
			if(m_bGoodWay)
				pos.x += fDeltaPos;
			else
				pos.x -= fDeltaPos;
			m_objCamera.transform.position = pos;
		}
	}
}
