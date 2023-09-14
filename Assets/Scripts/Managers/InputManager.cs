using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager 
{
    private float sensitive = .5f; 

    public bool CheckTargetForMobile(out List<Target> targets)
    {
        targets = new List<Target>();

        if (Input.touchCount == 0)
            return false;

        // touch 모두 가져와서 루프 돌리기
        foreach (var touch in Input.touches)
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (Physics2D.OverlapCircle(touchPosition, sensitive)
                .TryGetComponent<Target>(out Target target))
            {
                targets.Add(target);
            }
        }

        if(targets.Count > 0)
            return true;

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
