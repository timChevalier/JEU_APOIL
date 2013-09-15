using UnityEngine;
using System.Collections;

public class CSoundEngine {
	
	uint bankID;
	
	// Use this for initialization
	void Start () {		
		
	}
	
	public void LoadBank(string soundbankName) {
		AKRESULT result;
		if((result = AkSoundEngine.LoadBank(soundbankName, AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID)) != AKRESULT.AK_Success){
			Debug.LogError("Unable to load "+soundbankName+" with result: " + result);
		}
		
	}
	
	public void setSwitch(string name, string val, GameObject obj){
		AkSoundEngine.SetSwitch(name, val, obj);
	}
	
	public void setRTPC(string name, float val, GameObject obj){
		AkSoundEngine.SetRTPCValue(name, val, obj);
		Debug.Log("Posted "+name+" by "+obj.name);
	}
	
	public void postEvent(string name, GameObject obj){
		AkSoundEngine.PostEvent(name, obj);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

}
