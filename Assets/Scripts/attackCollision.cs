using UnityEngine;

public class attackCollision : MonoBehaviour
{

    [SerializeField] float orcHealth = 2.0f;
    public GameObject attackSphere;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (orcHealth <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            Destroy(other.gameObject);
            orcHealth -= 1;
        }
    }
}
