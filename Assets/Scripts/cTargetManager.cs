using System;
using System.Collections.Generic;
using UnityEngine;

public class cTargetManager : MonoBehaviour
{
   public PoolElement m_TargetTemplate;
   List<GameObject> m_ActiveObjectPool;
   Queue<GameObject> m_AvailableObjectPool;

   const int m_NumTargets = 4;

   float m_fPlaceNextTargetIn = 0.0f;

   public List<float> m_AvailableOffsets = new List<float>();

   void Awake()
   {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
      if (m_TargetTemplate.GetComponent<cTarget>() == null)
      {
         Debug.LogError("cTargetManager " + name + ": m_TargetTemplate does not have a cTarget component.");
         enabled = false;
         return;
      }
#endif

      //Init the pool
      m_AvailableObjectPool = new Queue<GameObject>();
      m_ActiveObjectPool = new List<GameObject>();
      for (int i = 0; i < m_NumTargets; i++)
      {
         PoolElement pElemet = Instantiate(m_TargetTemplate);
         cTarget target = pElemet.GetComponent<cTarget>();
         target.SetZOffset(15.0f - i);
         target.SetManager(this);
         target.transform.parent = transform;
         target.Deactivate();
         m_AvailableObjectPool.Enqueue(pElemet.gameObject);
      }

      m_fPlaceNextTargetIn = UnityEngine.Random.Range(0.0f, 1.0f);
   }

   void Update()
   {
      if (m_AvailableObjectPool.Count > 0)
      {
         m_fPlaceNextTargetIn -= Time.deltaTime;

         if (m_fPlaceNextTargetIn <= 0.0f)
         {
            m_fPlaceNextTargetIn = UnityEngine.Random.Range(0.5f, 1.0f);
            SpawnTarget();
         }
      }
   }

   internal void NotifyDestroy(cTarget cTarget)
   {
      m_ActiveObjectPool.Remove(cTarget.gameObject);
      m_AvailableObjectPool.Enqueue(cTarget.gameObject);
   }

   public void SpawnTarget()
   {
      //Get the first element
      GameObject targetInstance = m_AvailableObjectPool.Dequeue();
      m_ActiveObjectPool.Add(targetInstance);

      cTarget targetComponent = targetInstance.GetComponent<cTarget>();
      targetComponent.Reset();
      targetInstance.transform.position = new Vector3(-4.0f, 0.7f, targetComponent.GetZOffset());
   }
}