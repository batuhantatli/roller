using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventSystem : MonoBehaviour
{
    public delegate void MoveControl();
    public static event MoveControl onLeftClicked;
    public static event MoveControl onLeftClickHolding;

    private void Update() 
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(onLeftClicked != null)
            {
                onLeftClicked();
            }
        }
        if(Input.GetMouseButton(0))
        {
            if(onLeftClicked != null)
            {
                onLeftClickHolding();
            }
        }
    }
}
