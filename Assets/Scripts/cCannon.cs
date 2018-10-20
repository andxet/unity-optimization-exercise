using UnityEngine;

public class cCannon : MonoBehaviour 
{
	public GameObject m_CannonballHolder;
	public cCannonball m_CannonballTemplate;

   cCannonball m_CannonBall;
   Vector3 m_cannonBallStartPosition;
   Vector3 m_cannonBallStartForce;
   float m_fShotCooldown = 0.0f;
   Material m_mat;

	void Awake()
   {
      if(m_CannonballTemplate != null)
         m_CannonBall = Instantiate(m_CannonballTemplate);

#if DEBUG //Let's assume that when the release is built, theese checks are passed
      if (/*m_CannonballHolder == null ||*/ m_CannonballTemplate == null || m_CannonBall == null)
      {
         Debug.LogError("cCannon " + name + ": component non correctly initialized.");
         enabled = false;
         return;
      }
#endif

      m_fShotCooldown = Random.Range( 0.0f, 2.0f );
      m_CannonBall.Deactivate();

      if (m_CannonballHolder != null)
         m_CannonBall.transform.SetParent(m_CannonballHolder.transform);

      m_cannonBallStartPosition = transform.position + (transform.forward + transform.up) * 0.5f;
      m_cannonBallStartForce = (transform.forward + transform.up * 0.35f);
      m_mat = GetComponentInChildren<Renderer>().material;

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
            m_mat.color = Color.white;
			}
		}
	}

	void Fire()
	{
      m_CannonBall.Reset();
      m_CannonBall.transform.position = m_cannonBallStartPosition;
		Vector3 shotForce = m_cannonBallStartForce * Random.Range( 12.0f, 15.0f );
      m_CannonBall.AddImpulse(shotForce);
		m_fShotCooldown = Random.Range( 0.0f, 2.0f );

      m_mat.color = Color.red;
	}
}