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
	
	// Update is called once per frame
	void Update () {
	
	}
	

}
