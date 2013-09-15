using UnityEngine;
using System.Collections;

public class CCercleDiscretion : MonoBehaviour {

	CPlayer parent;
	CGame game;
	
	float[] m_fCoeffState;
	float m_fspeedCoeff;
	float m_fBaseRadius;
	
	float m_fRadius;
	SphereCollider collider;
	
	// Use this for initialization
	public void Init(CPlayer pparent) {
		parent = pparent;
		game = GameObject.Find("_Game").GetComponent<CGame>();
		collider = GetComponent<SphereCollider>();
		
		//Get necessary coefficients for size calculation
		m_fCoeffState = new float[4];
		m_fCoeffState[0] = game.m_fCoeffDiscretionAttente;
		m_fCoeffState[1] = game.m_fCoeffDiscretionDiscret ;
		m_fCoeffState[2] = game.m_fCoeffDiscretionMarche;
		m_fCoeffState[3] = game.m_fCoeffDiscretionCours;
		m_fBaseRadius = game.m_fDiscretionBaseRadius;
		//TODO: Add avatar coefficients when and if there is multiple avatar types
	}
	
	public void Process() {
		m_fRadius = m_fBaseRadius*m_fCoeffState[(int)parent.getMoveModState()];
		collider.radius = m_fRadius;
	}
	
	public void OnTriggerEnter(Collider other){
		CMonster monster = game.getLevel().getMonster();
		if(other.gameObject == monster.getGameObject()){
			monster.detectedPlayer();
		}
	}
}
