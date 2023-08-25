using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLockUnitView : MonoBehaviour
{
    public Image icon;
    private ConfigUnitRecord cf;
   // Start is called before the first frame update
   public void Setup(ConfigUnitRecord cf)
    {
        this.cf = cf;
        icon.overrideSprite = Resources.Load("Icon/Unit/" + cf.prefab, typeof(Sprite)) as Sprite;
    }

    public void OnUnlock()
    {
        UnitData data = new UnitData();
        data.id = cf.id;
        data.level = 1;
        DialogUnitInfoParam param = new DialogUnitInfoParam();
        param.isBuy = true;
        param.cf = this.cf;
        param.data = data;
        DialogManager.instance.ShowDialog(DialogIndex.DialogUnitInfo, param);
    }
        
}
