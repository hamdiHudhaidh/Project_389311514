using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{

    override public void Execute()
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].Execute();
            if (children[i].stateOfNode == state.SUCCEED)
            {
                stateOfNode = state.SUCCEED;
                return;
            }
            if (children[i].stateOfNode == state.RUNNING)
            {
                stateOfNode = state.RUNNING;
                return;
            }
        }
        stateOfNode = state.FAILED;
    }
}
