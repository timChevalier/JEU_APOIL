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
	CGame game;
	
	GameObject m_enter_att;
	GameObject m_exit_att;
	public int attenuation_enter_size;
	public int attenuation_exit_size;
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void Start () 
	{
		game = GameObject.Find("_Game").GetComponent<CGame>();
		m_spriteSheet = new CSpriteSheet(gameObject);
		m_spriteSheet.Init();
		m_openAnimation = new CAnimation(m_openMat, 4, 1, 2.0f);
		m_spriteSheet.SetAnimation(m_openAnimation);
		m_spriteSheet.setEndCondition(CSpriteSheet.EEndCondition.e_PingPong);
		m_objCamera = GameObject.Find("Cameras");
		m_bGoodWay = true;
		
		m_enter_att = new GameObject();
		m_exit_att = new GameObject();
		
		m_enter_att.transform.parent = m_exit_att.transform.parent = gameObject.transform;
		m_enter_att.transform.position = m_exit_att.transform.position = transform.position;
		
		m_enter_att.transform.rotation = m_exit_att.transform.rotation = transform.rotation;
		m_exit_att.transform.Rotate(0, 0, 180);
		
		m_enter_att.AddComponent<CPorteAttenuation>().Init(m_PieceExit, m_PieceEnter, attenuation_enter_size);
		m_exit_att.AddComponent<CPorteAttenuation>().Init(m_PieceEnter, m_PieceExit, attenuation_exit_size);

	}
	
	
	public void OnDrawGizmosSelected(){
		for(int i = -90; i<90; i++){
			Gizmos.color = Color.red;
			Vector3 pos = attenuation_enter_size*(new Vector3(Mathf.Cos(i*3.14f/180f), Mathf.Sin(i*3.14f/180f), 0).normalized);
			Gizmos.DrawLine(transform.position, transform.TransformDirection(-pos)+transform.position);
			Gizmos.color = Color.green;
			pos = attenuation_exit_size*(new Vector3(Mathf.Cos(i*3.14f/180f), Mathf.Sin(i*3.14f/180f), 0).normalized);
			Gizmos.DrawLine(transform.position, transform.TransformDirection(pos)+transform.position);
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
		if (other.gameObject == game.getLevel().getPlayer().getGameObject())
		{
			Vector3 player_pos = getRelativePosition(gameObject.transform, other.gameObject.transform.position);
			
			m_bGoodWay = player_pos.x > 0;
			
			Vector3 pos = m_objCamera.transform.position;
			float fDeltaPos = (m_PieceExit.transform.position - m_PieceEnter.transform.position).x;
			if(m_bGoodWay){
				game.getCamera().SetCurrentRoom(m_PieceExit);
			}
			else {				
				game.getCamera().SetCurrentRoom(m_PieceEnter);
			}
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
