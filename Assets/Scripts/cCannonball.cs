using UnityEngine;

public class cCannonball : PoolElement 
{
	public int m_ID = -1;

   public override void Reset()
   {
      base.Reset();

      //Reset 
      transform.rotation = Quaternion.identity;

      Rigidbody rbody = GetComponent<Rigidbody>();
      if (rbody != null)
      {
         rbody.velocity = Vector3.zero;
         rbody.angularVelocity = Vector3.zero;
      }
   }

   void Update()
	{
		if( transform.localPosition.y < -1.0f )
		{
			Deactivate();
		}
	}

	void OnTriggerEnter( Collider other ) 
	{//TODO
		if( other.name == "Ground" )
		{
         Deactivate();
      }
		else if(  other.transform.parent != null && other.transform.parent.GetComponent<cTarget>() && other.transform.parent.GetComponent<cTarget>().IsAlive() )
		{
			other.transform.parent.GetComponent<cTarget>().DelayedDestroy();
         Deactivate();
		}
	}
}