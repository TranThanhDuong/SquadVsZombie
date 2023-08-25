using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogResultMission : BaseDialog
{
    public GameObject victory;
    public GameObject defeat;
    public GameObject nextlvBtn;

    public GoalsCollection goalsCollection;
    public RewardCollection rewardsCollection;

    private ConfigMissionRecord record;

    public List<GameObject> stars;
    private List<GoalSetup> goals;
    public override void OnSetup(DialogParam param)
    {
        base.OnSetup(param);
        DialogResultMissionParam create = (DialogResultMissionParam)param;

        if (create.cf == null)
            return;

        Time.timeScale = 0;

        if (create.isVictory)
        {
            victory.SetActive(true);
            defeat.SetActive(false);
            nextlvBtn.SetActive(true);
        }
        else
        {
            victory.SetActive(false);
            defeat.SetActive(true);
            nextlvBtn.SetActive(false);
        }

        record = create.cf;

        int totalDone = 0;
        if (create.data != null)
        {
            totalDone++;
            for (int i = 0; i < create.cf.lsMissionType.Count; i++)
            {
                if (create.data.goals[i] <= record.lsMissionNeed[i])
                    totalDone++;
            }
        }
        SetAchievedStars(totalDone);
        ConfigMission config = ConfigManager.instance.configMission;
        List<string> msType = config.GetMissionTypeName(record.lsMissionType);
        goalsCollection.Setup(msType, record.lsMissionNeed);

        List<string> msReward = config.GetMissionReward(record.lsReward_Type, record.lsReward_Num);
        rewardsCollection.Setup(msReward);
    }

    public void SetAchievedStars(int total)
    {
        for(int i = 0; i < stars.Count; i++)
        {
            if(i < total)
            {
                stars[i].SetActive(true);
            }
            else
            {
                stars[i].SetActive(false);
            }
        }
    }
    public override void OnHidewDialog()
    {
        base.OnHidewDialog();
        Time.timeScale = 1;
    }
    public void OnBack()
    {
        DialogManager.instance.HideDialog(DialogIndex.DialogResultMission);
    }
    public void OnMenu()
    {
        DialogManager.instance.HideDialog(DialogIndex.DialogResultMission);
        LoadSceneManager.instance.LoadSceneByIndex(1, () => {
            ViewManager.instance.OnSwitchView(ViewIndex.HomeView);
        });
    }
    public void OnRetry()
    {
        DialogManager.instance.HideDialog(DialogIndex.DialogResultMission);
        LoadSceneManager.instance.LoadSceneByIndex(2, () => {
            MissionControl.instance.SetUp(record.id);
        });
    }

    public void OnNextLevel()
    {
        DialogManager.instance.HideDialog(DialogIndex.DialogResultMission);
        LoadSceneManager.instance.LoadSceneByIndex(2, ()=> {
            MissionControl.instance.SetUp(record.id + 1);
        });
    }    
}
