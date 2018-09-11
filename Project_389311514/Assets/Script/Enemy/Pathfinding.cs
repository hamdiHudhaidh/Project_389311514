using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public GameObject player;

    GameObject[] points;
    Point[] pointsScripts;

    Point startPoint;
    Point endPoint;

    float[,] table;
    int startIndex;

    Queue<int> waitingList;
    Stack<Point> path;
    //List<Transform> way;
    float clooseEnough;
    Point previousPoint;

    //Transform nextPoint;
    //bool firstDone;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        points = GameObject.FindGameObjectsWithTag("PathFindingPoint");
        pointsScripts = new Point[4];
        for (int i = 0; i < points.Length; i++)
        {
            pointsScripts[i] = points[i].GetComponent<Point>();
        }
        //way = new List<Transform>();
        path = new Stack<Point>();
        table = new float[points.Length, points.Length];
        CreateTable();
        clooseEnough = 2f;

        //print(table.Length);
        /*for (int r = 0; r < points.Length; r++)
        {
            for (int c = 0; c < points.Length; c++)
            {
                print(table[r, c]);
            }
        }*/

        /*for (int i = 0; i < points.Length; i++)
        {
            for (int h = 0; h < points.Length; h++)
            {
                print("distance from " + points[i].name + " to " + points[h].name + " is " + Vector3.Distance(points[i].transform.position, points[h].transform.position));
            }
        }*/
    }

    public void FindPath()
    {
        GetStartAndEnd();

        for (int i = 0; i < points.Length; i++)//setting the start point 
        {
            if (points[i] == startPoint.gameObject)
            {
                startIndex = i;
            }
        }

        waitingList = new Queue<int>();//creating an empty waiting list
        waitingList.Enqueue(startIndex);//adding the first point to it
        pointsScripts[startIndex].accShortDist = 0;//setting it to zero at first

        while (waitingList.Count != 0)
        {
            int r = waitingList.Dequeue();//row = the first point of the waiting list
            for (int c = 0; c < points.Length; c++)//going through the cocloumns of the current row
            {
                if (table[r,c] != -1 && !pointsScripts[c].visited)//if there is a connection and the colomn is not visited
                {
                    waitingList.Enqueue(c);//next point
                    if ((table[r,c] + pointsScripts[r].accShortDist) <= pointsScripts[c].accShortDist)//
                    {
                        pointsScripts[c].accShortDist = table[r, c] + pointsScripts[r].accShortDist;
                        pointsScripts[c].from = pointsScripts[r];//.GetComponent<Point>();
                    }
                }
            }
            points[r].GetComponent<Point>().visited = true;
        }
        //print()
        Backtrack();
    }

    void Backtrack()
    {
        path = new Stack<Point>();//resiising the path
        Point i = endPoint;
        path.Push(i);//adding the first point of the path

        while (i.from != null)//while there is a point before
        {
            i = i.from;//assigning i to the new from to recheck the new point
                //adding the new from to the path list
                path.Push(i);
        }

        while (path.Count != 0)
        {
            print(path.Pop());
        }
        /*while(path.Peek().doneWithPoint == true)
        {
            path.Pop();
            //print("did it");
        }

        EnemyAi eAiS = GetComponent<EnemyAi>();

        if (eAiS.shouldCont == true)
        {
            path.Peek().doneWithPoint = true;
            path.Pop();
            eAiS.shouldCont = false;
        }

        eAiS.target = path.Peek().transform.position;
        //print(path.Peek().transform.position);//
        /*if (path.Count != 0)
        {
            EnemyAi eAiS = GetComponent<EnemyAi>();

            eAiS.target = path.Peek().transform.position;

            if (eAiS.shouldCont == true)
            {
                eAiS.shouldCont = false;
                //firstDone = true;
                path.Pop();
                eAiS.target = path.Peek().transform.position;
                //nextPoint = path.Peek();
            }
        }*/
    }

    void GetStartAndEnd()
    {
        float distanceToSelf = 0;
        float distanceFromTarget = 0;

        for (int i = 0; i < points.Length; i++)
        {
            //start
            if (distanceToSelf == 0)
            {
                distanceToSelf = Vector3.Distance(transform.position, points[i].transform.position);
                startPoint = points[i].GetComponent<Point>();
            }
            else if (Vector3.Distance(transform.position, points[i].transform.position) < distanceToSelf)
            {
                distanceToSelf = Vector3.Distance(transform.position, points[i].transform.position);
                startPoint = points[i].GetComponent<Point>();
            }

            //end
            if (distanceFromTarget == 0)
            {
                distanceFromTarget = Vector3.Distance(player.transform.position, points[i].transform.position);
                endPoint = points[i].GetComponent<Point>();
            }
            else if (Vector3.Distance(player.transform.position, points[i].transform.position) < distanceFromTarget)
            {
                distanceFromTarget = Vector3.Distance(player.transform.position, points[i].transform.position);
                endPoint = points[i].GetComponent<Point>();
            }
        }
        print(startPoint.name + " " + endPoint.name);//
        if (previousPoint == null)
        {
            previousPoint = endPoint;
        }
        else if (previousPoint != endPoint)
        {
            previousPoint = endPoint;
            distanceToSelf = 0;
            distanceFromTarget = 0;
            ResetPoints();
        }
    }

    void ResetPoints()
    {
        for (int i = 0; i < points.Length; i++)
        {
            pointsScripts[i].doneWithPoint = false;
        }
    }

    void CreateTable()
    {
        for (int r = 0; r < points.Length; r++)//
        {
            for (int c = 0; c < points.Length; c++)//
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
        if (a.gameObject == b.gameObject)// if they are the same point
        {
            return false;
        }
        Vector3 direction = b.position - a.position;
        RaycastHit hit;
        if (Physics.Raycast(a.position, direction, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject != b.gameObject)//way is blocked
            {
                return false;
            }
        }
        return true;
    }
}
