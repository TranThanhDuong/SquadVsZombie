using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeView : BaseView
{
    public void OnUnit()
    {
        ViewManager.instance.OnSwitchView(ViewIndex.UnitView);
    }
    public void OnSkill()
    {
        ViewManager.instance.OnSwitchView(ViewIndex.SkillView);
    }
    public void OnQuest()
    {
        ViewManager.instance.OnSwitchView(ViewIndex.QuestView);

    }
    public void OnShop()
    {
        ViewManager.instance.OnSwitchView(ViewIndex.ShopView);
    }
    public void OnLibrary()
    {
        ViewManager.instance.OnSwitchView(ViewIndex.LibraryView);
    }

}
