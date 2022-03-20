using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class illusion : MonoBehaviour
{
    Sequence sequence;
    Sequence sequence2;

    private void Start()
    {
        tweenScale();
    }
    public void tweenScale()
    {
        //foreach (var mtrl in material)
        //{
        //    Color32 clr = mtrl.GetColor("_Color");
        //    mtrl.DOColor(clr + new Color(0,0,0,0.2f), 0.5f).SetLoops(-1, LoopType.Yoyo);
        //}

        sequence = DOTween.Sequence();
        sequence2 = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(0.05f, 0.5f).SetLoops(-1, LoopType.Yoyo));
        sequence2.Append(transform.DORotate(new Vector3(0, 360, 0), 3f).SetLoops(-1, LoopType.Incremental));

        sequence.AppendInterval(0f);
        sequence.SetLoops(-1, LoopType.Yoyo);
        sequence.SetRelative(true);
        sequence2.AppendInterval(0f);
        sequence2.SetLoops(-1, LoopType.Incremental);
        sequence2.SetRelative(true);
    }
    public void tweenStop()
    {
        sequence.Kill(this);
        sequence2.Kill(this);
    }
}
