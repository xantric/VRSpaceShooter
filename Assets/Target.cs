using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip destroySound;
    public GameObject particles;
    // Start is called before the first frame update
    public void Burst()
    {
        
        Score score = FindAnyObjectByType<Score>();
        if (gameObject != null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            if (destroySound != null && audioSource != null)
            {
                Debug.Log("Play Sound");
                audioSource.PlayOneShot(destroySound);
            }
            score.change();
            Instantiate(particles, transform.position, Quaternion.identity);
            var replacement = Instantiate(particles, transform.position, transform.rotation);

            var rbs = replacement.GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rbs)
            {
                rb.AddExplosionForce(50, transform.position, 2);
            }
            Destroy(gameObject);
        }
    }
}
