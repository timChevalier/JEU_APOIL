using UnityEngine;
using System.Collections;

public class CTakeElement : CElement {
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	public CTakeElement()
	{
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	public override void Init()
	{	
		base.Init();
		m_GameObject.GetComponent<CScriptTakeElement>().SetTakeElement(this);
		
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public override void Reset()
	{
		base.Reset();
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	public override void Process(float fDeltatime)
	{
		base.Process(fDeltatime);
	}
}
