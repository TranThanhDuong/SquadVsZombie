using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CoinsControl : MonoBehaviour
{
    public Vector3 endPos = new Vector3(-8, 5, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(Vector3 originPos, int gold)
    {
        this.transform.position = originPos;
        Vector3 randomPos =new Vector3(Random.Range(-5f, 5f) + originPos.x, originPos.y, 0);
        this.transform.DOJump(randomPos, 3, 1, 1.5f).OnComplete(() =>
        {
            this.transform.DOMove(endPos, 1).OnComplete(() =>
            {
                MissionControl.instance.GoldChange(gold);
                Destroy(this.gameObject);
            });
        });
    }
}
