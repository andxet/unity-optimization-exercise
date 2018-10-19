using UnityEngine;

public class cTarget : MonoBehaviour 
{
	bool m_bSelfDestruct = false;
	float m_fSpeed = 1.0f;
	float m_fTargetOffsetZ = 0.0f;
	float m_fSelfDestructTimer = 0.0f;

	void Start()
	{
		m_fTargetOffsetZ = transform.position.z;
		m_fSpeed = Random.Range( 1.3f, 2.2f );
	}
	
	void Update()
	{
		if( m_bSelfDestruct )
		{
			m_fSelfDestructTimer -= Time.deltaTime;
			
			if( m_fSelfDestructTimer <= 0.0f )
			{
				m_bSelfDestruct = false;
				DestroyNow();
			}
		}
		else
		{
			Vector3 newPosition = transform.position + new Vector3( m_fSpeed, 0.0f, 0.0f ) * Time.deltaTime;
			transform.position = newPosition;

			if( transform.position.x > 3.0f )
			{
				DestroyNow();
			}
		}
	}

	public void DelayedDestroy()
	{	
		m_bSelfDestruct = true;
		m_fSelfDestructTimer = 1.0f;

		GetComponentInChildren<Renderer>().material.color = Color.red;
	}

	void DestroyNow()
	{
		cTargetManager targetManager = GameObject.FindObjectOfType<cTargetManager>().GetComponent<cTargetManager>();
		if( targetManager != null )
		{
			targetManager.TargetDestroyed( m_fTargetOffsetZ );
		}
		
		Destroy( gameObject );
	}
	
	public bool IsAlive()
	{
		return m_bSelfDestruct == false;
	}
}