using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public GameObject player;

    public GameObject[] points;

    float distanceToSelf = 0;
    float distanceFromTarget = 0;
    Transform startPoint;
    Transform endPoint;

    float[,] table;
    int startIndex;

    Queue<int> waitingList;
    Stack<Transform> path;
    List<Transform> way;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        points = GameObject.FindGameObjectsWithTag("PathFindingPoint");
        way = new List<Transform>();
        path = new Stack<Transform>();
        table = new float[points.Length, points.Length];
        CreateTable();
    }

    public void FindPath(int r)
    {
        GetStartAndEnd();

        waitingList = new Queue<int>();
        waitingList.Enqueue(startIndex);
        points[startIndex].GetComponent<Point>().accShortDist = 0;

        while (waitingList.Count != 0)
        {
            r = waitingList.Dequeue();
            for (int c = 0; c < points.Length; c++)
            {
                if (table[r,c] != -1 && !points[c].GetComponent<Point>().visited)
                {
                    waitingList.Enqueue(c);
                    if ((table[r,c] + points[r].GetComponent<Point>().accShortDist) <= points[c].GetComponent<Point>().accShortDist)
                    {
                        points[c].GetComponent<Point>().accShortDist = table[r, c] + points[r].GetComponent<Point>().accShortDist;
                        points[c].GetComponent<Point>().from = points[r].transform;
                    }
                }
            }
            points[r].GetComponent<Point>().visited = true;
        }
        Backtrack();
    }

    void Backtrack()
    {
        Transform i = endPoint;
        path.Push(i);
        while (i.GetComponent<Point>().from != null)
        {
            i = i.GetComponent<Point>().from;
            path.Push(i);
        }

        while (path.Count != 0)
        {
            EnemyAi eAiS = GetComponent<EnemyAi>();
            eAiS.target = path.Pop().position;
        }
    }

    void GetStartAndEnd()
    {
        for (int i = 0; i < points.Length; i++)
        {
            //start
            if (distanceToSelf <= 0)
            {
                distanceToSelf = Vector3.Distance(this.transform.position, points[i].transform.position);
                startPoint = points[i].transform;
            }
            else if (Vector3.Distance(this.transform.position, points[i].transform.position) < distanceToSelf)
            {
                distanceToSelf = Vector3.Distance(this.transform.position, points[i].transform.position);
                startPoint = points[i].transform;
            }

            //end
            if (distanceFromTarget <= 0)
            {
                distanceFromTarget = Vector3.Distance(player.transform.position, points[i].transform.position);
                endPoint = points[i].transform;
            }
            else if (Vector3.Distance(player.transform.position, points[i].transform.position) < distanceFromTarget)
            {
                distanceFromTarget = Vector3.Distance(player.transform.position, points[i].transform.position);
                endPoint = points[i].transform;
            }
        }
    }

    void CreateTable()
    {
        for (int r = 0; r < points.Length; r++)
        {
            /*if (points[r] == startPoint)
            {
                startIndex = r;
            }*/
            for (int c = 0; c < points.Length; c++)
            {
                if (CheckConnection(points[r].transform, points[c].transform))
                {
                    table[r, c] = Vector3.Distance(points[r].transform.position, points[c].transform.position);
                }
                else
                {
                    table[r, c] = -1;
                }
            }
        }
    }

    bool CheckConnection(Transform a, Transform b)
    {
        if (a == b)
        {
            return false;
        }
        Vector3 direction = b.position - a.position;
        RaycastHit hit;
        if (Physics.Raycast(a.position, direction, out hit, Mathf.Infinity))
        {
            if (hit.collider != b.gameObject)
            {
                return false;
            }
        }
        return true;
    }
}
