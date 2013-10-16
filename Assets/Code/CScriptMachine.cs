using UnityEngine;
using System.Collections;

public class CScriptMachine : MonoBehaviour {
	
	CGame m_Game;
	CMachine m_Machine;
	CAnimation m_Animation;
	
	public Material m_material;
	public int m_columns, m_rows;
	public float m_fFPS;
	public string[] m_sounds = {""};
	
	// Use this for initialization
	void Start() 
	{
		m_Game = GameObject.Find("_Game").GetComponent<CGame>();
		m_Animation = new CAnimation(m_material, m_columns, m_rows, m_fFPS);
		m_Game.getLevel().CreateElement<CMachine>(gameObject);
	}
	
	// Update is called once per frame
	void Update() 
	{
	
	}
	
	public void SetMachine(CMachine obj)
	{
		m_Machine = obj;
	}
	
	public CAnimation GetAnimation(){ 
		return m_Animation;
	}
}
