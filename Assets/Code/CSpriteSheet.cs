using UnityEngine;
using System.Collections;

public class CSpriteSheet // : MonoBehaviour 
{
	bool m_bIsPlaying;
	int m_nColumns = 1;
	int m_nRows = 1;	
	float m_fFPS = 1.0f;
	float m_fTemps;
	Vector2 m_Size;
	string[] m_sounds;
	
	private GameObject m_parent;
	private Renderer m_myRenderer;
	private int m_nIndex = 0;
	
	public CSpriteSheet(GameObject parent){
		m_parent=parent;
	}
	
	//-------------------------------------------------------------------------------
	///	
	//-------------------------------------------------------------------------------
	public void Init () {
		m_bIsPlaying = false;
		m_myRenderer = m_parent.renderer;
		m_fTemps = 0.0f;
	}
	
	//-------------------------------------------------------------------------------
	/// 
	//-------------------------------------------------------------------------------
	public void Process () {
		m_fTemps += 1.0f/m_fFPS;
		if (m_fTemps > 1.0f)
		{
			// Calculate index
			m_nIndex++;
            if (m_nIndex >= m_nRows * m_nColumns)
                m_nIndex = 0;	
			
			//Play sound if necessary
			if(m_sounds[m_nIndex] != "" && m_sounds[m_nIndex] != null)
				GameObject.Find("_Game").GetComponent<CGame>().getSoundEngine().postEvent(m_sounds[m_nIndex], m_parent);
			
			m_fTemps = 0.0f;
		}
		
		 Vector2 offset = new Vector2((float)m_nIndex / m_nColumns - (m_nIndex / m_nColumns), //x index
                                      1 - ((m_nIndex / m_nColumns) / (float)m_nRows));    //y index
		
		Vector2 textureSize = new Vector2(1f / m_nColumns, 1f / m_nRows);
        // Reset the y offset, if needed
        if (offset.y == 1)
          offset.y = 0.0f;
		
		// If we have scaled the texture, we need to reposition the texture to the center of the object
        offset.x += ((1f / m_nColumns) - textureSize.x) / 2.0f;
        offset.y += ((1f / m_nRows) - textureSize.y) / 2.0f;
  
		m_myRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
	}	
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void SetAnimation(CAnimation anim)
	{
		m_nColumns = anim.m_nColumns;
		m_nRows = anim.m_nRows;
		m_Size = new Vector2 (1.0f / m_nColumns , 1.0f / m_nRows);
		m_myRenderer.material = anim.m_Material;
		m_myRenderer.material.SetTextureScale("_MainTex", m_Size);
		m_fFPS = anim.m_fFPS;
		m_sounds = anim.m_sounds;
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void AnimationStart()
	{
		m_bIsPlaying = true;
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void AnimationStop()
	{
		m_bIsPlaying = false;
	}
}
