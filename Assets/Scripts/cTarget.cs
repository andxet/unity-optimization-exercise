using UnityEngine;

public class cTarget : PoolElement
{
   bool m_bSelfDestruct = false;
   float m_fSpeed = 1.0f;
   float m_fTargetOffsetZ = 0.0f;
   Vector3 m_fSpeedV;
   cTargetManager m_manager;
   Material m_mat;

   void Awake()
   {
      m_fSpeed = Random.Range(1.3f, 2.2f);
      m_fSpeedV = new Vector3(m_fSpeed, 0.0f, 0.0f);
   }

   public override void Reset()
   {
      base.Reset();
      m_bSelfDestruct = false;
      GetComponentInChildren<Renderer>().material.color = Color.white;
      m_fSpeed = Random.Range(1.3f, 2.2f);
      m_fSpeedV = new Vector3(m_fSpeed, 0.0f, 0.0f);
      m_mat = GetComponentInChildren<Renderer>().material;
   }

   void Update()
   {
      if (!m_bSelfDestruct)
      {
         if (transform.position.x > 3.0f)
         {
            DestroyNow();
         }
         else
         {
            transform.position += m_fSpeedV * Time.deltaTime;
         }         
      }
   }

   public void DelayedDestroy()
   {
      m_bSelfDestruct = true;
      m_mat.color = Color.red;

      Invoke("DestroyNow", 1.0f);
   }

   void DestroyNow()
   {
      m_manager.NotifyDestroy(this);
      Deactivate();
   }

   public bool IsAlive()
   {
      return m_bSelfDestruct == false;
   }

   public void SetZOffset(float value)
   {
      m_fTargetOffsetZ = value;
   }

   public float GetZOffset()
   {
      return m_fTargetOffsetZ;
   }

   public void SetManager(cTargetManager manager)
   {
      m_manager = manager;
   }
}