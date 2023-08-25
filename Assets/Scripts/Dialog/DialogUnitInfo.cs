using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUnitInfo : BaseDialog
{
    public Image icon;
    public Text nameLB;
    public Text costLB;
    public Text cdLB;
    public Text deployLB;
    public Text LevelLB;
    public Text hpLB;
    public Text damageLB;
    public Text rofLB;
    public Text rangeLB;
    public Text gemLB;
    public GameObject lockObj;
    Dictionary<int, GameObject> dicUnitIcon = new Dictionary<int, GameObject>();
    public Button btnUp;
    private DialogUnitInfoParam param_Unit;
    public override void OnSetup(DialogParam param)
    {
        param_Unit = (DialogUnitInfoParam)param;
        lockObj.SetActive(param_Unit.isBuy);
        nameLB.text = param_Unit.cf.name;
        costLB.text = param_Unit.cf.cost.ToString();
        cdLB.text = param_Unit.cf.countDown.ToString();
        deployLB.text = param_Unit.cf.timeDeploy.ToString();
        UpdateUI(param_Unit.data);
        if (dicUnitIcon.ContainsKey(param_Unit.data.id))
        {
            dicUnitIcon[param_Unit.data.id].SetActive(true);
        }
        else
        {
            GameObject iconPrefab = Instantiate(Resources.Load("Icon/Unit Anim/" + param_Unit.cf.prefab, typeof(GameObject))) as GameObject;
            iconPrefab.transform.SetParent(icon.transform, false);
            iconPrefab.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            dicUnitIcon[param_Unit.data.id] = iconPrefab;
        }

    }
    // Start is called before the first frame update
    public void OnClose()
    {
        DialogManager.instance.HideDialog(index);
    }
    public void OnBtnClick()
    {
        if (param_Unit.isBuy)
            OnBuyUnit();
        else
            OnUpgrade();
    }
    private void UpdateUI(UnitData data)
    {
        ConfigUnitlevelKey key = new ConfigUnitlevelKey();
        key.id_Unit = data.id;
        key.level = data.level;
        ConfigUnitLevelRecord cfLevel = ConfigManager.instance.configUnitLevel.GetRecordByKeySearch(key);

        LevelLB.text = "Level: <color=#00ffffff>" + data.level.ToString() + "</color>";

        hpLB.text = cfLevel.hp.ToString();
        damageLB.text = cfLevel.damage.ToString();
        rofLB.text = cfLevel.rof.ToString();
        rangeLB.text = cfLevel.range.ToString();

        if (param_Unit.isBuy)
        {
            btnUp.interactable = true;
            gemLB.text = param_Unit.cf.buycost.ToString();
        }
        else
        {
            key.id_Unit = data.id;
            key.level = data.level + 1;
            ConfigUnitLevelRecord cfLevelNext = ConfigManager.instance.configUnitLevel.GetRecordByKeySearch(key);
            if (cfLevelNext != null)
            {
                btnUp.interactable = true;
                gemLB.text = cfLevelNext.gemUpgrade.ToString();
            }
            else
            {
                btnUp.interactable = false;
                gemLB.text = "<color=#00ffffff>MAX</color>";

            }
        }
    }
    public override void OnHidewDialog()
    {
        if (dicUnitIcon.ContainsKey(param_Unit.data.id))
        {
            dicUnitIcon[param_Unit.data.id].SetActive(false);
        }
    }

    public void OnUpgrade()
    {
        DialogTextParam param = new DialogTextParam();
        param.text = "Upgrade cost " + gemLB.text + " gem";
        param.callback = (isOk) => {

            if (isOk)
            {
                DataAPIControler.instance.UpGradeUnit(param_Unit.data, (result) =>
                {
                    if (result != null)
                    {
                        UpdateUI(result);

                    }
                    else
                    {
                        DialogManager.instance.ShowDialog(DialogIndex.DialogText, new DialogWarningParam { text = "Not enough Gem!!!!" });
                    }
                });
            }

        };
        DialogManager.instance.ShowDialog(DialogIndex.DialogText, param);


    }

    public void OnBuyUnit()
    {
        DialogTextParam param = new DialogTextParam();
        param.text = "Buy cost " + gemLB.text + " gem";
        param.callback = (isOk) =>
        {
            if (isOk)
            {
                DataAPIControler.instance.UnLockUnit(param_Unit.data.id, (result) =>
                {
                    if (result)
                    {
                        lockObj.SetActive(false);
                        ConfigUnitlevelKey key = new ConfigUnitlevelKey();
                        key.id_Unit = param_Unit.data.id;
                        key.level = param_Unit.data.level + 1;
                        ConfigUnitLevelRecord cfLevel = ConfigManager.instance.configUnitLevel.GetRecordByKeySearch(key);

                        if (cfLevel != null)
                        {
                            btnUp.interactable = true;
                            gemLB.text = cfLevel.gemUpgrade.ToString();
                        }
                    }
                    else
                    {
                        DialogManager.instance.ShowDialog(DialogIndex.DialogText, new DialogWarningParam { text = "Not enough Gem!!!!" });
                    }
                });
            }
        };
        DialogManager.instance.ShowDialog(DialogIndex.DialogText, param);
    }
}
