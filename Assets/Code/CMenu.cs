using UnityEngine;
using System.Collections;

public class CMenu : MonoBehaviour{
	
	enum e_menu_state
	{
		e_menu_state_splash,
		e_menu_state_main,
		e_menu_state_credits,
		e_menu_state_inGame
	}
	
	e_menu_state e_state;
	
	public Texture m_Texture_Fond;
	public Texture m_Texture_ButtonPlay;
	public Texture m_Texture_ButtonCredit;
	public Texture m_Texture_ButtonMenu;
	public Texture m_Texture_ButtonQuit;
	public Texture m_Texture_Splash;
	public Texture m_Texture_Credit;
	
	
	float m_fTempsSplash;
	const float m_fTempsSplashInit = 2.0f;
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void Start()
	{
		CGame game = gameObject.GetComponent<CGame>();
		
		if(!game.IsDebug())	
		{
			e_state = e_menu_state.e_menu_state_splash;
			m_fTempsSplash = m_fTempsSplashInit;
		}
		else {
			e_state = e_menu_state.e_menu_state_inGame;
		}
	}
	
	//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void Update()
	{
		if(e_state == e_menu_state.e_menu_state_splash && m_fTempsSplash>0.0f)
			m_fTempsSplash -= Time.deltaTime;
	}
	
//-------------------------------------------------------------------------------
	/// Unity
	//-------------------------------------------------------------------------------
	void OnGUI() 
	{
		CGame game = gameObject.GetComponent<CGame>();
		switch(e_state)
		{
			case e_menu_state.e_menu_state_splash:
			{
				if(m_fTempsSplash > 0.0f)
				{
					float fCoeffScale = 1.0f + (m_fTempsSplashInit - m_fTempsSplash)/(10.0f*m_fTempsSplashInit);
					float fWidth = 1280 * fCoeffScale;
					float fHeight = 800 * fCoeffScale;
					GUI.DrawTexture(new Rect((1280 - fWidth)/2.0f, (800 - fHeight)/2.0f, fWidth, fHeight), m_Texture_Splash);
				}
				else
					e_state = e_menu_state.e_menu_state_main;
				break;
			}	
			
			case e_menu_state.e_menu_state_main:
			{
				GUI.DrawTexture(new Rect(0, 0, 1280, 800), m_Texture_Fond);
			
				if (GUI.Button(new Rect(390, 100, 500, 150), m_Texture_ButtonPlay))
				{
		            game.StartGame();
					e_state = e_menu_state.e_menu_state_inGame;
				}
				
				if (GUI.Button(new Rect(390, 400, 500, 150), m_Texture_ButtonCredit))
				{
					e_state = e_menu_state.e_menu_state_credits;
				}
			
				if (GUI.Button(new Rect(940, 10, 200, 60), m_Texture_ButtonMenu))
				{
					e_state = e_menu_state.e_menu_state_main;
				}
			
				if (GUI.Button(new Rect(1160, 10, 60, 60), m_Texture_ButtonQuit))
				{
					Application.Quit();
				}
				break;
			}	
			
			case e_menu_state.e_menu_state_credits:
			{
				GUI.DrawTexture(new Rect(0, 0, 1280, 800), m_Texture_Credit);
			
				if (GUI.Button(new Rect(940, 10, 200, 60), m_Texture_ButtonMenu))
				{
					e_state = e_menu_state.e_menu_state_main;
				}
			
				if (GUI.Button(new Rect(1160, 10, 60, 60), m_Texture_ButtonQuit))
				{
					Application.Quit();
				}
				break;
			}	
			
			case e_menu_state.e_menu_state_inGame:
			{
				//if (GUI.Button(new Rect(940, 10, 200, 60), m_Texture_ButtonMenu))
				if (GUI.Button(new Rect(10, 10, 200, 60), m_Texture_ButtonMenu))
				{
					e_state = e_menu_state.e_menu_state_main;
				}
			
				if (GUI.Button(new Rect(1160, 10, 60, 60), m_Texture_ButtonQuit))
				{
					Application.Quit();
				}
				break;
			}	
		}
		
		
    }
	
	
}
