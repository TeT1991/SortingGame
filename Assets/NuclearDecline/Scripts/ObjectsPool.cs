using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool<T> where T : class
{
    private List<T> _objects;
    public ObjectsPool()
    {
        _objects = new List<T>();
    }

    public void AddOBject(T obj)
    {
        _objects.Add(obj);
    }

    public T GetOBject(int id)
    {
        var obj = _objects[id];
        return obj;
    }
}
