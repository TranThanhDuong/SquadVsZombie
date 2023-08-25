using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GoalSetup : MonoBehaviour
{
    public TextMeshProUGUI lbVal_1;
    public TextMeshProUGUI lbVal_2;
    public void Setup(string val_1, string val_2)
    {
        lbVal_1.text = val_1;
        lbVal_2.text = val_2;
    }
}
