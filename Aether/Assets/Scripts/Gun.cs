using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float impactForce = 100f;

    public GameObject player;

    private void Start() {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // By default, it is mapped to the left mouse button. Here, we will add the check to see if the gun powerup has been collected
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.F)) {
            Shoot();
        }
    }

    public void Shoot() {
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, range)) {
            Debug.Log("Object hit! " + hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null) {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody) {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
