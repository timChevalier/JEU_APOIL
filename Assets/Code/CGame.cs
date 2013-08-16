using UnityEngine;
using System.Collections;

public class CGame : MonoBehaviour
{
	// Objets
	public GameObject prefabPlayer;
	public Camera m_CameraCone;
	public GameObject m_Room1;
	public GameObject m_Room2;
	public GameObject m_debugDraw;
	
	// materials
	public Material m_materialPlayerRepos;
	public Material m_materialPlayerHorizontal;
	public Material m_materialPlayerVertical;
	
	// variables de LD
	public bool m_bDebug = false;
	public float m_fSpeedPlayer = 1.0f;
	
	public float m_fCoeffReverseWalk = 1.0f;
	public float m_fCoeffSlowWalk = 1.0f;
	public float m_fCoeffNormalWalk = 1.0f;
	public float m_fCoeffRunWalk = 1.0f;
	
	public float m_fAngleConeDeVision = 1.0f;
	public float m_fDistanceConeDeVision = 1f;
	public int m_fPrecisionConeDeVision = 1; 
	
	// variables
	bool m_bInGame;
	bool m_bGameStarted;
	int m_nScreenWidth;
	int m_nScreenHeight;
	CLevel m_Level;
	CCamera m_Camera;
	
	public enum ECurrentRoom //faudra mettre les vrais noms des salles
	{
		e_CurrentRoom_1,
		e_CurrentRoom_2,
		e_CurrentRoom_3
		
	};
	ECurrentRoom m_eCurrentRoom;
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void Init()
	{	
		m_Level = new CLevel();
		m_Level.Init();
		m_nScreenWidth = 1280;
		m_nScreenHeight = 800;
		m_Camera = new CCamera();
		m_Camera.Init();
		
		m_eCurrentRoom = ECurrentRoom.e_CurrentRoom_1;
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
		}
		
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void ProcessRoomState()
	{	
		switch(m_eCurrentRoom)
		{
			case ECurrentRoom.e_CurrentRoom_1:
			{
				m_Camera.SetCurrentRoom(m_Room1);
				break;	
			}
			
			case ECurrentRoom.e_CurrentRoom_2:
			{
				m_Camera.SetCurrentRoom(m_Room2);
				break;	
			}
		}
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void SetRoomState(ECurrentRoom eRoomState)
	{
		m_eCurrentRoom = eRoomState;
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void DisplayDebug()
	{
		GUI.Label(new Rect(10, 10, 100, 20), System.Convert.ToString(Time.deltaTime));
		GUI.Label(new Rect(10, 30, 100, 20), System.Convert.ToString(1f/Time.deltaTime));
		GUI.Label(new Rect(10, 50, 100, 20), System.Convert.ToString(getLevel().getPlayer().getState()));
		GUI.Label(new Rect(10, 70, 150, 20), System.Convert.ToString(m_eCurrentRoom));
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
		if (m_bDebug)
		{
			StartGame();
			m_debugDraw.SetActiveRecursively(true);
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
}
