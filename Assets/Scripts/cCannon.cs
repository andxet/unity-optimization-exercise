using UnityEngine;

public class cCannon : MonoBehaviour 
{
	public GameObject m_CannonballHolder;
	public GameObject m_CannonballTemplate;
	public int m_ID = 0;

	float m_fShotCooldown = 0.0f;

	void Awake()
	{
		m_fShotCooldown = Random.Range( 0.0f, 2.0f );
	}

	void Update()
	{
		cCannonball[] balls = GameObject.FindObjectsOfType<cCannonball>();

		bool bFound = false;

		for( int i = 0; i < balls.Length; i++ )
		{
			if( balls[i].m_ID == m_ID )
			{
				bFound = true;
				break;
			}
		}

		if( bFound == false )
		{
			m_fShotCooldown -= Time.deltaTime;

			if( m_fShotCooldown <= 0.0f )
			{
				Fire();
			}
			else
			{
				GetComponentInChildren<Renderer>().material.color = Color.white;
			}
		}
	}

	void Fire()
	{
		GameObject newBall = Instantiate<GameObject>( m_CannonballTemplate );
		newBall.transform.position = transform.position + ( transform.forward + transform.up ) * 0.5f;
		Vector3 shotForce = ( transform.forward + transform.up * 0.35f ) * Random.Range( 12.0f, 15.0f );
		newBall.GetComponent<Rigidbody>().AddForce( shotForce, ForceMode.Impulse );
		newBall.GetComponent<cCannonball>().m_ID = m_ID;
		m_fShotCooldown = Random.Range( 0.0f, 2.0f );

		if( m_CannonballTemplate != null )
		{
			newBall.transform.SetParent( m_CannonballHolder.transform );
		}

		GetComponentInChildren<Renderer>().material.color = Color.red;
	}
}