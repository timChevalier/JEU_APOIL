using UnityEngine;
using System.Collections;

public class CPorteAttenuation : MonoBehaviour {

	GameObject m_piece_exit;
	GameObject m_piece_enter;
	CGame game;
	int m_size;
	BoxCollider m_collider;	

	public void Init(GameObject piece_exit, GameObject piece_enter, int size){
		m_piece_exit = piece_exit;
		m_piece_enter = piece_enter;
		game = GameObject.Find("_Game").GetComponent<CGame>();
		m_size = size;
		
		m_collider = gameObject.AddComponent<BoxCollider>();
		m_collider.isTrigger = true;
		m_collider.center = new Vector3(-m_size/2, 0, 0);
		m_collider.extents = new Vector3(m_size/2, m_size, 30);
		
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider other){
		
		if(other.gameObject != game.getLevel().getPlayer().getGameObject())
			return;

		
		if(getRelativePosition(transform, other.gameObject.transform.position).x<=0){ //Entering from outside
			CAmbiancePiece[] ambs = m_piece_exit.GetComponents<CAmbiancePiece>(); 
				foreach(CAmbiancePiece amb in ambs){
					amb.PlayAmbiance();
				}
		}
		
	}
	
	void OnTriggerExit(Collider other){
		if(other.gameObject != game.getLevel().getPlayer().getGameObject())
			return;
		
		
		if(getRelativePosition(transform, other.gameObject.transform.position).x<=0){ //Exiting to the outside
			
			CAmbiancePiece[] ambs = m_piece_exit.GetComponents<CAmbiancePiece>(); 
			foreach(CAmbiancePiece amb in ambs){
				amb.StopAmbiance();
			}
			
		}
		else { //Exiting to other room
			
			CAmbiancePiece[] ambs = m_piece_exit.GetComponents<CAmbiancePiece>(); 
			foreach(CAmbiancePiece amb in ambs){
					amb.switchGround();
			}
		}

	}
	
	
	void OnTriggerStay(Collider other){
		if(other.gameObject != game.getLevel().getPlayer().getGameObject() )
			return;
		
		float dist = Mathf.InverseLerp(0, m_size, Vector3.Distance(transform.position, other.transform.position));
		CAmbiancePiece[] ambs = m_piece_exit.GetComponents<CAmbiancePiece>(); 
		foreach(CAmbiancePiece amb in ambs){
			amb.SetInAttenuation(dist);
		}
		
		ambs = m_piece_enter.GetComponents<CAmbiancePiece>(); 
		foreach(CAmbiancePiece amb in ambs){
			amb.SetOutAttenuation(1-dist);
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
