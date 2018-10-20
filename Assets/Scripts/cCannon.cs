using UnityEngine;

public class cCannon : MonoBehaviour 
{
	public GameObject m_CannonballHolder;
	public PoolElement m_CannonballTemplate;

   PoolElement m_CannonBall;
	float m_fShotCooldown = 0.0f;

	void Awake()
	{
		m_fShotCooldown = Random.Range( 0.0f, 2.0f );
      m_CannonBall = Instantiate(m_CannonballTemplate);
      m_CannonBall.Deactivate();
	}

   void Start()
   {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
      if (/*m_CannonballHolder == null ||*/ m_CannonballTemplate == null || m_CannonBall == null)
      {
         Debug.LogError("cCannon " + name + ": component non correctly initialized.");
         enabled = false;
         return;
      }
      if(m_CannonballTemplate.GetComponent<Rigidbody>() == null)
      {
         Debug.LogError("cCannon " + name + ": m_CannonballTemplate does not have a Rigidbody.");
         enabled = false;
         return;
      }
#endif

      if (m_CannonballHolder != null)
         m_CannonBall.transform.SetParent(m_CannonballHolder.transform);
   }

   void Update()
	{
		if(!m_CannonBall.Alive())
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
      m_CannonBall.Reset();
      m_CannonBall.transform.position = transform.position + ( transform.forward + transform.up ) * 0.5f;
		Vector3 shotForce = ( transform.forward + transform.up * 0.35f ) * Random.Range( 12.0f, 15.0f );
      m_CannonBall.GetComponent<Rigidbody>().AddForce( shotForce, ForceMode.Impulse );
		m_fShotCooldown = Random.Range( 0.0f, 2.0f );

		GetComponentInChildren<Renderer>().material.color = Color.red;
	}
}