using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] float damage;
    private AudioSource audioSource;

    [SerializeField] AudioClip clip;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ok");
        // Log when a collision is detected and with which object
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        // Check if the collided object has the "EnemyCube" tag
        if (collision.gameObject.CompareTag("Enemy"))
        {
            healthBar.Damage(damage);
            audioSource.PlayOneShot(clip);
            Destroy(collision.gameObject);
        }
        else
        {
            // Log if the collision was with an object that is not tagged as "EnemyCube"
            Debug.Log("Collision with non-enemy object: " + collision.gameObject.name);
        }
    }
}
