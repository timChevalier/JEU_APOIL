using UnityEngine;
using System.Collections;

public class CApoilInput
{
	public static bool MoveLeft;
	public static bool MoveRight;
	public static bool MoveUp;
	public static bool MoveDown;
	public static bool WalkFast;
	public static bool WalkSlow;
	public static bool PickUpObject;
	public static bool DropObject;
	public static bool ActivateMachine;
	public static float PadLightHorizontal;
	public static float PadLightVertical;
	public static Vector2 MousePosition;
	
	static CGame m_Game = GameObject.Find("_Game").GetComponent<CGame>();
		
	public static void Process(float fDeltatime) 
	{
		// if we use gamepad !
		if(m_Game.IsPadXBoxMod())
		{	
			float fTolerance = 0.05f; // ???
			MoveUp = (Input.GetAxis("moveVertical")) < -fTolerance; // ???
			MoveDown = (Input.GetAxis("moveVertical")) > fTolerance;
			MoveLeft = (Input.GetAxis("moveHorizontal")) < -fTolerance;
			MoveRight = (Input.GetAxis("moveHorizontal")) > fTolerance;
			WalkFast = Input.GetKey(KeyCode.JoystickButton5); //Right Mouse Button ?
			WalkSlow = Input.GetKey(KeyCode.JoystickButton4); //L. M. Button ?
			
			PickUpObject = Input.GetKey(KeyCode.JoystickButton0); //Letter A
			DropObject = Input.GetKey(KeyCode.JoystickButton2); //X
			ActivateMachine = Input.GetKey(KeyCode.JoystickButton0); //A 
				
			MousePosition = new Vector2(0.0f, 0.0f);
			
			PadLightHorizontal = Input.GetAxis("lightHorizontal"); // ??
			PadLightVertical = Input.GetAxis("lightVertical");
		}
		else
		{
			MoveUp = Input.GetKey(KeyCode.Z);
			MoveDown = Input.GetKey(KeyCode.S);
			MoveLeft = Input.GetKey(KeyCode.Q);
			MoveRight = Input.GetKey(KeyCode.D);
			WalkFast = Input.GetKey(KeyCode.LeftShift);
			WalkSlow = Input.GetKey(KeyCode.LeftControl);
			
			PickUpObject = Input.GetMouseButton(0);
			DropObject = Input.GetMouseButton(2);
			ActivateMachine = Input.GetMouseButton(0);
			
			MousePosition = CalculateMousePosition();
			
			PadLightHorizontal = 0.0f;
			PadLightVertical = 0.0f;
		}
	}
	
	/// <summary>
	/// ????????? Why !!!
	/// </summary>
	/// <returns>
	/// ???
	/// </returns>
	public static Vector2 CalculateMousePosition()
	{
		Vector3 posMouseTmp = Vector3.zero;
		RaycastHit vHit = new RaycastHit();
		Ray vRay = m_Game.m_CameraCone.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(vRay, out vHit, 100)) 
		{
			posMouseTmp = vHit.point;
		}
		return new Vector2(posMouseTmp.x, posMouseTmp.y);
	}
}
