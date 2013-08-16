using UnityEngine;
using System.Collections;

public class CCamera 
{
	GameObject m_GameObject;
	GameObject m_CurrentRoom;
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	public CCamera()
	{
		CGame game = GameObject.Find("_Game").GetComponent<CGame>();
		m_GameObject = GameObject.Find("Cameras");
		m_CurrentRoom = game.m_Room1;
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	public void Init()
	{	
		
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void Reset()
	{
		
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	public void Process(float fDeltatime)
	{
		SetPositionFromRoom();
		//SetPositionFromObj(GameObject.Find("_Game").GetComponent<CGame>().getLevel().getPlayer().getGameObject());
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void SetCurrentRoom(GameObject room)
	{
		m_CurrentRoom = room;
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void SetPositionFromRoom()
	{
		SetPositionFromObj(m_CurrentRoom);
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void SetPositionFromObj(GameObject obj)
	{
		Vector3 pos = m_GameObject.transform.position;
		pos.x =  obj.transform.position.x - 65;
		pos.y =  obj.transform.position.y + 17;
		m_GameObject.transform.position = pos;
	}
}
