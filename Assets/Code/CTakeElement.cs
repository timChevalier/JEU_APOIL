using UnityEngine;
using System.Collections;

public class CTakeElement : CElement {
	
	public enum ETypeObject
	{
		e_TypeObject_NoTakeElement,
		e_TypeObject_Medicament,
		e_TypeObject_Chalumeau,
		e_TypeObject_RechargeChalumeau,
		e_TypeObject_Rouage,
		e_TypeObject_OutilMecano,
		e_TypeObject_ClefCapitaine,
		e_TypeObject_ClefSecond,
		e_TypeObject_BouteilleCryo,
		e_TypeObject_Torche
	}
	
	ETypeObject m_eTypeObject;
	
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
		m_eTypeObject = m_GameObject.GetComponent<CScriptTakeElement>().GetTypeElement();
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
