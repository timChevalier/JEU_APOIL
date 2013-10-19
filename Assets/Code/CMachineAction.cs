using UnityEngine;
using System.Collections;

/// <summary>
/// ???
/// </summary>
public class CMachineAction : MonoBehaviour
{
	int i = 0;
	public void Activate(CPlayer player){
		//Do noooooooothing
		Debug.Log("MachineAction Activated "+i++);
	}
}
