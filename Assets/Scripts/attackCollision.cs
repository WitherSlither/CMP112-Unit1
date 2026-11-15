using UnityEngine;

public class attackCollision : MonoBehaviour
{

    [SerializeField] float orcHealth = 2.0f;
    [SerializeField] float orcXpWorth = 5.0f;
    public GameObject attackSphere;
    public playerController Player;
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
            Player.updateExp(orcXpWorth);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            Destroy(other.gameObject);
            orcHealth -= Player.getDamage();
        }
    }
}
