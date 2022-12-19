using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeAction
{
    // Start is called before the first frame update

    public string actionName;
    public bool canDoAction;
    public int actionValue;

    public OrangeAction(string name, bool achieve, int value) {
        actionName = name;
        canDoAction = achieve;
        actionValue = value;
    }

}
