using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage = 25f;
    public float range = 100f;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float impactForce = 100f;
    public float laserDuration = 0.05f;
    public Transform laserOrigin;

    public GameObject player;
    public LineRenderer laserLine;

    private void Start() {
        player = GameObject.FindWithTag("Player");
        laserLine = GetComponent<LineRenderer>();
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
        laserLine.SetPosition(0, laserOrigin.position);
        
        muzzleFlash.Play();
        AudioManager temp = FindObjectOfType<AudioManager>();
        temp.Play(SoundEnums.LASER_SHOOT.GetString());
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, range)) {

            Target target = hit.transform.GetComponent<Target>();
            if (target != null) {
                laserLine.SetPosition(1, hit.point);
                target.TakeDamage(damage);
            }

            if (hit.rigidbody) {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
            StartCoroutine(ShowLaser());
        }
    }

    IEnumerator ShowLaser() {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}
