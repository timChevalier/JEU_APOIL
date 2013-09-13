using UnityEngine;
using System.Collections;

public class CAnimation {
	
	public Material m_Material;
	public int m_nColumns;
	public int m_nRows;
	public float m_fFPS;
	public string[] m_sounds;
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public CAnimation(Material material, int columns, int rows, float fFPS, string[] sounds = null)
	{
		m_Material = material;
		m_nColumns = columns;
		m_nRows = rows;
		m_fFPS = fFPS;
		m_sounds = sounds ?? new string[m_nRows*m_nColumns];
	}
	

}
