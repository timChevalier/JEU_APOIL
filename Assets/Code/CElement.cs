using UnityEngine;
using System.Collections;

/// <summary>
/// Encapsulates the objects with a position (on the screen, displayable or not) in the game.
/// </summary>
public class CElement
{
	protected GameObject m_GameObject;

	public CElement(){}
	
	/// <summary>
	/// Init the instances. Virtual to be override by inheriting classes
	/// </summary>
	public virtual void Init(){}
	
	/// <summary>
	/// Reset the instances. Virtual to be override by inheriting classes
	/// </summary>
	public virtual void Reset(){}

	/// <summary>
	/// Run with the specified fDeltatime. Virtual to be override by inheriting classes
	/// </summary>
	/// <param name='fDeltatime'>
	/// F deltatime : Time between 2 updates.
	/// </param>
	public virtual void Process(float fDeltatime){}
	
	/// <summary>
	/// ???
	/// </summary>
	/// <param name='go'>
	/// Go ???
	/// </param>
	public virtual void Add(GameObject go){}
	
	/// <summary>
	/// Gets the game object.
	/// </summary>
	/// <returns>
	/// The game object.
	/// </returns>
	public GameObject getGameObject()
	{
		return m_GameObject;	
	}
	
	/// <summary>
	/// Sets the game object.
	/// </summary>
	/// <param name='gameObject'>
	/// Game object.
	/// </param>
	public void setGameObject(GameObject gameObject){
		m_GameObject = gameObject;
	}
	
	/// <summary>
	/// Sets the position of the object.
	/// </summary>
	/// <param name='pos2D'>
	/// Pos2 d.
	/// </param>
	public void SetPosition2D(Vector2 pos2D)
	{
		Vector3 pos = m_GameObject.transform.position;
		pos.x = pos2D.x;
		pos.y = pos2D.y;
		pos.z = -2;
		m_GameObject.transform.position = pos;
	}

}
