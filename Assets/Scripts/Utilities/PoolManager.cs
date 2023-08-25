using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public List<BYPool> pools;
    public static Dictionary<string, BYPool> dicPool = new Dictionary<string, BYPool>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(BYPool pool in pools)
        {
            CreatePrefab(pool);
            dicPool[pool.namePool] = pool;
            //dicPool.Add(pool.namePool, pool);
        }
    }
    public static void AddNewPool(BYPool pool)
    {
        if (!dicPool.ContainsKey(pool.namePool))
        {
            for (int i = 0; i < pool.total; i++)
            {
                Transform trans = Instantiate(pool.prefab, Vector3.zero, Quaternion.identity);
                pool.elements.Add(trans);
                trans.gameObject.SetActive(false);
            }
            dicPool[pool.namePool] = pool;
        }
        else
        {
            for (int i = 0; i < pool.total/2; i++)
            {
                Transform trans = Instantiate(pool.prefab, Vector3.zero, Quaternion.identity);
                trans.gameObject.SetActive(false);
                dicPool[pool.namePool].elements.Add(trans);
            }
        }

    }
    public void CreatePrefab(BYPool pool )
    {
   
        for (int i = 0; i < pool.total; i++)
        {
            Transform trans = Instantiate(pool.prefab, Vector3.zero, Quaternion.identity);
            pool.elements.Add(trans);
            trans.gameObject.SetActive(false);
        }
       
    }

}


