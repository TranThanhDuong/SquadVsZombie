using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultImpact : MonoBehaviour
{
    public float timeLife = 0.5f;
    private string namePool;
    // Start is called before the first frame update
    IEnumerator OnStart()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Play");
        yield return new WaitForSeconds(timeLife);
        EndAnimation();
    }
    public void OnSpawned()
    {
        StopCoroutine("OnStart");
        StartCoroutine("OnStart");
    }
    public void Setup(string namePool)
    {
        this.namePool = namePool;
    }
    public void EndAnimation()
    {
        PoolManager.dicPool[namePool].OnDespawned(transform);
    }
}
