using UnityEngine;
using System.Collections;

//-------------------------------------------------------------------------------
/// Run an animation CAnimation
//-------------------------------------------------------------------------------
public class CSpriteSheet // : MonoBehaviour 
{
	bool m_bIsPlaying;
	bool m_bIsForward;
	int m_nColumns = 1;
	int m_nRows = 1;
	float m_fFPS = 1.0f;
	/// <summary>
	/// ???
	/// </summary>
	float m_fTemps;
	Vector2 m_Size;
	string[] m_sounds;
	CGame game;
	
	/// Animation stop behavior : loop, stop, pingpong...
	public enum EEndCondition{
		e_Loop,
		e_PingPong,
		e_Stop
	};
	
	private EEndCondition m_endCondition;
	
	private GameObject m_parent;
	private Renderer m_myRenderer;
	private int m_nIndex = 0;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="CSpriteSheet"/> class.
	/// </summary>
	/// <param name='parent'>
	/// Parent. The animation will belong to this object
	/// </param>
	public CSpriteSheet(GameObject parent){
		m_parent=parent;
	}
	
	/// <summary>
	/// Init this instance.
	/// By default : at the beginning, forward
	/// </summary>/
	public void Init () {
		m_bIsPlaying = false;
		m_bIsForward = true;
		m_myRenderer = m_parent.renderer;
		m_fTemps = 0.0f;
		game = GameObject.Find("_Game").GetComponent<CGame>();
		m_endCondition = EEndCondition.e_Loop;
	}
	
	/// <summary>
	/// Run the animation
	/// @todo : Change m_fTemp to int
	/// Simplify function
	/// </summary>
	public void Process () {
		m_fTemps += 1.0f/m_fFPS;
		if (m_fTemps > 1.0f)
		{
			// Calculate index
			if(m_bIsForward){
				m_nIndex++;
	            if (m_nIndex >= m_nRows * m_nColumns)
	                switch(m_endCondition){
						case EEndCondition.e_Stop:
							AnimationStop();
					 		break;
						case EEndCondition.e_Loop:
							m_nIndex = 0;
							break;
						case EEndCondition.e_PingPong:
							m_nIndex--;
							Reverse();
							break;
				}
				
			}
			else {
				m_nIndex--;
	            if (m_nIndex < 0)
					switch(m_endCondition){
						case EEndCondition.e_Stop:
							AnimationStop();
					 		break;
						case EEndCondition.e_Loop:
							m_nIndex = m_nRows * m_nColumns;
							break;
						case EEndCondition.e_PingPong:
							m_nIndex++;
							Reverse();
							break;
				}
			}
			
			//Play sound if necessary
			if(m_sounds[m_nIndex] != "" && m_sounds[m_nIndex] != null)
				game.getSoundEngine().postEvent(m_sounds[m_nIndex], m_parent);
			
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
	
	/// <summary>
	/// Sets a new animation.
	/// </summary>
	/// <param name='anim'> 
	/// The animation to set
	/// </param>
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
	
	/// <summary>
	/// start animation
	/// </summary>
	public void AnimationStart()
	{
		m_bIsPlaying = true;
	}
	
	/// <summary>
	/// Stop animation
	/// </summary>
	public void AnimationStop()
	{
		m_bIsPlaying = false;	
	}
	
	/// <summary>
	/// Animation forward or backword ?
	/// </summary>
	/// <param name='forward'>
	/// true => forward, false => backword
	/// </param>
	public void SetDirection(bool forward){
		m_bIsForward = forward;
	}
	
	/// <summary>
	/// Reverse the animation (forward => backword, backword => forward).
	/// </summary>
	public void Reverse(){
		m_bIsForward = !m_bIsForward;
	}
	
	/// <summary>
	/// Sets the end behavior
	/// </summary>
	/// <param name='c'>
	/// The enging behavior
	/// </param>
	public void setEndCondition(EEndCondition c){
		m_endCondition = c;
	}
}
