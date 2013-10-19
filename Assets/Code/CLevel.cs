using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manage players, monsters and other elements
/// </summary>
public class CLevel
{
	CGame game;
	CPlayer m_Player;
	CPlayer m_Player2;
	CPlayer m_Player3;
	CMonster m_Monster;
	// Allows to switch between lights on/lights off.
	float m_bTimerLightSwitch;
	
	List<CElement> m_pElement;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="CLevel"/> class.
	/// </summary>
	public CLevel()
	{
		game = GameObject.Find("_Game").GetComponent<CGame>();
		Vector2 posPlayer = new Vector2(0.0f, 0.0f);
		Vector2 posMonster = new Vector2(100.0f, 0.0f);
		m_Player = new CPlayer(posPlayer, true);
		//m_Player2 =  new CPlayer();
		//m_Player3 =  new CPlayer();
		m_Monster = new CMonster(posMonster);
		m_bTimerLightSwitch = 0;
		
		// all game elements ?
		m_pElement = new List<CElement>();
		
	}
	
	/// <summary>
	/// Init Player1, monster and each element
	/// </summary>
	public void Init()
	{	
		m_Player.Init();
		m_Monster.Init();
		
		foreach(CElement elem in m_pElement)
			elem.Init();
		
	}
	
	/// <summary>
	/// Reset player1, monster and each element
	/// </summary>
	public void Reset()
	{
		m_Player.Reset();
		m_Monster.Reset();
		
		foreach(CElement elem in m_pElement)
			elem.Reset();
	}
	
	/// <summary>
	/// Process payer1, monster
	/// </summary>
	/// <param name='fDeltatime'>
	/// F deltatime.
	/// </param>
	public void Process(float fDeltatime)
	{
		m_Player.Process(fDeltatime);
		m_Monster.Process(fDeltatime);
		
		if(Input.GetKey(KeyCode.L) && m_bTimerLightSwitch <= 0){
			game.m_bLightIsOn = !game.m_bLightIsOn;
			m_bTimerLightSwitch = 10f;
		}
		m_bTimerLightSwitch -= 0.5f; // always decreasing if previous condition is not satisfied ? why ?
		if(game.m_bLightIsOn == false)
			TurnLight(false);
		else
			TurnLight(true);
		
		foreach(CElement elem in m_pElement)
			elem.Process(fDeltatime);
	}
	
	/// <summary>
	/// Turns the light on/off
	/// </summary>
	/// <param name='bOn'>
	/// True => light on, otherwise => light off
	/// </param>
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
	
	/// <summary>
	/// Add a new element in the level
	/// </summary>
	/// <param name='obj'>
	/// Object. The element to add
	/// </param>
	/// <typeparam name='ElemType'>
	/// The element must inherit from CElement
	/// </typeparam>
	public void CreateElement<ElemType>(GameObject obj) where ElemType : CElement, new()
	{
		ElemType elem = new ElemType();
		elem.setGameObject(obj);
		elem.Init();
		m_pElement.Add(elem);
	}
	
	/// <summary>
	/// Gets the player.
	/// </summary>
	/// <returns>
	/// The player.
	/// </returns>
	public CPlayer getPlayer()
	{
		return m_Player;
	}
	
	/// <summary>
	/// Gets the monster.
	/// </summary>
	/// <returns>
	/// The monster.
	/// </returns>
	public CMonster getMonster()
	{
		return m_Monster;
	}
}
