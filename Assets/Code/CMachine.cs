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
	
	/// <summary>
	/// Reset the coordonates ???(only)
	/// </summary>
	public override void Reset()
	{
		base.Reset();
	}

	/// <summary>
	/// Process coordinates(only ? Furthermore, CElement.Process is empty)
	/// </summary>
	/// <param name='fDeltatime'>
	/// F deltatime : time between 2 updates
	/// </param>
	public override void Process(float fDeltatime)
	{
		base.Process(fDeltatime);
	}
	
	/// <summary>
	/// ???
	/// </summary>
	/// <param name='player'>
	/// Player. A game player. Why not CCharacter ?
	/// </param>
	public void Activate(CPlayer player){
		CMachineAction[] actions = m_GameObject.GetComponents<CMachineAction>();
		foreach(CMachineAction action in actions){
			action.Activate(player);
		}
	}
	
}

