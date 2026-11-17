using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
public class BoundsTest : MonoBehaviour
{
    Collider col;

    [SerializeField] int wave = 1;
    private playerController Pc;
    private attackCollision Ac;
    Vector3 point = Vector3.zero;
    Vector3 powerupPoint = Vector3.zero;
    public GameObject Orc;
    public GameObject BossOrc;

    public GameObject Invincible;
    public GameObject SpeedUp;
    public GameObject DamageUp;
    public GameObject NoCooldown;
    public GameObject Powerup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Pc = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        StartCoroutine(Spawner());
        StartCoroutine(PowerupSpawner());
    }

    [ContextMenu("Randomise")]
    void Randomise()
    {
        col = GetComponent<Collider>();
        Bounds b = col.bounds;
        point = new Vector3(Random.Range(b.min.x, b.max.x), transform.position.y + 10f, Random.Range(b.min.z, b.max.z));
        
    }

    void PowerupRandomise()
    {
        col = GetComponent<Collider>();
        Bounds b = col.bounds;
        powerupPoint = new Vector3(Random.Range(b.min.x, b.max.x), transform.position.y, Random.Range(b.min.z, b.max.z));

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(point, 10.0f);
    }

    IEnumerator Spawner()
    {
        if (wave % 5 == 0)
        {
            Instantiate(BossOrc, point, Quaternion.identity);
        }
        for (int spawned = 1; spawned < 50 * wave; spawned++)
        {
            
            Randomise();
            yield return new WaitForSeconds(0.2f);
            
            Instantiate(Orc, point, Quaternion.identity);
        }
        yield return new WaitForSeconds(30f);
        wave += 1;
        Ac = GameObject.FindGameObjectWithTag("Enemy").GetComponent<attackCollision>();
        Ac.updateOrcHealthXp(wave);
        StartCoroutine(Spawner());

    }

    IEnumerator PowerupSpawner()
    {
        while (Pc.currentHealth > 0)
        {
            PowerupRandomise();
            int randomNum = Random.Range(0, 3);
            switch (randomNum)
            {
                case 0: Powerup = Invincible;
                break;
                case 1: Powerup = SpeedUp;
                break;  
                case 2: Powerup = DamageUp;
                break;
                case 3: Powerup = NoCooldown;
                break;  
            }
            Instantiate(Powerup, powerupPoint, Quaternion.identity);
            yield return new WaitForSeconds(15f);
            StartCoroutine(PowerupSpawner());

        }
    }
}
