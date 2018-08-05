using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourTree : MonoBehaviour
{
    Node root;



	void Start ()
    {
        root = new Sequencer();
        Node selector = new Selector();
        Node getEnemy = new GetEnemy();
        Node dontFindPath = new DontFindPath();
        Node findPath = new FindPath();
        Node chaseAndAttack = new ChaseAndAttack();

        root.children.Add(getEnemy);
        root.children.Add(selector);
        root.children.Add(chaseAndAttack);
        selector.children.Add(dontFindPath);
        selector.children.Add(findPath);
    }
	
	void Update ()
    {
        root.Execute();
    }
}
