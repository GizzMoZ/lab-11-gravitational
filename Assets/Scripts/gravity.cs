using NUnit.Framework;
using Unity.Android.Gradle.Manifest;
using Unity.Burst.CompilerServices;
using UnityEngine;
using System.Collections.Generic;

public class gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.006674f;

    public static List<gravity> otherObjectslist;

    [SerializeField] bool planet = false;
    [SerializeField] int orbitspeed = 1000;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (otherObjectslist == null)
        {
            otherObjectslist = new List<gravity>();
        }

        otherObjectslist.Add(this);

        if (!planet)
        { rb.AddForce(Vector3.left * orbitspeed); }

    }

    private void FixedUpdate()
    {
        foreach (gravity obj in otherObjectslist)
        {
            if (obj != this)
            {
                Attract(obj);
            }
        }
    }

    void Attract(gravity other)
    {
        Rigidbody otherRb = other.rb;

        Vector3 direction = rb.position - otherRb.position;

        float distance = direction.magnitude;

        if(distance == 0f) { return; }

        float forceMagnitude = G * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);

        Vector3 gravityForce = forceMagnitude * direction.normalized;

        otherRb.AddForce(gravityForce);
    }
}