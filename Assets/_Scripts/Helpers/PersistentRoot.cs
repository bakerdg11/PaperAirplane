using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentRoot : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }



}
