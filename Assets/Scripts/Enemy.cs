using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float speed = 2.5f;
    private Transform player;
    public float wanderRadius = 0f; // if >0, can wander when no player found

    void Start()
    {
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        // Enemy follows player
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // signal game over
            //GameManager.Instance.GameOver();
        }
    }
}
