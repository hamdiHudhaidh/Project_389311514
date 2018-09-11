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
    public GameObject selfMapArea;
    public GameObject[] mapAreas;

    public MemberConfig conf;
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;


    public bool shouldCont;

    void Awake ()
    {
        rB.GetComponent<Rigidbody>();
        anim.GetComponent<Animator>();
        chickenAudio.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        pFs = GetComponent<Pathfinding>();
        mapAreas = GameObject.FindGameObjectsWithTag("Map");
        pCmS = player.GetComponent<Player_Controller_Movement>();
        conf = FindObjectOfType<MemberConfig>();

        selfMapArea = GameObject.FindGameObjectWithTag("Map");

        speed = 10f;

        attackDistance = 2f;

        attackDamage = 30f;

        canAttack = true;
    }
	
	void FixedUpdate ()
    {
        IfNeedPathFinding();

        if (attacking == false)
        {
            rB.velocity += Combine();
            rB.velocity = Vector3.ClampMagnitude(rB.velocity, conf.maxVelocity);
            transform.LookAt(target);
        }
    }

    void IfNeedPathFinding()
    {
        if (selfMapArea == pCmS.playerMapArea)
        {
            ChaseAndAttack();
        }
        else
        {
            if (Vector3.Distance(gameObject.transform.position, target) < attackDistance)
            {
                shouldCont = true;
            }
            pFs.FindPath();
        }
    }

    void ChaseAndAttack()
    {
        //print(Vector3.Distance(player.transform.position, transform.position));
        if (Vector3.Distance(player.transform.position, transform.position) <= attackDistance && canAttack == true)
        {
            //rB.isKinematic = true;
            canAttack = false;
            //attacking = true;
            anim.SetBool("Attack", true);
            chickenAudio.clip = attackAudio;
            chickenAudio.Play();
        }
        else
        {
            target = player.transform.position;
        }
    }

    //called after animation
    void EnableAttack()
    {
        canAttack = true;
        //attacking = false;
        //rB.isKinematic = false;
        anim.SetBool("Attack", false);
        if (Vector3.Distance(player.transform.position, transform.position) <= attackDistance && canAttack == true)
        {
            Player_Health playerHealth = player.GetComponent<Player_Health>();
            playerHealth.TakeDamage(attackDamage);
        }
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
        Vector3 directionToTarget = target - transform.position;
        directionToTarget.Normalize();

        Vector3 position = (directionToTarget * speed) * Time.deltaTime;


        return position;
    }

    bool isInFov(Vector3 vec)
    {
        return Vector3.Angle(this.rB.velocity, vec - this.transform.position) <= conf.maxFOV;
    }

    public List<EnemyAi> GetNeighbors(EnemyAi member, float radius)
    {
        List<EnemyAi> neighborsFound = new List<EnemyAi>();

        foreach (var otherMember in conf.members)
        {
            if (otherMember == member)
            {
                continue;
            }

            if (Vector3.Distance(member.transform.position, otherMember.transform.position) <= radius)
            {
                neighborsFound.Add(otherMember.GetComponent<EnemyAi>());
            }
        }
        return neighborsFound;
    }
}
