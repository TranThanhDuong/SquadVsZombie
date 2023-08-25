using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameProgress : MonoBehaviour
{
    public Image progress;
    public float width;
    public RectTransform headTrans;
    public TextMeshProUGUI zomLB;
    void Start()
    {
        progress.fillAmount = 0;
        headTrans.localPosition = new Vector2(0,0);
        MissionControl.instance.OnStepChange += StepChange;
        MissionControl.instance.OnAliveChange += StepChange;
    }

    void StepChange(int step)
    {
        float percent = (float)(step + 1.0f) / MissionControl.instance.totalWave;
        progress.fillAmount = percent;
        headTrans.localPosition = new Vector2(-width * percent, 0);
    }
    void AliveChange(int step)
    {
        zomLB.text = step.ToString() + "/" + MissionControl.instance.Max_Enemy_Alive; 
    }
}
