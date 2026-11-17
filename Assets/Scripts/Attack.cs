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
            recentlyAttacked = true;
            StartCoroutine(cooldown());
        }
    }

    IEnumerator cooldown()
    { 
        yield return new WaitForSeconds(attackCooldown);
        recentlyAttacked = false;
    }
}