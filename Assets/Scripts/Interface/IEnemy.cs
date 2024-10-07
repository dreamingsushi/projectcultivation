using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void MoveTowardPlayer(float moveSpeed, Vector2 moveDirection);
}
