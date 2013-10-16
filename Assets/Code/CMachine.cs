using UnityEngine;
using System.Collections;

public class CMachine : CElement 
{
	CSpriteSheet m_SpriteSheet;
	CGame m_Game;
	
	CScriptMachine m_ScriptMachine;
	CMachineActiveZone m_ActiveZone;	
	

	public CMachine()
	{
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	public override void Init()
	{	
		base.Init();
		
		//m_GameObject = obj;
		m_ScriptMachine = m_GameObject.GetComponent<CScriptMachine>();
		m_ScriptMachine.SetMachine(this);
		m_ActiveZone = m_GameObject.transform.GetComponentInChildren<CMachineActiveZone>();
		m_ActiveZone.Init(this);
		
		m_SpriteSheet = new CSpriteSheet(m_GameObject);
		m_SpriteSheet.Init();
		m_SpriteSheet.SetAnimation(m_ScriptMachine.GetAnimation());
		
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public override  void Reset()
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
	
	public void Activate(CPlayer player){
		CMachineAction[] actions = m_GameObject.GetComponents<CMachineAction>();
		foreach(CMachineAction action in actions){
			action.Activate(player);
		}
	}
	
}

