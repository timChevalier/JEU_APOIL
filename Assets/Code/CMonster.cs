using UnityEngine;
using System.Collections;

public class CMonster : CCharacter 
{
	
	enum EMonsterState
	{
		e_MonsterState_errance,
		e_MonsterState_affut,
		e_MonsterState_alerte,
		e_MonsterState_attaque,
		e_MonsterState_mange
	};
	
	EMonsterState m_eMonsterState;
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	public CMonster(Vector2 posInit)
	{
		m_eMonsterState =  EMonsterState.e_MonsterState_errance;
		
		CGame game = GameObject.Find("_Game").GetComponent<CGame>();
		GameObject prefab = game.prefabMonster;
		m_GameObject = GameObject.Instantiate(prefab) as GameObject;
		SetPosition2D(posInit);
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	public new void Init()
	{	
		base.Init();
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public new void Reset()
	{
		base.Reset();
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	public new void Process(float fDeltatime)
	{
		base.Process(fDeltatime);
	}
}