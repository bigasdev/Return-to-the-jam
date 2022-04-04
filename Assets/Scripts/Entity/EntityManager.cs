using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    private static EntityManager instance;
    public static EntityManager Instance{
        get{
            if(instance == null)instance = FindObjectOfType<EntityManager>();
            return instance;
        }
    }
    public EntityData[] enemies;
}
