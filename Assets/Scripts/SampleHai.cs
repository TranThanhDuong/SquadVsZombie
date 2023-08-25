using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleHai : MonoBehaviour
{

    private Transform trans;
    public Transform transChild;
    // Start is called before the first frame update
    void Start()
    {
        trans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Input.mousePosition;
        Vector3 posW = Camera.main.ScreenToWorldPoint(pos);

        posW = new Vector3(posW.x, posW.y, trans.position.z);

        Vector3 dir = posW - trans.position;

        trans.right = dir;

        float dot = Vector3.Dot(dir, Vector3.right);
        if(dot>=0)
        {
            transChild.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transChild.localScale = new Vector3(1, -1, 1);
        }
    }
}
