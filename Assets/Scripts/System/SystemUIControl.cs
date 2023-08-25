using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class SystemUIControl : MonoBehaviour
{
    public RectTransform rectTop;
    public Text viewLB;
    public Text levelLB;
    public Text gemLB;
    public Text starLB;
    public Text energyLB;
    public Button btnBack;
    public Sprite s_Back;
    public Sprite s_Home;
    private BaseView baseView;

    // Start is called before the first frame update
    void Awake()
    {
        SceneManager.sceneLoaded+= SceneManager_SceneLoaded;
        SceneManager.sceneUnloaded+= SceneManager_SceneUnloaded;
       
    }
    private void Start()
    {
        ViewManager.instance.OnSwitchNewView += Instance_OnSwitchNewView;


    }
    void SceneManager_SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
      if(arg0.name=="Buffer")
        {
            PlayerInfo info = DataAPIControler.instance.GetPlayerInfo();
            levelLB.text = "Lv " + info.level.ToString();
            // 
            PlayerInventory playerInventory = DataAPIControler.instance.GetPlayerInventory();
            gemLB.text = playerInventory.gem.ToString();
            starLB.text = playerInventory.star.ToString();
            DataTrigger.RegisterValueChange(DataPath.PLAYER_GEM, (data)=> {
                int gem = (int)data;
                gemLB.text = gem.ToString();
            });
            DataTrigger.RegisterValueChange(DataPath.PLAYER_STAR, (data) => {
                int star = (int)data;
                starLB.text = star.ToString();
            });
        }
    }




    void Instance_OnSwitchNewView(BaseView obj)
    {
        if (obj.index == ViewIndex.HomeView)
        {
            rectTop.DOAnchorPosY(0, 0.5f);
            btnBack.GetComponent<Image>().overrideSprite = s_Home;
            btnBack.interactable = false;
        }
        else
        {
            btnBack.GetComponent<Image>().overrideSprite = s_Back;
            btnBack.interactable = true;
        }
        viewLB.text = obj.nameView;
        baseView = obj;
       
    }


    void SceneManager_SceneUnloaded(Scene arg0)
    {
        if(arg0.name=="Buffer")
        {
            rectTop.DOAnchorPosY(200, 0.5f);
        }
    }



    public void OnBack()
    {
        ViewManager.instance.OnSwitchView(ViewIndex.HomeView);
       
    }
    public void OnShop()
    {
        ViewManager.instance.OnSwitchView(ViewIndex.ShopView);
    }
}
