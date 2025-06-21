using UnityEngine;

public class RigidbodyMod : MonoBehaviour
{
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            //_rb.isKinematic = true;
            _rb.isKinematic = false;
            _rb.mass= 2000.0f;
            _rb.angularDamping = 0f;
        }
    }
}

