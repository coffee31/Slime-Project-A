using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    //Monster Move set
    public float speed = 0f;
    [SerializeField]
    Vector3 MoveDir = Vector3.zero;

    void Update()
    {
        move();
    }

    void move()
    {
        transform.position += MoveDir * speed * Time.deltaTime;
    }

    public void movedirection(Vector3 movedire)
    {
        MoveDir = movedire;
    }
}
