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
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public CPlayer()
	{
		CGame game = GameObject.Find("_Game").GetComponent<CGame>();
		GameObject prefab = game.prefabPlayer;
		m_GameObject = GameObject.Instantiate(prefab) as GameObject;
		
		m_ConeVision = m_GameObject.GetComponent<CConeVision>();
		
		m_fSpeed = game.m_fSpeedPlayer;
		m_spriteSheet = m_GameObject.GetComponent<CSpriteSheet>();	
		
		m_AnimRepos = new CAnimation(game.m_materialPlayerRepos, 1, 1, 1.0f);
		m_AnimHorizontal = new CAnimation(game.m_materialPlayerHorizontal, 7, 4, 1.0f);
		m_AnimVertical = new CAnimation(game.m_materialPlayerVertical, 6, 1, 2.0f);
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------	
	public new void Init()
	{	
		base.Init();
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
				velocity += m_fSpeed * new Vector3(0,1,0); 
				m_spriteSheet.SetAnimation(m_AnimVertical);
				m_spriteSheet.AnimationStart();
			}
			if (downPressed) 
			{ 
				velocity += m_fSpeed * new Vector3(0,-1,0); 
				m_spriteSheet.SetAnimation(m_AnimVertical);
				m_spriteSheet.AnimationStart();
			}
			if (leftPressed) 
			{
				velocity += m_fSpeed * new Vector3(-1,0,0); 
				m_spriteSheet.SetAnimation(m_AnimHorizontal);
				m_spriteSheet.AnimationStart();
				flipLeft();
				
			}
			if (rightPressed) { 
				velocity += m_fSpeed * new Vector3(1,0,0); 
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
			
			m_GameObject.transform.position += velocity * fDeltatime;	
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
	    Ray vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
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
}
