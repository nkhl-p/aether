using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    #region Gun variables
    public float damage = 25f;
    public float range = 100f;
    #endregion

    #region Player variables
    public GameObject player;
    public ParticleSystem muzzleFlash;
    #endregion

    #region Impact variables
    public float impactForce = 100f;
    public GameObject impactEffect;
    #endregion

    #region LaserVariables
    public LineRenderer laserLine;
    public Transform laserOrigin;
    public float laserDuration = 0.05f;
    #endregion

    #region Enable Gun
    public static bool IsGunEnabled = false;
    #endregion


    private void Start() {
        player = GameObject.FindWithTag("Player");
        laserLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // By default, it is mapped to the left mouse button. Here, we will add the check to see if the gun powerup has been collected
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.F)) {
            if (IsGunEnabled) {
                Shoot();
            } else {
                // Managing gun shooting sounds - If target is not hit
                AudioManager temp = FindObjectOfType<AudioManager>();
                temp.Play(SoundEnums.TARGET_MISS_LASER.GetString());
            }
        }
    }

    public void Shoot() {
        // Actual shoot logic
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, range)) {
            // Gun effects
            muzzleFlash.Play();

            // Managing gun shooting sounds
            AudioManager temp = FindObjectOfType<AudioManager>();
            temp.Play(SoundEnums.LASER_SHOOT.GetString());

            // Shooting the laser using Line renderer
            laserLine.SetPosition(0, laserOrigin.position);

            // Shoot laser only if the target is in range and if the target is actually hit
            Target target = hit.transform.GetComponent<Target>();
            if (target != null) {
                laserLine.SetPosition(1, hit.point);
                target.TakeDamage(damage);
            }

            // Impact effect on the target (obstacles in our case) - this is not taking effect right now
            if (hit.rigidbody) {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            // Instantiate the particle effect upon successful hit at the target site
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            // Destroying the targets
            Destroy(impactGO, 2f);

            // Coroutine to show the laser effect
            StartCoroutine(ShowLaser());
        } else {
            // Managing gun shooting sounds - If target is not hit
            AudioManager temp = FindObjectOfType<AudioManager>();
            temp.Play(SoundEnums.TARGET_MISS_LASER.GetString());
        }
    }

    IEnumerator ShowLaser() {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}
