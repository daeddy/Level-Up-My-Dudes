using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agro : MonoBehaviour
{
    public GameUnit parent;

    void OnTriggerStay2D(Collider2D other)
    {
        GameEntity otherEntity = other.GetComponent<GameEntity>();

        if (otherEntity)
            parent.OnAgro(otherEntity);
    }
}
