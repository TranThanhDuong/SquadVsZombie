using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapSetting : Singleton<MiniMapSetting>
{
    public List<Transform> anchor_Missions;
    public int index_Min;
    public int index_max;
    public Transform transCam;
    public float widthmap;
    public Transform itemPrefab;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        widthmap = Screen.width * 0.5f * 0.01f;

        for (int i=0;i<10;i++)
        {
            Transform transItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            transItem.SetParent(null);
            transItem.position = anchor_Missions[i].position;
            ItemMiniMap item = transItem.GetComponent<ItemMiniMap>();
            item.Setup(i);
        }
        index_Min = 0;
        index_max = 9;
    }
    public void ItemInvisible(ItemMiniMap item)
    {
       
        if (BufferCameraControl.sideMove>0)
        {
            index_Min++;
            index_max++;
            if (index_max >= anchor_Missions.Count)
            {
                index_Min = anchor_Missions.Count - 10;
                index_max = anchor_Missions.Count -1;
                return;
            }
            // an o ben trai  , set cho ben phai 

            item.transform.position = anchor_Missions[index_max].position;
            item.Setup(index_max);
        }
        else 
        {
            index_Min--;
            index_max--;
            if (index_Min <0)
            {
                index_Min = 0;
                index_max = 9;
                return;

            }
            // an o ben phai  , set cho ben trai 
            item.transform.position = anchor_Missions[index_Min].position;
            item.Setup(index_Min);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
