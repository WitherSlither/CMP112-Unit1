using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private AudioSource source;

    public AudioClip attacksound;

    public GameObject attack;
    public float speed = 500.0f;
    private bool recentlyAttacked = false;
    [SerializeField] private float attackCooldown = 1.5f;
    private bool attackPowerup = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            source.PlayOneShot(attacksound, 1.0f);
            spawn();
        }
    }
    

    void spawn()
    {
        if (recentlyAttacked == false)
        {
            GameObject attackClone = Instantiate(attack, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), transform.rotation) as GameObject;
            Rigidbody attackCloneRigidBody = attackClone.GetComponent<Rigidbody>();
            attackCloneRigidBody.AddForce(Vector3.forward * speed);
            if (attackPowerup == false)
            {
                recentlyAttacked = true;
                StartCoroutine(cooldown());
            }
        }
    }


   
    
    IEnumerator cooldown()
    { 
        yield return new WaitForSeconds(attackCooldown);
        recentlyAttacked = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletCD"))
        {
            Debug.Log("BCD");
            Destroy(other.gameObject);
            attackCooldown = 0.0f;
            StartCoroutine(timer());
        }
       
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(5.0f);
        attackCooldown = 1.5f;
    }
}