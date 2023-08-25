using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DotweenSample : MonoBehaviour
{
    public Text lb;
    // Start is called before the first frame update
    void Start()
    {
        //1.  The shortcuts way
        //transform.DOMoveX(0,1).SetEase(Ease.Linear).OnComplete(()=> {

        //    Debug.LogError("Ned move");
        //});
        // The generic way
      
        string s = string.Empty;
        DOTween.To(() => s, x => s = x, "Helo world!!!", 3).OnUpdate(() =>
        {

            lb.text = s;
        });
    }
  
    // Update is called once per frame
    void Update()
    {
        
    }
}
