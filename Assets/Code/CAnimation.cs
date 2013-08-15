using UnityEngine;
using System.Collections;

public class CAnimation {
	
	public Material m_Material;
	public int m_nColumns;
	public int m_nRows;
	public float m_fFPS;
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public CAnimation(Material material, int columns, int rows, float fFPS)
	{
		m_Material = material;
		m_nColumns = columns;
		m_nRows = rows;
		m_fFPS = fFPS;
	}

}
