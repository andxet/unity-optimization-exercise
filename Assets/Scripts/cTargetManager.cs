using System;
using System.Collections.Generic;
using UnityEngine;

public class cTargetManager : MonoBehaviour
{
   public cTarget m_TargetTemplate;
   List<cTarget> m_ActiveObjectPool;
   Queue<cTarget> m_AvailableObjectPool;

   const int m_NumTargets = 4;
   float m_fPlaceNextTargetIn;


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
      m_AvailableObjectPool = new Queue<cTarget>();
      m_ActiveObjectPool = new List<cTarget>();
      for (int i = 0; i < m_NumTargets; i++)
      {
         cTarget target = Instantiate(m_TargetTemplate);
         target.SetZOffset(15.0f - i);
         target.SetManager(this);
         target.transform.parent = transform;
         target.Deactivate();
         m_AvailableObjectPool.Enqueue(target);
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
      //Move back the element to available queue
      m_ActiveObjectPool.Remove(cTarget);
      m_AvailableObjectPool.Enqueue(cTarget);
   }

   public void SpawnTarget()
   {
      //Get the first element and move to active list
      cTarget targetComponent = m_AvailableObjectPool.Dequeue();
      m_ActiveObjectPool.Add(targetComponent);

      targetComponent.Reset();
      targetComponent.transform.position = new Vector3(-4.0f, 0.7f, targetComponent.GetZOffset());
   }
}