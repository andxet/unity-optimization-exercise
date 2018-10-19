using UnityEngine;
using System.Collections.Generic;

public class cTargetManager : MonoBehaviour 
{
	public GameObject m_TargetTemplate;

	int m_TargetsPlaced = 0;
	float m_fPlaceNextTargetIn = 0.0f;

	public List<float> m_AvailableOffsets = new List<float>();

	void Awake()
	{
		m_fPlaceNextTargetIn = Random.Range( 0.0f, 1.0f );
		for( int i = 0; i < 4; i++ )
		{
			m_AvailableOffsets.Add( 15.0f - i );
		}
	}

	void Update()
	{
		if( m_TargetsPlaced < 4 )
		{
			m_fPlaceNextTargetIn -= Time.deltaTime;

			if( m_fPlaceNextTargetIn <= 0.0f )
			{
				float fTargetOffset = 15.0f;

				foreach( float offset in m_AvailableOffsets )
				{
					fTargetOffset = offset;
					m_AvailableOffsets.Remove( fTargetOffset );
					break;
				}

				m_fPlaceNextTargetIn = Random.Range( 0.5f, 1.0f );
				SpawnTarget( fTargetOffset );
			}
		}
	}

	public void SpawnTarget( float targetOffset )
	{
		GameObject targetInstance = Instantiate<GameObject>( m_TargetTemplate );
		targetInstance.transform.position = new Vector3(-4.0f, 0.7f, targetOffset);
		targetInstance.transform.parent = transform;
		m_TargetsPlaced++;
	}

	public void TargetDestroyed( float zOffset )
	{
		m_AvailableOffsets.Add( zOffset );
		m_TargetsPlaced--;
	}
}