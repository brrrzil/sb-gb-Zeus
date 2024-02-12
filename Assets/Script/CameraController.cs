using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject lookTarget;
    [SerializeField, Range(0, 1)] private float moveSpeed;

    private Vector3 moveTarget;
    private Vector3 startPosition;
    float x, y, z;

    void Start()
    {
        startPosition = gameObject.transform.position;
        moveTarget = startPosition;
        StartCoroutine(NewMoveTarget());
    }

    void Update()
    {
        gameObject.transform.LookAt(lookTarget.transform.position);
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, moveTarget, Time.deltaTime * moveSpeed);
    }

    private IEnumerator NewMoveTarget()
    {
        x = (float)Random.Range(-1, 2);
        y = (float)Random.Range(-1, 2);
        z = (float)Random.Range(-1, 2);

        moveTarget = new Vector3(x, y, z) + startPosition;
        yield return new WaitForSeconds(3);
        StartCoroutine(NewMoveTarget());
    }
}