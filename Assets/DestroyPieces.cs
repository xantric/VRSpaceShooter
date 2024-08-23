using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPieces : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    float scale = 0.1f;
    float duration = 0;

    // Update is called once per frame
    void Update()
    {
        duration += Time.deltaTime;
        scale -= Time.deltaTime * scale * speed;
        transform.localScale = new Vector3(scale, scale, scale);
        if(scale <= 0 || duration > 1)
        {
            Destroy(gameObject);
        }
    }
}
