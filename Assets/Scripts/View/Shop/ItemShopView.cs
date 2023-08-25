using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopView : MonoBehaviour
{
    public Image iconType;
    public Image icon;
    public Text valueLB;
    public Text costLB;
    private ConfigShopRecord cf;
    public void Setup(ConfigShopRecord configShopRecord)
    {
        this.cf = configShopRecord;
        // load icon 
        switch(configShopRecord.itemType)
        {
            case ItemType.gem:
                iconType.overrideSprite = Resources.Load("Icon/Shop/gem_icon", typeof(Sprite)) as Sprite;
                break;
            case ItemType.Star:
                iconType.overrideSprite = Resources.Load("Icon/Shop/star_icon", typeof(Sprite)) as Sprite;
                break;

        }
        // load image

        icon.overrideSprite= Resources.Load("Icon/Shop/"+configShopRecord.imageIcon, typeof(Sprite)) as Sprite;
        // setting thong so
        valueLB.text = configShopRecord.value.ToString();
        costLB.text = configShopRecord.cost.ToString();
    }
    public void OnBuy()
    {
        // data api controler 
      
        switch (cf.itemType)
        {
            case ItemType.gem:
                DataAPIControler.instance.AddGem(cf.id, () =>
                {
                   //
                });
                break;
            case ItemType.Star:
                DataAPIControler.instance.AddStar(cf.id, () =>
                {
                    //
                });
                break;

        }
    }
}
