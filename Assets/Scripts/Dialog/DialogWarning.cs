using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogWarning : BaseDialog
{
    public TextMeshProUGUI text;
    public override void OnSetup(DialogParam param)
    {
        base.OnSetup(param);
        DialogWarningParam p = (DialogWarningParam)param;
        text.text = p.text;
    }

    public void OnBtnClick()
    {
        DialogManager.instance.HideDialog(DialogIndex.DialogWarning);
    }
}
