using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StartTrainAnimation : MonoBehaviour
{
    private TrainBehaviours departureTrain;

    // Start is called before the first frame update
    void Start()
    {
        departureTrain = FindObjectOfType<TrainBehaviours>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        StartCoroutine(departureTrain.MoveDirection(3f));
    }
}
