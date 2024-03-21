using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public static AIController Obj { get; private set; }

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Transform AIPosition;

    public NavMeshAgent agent;

    public Transform player;

    bool isImmune = false;
    float immuneTime = 2f;

    public AIStates currentState = AIStates.Idle;

    public int bombsOnScreen;
    public bool canPuMoreBombs = true;

    //Random movement
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float sightRange, attackRange, blockRange, powerupRange, bombRange;
    public bool playerInRange, playerInAttackRange, blockInRange, upgradeInRange, bombInRange;

    public LayerMask whatIsGround, whatIsPlayer, block, powerup, bomb;

    public float AIspeed = 3f;
    public int lifes = 3;
    public int explosion_power = 2;
    public bool kickBomb = false;
    public float speedbomb = 10f; 
    public int bombs = 1;  

    int maxBombs = 10;

    [SerializeField] LayerMask m_LayerMask;

    [SerializeField] Transform puntoDeReferencia;
    [SerializeField] float radioDeColision = 0.5f;

    public float fleeDistance = 10f;

    public float blockStoppingDistance = 3f;
    public float powerupStoppingDistance = 0f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("First Person Controller").transform;
    }

    void FixedUpdate()
    {
        playerInRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        blockInRange = Physics.CheckSphere(transform.position, blockRange, block);
        upgradeInRange = Physics.CheckSphere(transform.position, powerupRange, powerup);
        bombInRange = Physics.CheckSphere(transform.position, bombRange, bomb);

        switch (currentState)
        {
            case AIStates.Idle:
                Debug.Log("Waiting..."); //si
                RandomMovement();
                break;

            case AIStates.Farm:
                Debug.Log("Go to block"); //hay que hacer que vaya hasta alli
                GoToBlock();
                break;

            case AIStates.Follow:
                Debug.Log("Following player"); //si
                FollowingPlayer();
                break;

            case AIStates.Dodge:
                Debug.Log("Run Away!"); //alejarse de la bomba
                DodgeBomb();
                break;

            case AIStates.Recolec:
                Debug.Log("Taking powerup"); //hay que hacer que vaya hasta alli
                RecollectPowerUp();
                break;

            case AIStates.Attack:
                Debug.Log("Attacking!"); //si
                AttackingPlayer();
                break;   
        }

        if (isImmune)
        {
            immuneTime -= Time.deltaTime;

            if (immuneTime <= 0)
                isImmune = false;
        }

        if(bombInRange) currentState = AIStates.Dodge;
    }

    void RandomMovement()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

        if (blockInRange == true) currentState = AIStates.Farm;
        if (playerInRange == true) currentState = AIStates.Follow;
        if (upgradeInRange == true) currentState = AIStates.Recolec;
    }
    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    void GoToBlock()
    {
        if (currentState != AIStates.Dodge)
        {
            Collider[] blockColliders = Physics.OverlapSphere(transform.position, blockRange, block);
            Transform closestBlock = null;
            float closestDistance = float.MaxValue;

            foreach (Collider col in blockColliders)
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestBlock = col.transform;
                }
            }

            if (closestBlock != null)
            {
                agent.stoppingDistance = blockStoppingDistance;
                agent.SetDestination(closestBlock.position);

                if (Vector3.Distance(transform.position, closestBlock.position) < agent.stoppingDistance)
                {
                    SetBomb();
                }
            }
            _ = currentState != AIStates.Farm;
        }
    }

    void FollowingPlayer()
    {
        agent.SetDestination(player.position);

        if (playerInAttackRange == true) currentState = AIStates.Attack;
        if(playerInRange == false) currentState = AIStates.Idle;
    }

    void AttackingPlayer()
    {
        agent.SetDestination(player.position);
        if (playerInAttackRange == false && playerInRange == true) currentState = AIStates.Follow;

        SetBomb();
        if (bombInRange == true) currentState = AIStates.Dodge;
    }

    void SetBomb()
    {
        if (!IsCollosion() && canPuMoreBombs)
        {
            PlantBomb();
            BombsOnScreen();
        }
    }
    bool IsCollosion()
    {
        Collider[] hitColliders = Physics.OverlapSphere(puntoDeReferencia.position, radioDeColision, m_LayerMask);

        return hitColliders.Length > 0;
    }

    void PlantBomb()
    {
        //audioClip.Play();
        GameObject bomb = PoolManager.Obj.BombPool.GetElement();

        Bomba bombBehaviour = bomb.GetComponent<Bomba>();
        bombBehaviour.SummonBomb(AIPosition.position);

        bomb.GetComponent<Bomba>().explosion_power = explosion_power;

        if (kickBomb)
            bomb.GetComponent<Rigidbody>().isKinematic = false;

       // if (playerInRange == false) currentState = AIStates.Dodge;
       // if (bombInRange == true) currentState= AIStates.Dodge;
    }
    public void BombsOnScreen()
    {
        bombsOnScreen++;

        if (bombs == bombsOnScreen)
            canPuMoreBombs = false;
    }

    public void BombExploded()
    {
        bombsOnScreen--;

        if (bombs != bombsOnScreen)
            canPuMoreBombs = true;
    }

    private void DodgeBomb()
    {    
        if(bombInRange)
        {
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet)
                agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
        }
        else
        {
            currentState = AIStates.Idle;
        }
    }

    void RecollectPowerUp()
    {
        if (currentState != AIStates.Dodge)
        {
            Collider[] upgradeColliders = Physics.OverlapSphere(transform.position, powerupRange, powerup);
            Transform closestUpgrade = null;
            float closestDistance = float.MaxValue;

            foreach (Collider col in upgradeColliders)
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestUpgrade = col.transform;
                }
            }

            if (closestUpgrade != null)
            {
                agent.stoppingDistance = powerupStoppingDistance;
                agent.SetDestination(closestUpgrade.position);

                if (Vector3.Distance(transform.position, closestUpgrade.position) < agent.stoppingDistance)
                {
                    Debug.LogWarning("Upgrade Recolected");
                    currentState = AIStates.Idle;
                }
            }
        }
    }

        void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, blockRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, powerupRange);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, bombRange);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Explotion") && !isImmune)
        {
            Debug.Log("enemy hurt");
            LoseHealth();
        }
    }

    void LoseHealth()
    {
        lifes--;

        goImmune();

        if (lifes == 0)
        {
            // gameOverSound.Play();
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
            GameObject.Find("First Person Camera").GetComponent<FirstPersonLook>().enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    void goImmune()
    {
        isImmune = true;
    }

    public void CheckOnLifes()
    {
        if (lifes >= 3)
            lifes = 3;
    }

    public void CheckOnBombs()
    {
        if (maxBombs <= bombs)
            bombs = 10;
    }
}