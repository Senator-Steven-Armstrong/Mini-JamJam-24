using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFollowMouse : MonoBehaviour
{
    private Vector3 _mousePosition;
    public AudioSource _audioSource;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePosition.z = 0;
        transform.position = _mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            _audioSource.Play();
        }
        
    }
}
