using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bom : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    private void OnMouseDown()
    {
        levelManager.BomDown(this);
    }
}
