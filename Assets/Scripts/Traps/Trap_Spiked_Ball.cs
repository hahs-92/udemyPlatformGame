using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Spiked_Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Vector2 pushDirection;

    private void Start()
    {
        _rb.AddForce(pushDirection, ForceMode2D.Impulse);
    }
}
