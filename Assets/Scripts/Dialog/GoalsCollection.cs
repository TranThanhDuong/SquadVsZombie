using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalsCollection : MonoBehaviour
{
    public Transform itemGoalPreb;
    private List<GoalSetup> items = new List<GoalSetup>();
    public void Setup(List<string> lb_1, List<int> lb_2)
    {

        for (int i = 0; i < items.Count; i++)
            items[i].gameObject.SetActive(false);

        if (lb_1.Count <= 0 || lb_2.Count <= 0)
            return;

        int numberNeed = lb_1.Count - items.Count;
        if (numberNeed > 0)
        {
            for (int i = 0; i < numberNeed; i++)
            {
                Transform item_trans = Instantiate(itemGoalPreb) as Transform;
                GoalSetup item = item_trans.GetComponent<GoalSetup>();
                item.transform.SetParent(this.transform, false);
                items.Add(item);
                item.gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < lb_1.Count; i++)
        {
            items[i].gameObject.SetActive(true);
            items[i].Setup(lb_1[i], lb_2[i].ToString());
        }
    }
}
