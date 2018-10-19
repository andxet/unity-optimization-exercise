using UnityEngine;

public class cCannonball : MonoBehaviour 
{
	public int m_ID = -1;

	void Update()
	{
		if( transform.localPosition.y < -1.0f )
		{
			Destroy( gameObject );
		}
	}

	void OnTriggerEnter( Collider other ) 
	{
		if( other.name == "Ground" )
		{
			Destroy( gameObject );
		}
		else if(  other.transform.parent != null && other.transform.parent.GetComponent<cTarget>() && other.transform.parent.GetComponent<cTarget>().IsAlive() )
		{
			other.transform.parent.GetComponent<cTarget>().DelayedDestroy();			
			Destroy( gameObject );
		}
	}
}