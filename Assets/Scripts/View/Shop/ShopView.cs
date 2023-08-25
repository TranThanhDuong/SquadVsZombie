using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopView : BaseView
{
    public GridLayoutGroup gridLayout;
    public Transform itemprefab;
    public Transform layoutParent;
    private List<ItemShopView> ls = new List<ItemShopView>();
    public override void OnSetup(ViewParam param)
    {
    
        //float scale = (float)Screen.width / 1920f;
        //gridLayout.padding = new RectOffset { left =Mathf.RoundToInt(100 * scale) , right =Mathf.RoundToInt(100 * scale),bottom=0,top=0 };
        //gridLayout.cellSize = new Vector2 { x = 400 * scale, y = 450 * scale };
        //gridLayout.spacing = new Vector2 { x = 40 * scale, y = 30 * scale };
         // 1. load config
         // 2 tao item 
         //3. push config vo item 
        if (ls.Count==0)
        {
            foreach (ConfigShopRecord e in ConfigManager.instance.configShop.GetAllRecord())
            {
                Transform item = Instantiate(itemprefab);
                item.SetParent(layoutParent,false);
                ItemShopView itemShop = item.GetComponent<ItemShopView>();
                itemShop.Setup(e);

                ls.Add(itemShop);

            }
        }
     
   
    }
}
