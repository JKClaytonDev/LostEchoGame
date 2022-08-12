using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieBehavior : MonoBehaviour
{
    public GameObject ragdoll;
    NavMeshAgent agent;
    Animator anim;
    public bool playerHeard;
    public bool playerSeen;
    float checkTime;
    public GameObject player;

    AnimationEvent soundEvent;
    // Start is called before the first frame update
    void Start()
    {
        soundEvent = new AnimationEvent();
        soundEvent.functionName = "MakeSound";
        playerSeen = false;
        playerHeard = false;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        anim.SetInteger("RandomValue", Random.Range(0, 3));
        player = Camera.main.gameObject;
    }
    void MakeSound(float range)
    {
        FindObjectOfType<SceneSoundManager>().SpawnSound(transform.position, range);
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup < checkTime)
            return;
        if (playerHeard && !playerSeen)
        {
                Debug.Log("CHECKING FOR PLAYER");
            RaycastHit hit;
            Vector3 rayAngle = Vector3.MoveTowards(transform.position, Camera.main.transform.position, 1) - transform.position;
            if (Physics.Raycast(transform.position, rayAngle, out hit))
            {
                playerSeen = true;
                    Debug.Log("RAYCAST HIT");
                anim.SetBool("PlayerSeen", true);
            }
                checkTime = Time.realtimeSinceStartup + 1;
        }
        if (playerSeen)
        {
            agent.SetDestination(Camera.main.transform.position);
            if (Vector3.Distance(transform.position, player.transform.position) < 1)
            {
                anim.Play("ZombieSwipe", 1);
            }
            checkTime = Time.realtimeSinceStartup + 0.3f;
        }
    }
    public void damageZombie()
    {
        anim.Play("ZombieShot" + Random.Range(1, 3));
        GetComponent<ObjectHealth>().health -= 40;
        if (GetComponent<ObjectHealth>().health < 1)
        {
            Destroy(gameObject);
            GameObject newDoll = Instantiate(ragdoll);
            newDoll.transform.position = transform.position;
            newDoll.transform.rotation = transform.rotation;
        }
    }
    public void hearPlayer()
    {
        if (!playerHeard)
        {
            playerHeard = true;
            anim.SetBool("PlayerHeard", true);

            RaycastHit hit;
            Vector3 rayAngle = Vector3.MoveTowards(transform.position, Camera.main.transform.position, 1) - transform.position;
            if (Physics.Raycast(transform.position, rayAngle, out hit))
            {
                playerSeen = true;
                Debug.Log("RAYCAST HIT");
                anim.SetBool("PlayerSeen", true);
                anim.Play("ZombieInstantAlert");
            }
        }

    }
    public void SetZombieDestination(Vector3 v)
    {
        agent.SetDestination(v);
    }
}
