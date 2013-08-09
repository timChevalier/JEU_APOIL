using UnityEngine;
using System.Collections;

public class CPlayer : CElement {
	
	//CGame m_game = GameObject.Find("_Game").GetComponent<CGame>();
	float m_fSpeed;
	CSpriteSheet m_spriteSheet;
	CAnimation m_AnimRepos;
	CAnimation m_AnimHorizontal;
	CAnimation m_AnimVertical;
	CConeVision m_ConeVision;
	Camera m_CameraCone;
	bool m_bGetOut;
	Vector3 m_PosObjToGetOut;
	
	public enum e_moveModState // mode de deplacement
	{
		e_moveModState_attente,
		e_moveModState_discret,
		e_moveModState_marche,
		e_moveModState_cours
	}
	
	public enum e_state //etat de l'avatar
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
	
	e_moveModState m_eMoveModState;
	e_state m_eState;
	
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public CPlayer()
	{
		CGame game = GameObject.Find("_Game").GetComponent<CGame>();
		GameObject prefab = game.prefabPlayer;
		m_GameObject = GameObject.Instantiate(prefab) as GameObject;
		
		m_ConeVision = m_GameObject.GetComponent<CConeVision>();
		m_CameraCone = game.m_CameraCone;
		
		m_fSpeed = game.m_fSpeedPlayer;
		m_bGetOut = false;
		m_PosObjToGetOut = new Vector3(0.0f,0.0f,0.0f);
		m_spriteSheet = m_GameObject.GetComponent<CSpriteSheet>();	
		
		m_AnimRepos = new CAnimation(game.m_materialPlayerRepos, 1, 1, 1.0f);
		m_AnimHorizontal = new CAnimation(game.m_materialPlayerHorizontal, 7, 4, 1.0f);
		m_AnimVertical = new CAnimation(game.m_materialPlayerVertical, 6, 1, 2.0f);
		
		m_eMoveModState = e_moveModState.e_moveModState_marche;
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	public new void Init()
	{	
		base.Init();
		
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
				if (m_eState >= e_state.e_state_nbState)
					m_eState = e_state.e_state_normal;
			}
		}
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
			bool upPressed = Input.GetKey(KeyCode.Z);
			bool downPressed = Input.GetKey(KeyCode.S);
			bool leftPressed = Input.GetKey(KeyCode.Q);
			bool rightPressed = Input.GetKey(KeyCode.D);
			
			Vector3 velocity = Vector3.zero;
			if (upPressed) 
			{ 
				velocity += new Vector3(0,1,0); 
				m_spriteSheet.SetAnimation(m_AnimVertical);
				m_spriteSheet.AnimationStart();
			}
			if (downPressed) 
			{ 
				velocity += new Vector3(0,-1,0); 
				m_spriteSheet.SetAnimation(m_AnimVertical);
				m_spriteSheet.AnimationStart();
			}
			if (leftPressed) 
			{
				velocity += new Vector3(-1,0,0); 
				m_spriteSheet.SetAnimation(m_AnimHorizontal);
				m_spriteSheet.AnimationStart();
				flipLeft();
				
			}
			if (rightPressed) { 
				velocity += new Vector3(1,0,0); 
				m_spriteSheet.SetAnimation(m_AnimHorizontal);
				m_spriteSheet.AnimationStart();
				flipRight();
			}
			if(!upPressed && !downPressed && !leftPressed && !rightPressed) 
			{
				m_spriteSheet.SetAnimation(m_AnimRepos);
				m_spriteSheet.AnimationStop();
				m_GameObject.rigidbody.velocity = Vector3.zero;
			}
			
			velocity.Normalize();
			
			if(m_bGetOut)
			{
				
				//velocity *= -1;
				Vector3 posPlayer = m_GameObject.transform.position;
				velocity += (posPlayer - m_PosObjToGetOut).normalized;
				Debug.Log((posPlayer - m_PosObjToGetOut).normalized);
			}
			
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
		float fAngle = Mathf.Acos(Vector2.Dot((posMouse - posPlayer).normalized, new Vector2(1,0)));
		if(posMouse.y < posPlayer.y)
		{
			fAngle *=-1;
		}
		float fAngleVise = -fAngle*180/3.14159f - 90 - 75/2;
		m_ConeVision.setAngleVise(fAngleVise);	
	}
		
	public void InAObject(Vector3 posObjet)
	{
		m_bGetOut = true;
		m_PosObjToGetOut = posObjet;
	}
	
	public void getOutOfObject()
	{
		m_bGetOut = false;
		m_PosObjToGetOut = new Vector3(0.0f, 0.0f, 0.0f);
	}
	
	public e_state getState()
	{
		return m_eState;	
	}
}
