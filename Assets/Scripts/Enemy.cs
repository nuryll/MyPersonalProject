using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 2.5f;        // speed while chasing
    public float rotationSpeed = 5f;      
    public float detectionRange = 4f;    // start chasing if player closer than this
    public float stopRange = 7f;         // stop chasing if player goes farther than this

    
    private Transform player;
    private Animator anim;

    private bool isChasing = false;

    private bool isFrozen = false; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (player == null) return;

        // When frozen: spin in place 
        if (isFrozen)
        {
            transform.Rotate(Vector3.up * 180 * Time.deltaTime);
            if (anim != null) anim.SetBool("isWalking", false);
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        // Switch between idle and chase
        if (!isChasing && distance < detectionRange)
        {
            StartChasing();
        }
        else if (isChasing && distance > stopRange)
        {
            StopChasing();
        }

        if (isChasing)
        {
            MoveTowardPlayer();
        }
        else if (anim != null)
        {
            anim.SetBool("isWalking", false);
        }
    }

    void MoveTowardPlayer()
    {
        Vector3 dir = (player.position - transform.position);
        dir.y = 0;
        dir.Normalize();

        // Rotate smoothly toward player — but flip 180° if model faces backward
        Quaternion targetRot = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 180, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

        // Move forward in the correct visual direction
        transform.position -= transform.forward * walkSpeed * Time.deltaTime;

        
        if (anim != null)
            anim.SetBool("isWalking", true);
    }

    void StartChasing()
    {
        isChasing = true;
        if (anim != null) anim.SetBool("isWalking", true);
    }

    void StopChasing()
    {
        isChasing = false;
        if (anim != null) anim.SetBool("isWalking", false);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }

    // ==============================
    // FREEZE + SPIN SYSTEM
    // ==============================

    public void FreezeAndSpin(float duration)
    {
        if (!gameObject.activeInHierarchy) return;
        StartCoroutine(FreezeRoutine(duration));
    }

    private IEnumerator FreezeRoutine(float duration)
    {
        isFrozen = true;
        isChasing = false; // stop chasing during freeze
        if (anim != null) anim.SetBool("isWalking", false);

        yield return new WaitForSeconds(duration);

        isFrozen = false;
    }
}
