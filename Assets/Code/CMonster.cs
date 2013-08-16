using UnityEngine;
using System.Collections;

public class CMonster : CCharacter 
{
	
	public enum EMonsterState
	{
		e_MonsterState_errance,
		e_MonsterState_affut,
		e_MonsterState_alerte,
		e_MonsterState_attaque,
		e_MonsterState_mange
	};
	
	EMonsterState m_eMonsterState;
	int m_nSpeed;
	bool m_bDetectionAudio;
	bool m_bDetectionVisuelle;
	bool m_bPlayerIsDetected;
	float m_fTimerErrance;
	CPlayer m_Player; 
	
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
		CGame game = GameObject.Find("_Game").GetComponent<CGame>();
		base.Init();
		SetState(m_eMonsterState);
		m_fTimerErrance = 0.0f;
		m_Player = game.getLevel().getPlayer();
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
		ProcessState(fDeltatime);
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	void ProcessState(float fDeltatime)
	{
		switch(m_eMonsterState)
		{
			case EMonsterState.e_MonsterState_errance:	
			{
				ProcessErrance(fDeltatime);
				break;	
			}
			
			case EMonsterState.e_MonsterState_affut:	
			{
				ProcessAffut(fDeltatime);
				break;	
			}
			case EMonsterState.e_MonsterState_alerte:	
			{
				ProcessAlerte(fDeltatime);
				break;	
			}
			case EMonsterState.e_MonsterState_attaque:	
			{
				ProcessAttaque(fDeltatime);
				break;	
			}
			case EMonsterState.e_MonsterState_mange:	
			{
				ProcessMange(fDeltatime);
				break;	
			}
		}
	}
	
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	void ProcessErrance(float fDeltatime)
	{
		if(m_fTimerErrance <= 0.0f)
		{
			CGame game = GameObject.Find("_Game").GetComponent<CGame>();
			Vector3 move = Vector3.zero;
			Vector2 rand = Random.insideUnitCircle;
			move += game.m_fSpeedMonster * m_nSpeed * new Vector3(rand.x, rand.y , 0.0f);
			m_GameObject.rigidbody.velocity += move;
			m_fTimerErrance = game.m_fTimeErrance;
		}
		else 
		{
			m_fTimerErrance -= fDeltatime;
		}
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	void ProcessAffut(float fDeltatime)
	{
		
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	void ProcessAlerte(float fDeltatime)
	{
		
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	void ProcessAttaque(float fDeltatime)
	{
		
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	void ProcessMange(float fDeltatime)
	{
		
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	void SetState(EMonsterState eState)
	{
		m_eMonsterState = eState;
		switch(m_eMonsterState)
		{
			case EMonsterState.e_MonsterState_errance:	
			{
				m_nSpeed = 2;
				m_bDetectionAudio = true;
				m_bDetectionVisuelle = true;
				break;	
			}
			
			case EMonsterState.e_MonsterState_affut:	
			{
				m_bDetectionAudio = true;
				m_bDetectionVisuelle = true;
				m_nSpeed = 3;
				break;	
			}
			case EMonsterState.e_MonsterState_alerte:	
			{
				m_bDetectionAudio = true;
				m_bDetectionVisuelle = true;
				m_nSpeed = 1;	
				break;	
			}
			case EMonsterState.e_MonsterState_attaque:	
			{
				m_bDetectionAudio = true;
				m_bDetectionVisuelle = false;
				m_nSpeed = 4;
				break;	
			}
			case EMonsterState.e_MonsterState_mange:	
			{
				m_bDetectionAudio = false;
				m_bDetectionVisuelle = false;
				m_nSpeed = 4;
				break;	
			}
		}
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	void SearchPlayer(float fDeltatime)
	{
		//TRUC BASIQUE DE DISTANCE POUR DEBUGER VITE FAIT
		
	}

}