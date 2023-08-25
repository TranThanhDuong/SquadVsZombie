using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class DialogText : BaseDialog
{
    public TextMeshProUGUI text;
    private Action<bool> action;
    public override void OnSetup(DialogParam param)
    {
        base.OnSetup(param);
        DialogTextParam p = (DialogTextParam)param;
        action = p.callback;
        text.text = p.text;
    }

    public void OnNo()
    {
        action?.Invoke(false);
        DialogManager.instance.HideDialog(DialogIndex.DialogText);
    }   
    
    public void OnYes()
    {
        action.Invoke(true);
        DialogManager.instance.HideDialog(DialogIndex.DialogText);
    }    
}
