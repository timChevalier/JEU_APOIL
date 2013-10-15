using UnityEngine;
using System.Collections;

public class CAmbiancePiece : MonoBehaviour {

	public string AmbianceName = "";
	public float transition = 0.5f;
	public string solSwitch = "";
	
	CGame m_Game;
	
	void Start(){
		m_Game = GameObject.Find("_Game").GetComponent<CGame>();
	}
	
	public void PlayAmbiance(){
		m_Game.getSoundEngine().postEvent("Play_"+AmbianceName, gameObject);
		
	}
	
	public void SetInAttenuation(float val){
		m_Game.getSoundEngine().setRTPC(AmbianceName+"_In", val, gameObject);
	}
	
	public void SetOutAttenuation(float val){
		m_Game.getSoundEngine().setRTPC(AmbianceName+"_Out", val, gameObject);
	}
	
	public void switchGround(){
		if(solSwitch != "")
			m_Game.getSoundEngine().setSwitch("Sol", solSwitch, m_Game.getLevel().getPlayer().getGameObject());
	}
	
	public void StopAmbiance(){
		m_Game.getSoundEngine().postEvent("Stop_"+AmbianceName, gameObject);
	}
}
