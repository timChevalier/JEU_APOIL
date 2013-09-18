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
		e_MonsterState_mange,
		
		e_MonsterState_nbState
	};
	
	EMonsterState m_eMonsterState;
	
	float m_fSpeed;
	float m_fRadiusAlerte;
	bool m_bDetectionAudio;
	bool m_bDetectionVisuelle;
	bool m_bPlayerIsDetected;
	float m_fTimerErrance;
	Vector2 m_PosDetection; // derniere position connu du player
	CPlayer m_Player; 
	CGame m_Game;
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	public CMonster(Vector2 posInit)
	{
		m_eMonsterState =  EMonsterState.e_MonsterState_errance;
		
		m_Game = GameObject.Find("_Game").GetComponent<CGame>();
		GameObject prefab = m_Game.prefabMonster;
		m_GameObject = GameObject.Instantiate(prefab) as GameObject;
		SetPosition2D(posInit);
		m_PosDetection = new Vector2(0.0f, 0.0f);
		m_fRadiusAlerte = m_Game.m_fMonsterRadiusAlerte;
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	public new void Init()
	{		
		base.Init();
		SetState(m_eMonsterState);
		m_fTimerErrance = 0.0f;
		m_Player = m_Game.getLevel().getPlayer();
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

		if(m_Game.IsDebug())
		{
			if(Input.GetKeyDown(KeyCode.E))
			{
				m_eMonsterState = (m_eMonsterState + 1);
				if (m_eMonsterState >= EMonsterState.e_MonsterState_nbState)
					m_eMonsterState = EMonsterState.e_MonsterState_errance;
				SetState(m_eMonsterState);
			}
		}
		
		if(m_Game.IsDebug())
			ProcessDebug(fDeltatime);
		
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
			Vector3 move = Vector3.zero;
			Vector2 rand = Random.insideUnitCircle;
			move += m_Game.m_fSpeedMonster * m_fSpeed * new Vector3(rand.x, rand.y , 0.0f);
			m_GameObject.rigidbody.velocity = Vector3.zero	; //+= move; //Imobility, as requested by game design documents...
			m_fTimerErrance = m_Game.m_fMonsterTimeErrance;
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
		Vector3 move = Vector3.zero;
		Vector3 direction = new Vector3(m_PosDetection.x, m_PosDetection.y, 0.0f) - m_GameObject.transform.position;
		move += m_Game.m_fSpeedMonster * m_fSpeed * direction.normalized;
		m_GameObject.rigidbody.velocity += move;
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	void ProcessAlerte(float fDeltatime)
	{
		Vector3 move = Vector3.zero;
		Vector2 rand = m_PosDetection + m_fRadiusAlerte * Random.insideUnitCircle;
		Vector3 direction = new Vector3(rand.x, rand.y, 0.0f) - m_GameObject.transform.position;
		move += m_Game.m_fSpeedMonster * m_fSpeed * direction.normalized;
		m_GameObject.rigidbody.velocity += move;
		
		if(m_Game.IsDebug())
		{
			Debug.DrawLine(new Vector3(m_PosDetection.x, m_PosDetection.y, 0.0f), new Vector3(rand.x, rand.y, 0.0f));
		}
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	void ProcessAttaque(float fDeltatime)
	{
		Vector3 move = Vector3.zero;
		Vector3 direction = new Vector3(m_PosDetection.x, m_PosDetection.y, 0.0f) - m_GameObject.transform.position;
		move += m_Game.m_fSpeedMonster * m_fSpeed * direction.normalized;
		m_GameObject.rigidbody.velocity += move;
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
	void ProcessDebug(float fDeltatime)
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
				m_fSpeed = 2;
				m_bDetectionAudio = true;
				m_bDetectionVisuelle = true;
				break;	
			}
			
			case EMonsterState.e_MonsterState_affut:	
			{
				m_bDetectionAudio = true;
				m_bDetectionVisuelle = true;
				m_fSpeed = 3;
				break;	
			}
			case EMonsterState.e_MonsterState_alerte:	
			{
				m_bDetectionAudio = true;
				m_bDetectionVisuelle = true;
				m_fSpeed = 1;	
				break;	
			}
			case EMonsterState.e_MonsterState_attaque:	
			{
				m_bDetectionAudio = true;
				m_bDetectionVisuelle = false;
				m_fSpeed = 4;
				break;	
			}
			case EMonsterState.e_MonsterState_mange:	
			{
				m_bDetectionAudio = false;
				m_bDetectionVisuelle = false;
				m_fSpeed = 4;
				break;	
			}
		}
	}
	
	public void detectedPlayerAudio(){
		//NOMNOMNOM
		if(!m_bDetectionAudio)
			return;
		
		switch(m_eMonsterState){
			
			case EMonsterState.e_MonsterState_errance:
				SetState(EMonsterState.e_MonsterState_affut);
				break;
			
		}
			
			
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	void SearchPlayer(float fDeltatime)
	{
		//TRUC BASIQUE DE DISTANCE POUR DEBUGER VITE FAIT
		
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	public EMonsterState getState()
	{
		return m_eMonsterState;	
	}

}