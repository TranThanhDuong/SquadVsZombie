using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUnlockUnitView : MonoBehaviour
{
    public Image icon;
    public Text namelb;
    public Text levelb;
    private ConfigUnitRecord cf;
    private UnitData data;
   
    public void Setup(UnitData data)
    {
        this.data = data;
        cf = ConfigManager.instance.configUnit.GetRecordByKeySearch(data.id);

        icon.overrideSprite = Resources.Load("Icon/Unit/"+cf.prefab, typeof(Sprite)) as Sprite;
        namelb.text = cf.name;
        levelb.text = "Lv: " + data.level.ToString();
    }
    public void OnDeck()
    {
        UnitView unitView = (UnitView)ViewManager.instance.currentView;
        unitView.UnitNeedChangeDeck(cf);
    }
    public void OnInfo()
    {
        DialogUnitInfoParam param = new DialogUnitInfoParam();
        param.isBuy = false;
        param.cf = this.cf;
        param.data = data;
        DialogManager.instance.ShowDialog(DialogIndex.DialogUnitInfo, param);
    }
}
