using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager 
{
    private float sensitive = .3f; 

    public bool CheckTargetForMobile(out Target target)
    {
        target = null;

        if (Input.touchCount == 0)
            return false;

        // touch 모두 가져와서 루프 돌리기
        Vector2 touchPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        if (Physics2D.OverlapCircle(touchPoint, sensitive)
            .TryGetComponent<Target>(out target))
        {
            return true;
        }

        return false;
    }

    public bool CheckTargetForPC(out Target target)
    {
        target = null;

        if (Input.GetMouseButtonDown(0) == false)
            return false;
        Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D col = Physics2D.OverlapCircle(mousePoint, sensitive);
        if(col != null) 
        { 
           if(col.TryGetComponent<Target>(out target))
                return true;
        }

        return false;
    }

    public bool CheckTouch()
    {
        return Input.GetMouseButton(0);
    }
}
