using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDeckUnitView : MonoBehaviour
{
    public Image icon;
    public Text namelb;
    public Text levelb;
    private ConfigUnitRecord cf;
    private UnitData data;
    public Button btnInfo;
    public GameObject group_Info;
    public GameObject group_Select;
    private UnitView unitView;
    private int index;
    public void Setup(UnitData data, int index)
    {
        this.index = index;
        if(data==null)
        {
            icon.overrideSprite = Resources.Load("Icon/Unit/Empty", typeof(Sprite)) as Sprite;
            namelb.text ="Need Equip";
            levelb.text = "";
            btnInfo.gameObject.SetActive(false);
        }
        else
        {
            this.data = data;
            cf = ConfigManager.instance.configUnit.GetRecordByKeySearch(data.id);

            icon.overrideSprite = Resources.Load("Icon/Unit/" + cf.prefab, typeof(Sprite)) as Sprite;
            namelb.text = cf.name;
            levelb.text = "Lv: " + data.level.ToString();
            btnInfo.gameObject.SetActive(true);
        }
     
    }
    public void OnInfo()
    {
        DialogUnitInfoParam param = new DialogUnitInfoParam();
        param.isBuy = false;
        param.cf = this.cf;
        param.data = data;
        DialogManager.instance.ShowDialog(DialogIndex.DialogUnitInfo, param);
    }
    public void OnSelected()
    {
        unitView.OnConfirmEquipUnit(index);
    }
    public void OnDeckEquip(UnitView unitView,bool isEqquip)
    {
        this.unitView = unitView;
        if(isEqquip)
        {
            group_Select.SetActive(true);
            group_Info.SetActive(false);
        }
      else
        {
            group_Select.SetActive(false);
            group_Info.SetActive(true);
        }
    }
}
