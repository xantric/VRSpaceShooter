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
            Destroy(gameObject);
        }
    }
}
