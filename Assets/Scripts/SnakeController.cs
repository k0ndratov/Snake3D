using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeController : MonoBehaviour
{
    public UnityEvent OnEat;

    public List<Transform> Tails;

    [Range(0,1)]
    public float SnakeMovementSpeed = 1;

    [Range(0, (float)0.1)]
    public float SnakeSpeedUpper;

    [Range(0, 10)]
    public float SnakeRotationSpeed;


    public GameObject bonePrefab;
    [Range(1, 10)]
    public float BonesDistance;

    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        MoveSnake(_transform.position + _transform.forward * SnakeMovementSpeed);

        float angel = Input.GetAxis("Horizontal") * SnakeRotationSpeed;;
        _transform.Rotate(0, angel, 0);
    }

    private void MoveSnake(Vector3 newPosition)
    {
        float sqlDistance = BonesDistance * BonesDistance;

        Vector3 previousPosition = _transform.position;

        foreach (var bone in Tails)
        {
            if ((bone.position - previousPosition).sqrMagnitude > sqlDistance)
            {   
                var temp = bone.position;
                bone.position = previousPosition;
                previousPosition = temp;
            }
            else
            {
                break;
            }
        }

        _transform.position = newPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            if (OnEat != null)
            {
                OnEat.Invoke();
            }
            Destroy(collision.gameObject);
            var bone = Instantiate(bonePrefab, _transform.position,_transform.rotation);

            Tails.Add(bone.transform);

            SnakeMovementSpeed += (float)0.2;
        }
    }
}
