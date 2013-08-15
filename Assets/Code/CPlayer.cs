using UnityEngine;
using System.Collections;

public class CPlayer : CCharacter {
	
	// a foutre dans le constructeur si on veut pouvoir le faire indifferement des jouerus
	int m_nResistance = 5;
	int m_nRadiusDiscrectionCircle = 10;
	
	//CGame m_game = GameObject.Find("_Game").GetComponent<CGame>();
	float m_fSpeed;
	CSpriteSheet m_spriteSheet;
	CAnimation m_AnimRepos;
	CAnimation m_AnimHorizontal;
	CAnimation m_AnimVertical;
	CConeVision m_ConeVision;
	
	Camera m_CameraCone;
	Vector2 m_DirectionRegard;
	Vector2 m_DirectionDeplacement;
	bool m_bMainCharacter;
	
	public enum EMoveModState // mode de deplacement
	{
		e_MoveModState_attente,
		e_MoveModState_discret,
		e_MoveModState_marche,
		e_MoveModState_cours
	}
	
	public enum EState //etat de l'avatar
	{
		e_state_normal,
		e_state_enflamme,
		e_state_oxygeneManque,
		e_state_empoisonne,
		e_state_parasite,
		e_state_frigorifie,
		e_state_aveugle,
		
		e_state_nbState
	}
	
	EMoveModState m_eMoveModState;
	EState m_eState;
	
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public CPlayer(bool bIsMainCharacter = false)
	{
		CGame game = GameObject.Find("_Game").GetComponent<CGame>();
		GameObject prefab = game.prefabPlayer;
		m_GameObject = GameObject.Instantiate(prefab) as GameObject;
		
		m_ConeVision = m_GameObject.GetComponent<CConeVision>();
		m_CameraCone = game.m_CameraCone;
		
		m_fSpeed = game.m_fSpeedPlayer;
		m_spriteSheet = m_GameObject.GetComponent<CSpriteSheet>();	
		
		m_AnimRepos = new CAnimation(game.m_materialPlayerRepos, 1, 1, 1.0f);
		m_AnimHorizontal = new CAnimation(game.m_materialPlayerHorizontal, 7, 4, 1.0f);
		m_AnimVertical = new CAnimation(game.m_materialPlayerVertical, 6, 1, 2.0f);
		
		m_eMoveModState = EMoveModState.e_MoveModState_marche;
		m_bMainCharacter = bIsMainCharacter;
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	public new void Init()
	{	
		base.Init();
		
		//Appel a la main des scripts du gameObject
		m_spriteSheet.Init();
		m_ConeVision.Init();
		//m_spriteSheet.SetAnimation(m_AnimRepos);
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public new void Reset()
	{
		base.Reset();
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	public new void Process(float fDeltatime)
	{
		base.Process(fDeltatime);
		EventClavier(fDeltatime);
		EventSouris(fDeltatime);
		
		CGame game = GameObject.Find("_Game").GetComponent<CGame>();
		if(game.IsDebug())
		{
			if(Input.GetKeyDown(KeyCode.A))
			{
				m_eState = (m_eState + 1);
				if (m_eState >= EState.e_state_nbState)
					m_eState = EState.e_state_normal;
			}
		}
		
		//Appel a la main des scripts du gameObject
		m_spriteSheet.Process();
		if(m_bMainCharacter)
			m_ConeVision.Process();
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	void CalculateSpeed()
	{
		CGame game = GameObject.Find("_Game").GetComponent<CGame>();
		float fVitesseEtat = 1.0f;
		float fVitesseAttitude;
		switch(m_eMoveModState)
		{
			case EMoveModState.e_MoveModState_attente:
			{
				fVitesseAttitude = 0.0f;
				break;
			}
			case EMoveModState.e_MoveModState_discret:
			{
				fVitesseAttitude = game.m_fCoeffSlowWalk;
				break;
			}
			case EMoveModState.e_MoveModState_marche:
			{
				fVitesseAttitude = game.m_fCoeffNormalWalk;
				break;
			}
			case EMoveModState.e_MoveModState_cours	:
			{
				fVitesseAttitude = game.m_fCoeffRunWalk;
				break;
			}
			default: fVitesseAttitude = 1.0f; break;
		}
		
		float fCoeffDirection = Vector2.Dot(m_DirectionRegard, m_DirectionDeplacement);
		fCoeffDirection = game.m_fCoeffReverseWalk + (1.0f - game.m_fCoeffReverseWalk)*(fCoeffDirection + 1)/2;
		
		m_fSpeed = game.m_fSpeedPlayer * fVitesseEtat * fVitesseAttitude * fCoeffDirection;
	}

	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void flipRight ()
	{
		if (m_GameObject.transform.localScale.x < 0)
		{
			Vector3 nTrans = m_GameObject.transform.localScale;
			nTrans.x *= -1;
			m_GameObject.transform.localScale = nTrans;
		}
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void flipLeft ()
	{
		if (m_GameObject.transform.localScale.x > 0)
		{
			Vector3 nTrans = m_GameObject.transform.localScale;
			nTrans.x *= -1;
			m_GameObject.transform.localScale = nTrans;
		}
	}

	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void EventClavier(float fDeltatime)
	{
		if (m_GameObject.rigidbody != null)
		{
			bool bUpPressed = Input.GetKey(KeyCode.Z);
			bool bDownPressed = Input.GetKey(KeyCode.S);
			bool bLeftPressed = Input.GetKey(KeyCode.Q);
			bool bRightPressed = Input.GetKey(KeyCode.D);
			bool bRunPressed = Input.GetKey(KeyCode.LeftShift);
			bool bSlowPressed = Input.GetKey(KeyCode.LeftControl);
			
			Vector3 velocity = Vector3.zero;
			if (bUpPressed) 
			{ 
				velocity += new Vector3(0,1,0); 
				m_spriteSheet.SetAnimation(m_AnimVertical);
				m_spriteSheet.AnimationStart();
				m_eMoveModState = EMoveModState.e_MoveModState_marche;
			}
			if (bDownPressed) 
			{ 
				velocity += new Vector3(0,-1,0); 
				m_spriteSheet.SetAnimation(m_AnimVertical);
				m_spriteSheet.AnimationStart();
				m_eMoveModState = EMoveModState.e_MoveModState_marche;
			}
			if (bLeftPressed) 
			{
				velocity += new Vector3(-1,0,0); 
				m_spriteSheet.SetAnimation(m_AnimHorizontal);
				m_spriteSheet.AnimationStart();
				flipLeft();
				m_eMoveModState = EMoveModState.e_MoveModState_marche;
				
			}
			if (bRightPressed) { 
				velocity += new Vector3(1,0,0); 
				m_spriteSheet.SetAnimation(m_AnimHorizontal);
				m_spriteSheet.AnimationStart();
				flipRight();
				m_eMoveModState = EMoveModState.e_MoveModState_marche;
			}
			if(!bUpPressed && !bDownPressed && !bLeftPressed && !bRightPressed) 
			{
				m_spriteSheet.SetAnimation(m_AnimRepos);
				m_spriteSheet.AnimationStop();
				m_eMoveModState = EMoveModState.e_MoveModState_attente;
				m_GameObject.rigidbody.velocity = Vector3.zero;
			}
			
			velocity.Normalize();
			m_DirectionDeplacement = velocity;
			
			if(bRunPressed)
			{
				m_eMoveModState = EMoveModState.e_MoveModState_cours;
			}	
			if(bSlowPressed)
			{
				m_eMoveModState = EMoveModState.e_MoveModState_discret;	
			}
			
			CalculateSpeed();
			
			m_GameObject.transform.position += m_fSpeed * velocity * fDeltatime;	
		}
		else
		{
			Debug.LogError("Pas de rigid body sur "+m_GameObject.name);
		}
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void EventSouris(float fDeltatime)
	{
		Vector3 posMouseTmp = Vector3.zero;
		RaycastHit vHit = new RaycastHit();
	    Ray vRay = m_CameraCone.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(vRay, out vHit, 1000)) 
		{
			posMouseTmp = vHit.point;
		}
		
		//Vector3 posMouse = vRay.origin;
		Vector3 posPlayerTmp = m_GameObject.transform.position;
		Vector2 posMouse = new Vector2(posMouseTmp.x, posMouseTmp.y);
		Vector2 posPlayer = new Vector2(posPlayerTmp.x, posPlayerTmp.y);
		m_DirectionRegard = (posMouse - posPlayer).normalized;
		float fAngle = Mathf.Acos(Vector2.Dot(m_DirectionRegard, new Vector2(1,0)));
		if(posMouse.y < posPlayer.y)
		{
			fAngle *=-1;
		}
		float fAngleVise = -fAngle*180/3.14159f - 90 - 75/2;
		m_ConeVision.setAngleVise(fAngleVise);	
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public EState getState()
	{
		return m_eState;	
	}
	
	public Vector2 getDirectionDeplacement()
	{
		return m_DirectionDeplacement;	
	}
}
