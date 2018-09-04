using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public GameObject player;
    public Player_Controller_Movement pCmS;

    public Rigidbody rB;

    public Animator anim;

    public AudioSource chickenAudio;
    public AudioClip attackAudio;

    Pathfinding pFs;
    public Vector3 target;
    bool needPathFinding;
    float speed;
    float attackDistance;
    float attackDamage;
    bool canAttack;
    bool attacking;

    //for first step
    GameObject selfMapArea;
    public GameObject[] mapAreas;

    public MemberConfig conf;
    public List<EnemyAi> members;
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;


    void Awake ()
    {
        rB.GetComponent<Rigidbody>();
        anim.GetComponent<Animator>();
        members = new List<EnemyAi>();
        chickenAudio.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        pFs = GetComponent<Pathfinding>();
        mapAreas = GameObject.FindGameObjectsWithTag("Map");
        pCmS = player.GetComponent<Player_Controller_Movement>();
        conf = FindObjectOfType<MemberConfig>();

        selfMapArea = GameObject.FindGameObjectWithTag("Map");

        //target = player.transform.position;

        speed = 3f;

        attackDistance = 2f;

        attackDamage = 30f;

        canAttack = true;
    }
	
	void FixedUpdate ()
    {
        IfNeedPathFinding();

        //movement
        transform.LookAt(target);
        rB.AddForce(transform.forward * speed * 10f);//for friction
        rB.velocity = Vector3.ClampMagnitude(rB.velocity, speed);

        //
        /*acceleration = Combine();
        acceleration = Vector3.ClampMagnitude(acceleration, conf.maxAcceleration);
        velocity = velocity + acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, conf.maxVelocity);
        position = position + velocity * Time.deltaTime;
        transform.LookAt(target);
        transform.position = transform.position;*/
    }

    void IfNeedPathFinding()
    {
        //check if the player is on the same map area
        if (selfMapArea == pCmS.playerMapArea)
        {
            ChaseAndAttack();
        }
        else
        {
            pFs.FindPath(0);
        }
    }

    void ChaseAndAttack()//add collision avoidancce
    {
        if (Vector3.Distance(target, transform.position) <= attackDistance && canAttack == true)
        {
            //change canAttack to false and change it backafter anim
            canAttack = false;
            attacking = true;
            //play attack anim
            anim.SetBool("Attack", true);
            //play sound
            chickenAudio.clip = attackAudio;
            chickenAudio.Play();
            //deduct health
            Player_Health playerHealth = player.GetComponent<Player_Health>();//change to check if at point of attack the playert is still close
            playerHealth.currentHealth -= attackDamage;
        }
        else if (attacking == false)
        {
            target = player.transform.position;
        }
    }

    //called after animation
    void EnableAttack()
    {
        canAttack = true;
        attacking = false;
        anim.SetBool("Attack", false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            for (int i = 0; i < mapAreas.Length; i++)
            {
                if (collision.gameObject == mapAreas[i])
                {
                    selfMapArea = mapAreas[i];
                }
            }
        }
    }

    virtual protected Vector3 Combine()
    {
        Vector3 finalVec = conf.cohesionPriority * Cohesion()
            + conf.alignmentPriority * Alignment()
            + conf.seperationPriority * Seperation()
            + conf.followPriority * Follow();
        return finalVec;
    }

    Vector3 Cohesion()
    {
        Vector3 cohesionVector = new Vector3();
        int countMember = 0;

        var neigbors = GetNeighbors(this, conf.cohesionRadius);
        if (neigbors.Count == 0)
        {
            return cohesionVector;
        }

        foreach (var member in neigbors)
        {
            if (isInFov(member.transform.position))
            {
                cohesionVector += member.transform.position;
                countMember++;
            }
        }

        if (countMember == 0)
        {
            return cohesionVector;
        }

        cohesionVector /= countMember;
        cohesionVector = cohesionVector - this.transform.position;
        cohesionVector = Vector3.Normalize(cohesionVector);
        return cohesionVector;
    }

    Vector3 Alignment()
    {
        Vector3 alignVector = new Vector3();
        var members = GetNeighbors(this, conf.alignmentRadius);

        if (members.Count == 0)
        {
            return alignVector;
        }

        foreach (var member in members)
        {
            if (isInFov(member.transform.position))
            {
                alignVector += member.rB.velocity;
            }
        }

        return alignVector.normalized;
    }

    Vector3 Seperation()
    {
        Vector3 seperateVector = new Vector3();
        var members = GetNeighbors(this, conf.seperationRadius);

        if (members.Count == 0)
        {
            return seperateVector;
        }

        foreach (var member in members)
        {
            if (isInFov(member.transform.position))
            {
                Vector3 movingTowords = this.transform.position - member.transform.position;
                if (movingTowords.magnitude > 0)
                {
                    seperateVector += movingTowords.normalized / movingTowords.magnitude;
                }
            }
        }

        return seperateVector.normalized;
    }

    Vector3 Follow()
    {
        return new Vector3(0,0,0);
    }

    /*Vector3 Avoidance()
    {
        Vector3 avoidVector = new Vector3();
        var enemyList = level.GetEnemies(this, conf.avoidanceRadius);

        if (enemyList.Count == 0)
        {
            return avoidVector;
        }

        foreach (var enemy in enemyList)
        {
            avoidVector += RunAway(enemy.position);
        }

        return avoidVector.normalized;
    }

    Vector3 RunAway(Vector3 target)
    {
        Vector3 neededVelocity = (transform.position - target).normalized * conf.maxVelocity;
        return neededVelocity - velocity;
    }*/

    bool isInFov(Vector3 vec)
    {
        return Vector3.Angle(this.rB.velocity, vec - this.transform.position) <= conf.maxFOV;
    }

    public List<EnemyAi> GetNeighbors(EnemyAi member, float radius)
    {
        List<EnemyAi> neighborsFound = new List<EnemyAi>();

        foreach (var otherMember in members)
        {
            if (otherMember == member)
            {
                continue;
            }

            if (Vector3.Distance(member.transform.position, otherMember.transform.position) <= radius)
            {
                neighborsFound.Add(otherMember);
            }
        }

        return neighborsFound;
    }
}
