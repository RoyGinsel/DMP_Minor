using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroll : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 3f;

    private float rightEdge;
    private float leftEdge;
    private Vector3 distanceBetweendEdges;

    void Start()
    {
        calculateEdges();
        distanceBetweendEdges = new Vector3(rightEdge - leftEdge, 0f, 0f);
    }

    private void calculateEdges()
    {
        var sprite = GetComponent<SpriteRenderer>();
        rightEdge = transform.position.x + sprite.bounds.extents.x / 4f;
        leftEdge = transform.position.x - sprite.bounds.extents.x / 4f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += scrollSpeed * Vector3.right * Time.deltaTime;
        if (transform.position.x > rightEdge || transform.position.x < leftEdge)
        {
            transform.position -= distanceBetweendEdges;
        }
    }
}
