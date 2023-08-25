using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckUIControl : MonoBehaviour
{
    public Transform parentAnchor;
    public Camera cam;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        cam = Camera.main;
        yield return new WaitForSeconds(2);
        foreach (UnitData e in DataAPIControler.instance.GetDecks())
        {
            GameObject go = Instantiate(Resources.Load("UIIngame/CardUiItem", typeof(GameObject))) as GameObject;
            go.transform.SetParent(parentAnchor, false);
            go.GetComponent<CardUIItemControl>().Setup(e, cam);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
