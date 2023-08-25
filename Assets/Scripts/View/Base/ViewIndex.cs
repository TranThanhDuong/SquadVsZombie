using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewIndex
{
    EmptyView=0,
    HomeView=1,
    UnitView=2,
    SkillView=3,
    QuestView=4,
    ShopView =5,
    LibraryView=6

}
public class ViewConfig 
{
    public static ViewIndex[] viewIndices = {
    ViewIndex.EmptyView,
     ViewIndex.HomeView,
      ViewIndex.ShopView,
       ViewIndex.UnitView,
        ViewIndex.QuestView,
        ViewIndex.SkillView,
        ViewIndex.LibraryView };
}
public class ViewParam
{

}
public class HomeViewParam: ViewParam
{

}