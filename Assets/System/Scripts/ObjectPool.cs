﻿using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This class allows us to create multiple instances of a prefabs and reuse them.
/// It allows us to avoid the cost of destroying and creating objects.
/// </summary>
public class ObjectPool
{
    protected List<GameObject> m_UsedInstances = new List<GameObject>();
    protected Stack<GameObject> m_FreeInstances = new Stack<GameObject>();
    protected GameObject m_Original;
    public string name;

    public ObjectPool (GameObject original, int initialSize)
    {
        name = original.name;
        m_Original = original;
        m_FreeInstances = new Stack<GameObject>(initialSize);

        for (int i = 0; i < initialSize; ++i)
        {
            GameObject obj = Object.Instantiate(original);
            obj.SetActive(false);
            m_FreeInstances.Push(obj);
        }
    }

    public GameObject Get ()
    {
        return Get(Vector3.zero, Quaternion.identity);
    }

    public GameObject Get (Vector3 pos, Quaternion quat)
    {
        GameObject ret = m_FreeInstances.Count > 0 ? m_FreeInstances.Pop() : Object.Instantiate(m_Original);

        ret.SetActive(true);
        ret.transform.position = pos;
        ret.transform.rotation = quat;
        m_UsedInstances.Add(ret);
        return ret;
    }

    public void Free (GameObject obj)
    {
        obj.transform.SetParent(null);
        obj.SetActive(false);
        m_FreeInstances.Push(obj);
        m_UsedInstances.Remove(obj);
    }

    public void CleanPool ()
    {
        for (int i = 0; i < m_FreeInstances.Count; ++i)
        {
            GameObject.Destroy(m_FreeInstances.Pop());
        }
        m_FreeInstances.Clear();

        for (int i = 0; i < m_UsedInstances.Count; ++i)
        {
            GameObject.Destroy(m_UsedInstances[i]);
        }
        m_UsedInstances.Clear();

    }
}
