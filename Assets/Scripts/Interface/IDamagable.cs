using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(float amount, float knockBackForce, Vector2 damageSourcePosition);
}
