using UnityEngine;
using System.Collections;
public class BoundsTest : MonoBehaviour
{
    Collider col;

    Vector3 point = Vector3.zero;
    public GameObject Orc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Spawner());
    }

    [ContextMenu("Randomise")]
    void Randomise()
    {
        col = GetComponent<Collider>();
        Bounds b = col.bounds;
        point = new Vector3(Random.Range(b.min.x, b.max.x), transform.position.y + 10f, Random.Range(b.min.z, b.max.z));
        Debug.Log(point);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(point, 10.0f);
    }

    IEnumerator Spawner()
    {
        for (int spawned = 1; spawned < 100; spawned++)
        {
            Randomise();
            yield return new WaitForSeconds(0.2f);
            Debug.Log("Spawned");
            Instantiate(Orc, point, Quaternion.identity);
        }

    }

}
