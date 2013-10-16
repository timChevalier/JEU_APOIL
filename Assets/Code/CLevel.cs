using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CLevel
{
	CGame game;
	CPlayer m_Player;
	CPlayer m_Player2;
	CPlayer m_Player3;
	CMonster m_Monster;
	float m_bTimerLightSwitch;
	
	List<CElement> m_pElement;
	
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
		
		m_pElement = new List<CElement>();
		
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void Init()
	{	
		m_Player.Init();
		m_Monster.Init();
		
		foreach(CElement elem in m_pElement)
			elem.Init();
		
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void Reset()
	{
		m_Player.Reset();
		m_Monster.Reset();
		
		foreach(CElement elem in m_pElement)
			elem.Reset();
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
		
		foreach(CElement elem in m_pElement)
			elem.Process(fDeltatime);
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
	public void CreateElement<ElemType>(GameObject obj) where ElemType : CElement, new()
	{
		ElemType elem = new ElemType();
		elem.setGameObject(obj);
		elem.Init();
		m_pElement.Add(elem);
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
