using UnityEngine;

public class cCannonball : PoolElement 
{
	public int m_ID = -1;

	void Update()
	{
		if( transform.localPosition.y < -1.0f )
		{
			Deactivate();
		}
	}

	void OnTriggerEnter( Collider other ) 
	{
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