using UnityEngine;

public interface ICollectable
{
    void Collect(Transform target);
    bool CanBeCollect(Transform target);
}
