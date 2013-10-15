using UnityEngine;
using System.Collections;

public class CLevel
{
	CGame game;
	CPlayer m_Player;
	CPlayer m_Player2;
	CPlayer m_Player3;
	CMonster m_Monster;
	float m_bTimerLightSwitch;
	
	CTakeElement[] m_pElement;
	const int m_nMaxTakeElement = 64;
	int m_nNbElement;
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public CLevel()
	{
		game = GameObject.Find("_Game").GetComponent<CGame>();
		Vector2 posInit = new Vector2(0.0f, 0.0f);
		Vector2 posInitM = new Vector2(100.0f, 0.0f);
		m_Player = new CPlayer(posInit, true);
		//m_Player2 =  new CPlayer();
		//m_Player3 =  new CPlayer();
		m_Monster = new CMonster(posInitM);
		m_bTimerLightSwitch = 0;
		
		m_pElement = new CTakeElement[m_nMaxTakeElement];
		m_nNbElement = 0;
		
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void Init()
	{	
		m_Player.Init();
		m_Monster.Init();
		
		for(int i = 0 ; i < m_nNbElement ; ++i)
			m_pElement[i].Init();
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void Reset()
	{
		m_Player.Reset();
		m_Monster.Reset();
		
		for(int i = 0 ; i < m_nNbElement ; ++i)
			m_pElement[i].Reset();
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void Process(float fDeltatime)
	{
		m_Player.Process(fDeltatime);
		m_Monster.Process(fDeltatime);
		
		if(Input.GetKey(KeyCode.L) && m_bTimerLightSwitch <= 0){
			game.m_bLightIsOn = !game.m_bLightIsOn;
			m_bTimerLightSwitch = 10f;
		}
		m_bTimerLightSwitch -= 0.5f;
		if(game.m_bLightIsOn == false)
			TurnLight(false);
		else
			TurnLight(true);
		
		for(int i = 0 ; i < m_nNbElement ; ++i)
			m_pElement[i].Process(fDeltatime);
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void TurnLight(bool bOn)
	{
		GameObject[] ShipLight;
		ShipLight = GameObject.FindGameObjectsWithTag("ShipLight");
		
		foreach(GameObject currentLight in ShipLight)
		{
			if(bOn)
			{
				currentLight.SetActiveRecursively(true);
			}
			else
			{
				currentLight.SetActiveRecursively(false);
				currentLight.active = true;
			}
		}
	}
	
	//-------------------------------------------------------------------------------
	/// 
	//-------------------------------------------------------------------------------
	public void CreateTakeElement(GameObject obj)
	{
		m_pElement[m_nNbElement] = new CTakeElement(obj);
		m_pElement[m_nNbElement].Init();
		m_nNbElement++;
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public CPlayer getPlayer()
	{
		return m_Player;
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public CMonster getMonster()
	{
		return m_Monster;
	}
}
