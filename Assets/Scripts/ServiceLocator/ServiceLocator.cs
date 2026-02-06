using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    public static ServiceLocator CurrentSericeLocator { get; private set; }
    private readonly Dictionary<string, IService> _services = new();
    private ServiceLocator()
    {
    }

    public static void Initialize()
    {
        CurrentSericeLocator = new ServiceLocator();
    }

    public T GetServise<T>() where T : IService
    {
        string key = typeof(T).Name;
        if (!_services.ContainsKey(key))
        {
            throw new Exception(typeof(T).Name + " not registred");
        }

        return (T)_services[key];
    }

    public void RegisterService<T>(T sercive) where T : IService
    {
        string key = typeof(T).Name;
        if (_services.ContainsKey(key))
        {
            Debug.Log(key.GetType().Name + " Already Registred");
            return;
        }
        _services.Add(key, sercive);
    }

    public void UnregisterService<T>() where T : IService
    {
        string key = typeof(T).Name;
        if (!_services.ContainsKey(key))
        {
            Debug.Log(typeof(T).Name + " not Registred");
            return;
        }

        _services.Remove(key);
    }

    public bool CheckServise<T>() where T : IService
    {
        string key = typeof(T).Name;
        if (_services.ContainsKey(key)) return true;
       return false;

    }
}