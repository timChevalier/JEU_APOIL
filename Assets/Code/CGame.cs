using UnityEngine;
using System.Collections;

/// <summary>
/// Instance of the game
/// This class contains elements of the motor : the camera, the sound engine, the screen resolution
/// It also contains the game objects : the characters of the games (players, monsters...), the Level
/// </summary>
public class CGame : MonoBehaviour
{
	// Objets
	public GameObject prefabPlayer;
	public GameObject prefabMonster;
	public Camera m_CameraCone;
	public GameObject m_debugDraw; // ???
	public GameObject m_renderScreen;
	
	// player materials
	public Material m_materialPlayerRepos;
	public Material m_materialPlayerHorizontal;
	public Material m_materialPlayerVertical;
	public Material m_materialDEBUGscreen;
	
	// variables de LD ??????
	public bool m_bPadXBox = false;
	public bool m_bDebug = false;
	public bool m_bDebugRendu = false;
	public bool m_bNotUseMasterGame = false;
	public float m_fSpeedPlayer = 1.0f;
	public float m_fSpeedMonster = 1.0f; // what is that ? Monster speed is already defined in the class monster.
	
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
	public bool m_bLightIsOn = true;
	
	
	// variables
	bool m_bInGame; // pause boolean
	bool m_bGameStarted;
	int m_nScreenWidth;
	int m_nScreenHeight;
	CLevel m_Level;
	CCamera m_Camera;
	CSoundEngine m_SoundEngine;
	
	/// <summary>
	/// Init CSoundEngine, CLevel, the screen size and CCamera
	/// </summary>
	public void Init()
	{		
		m_SoundEngine = new CSoundEngine();
		m_SoundEngine.Init();
		m_SoundEngine.LoadBank(soundbankName);
		
		m_Level = new CLevel();
		m_Level.Init();
		m_nScreenWidth = 1024;
		m_nScreenHeight = 768;
		m_Camera = new CCamera();
		m_Camera.Init();
	}
	
	/// <summary>
	/// Reset CLevel and CCamera
	/// </summary>
	void Reset()
	{
		m_Level.Reset();
		m_Camera.Reset();
	}
	
	/// <summary>
	/// Run the game at the specified fDeltatime.
	/// </summary>
	/// <param name='fDeltatime'>
	/// F deltatime.
	/// </param>
	void Process(float fDeltatime)
	{	
		if(m_bInGame)
		{
			CApoilInput.Process(fDeltatime);
			m_Level.Process(fDeltatime);
			m_Camera.Process(fDeltatime);
			//ProcessRoomState();
			//Quit on Escape
			if(Input.GetKey(KeyCode.Escape))
				Application.Quit();
		}
		
	}

	/// <summary>
	/// Displaies debug messages.
	/// </summary>
	void DisplayDebug()
	{
		GUI.Label(new Rect(10, 10, 100, 20), System.Convert.ToString(Time.deltaTime));
		GUI.Label(new Rect(10, 30, 100, 20), System.Convert.ToString(1f/Time.deltaTime));
		GUI.Label(new Rect(10, 50, 100, 20), System.Convert.ToString(getLevel().getPlayer().getState()));
		GUI.Label(new Rect(10, 70, 150, 20), System.Convert.ToString(getLevel().getMonster().getState()));
	}
	
	/// <summary>
	/// Gets the level.
	/// </summary>
	/// <returns>
	/// Current level.
	/// </returns>
	public CLevel getLevel()
	{
		return m_Level;
	}
	
	/// <summary>
	/// Gets the size screen.
	/// </summary>
	/// <returns>
	/// The size screen.
	/// </returns>
	public Vector2 GetSizeScreen()
	{
		return new Vector2(m_nScreenWidth, m_nScreenHeight);
	}
	
	/// <summary>
	/// Starts the game if not already started
	/// </summary>
	public void StartGame()
	{
		if(!m_bGameStarted)
		{
			Init();
			m_bGameStarted = true;
		}
		
		m_bInGame = true;	
	}

	
	/// <summary>
	/// Determines whether this instance is pad X box mod.
	/// </summary>
	/// <returns>
	/// <c>true</c> if this instance is pad X box mod; otherwise, <c>false</c>.
	/// </returns>
	public bool IsPadXBoxMod()
	{
		return m_bPadXBox;	
	}
	
	/// <summary>
	/// Determines whether this instance is debug.
	/// </summary>
	/// <returns>
	/// <c>true</c> if this instance is debug; otherwise, <c>false</c>.
	/// </returns>
	public bool IsDebug()
	{
		return m_bDebug;	
	}
	
	/// <summary>
	/// Determines whether this instance is debug rendu.
	/// </summary>
	/// <returns>
	/// <c>true</c> if this instance is debug rendu; otherwise, <c>false</c>.
	/// </returns>
	public bool IsDebugRendu()
	{
		return m_bDebugRendu;	
	}
	
	/// <summary>
	/// Determines whether this instance is not use master game.
	/// </summary>
	/// <returns>
	/// <c>true</c> if this instance is not use master game; otherwise, <c>false</c>.
	/// </returns>
	public bool IsNotUseMasterGame()
	{
		return m_bNotUseMasterGame;	
	}
	
	/// <summary>
	/// Pauses the game.
	/// </summary>
	public void PauseGame()
	{
		m_bInGame = false;	
	}
	
	/// <summary>
	/// Raises the GU event. Display debug if in debug mode.
	/// </summary>
	void OnGUI() 
	{
        if (m_bDebug)
		{
			DisplayDebug();
		}

    }

	
	/// <summary>
	/// Start this instance. ???
	/// Need more comments...
	/// </summary>
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
	
	/// <summary>
	/// Update the game.
	/// </summary>
	void Update()
	{
		if(m_bInGame)
		{
			Process(Time.deltaTime);
		}
	}
	
	/// <summary>
	/// Gets the camera object.
	/// </summary>
	/// <returns>
	/// The camera object.
	/// </returns>
	public CCamera getCamera()
	{
		return m_Camera;	
	}
	
	public CSoundEngine getSoundEngine(){
		return m_SoundEngine;
	}
}
