using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolElement : MonoBehaviour
{
   protected ObjectPool mProprietaryPool;

   /////////////////////////////////////////////
   public void SetProprietaryPool(ObjectPool pool)
   {
      if (mProprietaryPool != null)
      {
         Debug.LogWarning("PoolElement: Trying to assign a new pool.");
         return;
      }
      mProprietaryPool = pool;
   }

   /////////////////////////////////////////////
   public bool Alive()
   {
      return gameObject.activeSelf;
   }

   /////////////////////////////////////////////
   public virtual void Reset()
   {
      gameObject.SetActive(true);      
   }

   /////////////////////////////////////////////
   public void Deactivate()
   {
      gameObject.SetActive(false);
   }

   /////////////////////////////////////////////
   public bool Destroy()
   {
      bool success = mProprietaryPool != null;
      if (success)
         mProprietaryPool.Destroy(this);
      return success;
   }
}