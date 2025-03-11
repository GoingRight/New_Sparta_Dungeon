using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingPlatform : MonoBehaviour
{
    public int pauseTime;
    public float speed;
    public float moveDistance;
    public Vector3 startposition;

    void Start()
    {
        startposition = transform.position;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        Vector3[] directions = { Vector3.forward, Vector3.left, Vector3.back, Vector3.right };

        while (true)
        {
            foreach(Vector3 dir in directions)
            {
                while ((transform.position - startposition).magnitude < moveDistance)
                {
                    transform.Translate(dir * speed * Time.deltaTime);
                    yield return null;
                }
                startposition = transform.position;
                yield return new WaitForSeconds(pauseTime);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.collider.bounds.min.y > transform.position.y)
        {
            collision.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
