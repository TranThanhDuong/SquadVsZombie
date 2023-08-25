using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BYPool 
{
    public int total;
    public string namePool;
    public Transform prefab;
    [System.NonSerialized]
    public List<Transform> elements= new List<Transform>();
    private int index=-1;
    // Start is called before the first frame update
 
    public BYPool()
    {
    }
    public BYPool(string name, int total,Transform prefab)
    {
        this.namePool = name;
        this.total = total;
        this.prefab = prefab;
    }
    public Transform OnSpawned()
    {
        index++;
        if(index>=elements.Count)
        {
            index = 0;
        }
        Transform trans = elements[index];
        trans.gameObject.SetActive(true);
        trans.gameObject.SendMessage("OnSpawned", SendMessageOptions.DontRequireReceiver);
        return trans;
    }
    public void OnDespawned( Transform trans)
    {
      if(elements.Contains(trans))
        {
            trans.gameObject.SendMessage("OnDespawned", SendMessageOptions.DontRequireReceiver);
            trans.gameObject.SetActive(false);
        }
    }
}
