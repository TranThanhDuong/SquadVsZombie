using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RewardCollection : MonoBehaviour
{
    public Transform itemGoalPreb;
    private List<TextMeshProUGUI> items = new List<TextMeshProUGUI>();
    public void Setup(List<string> lb_1)
    {

        for (int i = 0; i < items.Count; i++)
            items[i].gameObject.SetActive(false);

        if (lb_1.Count <= 0)
            return;

        int numberNeed = lb_1.Count - items.Count;
        if (numberNeed > 0)
        {
            for (int i = 0; i < numberNeed; i++)
            {
                Transform item_trans = Instantiate(itemGoalPreb) as Transform;
                TextMeshProUGUI item = item_trans.GetComponent<TextMeshProUGUI>();
                item.transform.SetParent(this.transform, false);
                items.Add(item);
                item.gameObject.SetActive(false);
            }
        }
        int temp = 0;
        foreach (var item in lb_1)
        {
            items[temp].gameObject.SetActive(true);
            items[temp].text = item;
            temp++;
        }
    }
}
