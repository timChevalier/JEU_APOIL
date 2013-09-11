using UnityEngine;
using System.Collections;

public class CAmbiancePiece : MonoBehaviour {

	public string AmbianceName = "";
	public float transition = 0.5f;
	public string TransitionName = "";
	
	public void PlayAmbiance(){
		AkSoundEngine.PostEvent("Play_"+AmbianceName, gameObject);
		AkSoundEngine.SetRTPCValue(TransitionName, transition, gameObject);
	}
	
	public void StopAmbiance(){
		AkSoundEngine.PostEvent("Stop_"+AmbianceName, gameObject);
	}
}
