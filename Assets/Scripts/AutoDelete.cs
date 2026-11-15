using System.Collections;
using UnityEngine;

public class AutoDelete : MonoBehaviour
{
    [SerializeField] float bulletStayTime = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(delete());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator delete()
    {
        yield return new WaitForSeconds(bulletStayTime);
        Destroy(gameObject);
    }
}
