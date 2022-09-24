using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public interface IDraggable
{
    public void OnBeginDrag(Vector3 mousePos);
    public void OnExitDrag(Vector3 mousePos);
}
