using UnityEngine;

public class attackCollision : MonoBehaviour
{
    private playerController Pc;
    public double orcHealth = 2.0f;
    public float orcXpWorth = 5.0f;
    public GameObject attackSphere;
    public playerController Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Pc = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (orcHealth <= 0)
        {
            Destroy(gameObject);
            Pc.updateExp(orcXpWorth);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            Destroy(other.gameObject);
            orcHealth -= Pc.playerDamage;
        }
    }

    public void  updateOrcHealthXp(int wave)
    {
        orcHealth = 4 * 1.1 * wave;
        orcXpWorth = 5 * wave / 2 * 1.1f;
    }
}
