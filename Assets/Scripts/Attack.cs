using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject attack;
    public float speed = 500.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spawn();
        }
    }

    void spawn()
    {

        GameObject attackClone = Instantiate(attack, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), transform.rotation) as GameObject;
        Rigidbody attackCloneRigidBody = attackClone.GetComponent<Rigidbody>();
        attackCloneRigidBody.AddForce(Vector3.forward * speed);
    }
}