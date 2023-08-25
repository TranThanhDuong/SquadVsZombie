using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum DialogIndex
{

    DialogUnitInfo=1,
    DialogCreateMission,
    DialogResultMission,
    DialogText,
    DialogWarning,
}
public class DialogConfig
{
    public static DialogIndex[] dialogIndexs = {
        DialogIndex.DialogUnitInfo,
        DialogIndex.DialogCreateMission,
        DialogIndex.DialogResultMission,
        DialogIndex.DialogText,
        DialogIndex.DialogWarning
};
}
public class DialogParam
{

}

public class DialogUnitInfoParam: DialogParam
{
    public bool isBuy;
    public ConfigUnitRecord cf;
    public UnitData data;
}
public class DialogWarningParam : DialogParam
{
    public string text;
}
public class DialogTextParam : DialogParam
{
    public string text;
    public Action<bool> callback;
}

public class DialogCreateMissionParam : DialogParam
{
    public ConfigMissionRecord cf;
    public MissionData data;
    public bool isPause = false;
}

public class DialogResultMissionParam : DialogParam
{
    public ConfigMissionRecord cf;
    public MissionData data;
    public bool isVictory;
}