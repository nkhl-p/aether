using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage(float amount) {
        Debug.Log("TakeDamage called!" + amount);
        health -= amount;
        if (health <= 0) {
            Debug.Log("No health remaining", gameObject);
            Destroy(gameObject);
        }
    }
}
