using UnityEngine;
using System.Collections;

public class CGame : MonoBehaviour
{
	// Objets
	public GameObject prefabPlayer;
	
	// materials
	public Material m_materialPlayerRepos;
	public Material m_materialPlayerHorizontal;
	public Material m_materialPlayerVertical;
	
	// variables de LD
	public bool m_bDebug = false;
	public float m_fSpeedPlayer = 1.0f;
	public float m_fAngleConeDeVision = 1.0f;
	public float m_fDistanceConeDeVision = 1f;
	public int m_fPrecisionConeDeVision = 1; 
	
	CLevel m_Level;
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void Init()
	{	
		m_Level = new CLevel();
		m_Level.Init();
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void Reset()
	{
		m_Level.Reset();
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void Process(float fDeltatime)
	{
		m_Level.Process(fDeltatime);
		
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void DisplayDebug()
	{
		GUI.Label(new Rect(10, 10, 100, 20), System.Convert.ToString(1f/Time.deltaTime));
	}
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void OnGUI() {
        if (m_bDebug)
			DisplayDebug();
    }
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void Start()
	{
		Init();
	}
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void Update()
	{
		Process(Time.deltaTime);
	}
}
