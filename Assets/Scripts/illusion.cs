using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class illusion : MonoBehaviour
{
    Sequence sequence;
    Vector3 firstPos;
    //Sequence sequence2;

    private void Start()
    {
        //firstPos = transform.position;
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
        //sequence2 = DOTween.Sequence();
        sequence.Append(transform.DOLocalMoveY(0.05f, 0.5f).SetLoops(-1, LoopType.Yoyo));
        //sequence2.Append(transform.DOLocalRotate(new Vector3(0, 360, 0), 3f).SetLoops(-1, LoopType.Incremental));

        sequence.AppendInterval(0f);
        sequence.SetLoops(-1, LoopType.Yoyo);
        sequence.SetRelative(true);
        //sequence2.AppendInterval(0f);
        //sequence2.SetLoops(-1, LoopType.Incremental);
        //sequence2.SetRelative(true);
    }
    public void tweenStop()
    {
        sequence.Kill(this);
        //transform.position = firstPos;

        //sequence2.Kill(this);
    }
}
