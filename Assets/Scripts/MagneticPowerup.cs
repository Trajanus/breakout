using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticPowerup : MonoBehaviour
{
    public AudioClip collisionSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySingle(collisionSound);
        Destroy(gameObject);
    }
}
