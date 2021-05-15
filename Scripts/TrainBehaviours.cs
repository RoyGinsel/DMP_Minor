using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TrainBehaviours : MonoBehaviour
{
    public float speed = 2.5f;
    public Vector3 beginPosition;
    // Start is called before the first frame update
    void Start()
    {
        beginPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartMoveDirection(float totalMovementX)
    {
        StartCoroutine(MoveDirection(totalMovementX));
    }

    public IEnumerator MoveDirection(float totalMovementX)
    {
        while (transform.position.x < beginPosition.x + totalMovementX)
        {
            transform.position += transform.right * Time.deltaTime * speed;
            yield return null;
        }
    }
}
