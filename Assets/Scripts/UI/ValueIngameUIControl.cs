using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueIngameUIControl : MonoBehaviour
{
    public Image imageGold;
    public Image imageCost;
    public Text goldLB;
    public Text costLB;
    // Start is called before the first frame update
    void Start()
    {
        imageGold.fillAmount = 0;
        goldLB.text = "0/100";
        imageCost.fillAmount = 0;
        costLB.text = "0/10";
        MissionControl.instance.OnCostChange+= Instance_OnCostChange;
        MissionControl.instance.OnGoldChange+= Instance_OnGoldChange;
    }

    void Instance_OnGoldChange(int gold)
    {
        imageGold.fillAmount = (float)gold / MissionControl.instance.max_Money;
        goldLB.text = gold.ToString()+"/" + MissionControl.instance.max_Money;
    }


    void Instance_OnCostChange(int obj)
    {
        imageCost.fillAmount = (float)obj / MissionControl.instance.max_Time;
        costLB.text = obj.ToString() + "/" + MissionControl.instance.max_Time;
    }

    public void StopGame()
    {
        ConfigMissionRecord conf = ConfigManager.instance.configMission.GetRecordByKeySearch(MissionControl.instance.mission_ID);
        MissionData data = DataAPIControler.instance.GetMissionDataByID(MissionControl.instance.mission_ID);

        DialogCreateMissionParam param = new DialogCreateMissionParam { cf = conf, data = data, isPause = true };
        DialogManager.instance.ShowDialog(DialogIndex.DialogCreateMission, param);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
