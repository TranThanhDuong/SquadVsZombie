using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(var col in collision.contacts)
        {
            if(col.collider.gameObject.layer == 6)
                col.collider.GetComponent<EnemyControl>().OnPass();
        }    
    }
}
