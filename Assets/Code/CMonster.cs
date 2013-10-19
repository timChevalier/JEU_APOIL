using UnityEngine;
using System.Collections;

public class CMonster : CCharacter 
{
	/// <summary>
	/// Monster states (errance, attaque...)
	/// </summary>
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
	
	// Publique ? Private ?
	float m_fSpeed; // speed of the monster
	float m_fRadiusAlerte; // If a player is closer that this distance, the monster will be alerted
	bool m_bDetectionAudio; // false : the monster is dumb, else he sees
	bool m_bDetectionVisuelle; // false : monster is blind, else he sees
	bool m_bPlayerIsDetected; // If the monster know the player is here
	float m_fTimerErrance; // ??? time during which the monster is searching for the player before he forgots it ???
	Vector2 m_PosDetection; // Last position the player were see
	CPlayer m_Player; // The detected player (only one reference changing from one player to an other or must we have 1 variable per player ?)
	CGame m_Game;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="CMonster"/> class.
	/// Default : errance, default radius alert(in G<see cref="CGame"/>), use prefab monster(?). 
	/// </summary>
	/// <param name='posInit'>
	/// Position init. The initial position of the monster (no initial rooms ?)
	/// </param>
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
	
	/// <summary>
	/// Init errance timer, player1 become the researched player
	/// init position(base=CCharacter=CElement=position, non ?)
	/// </summary>
	public new void Init()
	{		
		base.Init();
		SetState(m_eMonsterState);
		m_fTimerErrance = 0.0f;
		m_Player = m_Game.getLevel().getPlayer();
	}

	/// <summary>
	///  Reset... what ? Position ?
	/// </summary>
	public new void Reset()
	{
		base.Reset();
	}

	/// <summary>
	///  Process 
	/// </summary>
	/// <param name='fDeltatime'>
	///  F deltatime. Time between 2 updates. 
	/// </param>
	public new void Process(float fDeltatime)
	{
		base.Process(fDeltatime);
		ProcessState(fDeltatime);

		if(m_Game.IsDebug())
		{
			if(Input.GetKeyDown(KeyCode.E))
			{
				// goto next state
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
	
	
	/// <summary>
	/// Errance behavior (questions ???)
	/// </summary>
	/// <param name='fDeltatime'>
	/// F deltatime.
	/// </param>
	void ProcessErrance(float fDeltatime)
	{
		if(m_fTimerErrance <= 0.0f) // why this condition ?
		{
			Vector3 move = Vector3.zero;
			Vector2 rand = Random.insideUnitCircle;
			move += m_Game.m_fSpeedMonster * m_fSpeed * new Vector3(rand.x, rand.y , 0.0f);
			m_GameObject.rigidbody.velocity = Vector3.zero	; //+= move; //Imobility, as requested by game design documents...
			m_fTimerErrance = m_Game.m_fMonsterTimeErrance;
		}
		else
		{
			m_fTimerErrance -= fDeltatime;// why ?
		}
	}
	
	/// <summary>
	/// Affut behavior
	/// </summary>
	/// <param name='fDeltatime'>
	/// F deltatime.
	/// </param>
	void ProcessAffut(float fDeltatime)
	{
		Vector3 move = Vector3.zero;
		Vector3 direction = new Vector3(m_PosDetection.x, m_PosDetection.y, 0.0f) - m_GameObject.transform.position;
		move += m_Game.m_fSpeedMonster * m_fSpeed * direction.normalized;
		m_GameObject.rigidbody.velocity += move;
	}
	
	/// <summary>
	/// Alerte behabior
	/// </summary>
	/// <param name='fDeltatime'>
	/// F deltatime.
	/// </param>
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
	
	/// <summary>
	/// Attaque. Behavior
	/// </summary>
	/// <param name='fDeltatime'>
	/// F deltatime.
	/// </param>
	void ProcessAttaque(float fDeltatime)
	{
		Vector3 move = Vector3.zero;
		Vector3 direction = new Vector3(m_PosDetection.x, m_PosDetection.y, 0.0f) - m_GameObject.transform.position;
		move += m_Game.m_fSpeedMonster * m_fSpeed * direction.normalized;
		m_GameObject.rigidbody.velocity += move;
	}
	
	/// <summary>
	/// Mange behavior
	/// </summary>
	/// <param name='fDeltatime'>
	/// F deltatime.
	/// </param>
	void ProcessMange(float fDeltatime)
	{
		
	}
	
	/// <summary>
	/// Debug ?
	/// </summary>
	/// <param name='fDeltatime'>
	/// F deltatime.
	/// </param>
	void ProcessDebug(float fDeltatime)
	{

	}
	
	/// <summary>
	/// set the state parameters
	/// </summary>
	/// <param name='eState'>
	/// eState : current state
	/// </param>
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
	
	/// <summary>
	/// Gets current state.
	/// </summary>
	/// <returns>
	/// Current state.
	/// </returns>
	public EMonsterState getState()
	{
		return m_eMonsterState;	
	}

}