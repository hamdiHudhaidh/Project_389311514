using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : Node
{

    override public void Execute()
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].Execute();
            if (children[i].stateOfNode == state.RUNNING)
            {
                stateOfNode = state.RUNNING;
                return;
            }
            if (children[i].stateOfNode == state.FAILED)
            {
                stateOfNode = state.FAILED;
                return;
            }
        }
        stateOfNode = state.SUCCEED;
    }
}
