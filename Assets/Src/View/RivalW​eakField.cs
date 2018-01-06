using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RivalW​eakField : MyW​eakField
{

    public override void Appear(Action endCall)
    {
        Logger.Log("RivalW​eakField.Appear");
        UpdateWeakRoot(new Vector2(-84, 258));

        _appearCallback = endCall;
    }

    protected override void _weakRootMoveEnd()
    {
        Logger.Log("RivalW​eakField._weakRootMoveEnd");
        base._weakRootMoveEnd();
    }

}
