using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    public List<Transform> Tails;
    [Range(0,3)]
    public float BonesDistance;
    public GameObject BonePrefab;
    [Range(0,4)]
    public float Speed;

    private bool _stop=false;

    private float _angel = 0;

    private Transform _transform;


    private void Start()
    {

        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        MoveSnake(_transform.position + _transform.forward * Speed);
         _angel = Input.GetAxis("Horizontal") *4;

        if (_stop)
        {
            Speed += Input.GetAxis("Vertical")*0 + 0.09F;
            if (Speed < 0) Speed = 0;
            _stop = false;
            
        }

        

        _transform.Rotate(0,_angel,0);
        
    }

    private void MoveSnake(Vector3 newPosition)
    {
        float sqrDistace = BonesDistance * BonesDistance;
        Vector3 previousPosition = _transform.position;

        foreach (var bone in Tails)
        {
            
            if ((bone.position - previousPosition).sqrMagnitude > sqrDistace)
            {
                var tempPosition = bone.position;
                bone.position = previousPosition;
                previousPosition = tempPosition;
                
            }
            else
            {
                break;
                
            }
            bone.rotation = _transform.rotation;
        }

        transform.position = newPosition;

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Food":
                Destroy(collision.gameObject);
                var bone = Instantiate(BonePrefab);
                Tails.Add(bone.transform);
                break;
            case "Wall":
                Speed = 0;
                break;
            case "Stone":
                Speed = 0;
                _stop = true;
                break;
        }
    }

}

