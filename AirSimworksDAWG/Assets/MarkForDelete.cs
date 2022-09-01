using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkForDelete : MonoBehaviour
{
    bool markForDelete;

    public void SetForDelete()
    {
        markForDelete = true;
    }

    public bool GetStatus()
    {
        return markForDelete;
    }
}
