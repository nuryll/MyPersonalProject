using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 2.5f;        // speed while chasing
    public float rotationSpeed = 5f;      // smooth rotation
    public float detectionRange = 4f;    // start chasing if player closer than this
    public float stopRange = 7f;         // stop chasing if player goes farther than this

    [Header("References")]
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
        if (player == null || isFrozen) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Switch between idle and chase states
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
    }

    void MoveTowardPlayer()
    {
        Vector3 dir = (player.position - transform.position);
        dir.y = 0;
        dir.Normalize();

        // Rotate smoothly toward player
        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

        // Move forward
        transform.position += transform.forward * walkSpeed * Time.deltaTime;

        // Animation
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

    // Optional freeze for hearts
    public void Freeze(float duration)
    {
        if (!isFrozen) StartCoroutine(FreezeRoutine(duration));
    }

    IEnumerator FreezeRoutine(float duration)
    {
        isFrozen = true;
        if (anim != null) anim.SetBool("isWalking", false);
        float t = 0f;
        while (t < duration)
        {
            transform.Rotate(Vector3.up, 100f * Time.deltaTime); // spin in place
            t += Time.deltaTime;
            yield return null;
        }
        isFrozen = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //GameManager.Instance.GameOver();
        }
    }
}
