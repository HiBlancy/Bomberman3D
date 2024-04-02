using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
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

    bool bombDown = false;
    float bombAgain = 1f;

    public AIStates currentState = AIStates.Idle;

    public int bombsOnScreen;
    public bool canPuMoreBombs = true;

    public float sightRange, attackRange, blockRange, powerupRange, bombRange;
    public bool playerInRange, playerInAttackRange, blockInRange, upgradeInRange, bombInRange;

    public LayerMask whatIsGround, whatIsPlayer, block, powerup, bomb;

    public float AIspeed = 3f;
    public int lifes = 3;
    public int explosion_power = 2;
    public bool kickBomb = false;
    public float speedbomb = 7f;
    public int bombs = 1;

    readonly int maxBombs = 10;

    [SerializeField] LayerMask m_LayerMask;

    [SerializeField] Transform puntoDeReferencia;
    [SerializeField] float radioDeColision = 0.5f;

    public float blockStoppingDistance = 3f;
    public float powerupStoppingDistance = 0.1f;

    public float dodgeDistance = 6f;
    Vector3 bombPosition;

    public float range; //radius of sphere

    public Transform centrePoint;
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
                if (currentState != AIStates.Dodge)
                {
                    Debug.Log("Go to block"); //si
                    GoToBlock();
                }
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
                Debug.Log("Taking powerup"); //si
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

        if (bombDown)
        {
            bombAgain -= Time.deltaTime;
            if (bombAgain <= 0)
                bombDown = false;
        }

        if (bombInRange) currentState = AIStates.Dodge;
    }

    void RandomMovement()
    {
        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, UnityEngine.Color.gray, 5f); //so you can see with gizmos
                agent.SetDestination(point);
            }
        }

        if (blockInRange == true) currentState = AIStates.Farm;
        if (blockInRange == true && bombInRange == true) currentState = AIStates.Dodge;
        if (playerInRange == true) currentState = AIStates.Follow;
        if (upgradeInRange == true) currentState = AIStates.Recolec;
        if (playerInRange == true && bombInRange == true) currentState = AIStates.Dodge;
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 10.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
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
        }
        else
        {
            currentState = AIStates.Idle;
        }
    }

    void FollowingPlayer()
    {
        agent.SetDestination(player.position);

        if (playerInAttackRange == true) currentState = AIStates.Attack;
        if (playerInRange == false) currentState = AIStates.Idle;
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
        if (!IsCollosion() && canPuMoreBombs && !bombDown)
        {
            PlantBomb();
            BombsOnScreen();
            waitTillNewBombTime();
        }
    }

    void waitTillNewBombTime()
    {
        bombDown = true;
    }
    bool IsCollosion()
    {
        Collider[] hitColliders = Physics.OverlapSphere(puntoDeReferencia.position, radioDeColision, m_LayerMask);

        return hitColliders.Length > 0;
    }

    void PlantBomb()
    {
        //audioClip.Play();
        GameObject bomb = PoolManager.Obj.BombEnemyPool.GetElement();

        BombaAI bombBehaviour = bomb.GetComponent<BombaAI>();
        bombBehaviour.SummonBomb(AIPosition.position);

        bomb.GetComponent<BombaAI>().explosion_power = explosion_power;

        if (kickBomb)
            bomb.GetComponent<Rigidbody>().isKinematic = false;
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
        if (bombInRange)
        {
            UpdateBombPosition();
            Vector3 dodgeDirection = (transform.position - bombPosition).normalized;
            Vector3 dodgeDestination = transform.position + dodgeDirection * dodgeDistance;

            // Establecer la posicion de esquiva como destino y cambiar el estado a Dodge
            agent.SetDestination(dodgeDestination);
        }
        else
        {
            currentState = AIStates.Idle;
        }
    }
    void UpdateBombPosition()
    {
        Collider[] bombColliders = Physics.OverlapSphere(transform.position, blockRange, bomb);
        float closestDistance = float.MaxValue;

        foreach (Collider col in bombColliders)
        {
            float distance = Vector3.Distance(transform.position, col.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                bombPosition = col.transform.position;
            }
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
                // Usar A* para encontrar la ruta m�s corta hacia el power-up
                NavMeshPath path = new NavMeshPath();
                NavMesh.CalculatePath(transform.position, closestUpgrade.position, NavMesh.AllAreas, path);

                if (path.status == NavMeshPathStatus.PathComplete && path.corners.Length > 1)
                {
                    // Configurar la ruta para el agente
                    agent.stoppingDistance = powerupStoppingDistance;
                    agent.SetPath(path);

                    // Comprobar si el agente ha alcanzado el power-up
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        Debug.LogWarning("Upgrade Recolected");
                        currentState = AIStates.Idle;
                    }
                }
            }
        }
        if (!upgradeInRange)
        { currentState = AIStates.Idle; }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = UnityEngine.Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawWireSphere(transform.position, blockRange);
        Gizmos.color = UnityEngine.Color.blue;
        Gizmos.DrawWireSphere(transform.position, powerupRange);
        Gizmos.color = UnityEngine.Color.white;
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