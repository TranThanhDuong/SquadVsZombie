using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UnitView : BaseView
{
    public Transform deckTrans;
    public Transform unLockTrans;
    public Transform lockTrans;
    private List<ItemUnlockUnitView> lsUnlock = new List<ItemUnlockUnitView>();
    
    public List<ItemDeckUnitView> lsDeck = new List<ItemDeckUnitView>();
    private List<ItemLockUnitView> lsLock = new List<ItemLockUnitView>();
    private bool isLoaded = false;
    public event Action<UnitView,bool> OnDeckEquip;
    public InputField idUnit;
    public Image iconUnitEquip;
    public GameObject gruop_Equip;
    private int currentUnitEquipID;
    public RectTransform rect_UnLock;
    public RectTransform rect_Lock;
    public override void OnSetup(ViewParam param)
    {
       if(isLoaded)
        {
            ReloadUnit();
        }
       else
        {
            isLoaded = true;

          // DataAPIControler.instance.UnLockUnit(4);
           
            Dictionary<string, UnitData> units = DataAPIControler.instance.GetUnits();
            //lock 
          
            foreach (ConfigUnitRecord e in ConfigManager.instance.configUnit.GetAllRecord())
            {
                if (!units.ContainsKey(e.id.ToKey()))
                {
                    GameObject item = Instantiate(Resources.Load("View/Unit/ItemLockUnitView", typeof(GameObject))) as GameObject;
                    item.transform.SetParent(lockTrans, false);
                    ItemLockUnitView itemDeckUnitView = item.GetComponent<ItemLockUnitView>();
                    itemDeckUnitView.Setup(e);

                    lsLock.Add(itemDeckUnitView);
                }

            }
            ScaleUnitLockBG(lsLock.Count);
            // deck
            List<int> decks = DataAPIControler.instance.GetDeckInfo();
            int indexDeckUnit = -1;
            for (int i = 0; i < lsDeck.Count; i++)
            {
                this.OnDeckEquip += lsDeck[i].OnDeckEquip;
            }
            foreach (int e in decks)
            {
                UnitData unitDeck = units[e.ToKey()];
                //
                indexDeckUnit++;
                lsDeck[indexDeckUnit].Setup(unitDeck, indexDeckUnit);
                units.Remove(e.ToKey());
            }
           for(int i=indexDeckUnit+1;i<lsDeck.Count;i++)
            {
                lsDeck[i].Setup(null, i);
            }
            // unit unlock
            foreach (KeyValuePair<string,UnitData> e in units)
            {
             
                GameObject item = Instantiate(Resources.Load("View/Unit/ItemUnlockUnitView", typeof(GameObject))) as GameObject;
                item.transform.SetParent(unLockTrans, false);
                ItemUnlockUnitView itemDeckUnitView = item.GetComponent<ItemUnlockUnitView>();
                itemDeckUnitView.Setup(e.Value);

                lsUnlock.Add(itemDeckUnitView);
            }
            ScaleUnitUNlcokBG(lsUnlock.Count);

            DataTrigger.RegisterValueChange(DataPath.PLAYER_UNIT, (arg0) =>
            {
                ReloadUnit();

            });
            DataTrigger.RegisterValueChange(DataPath.PLAYER_DECK, (arg0) =>
            {
                ReloadUnit();

            });

        }
    }
    public void AddUnit()
    {
        DataAPIControler.instance.UnLockUnit(int.Parse(idUnit.text),null);
       // ReloadUnit();
    }
    private void ReloadUnit()
    {
        Dictionary<string, UnitData> units = DataAPIControler.instance.GetUnits();
        //lock 
        int indexUnitLock = -1;
        foreach (ItemLockUnitView e in lsLock)
        {
            e.gameObject.SetActive(false);
        }
        foreach (ConfigUnitRecord e in ConfigManager.instance.configUnit.GetAllRecord())
        {
            if (!units.ContainsKey(e.id.ToKey()))
            {
                indexUnitLock++;
        
                lsLock[indexUnitLock].gameObject.SetActive(true);
                lsLock[indexUnitLock].Setup(e);
            }

        }
        ScaleUnitLockBG(indexUnitLock + 1);
        // deck
        List<int> decks = DataAPIControler.instance.GetDeckInfo();

        int indexDeckUnit = -1;
        foreach (int e in decks)
        {
            UnitData unitDeck = units[e.ToKey()];
            //
            indexDeckUnit++;

            lsDeck[indexDeckUnit].Setup(unitDeck,indexDeckUnit);


            units.Remove(e.ToKey());


        }
        for (int i = indexDeckUnit + 1; i < lsDeck.Count; i++)
        {
            lsDeck[i].Setup(null, i);
        }
        // unit unlock
        int indexUnitIUnlock = -1;
        foreach (ItemUnlockUnitView e in lsUnlock)
        {
            e.gameObject.SetActive(false);
        }
        foreach (KeyValuePair<string, UnitData> e in units)
        {

          
            indexUnitIUnlock++;
            if(indexUnitIUnlock<lsUnlock.Count)
            {
                lsUnlock[indexUnitIUnlock].gameObject.SetActive(true);
                lsUnlock[indexUnitIUnlock].Setup(e.Value);
            }
            else
            {
                GameObject item = Instantiate(Resources.Load("View/Unit/ItemUnlockUnitView", typeof(GameObject))) as GameObject;
                item.transform.SetParent(unLockTrans, false);
                ItemUnlockUnitView itemDeckUnitView = item.GetComponent<ItemUnlockUnitView>();
                itemDeckUnitView.Setup(e.Value);
                lsUnlock.Add(itemDeckUnitView);
            }
           
        }
        ScaleUnitUNlcokBG(indexUnitIUnlock + 1);


    }
    private void ScaleUnitUNlcokBG(int numberCar)
    {
        int numberUnlock = numberCar;
        int row = 0;
        if (numberUnlock % 4 != 0)
        {
            row++;
        }
        row += numberUnlock / 4;
        rect_UnLock.sizeDelta = new Vector2(rect_UnLock.sizeDelta.x, 55 + 379 * row);
    }

    private void ScaleUnitLockBG(int numberCar)
    {
        int numberUnlock = numberCar;
        int row = 0;
        if (numberUnlock % 4 != 0)
        {
            row++;
        }
        row += numberUnlock / 4;
        rect_Lock.sizeDelta = new Vector2(rect_Lock.sizeDelta.x, 55 + 379 * row);
    }
    public void UnitNeedChangeDeck(ConfigUnitRecord cf)
    {
        currentUnitEquipID = cf.id;
        OnDeckEquip?.Invoke(this,true);
        gruop_Equip.SetActive(true);
        iconUnitEquip.overrideSprite = Resources.Load("Icon/Unit/" + cf.prefab, typeof(Sprite)) as Sprite;
    }
    public void OnCancelEquip()
    {
        gruop_Equip.SetActive(false);
        OnDeckEquip?.Invoke(this, false);
    }
    public void OnConfirmEquipUnit(int index)
    {
        DataAPIControler.instance.OnEquipDeck(index, currentUnitEquipID,() => {

            OnCancelEquip();
        });
    }
}
