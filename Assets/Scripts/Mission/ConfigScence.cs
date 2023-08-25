using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigScence : MonoBehaviour
{
    public Transform[] posEnemyCreates;
    public Transform[] line_1;
    public Transform[] line_2;
    public Transform[] line_3;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<line_1.Length;i++)
        {
            GameObject go = Instantiate(Resources.Load("UIIngame/Unit_Slot", typeof(GameObject))) as GameObject;
            go.transform.SetParent(null);
            go.transform.localScale = Vector3.one;
            go.transform.localRotation = Quaternion.identity;
            go.transform.position = line_1[i].transform.position;
        }
        for (int i = 0; i < line_2.Length; i++)
        {
            GameObject go = Instantiate(Resources.Load("UIIngame/Unit_Slot", typeof(GameObject))) as GameObject;
            go.transform.SetParent(null);
            go.transform.localScale = Vector3.one;
            go.transform.localRotation = Quaternion.identity;
            go.transform.position = line_2[i].transform.position;
        }

        for (int i = 0; i < line_3.Length; i++)
        {
            GameObject go = Instantiate(Resources.Load("UIIngame/Unit_Slot", typeof(GameObject))) as GameObject;
            go.transform.SetParent(null);
            go.transform.localScale = Vector3.one;
            go.transform.localRotation = Quaternion.identity;
            go.transform.position = line_3[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
