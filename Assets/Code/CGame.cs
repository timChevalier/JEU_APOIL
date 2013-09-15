using UnityEngine;
using System.Collections;

public class CGame : MonoBehaviour
{
	// Objets
	public GameObject prefabPlayer;
	public GameObject prefabMonster;
	public Camera m_CameraCone;
	public GameObject m_debugDraw;
	public GameObject m_renderScreen;	
	
	// materials
	public Material m_materialPlayerRepos;
	public Material m_materialPlayerHorizontal;
	public Material m_materialPlayerVertical;
	public Material m_materialDEBUGscreen;
	
	// variables de LD
	public bool m_bDebug = false;
	public bool m_bDebugRendu = false;
	public bool m_bNotUseMasterGame = false;
	public float m_fSpeedPlayer = 1.0f;
	public float m_fSpeedMonster = 1.0f;
	
	public float m_fCoeffReverseWalk = 1.0f;
	public float m_fCoeffSlowWalk = 1.0f;
	public float m_fCoeffNormalWalk = 1.0f;
	public float m_fCoeffRunWalk = 1.0f;
	
	public float m_fDiscretionBaseRadius = 30;
	public float m_fCoeffDiscretionAttente = 0;
	public float m_fCoeffDiscretionDiscret = 1;
	public float m_fCoeffDiscretionMarche = 4;
	public float m_fCoeffDiscretionCours = 10;
	
	public float m_fAngleConeDeVision = 1.0f;
	public float m_fDistanceConeDeVision = 1f;
	public int m_fPrecisionConeDeVision = 1; 
	public float m_fMonsterTimeErrance = 2.0f;
	public float m_fMonsterRadiusAlerte = 1.0f;
	
	public bool m_BMute = false;
	public string soundbankName = "Jeu_apoil.bnk";
	
	
	// variables
	bool m_bInGame;
	bool m_bGameStarted;
	int m_nScreenWidth;
	int m_nScreenHeight;
	CLevel m_Level;
	CCamera m_Camera;
	CSoundEngine m_SoundEngine;
	
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void Init()
	{	
		
		m_SoundEngine = new CSoundEngine();
		m_SoundEngine.Init();
		m_SoundEngine.LoadBank(soundbankName);
		
		m_Level = new CLevel();
		m_Level.Init();
		m_nScreenWidth = 1280;
		m_nScreenHeight = 800;
		m_Camera = new CCamera();
		m_Camera.Init();
		
		
		
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void Reset()
	{
		m_Level.Reset();
		m_Camera.Reset();
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void Process(float fDeltatime)
	{
		
		if(m_bInGame)
		{
			m_Level.Process(fDeltatime);
			m_Camera.Process(fDeltatime);
			//ProcessRoomState();
			
			//Quit on Escape
			if(Input.GetKey(KeyCode.Escape))
				Application.Quit();
		}
		
	}

	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void DisplayDebug()
	{
		GUI.Label(new Rect(10, 10, 100, 20), System.Convert.ToString(Time.deltaTime));
		GUI.Label(new Rect(10, 30, 100, 20), System.Convert.ToString(1f/Time.deltaTime));
		GUI.Label(new Rect(10, 50, 100, 20), System.Convert.ToString(getLevel().getPlayer().getState()));
		GUI.Label(new Rect(10, 70, 150, 20), System.Convert.ToString(getLevel().getMonster().getState()));
	}
	
	//-------------------------------------------------------------------------------
	/// 
	//-------------------------------------------------------------------------------
	public CLevel getLevel()
	{
		return m_Level;
	}
	
	//-------------------------------------------------------------------------------
	/// 
	//-------------------------------------------------------------------------------
	public Vector2 GetSizeScreen()
	{
		return new Vector2(m_nScreenWidth, m_nScreenHeight);
	}
	
	//-------------------------------------------------------------------------------
	/// 
	//-------------------------------------------------------------------------------
	public void StartGame()
	{
		if(!m_bGameStarted)
		{
			Init();
			m_bGameStarted = true;
		}
		
		m_bInGame = true;	
	}
	
	//-------------------------------------------------------------------------------
	/// 
	//-------------------------------------------------------------------------------
	public bool IsDebug()
	{
		return m_bDebug;	
	}
	
	//-------------------------------------------------------------------------------
	/// 
	//-------------------------------------------------------------------------------
	public bool IsDebugRendu()
	{
		return m_bDebugRendu;	
	}
	
	//-------------------------------------------------------------------------------
	/// 
	//-------------------------------------------------------------------------------
	public bool IsNotUseMasterGame()
	{
		return m_bNotUseMasterGame;	
	}
	
	//-------------------------------------------------------------------------------
	/// 
	//-------------------------------------------------------------------------------
	public void PauseGame()
	{
		m_bInGame = false;	
	}
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void OnGUI() 
	{
        if (m_bDebug)
		{
			DisplayDebug();
		}

    }

	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void Start()
	{
		m_bInGame = false;
		m_bGameStarted = false;
		if (m_bNotUseMasterGame)
		{
			StartGame();
		}
		if (m_bDebugRendu)
		{
			m_debugDraw.SetActiveRecursively(true);
			m_renderScreen.renderer.material = m_materialDEBUGscreen;
		}
	}
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void Update()
	{
		if(m_bInGame)
		{
			Process(Time.deltaTime);
		}
	}
	
	//-------------------------------------------------------------------------------
	/// 
	//-------------------------------------------------------------------------------
	public CCamera getCamera()
	{
		return m_Camera;	
	}
	
	public CSoundEngine getSoundEngine(){
		return m_SoundEngine;
	}
}
