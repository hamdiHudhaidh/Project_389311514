using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public List<Node> children = new List<Node>();

    public enum state { SUCCEED = 1, RUNNING = 0, FAILED = -1 };

    public state stateOfNode;

    public virtual void Execute()
    {

    }
}
