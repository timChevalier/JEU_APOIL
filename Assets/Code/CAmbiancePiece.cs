using UnityEngine;
using System.Collections;

public class CAmbiancePiece : MonoBehaviour {

	public string AmbianceName = "";
	public float transition = 0.5f;
	public string solSwitch = "";
	
	CGame game;
	
	void Start(){
		game = GameObject.Find("_Game").GetComponent<CGame>();
	}
	
	public void PlayAmbiance(){
		game.getSoundEngine().postEvent("Play_"+AmbianceName, gameObject);
		
	}
	
	public void SetInAttenuation(float val){
		game.getSoundEngine().setRTPC(AmbianceName+"_In", val, gameObject);
	}
	
	public void SetOutAttenuation(float val){
		game.getSoundEngine().setRTPC(AmbianceName+"_Out", val, gameObject);
	}
	
	public void switchGround(){
		if(solSwitch != "")
			game.getSoundEngine().setSwitch("Sol", solSwitch, game.getLevel().getPlayer().getGameObject());
	}
	
	public void StopAmbiance(){
		game.getSoundEngine().postEvent("Stop_"+AmbianceName, gameObject);
	}
}
