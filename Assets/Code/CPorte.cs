using UnityEngine;
using System.Collections;

public class CPorte : MonoBehaviour 
{	
	public GameObject m_objCamera;
	public GameObject m_PieceEnter;
	public GameObject m_PieceExit;
	public Vector2 m_Direction;
	CSpriteSheet m_spriteSheet;
	public Material m_openMat;
	CAnimation m_openAnimation;
	bool m_bGoodWay;
	
	GameObject m_enter_att;
	GameObject m_exit_att;
	public int attenuation_enter_size;
	public int attenuation_exit_size;
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void Start () 
	{
		m_spriteSheet = new CSpriteSheet(gameObject);
		m_spriteSheet.Init();
		m_openAnimation = new CAnimation(m_openMat, 4, 1, 2.0f);
		m_spriteSheet.SetAnimation(m_openAnimation);  
		m_objCamera = GameObject.Find("Cameras");
		m_bGoodWay = true;
		
		m_enter_att = new GameObject();
		m_exit_att = new GameObject();
				
		m_enter_att.AddComponent<CPorteAttenuation>();
		m_exit_att.AddComponent<CPorteAttenuation>();
		
		m_enter_att.GetComponent<CPorteAttenuation>().Init(m_PieceEnter, attenuation_enter_size);
		m_exit_att.GetComponent<CPorteAttenuation>().Init(m_PieceExit, attenuation_exit_size);
	}
	
	
	public void OnDrawGizmosSelected(){
		for(int i = 0; i<100; i++){
			Gizmos.color = Color.red;
			Vector3 pos = new Vector3(attenuation_enter_size, 5-i/10, 0);
			Gizmos.DrawLine(transform.position, transform.position - pos);
			
			Gizmos.color = Color.green;
			pos = new Vector3(attenuation_exit_size, 5-i/10, 0);
			Gizmos.DrawLine(transform.position, transform.position + pos);
		}
		
	}

	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void Update () {
		m_spriteSheet.Process();
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
			Vector3 player_pos = getRelativePosition(gameObject.transform, other.gameObject.transform.position);
			
			m_bGoodWay = player_pos.x > 0;
			
			Vector3 pos = m_objCamera.transform.position;
			float fDeltaPos = (m_PieceExit.transform.position - m_PieceEnter.transform.position).x;
			if(m_bGoodWay){
				game.getCamera().SetCurrentRoom(m_PieceExit);
				changeAmbiance(m_PieceExit, m_PieceEnter);
			}
			else {
				changeAmbiance(m_PieceEnter, m_PieceExit);
				game.getCamera().SetCurrentRoom(m_PieceEnter);
				
			}
			
		}
	}
	
	void changeAmbiance(GameObject enter, GameObject exit){
		CAmbiancePiece[] ambs;
	
		ambs = exit.GetComponents<CAmbiancePiece>();
		foreach(CAmbiancePiece amb in ambs){
			amb.StopAmbiance();
		}
		
		ambs = enter.GetComponents<CAmbiancePiece>();
		foreach(CAmbiancePiece amb in ambs){
			amb.PlayAmbiance();
		}
	}
	
	static Vector3 getRelativePosition(Transform origin, Vector3 position) {
	    Vector3 distance = position - origin.position;
	    Vector3 relativePosition = Vector3.zero;
	    relativePosition.x = Vector3.Dot(distance, origin.right.normalized);
	    relativePosition.y = Vector3.Dot(distance, origin.up.normalized);
	    relativePosition.z = Vector3.Dot(distance, origin.forward.normalized);
	     
	    return relativePosition;
    }
	
}
