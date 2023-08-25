using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HomeViewAnimation : BaseViewAnimation
{
    public RectTransform rect;
    public override void OnShowAnim(Action callback)
    {
       rect.DOMoveY(0, 0.5f).OnComplete(()=> {
            callback?.Invoke();
        });

    }
    public override void OnHideAnim(Action callback)
    {
        rect.DOMoveY(-400, 0.5f).OnComplete(() => {
            callback?.Invoke();
        });
    }
}
