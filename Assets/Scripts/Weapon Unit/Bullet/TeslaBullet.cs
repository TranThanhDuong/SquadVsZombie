using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaBullet : MonoBehaviour
{
    public Transform impact;
    public SpriteRenderer tesla;
    private float tesla_Y;
    // Start is called before the first frame update
    void Start()
    {
        tesla_Y = tesla.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(tesla.transform.localPosition, impact.localPosition);
        tesla.size = new Vector2(dis, tesla_Y);
    }
}
