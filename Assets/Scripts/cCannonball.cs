using UnityEngine;

public class cCannonball : PoolElement
{
   public int m_ID = -1;
   Rigidbody rbody;

   private void Awake()
   {
      rbody = GetComponent<Rigidbody>();
   }

   public override void Reset()
   {
      base.Reset();

      //Reset 
      transform.rotation = Quaternion.identity;

      if (rbody != null)
      {
         rbody.velocity = Vector3.zero;
         rbody.angularVelocity = Vector3.zero;
      }
   }

   void Update()
   {
      if (transform.localPosition.y < -1.0f)
      {
         Deactivate();
      }
   }

   void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("BallOpaque"))
         Deactivate();
   }

   internal void AddImpulse(Vector3 shotForce)
   {
      if (rbody != null)
         rbody.AddForce(shotForce, ForceMode.Impulse);
   }
}