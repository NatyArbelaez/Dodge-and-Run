using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyScript4 : MonoBehaviour
{
    [Header("Enemy moving elements")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float range;
    NavMeshAgent myNavMeshAgent;
    bool confirmDestino = false;
    float t = 0.0f;
    float timer = 0.0f;
    public GameObject boxRay;
    public GameObject playerTarg4;
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
                if (playerTarg4 != null)
                {
                    StartCoroutine("Attack5");
                }
                break;
        }
        if (playerTarg4 == null)
        {
            StopAllCoroutines();
            status = "Search";
        }
    }
    IEnumerator Attack5()
    {
        myNavMeshAgent.SetDestination(playerTarg4.transform.position);
        float distance4 = Vector3.Distance(transform.position, playerTarg4.transform.position);
        if (distance4 < 1 && playerTarg4 != null)
        {
            Destroy(playerTarg4);
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