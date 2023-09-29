using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [Header("Enemy moving elements")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float range;
    NavMeshAgent myNavMeshAgent;
    bool confirmDestino = false;
    float t = 0.0f;
    float timer = 0.0f;
    public GameObject boxRay;
    public GameObject playerTarg;
    public Vector3 destino;
    string status = "Search";
    NavMeshPath navMeshPath;
    [Header("Objective components")]
    public GameObject patrolTarget;
    public Vector3 poscharTarget = new Vector3(0.0f, 0.0f, 0.0f);
    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        navMeshPath = GetComponent<NavMeshPath>();
    }
    void FixedUpdate()
    { 
        Debug.Log(timer);
        //Raycast
        t += 0.1f;
        timer += 0.1f;
        boxRay.transform.Rotate(0.0f, 40 * Mathf.Sin(t * 20), 0.0f, Space.Self);
        RaycastHit hit;
        if (Physics.Raycast(boxRay.transform.position, boxRay.transform.forward, out hit))
        {
            Debug.DrawRay(boxRay.transform.position, boxRay.transform.forward * hit.distance, Color.yellow);
            //Cambio de estado al detectar al jugador
            if (hit.collider is SphereCollider)
            {
                status = "Attack";
            }
        }
        else
        {
            Debug.DrawRay(boxRay.transform.position, boxRay.transform.forward * 1000, Color.red);
        }
        //Maquina de estados
        myNavMeshAgent.speed = 2.5f;
        switch (status)
        {
            case "Search":
                GoToDest();
                break;
            case "Attack":
                StartCoroutine(Attack());
                break;
        }
    }
    IEnumerator Attack()
    {
        myNavMeshAgent.SetDestination(playerTarg.transform.position);
        float distance = Vector3.Distance(transform.position, playerTarg.transform.position);
        if (distance < 2)
        {
            Destroy(playerTarg);
            status = "Search";
        }
        else if (distance > 5)
        {
            status = "Search";
        }
        yield return null;
    }
    void GoToDest()
    {
        if (!confirmDestino) SearchForDest();
        if (confirmDestino) myNavMeshAgent.SetDestination(destino);
        if (Vector3.Distance(transform.position, destino) < 2 || timer > 30)
        {
            confirmDestino = false;
            timer = 0;
        }
    }
    void SearchForDest()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);
        destino = new Vector3(transform.position.x + x, 1, transform.position.z + z);
        if (Physics.Raycast(destino, Vector3.down, groundLayer))
        {
            //move to target
            confirmDestino = true;
        }
    }
}
