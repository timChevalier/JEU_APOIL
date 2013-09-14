using UnityEngine;
using System.Collections;

public class CPorteAttenuation : MonoBehaviour {
	
	GameObject m_piece;
	CGame game;
	int m_size;
	Collider m_collider;	

	public void Init(GameObject piece, int size){
		m_piece = piece;
		game = GameObject.Find("_Game").GetComponent<CGame>();
		
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerStay(Collider other){
		if(other.gameObject != game.getLevel().getPlayer().getGameObject())
			return;
		
		
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
