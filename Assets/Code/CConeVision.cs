using UnityEngine;
using System.Collections;

public class CConeVision : MonoBehaviour 
{   
	
	float m_fAngleVise;
	float m_fAngleViseCible;
	float m_fAngleViseSpeed;
	
	public Material m_Material; 	// Mat appliqué au mesh de vue
	public bool debug = false; 		// Dessine les rayons dans la scene view
	public LayerMask mask;		 	// Layers qui vont bloquer la vue
   
	Vector3[] directions; // va contenir les rayons, déterminés par precision, distance et angle
	Mesh sightMesh; // Le mesh dont les vertex seront modifiés selons les obstacles
	Transform m_Transform;
	GameObject m_gameObject;
   
	float angle; // Angle d'ouverture, degré
	float distance;
	int precision; // Nombre de rayons lancé dans l'angle ci dessus
	int nbPoints;
	int nbTriangle;
	int nbFace;
	int nbIndice;
	int row;
	Vector3[] points;
	int[] indices;
   
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void setAngleVise(float fAngle)
	{
		m_fAngleViseCible = fAngle%360;
	}
	
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	void setDirection()
	{
		// Préparation des rayons
		precision = precision > 1 ? precision : 2;
		directions = new Vector3[precision];
		float angle_start = m_fAngleVise;
		//float angle_start = -angle*0.5F;
		float angle_step = angle / (precision-1);
		for( int i = 0; i < precision; i++ )
		{
			Matrix4x4 mat = Matrix4x4.TRS( Vector3.zero, Quaternion.Euler(0,angle_start + i*angle_step,0), Vector3.one );
			directions[i] = mat * Vector3.forward;
		}	
	}
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
 	public  void Init () 
	{
		CGame game = GameObject.Find("_Game").GetComponent<CGame>();
		angle = game.m_fAngleConeDeVision;
		distance = game.m_fDistanceConeDeVision;
		precision = game.m_fPrecisionConeDeVision;
		m_fAngleViseSpeed = game.m_fVitesseConeDeVision;
		// Initialisation du cone
		m_gameObject = new GameObject( "ConeSight" );
		sightMesh = new Mesh();
		
		(m_gameObject.AddComponent( typeof( MeshFilter )) as MeshFilter).mesh = sightMesh;
		(m_gameObject.AddComponent( typeof( MeshRenderer )) as MeshRenderer).material = m_Material;
		m_gameObject.layer = LayerMask.NameToLayer("Cone");
		m_gameObject.tag = "cone";
		m_Transform = game.getLevel().getPlayer().getGameObject().transform; //transform; // histoire de limiter les getcomponent dans la boucle
		
		// Préparation des rayons
		setDirection();
		
		// préparations des outils de manipulation du mesh
		nbPoints =  precision*2;
		nbTriangle = nbPoints - 2;
		nbFace = nbTriangle / 2;
		nbIndice =  nbTriangle * 3;
		row = nbFace+1;
		
		points = new Vector3[nbPoints];
		indices = new int[ nbIndice ];
		
		for( int i = 0; i < nbFace; i++ )
		{
			indices[i*6+0] = i+0;
			indices[i*6+1] = i+1;
			indices[i*6+2] = i+row;
			indices[i*6+3] = i+1;
			indices[i*6+4] = i+row+1;
			indices[i*6+5] = i+row;
		}
		
		sightMesh.vertices = new Vector3[nbPoints];
		sightMesh.uv = new Vector2[nbPoints];
		sightMesh.triangles = indices;      
		    
		
		//StartCoroutine( "Scan" );
	}
   
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	public void Process() 
	{
		UpdateAngleVise();
		UpdateSightMesh();
	}
	
	private void UpdateAngleVise()
	{
		if(m_fAngleVise != m_fAngleViseCible)
		{
			m_fAngleVise= Mathf.MoveTowardsAngle(m_fAngleVise, m_fAngleViseCible, m_fAngleViseSpeed);
		}

	}
   
	// Fonction qui modifie le mesh
	//-------------------------------------------------------------------------------
	///
	//-------------------------------------------------------------------------------
	private void UpdateSightMesh()
	{         
		setDirection();	
		
		// Lance les rayons pour placer les vertices le plus loin possible
		for( int i = 0; i < precision; i++ )
		{
			Vector3 dir = m_Transform.TransformDirection(directions[i]); // repere objet
			RaycastHit hit;
			float dist = distance;
			if(Physics.Raycast( m_Transform.position, dir, out hit, distance, mask ) ) // Si on touche, on rétrécit le rayon
			{
				CGameObject objet = hit.collider.gameObject.GetComponent<CGameObject>();	
				if(!hit.collider.gameObject.tag.Equals("player") && hit.collider.gameObject.GetComponent<CGameObject>() != null)
				{
					dist = hit.distance;
					objet.SetVisible();
				}
				
			}
			 
			if( debug ) Debug.DrawRay( m_Transform.position, dir * dist );
			
			// Positionnement du vertex
			points[i] = dir * dist;
			points[i+precision] = Vector3.zero;
		}
		
		// On réaffecte les vertices
		sightMesh.vertices = points;  
		sightMesh.RecalculateNormals(); // normales doivent être calculé pour tout changement 
		                      			// du tableau vertices, même si on travaille sur un plan*/
		
		//translate le centre a la position du player!!! J'AI PASSE 3 PUTAINS DE JOUR POUR TROUVER QU'IL FALLAIT FAIRE LE CONE EN (0,0) ET LE TRANSLATER ENSUITE!!!
		m_gameObject.transform.position = m_Transform.position;
	}
	
	
	
}