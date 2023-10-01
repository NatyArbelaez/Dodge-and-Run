using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyScript1 : MonoBehaviour
{
    [Header("Enemy moving elements")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float range;
    NavMeshAgent myNavMeshAgent;
    bool confirmDestino = false;
    float t = 0.0f;
    float timer = 0.0f;
    public GameObject boxRay;
    public GameObject playerTarg1;
    public Vector3 destino;
    string status = "Search";
    [Header("Objective components")]
    public GameObject patrolTarget;
    public Vector3 poscharTarget = new Vector3(0.0f, 0.0f, 0.0f);
    public GameObject finishPanel;
    public GameObject pausePanel;
    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
    }
    void FixedUpdate()
    {
        //Raycast
        t += 0.1f;
        timer += 0.1f;
        if (t >= 100)
        {
            myNavMeshAgent.speed = 4;
        }
        boxRay.transform.Rotate(0.0f, 40 * Mathf.Sin(t * 20), 0.0f, Space.Self);
        RaycastHit hit;
        if (Physics.Raycast(boxRay.transform.position, boxRay.transform.forward, out hit))
        {
            Debug.DrawRay(boxRay.transform.position, boxRay.transform.forward * hit.distance, Color.yellow);
            //Cambio de estado al detectar al jugador
            if (hit.collider is CapsuleCollider)
            {
                status = "Attack";
            }
        }
        else
        {
            Debug.DrawRay(boxRay.transform.position, boxRay.transform.forward * 1000, Color.red);
        }
        //Maquina de estados
        switch (status)
        {
            case "Search":
                GoToDest();
                break;
            case "Attack":
                if (playerTarg1 != null)
                {
                    StartCoroutine("Attack2");
                }
                break;
        }
        if (playerTarg1 == null)
        {
            StopAllCoroutines();
            status = "Search";
        }
    }
    IEnumerator Attack2()
    {
        myNavMeshAgent.SetDestination(playerTarg1.transform.position);
        float distance1 = Vector3.Distance(transform.position, playerTarg1.transform.position);
        if (distance1 < 1 && playerTarg1 != null)
        {
            Destroy(playerTarg1);
            finishPanel.SetActive(true);
            pausePanel.SetActive(false);
            Time.timeScale = 0f;
        }
        yield return null;
    }
    void GoToDest()
    {
        if (!confirmDestino) SearchForDest();
        if (confirmDestino) myNavMeshAgent.SetDestination(destino);
        if (Vector3.Distance(transform.position, destino) < 2 || timer > 10)
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