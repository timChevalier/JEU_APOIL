using UnityEngine;
using System.Collections;

public class CLevel
{
	CGame game;
	CPlayer m_Player;
	CPlayer m_Player2;
	CPlayer m_Player3;
	CMonster m_Monster;
	
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

		
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void Init()
	{	
		m_Player.Init();
		m_Monster.Init();
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void Reset()
	{
		m_Player.Reset();
		m_Monster.Reset();
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void Process(float fDeltatime)
	{
		m_Player.Process(fDeltatime);
		m_Monster.Process(fDeltatime);
		
		if(game.m_bLightIsOn == false)
			TurnLight(false);
		else
			TurnLight(true);
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
			}
		}
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
