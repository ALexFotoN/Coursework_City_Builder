using System.Collections.Generic;
using UnityEngine;

public class CarsPool : MonoBehaviour
{
    private List<Transform> _pool = new();

    public Transform GetCar(Transform prefab)
    {
        foreach (var car in _pool)
        {
            string originalName = car.name.Replace("(Clone)", "");
            if (!car.gameObject.activeInHierarchy && originalName == prefab.name)
            {
                return car;
            }
        }

        return CreateNewCar(prefab);
    }

    private Transform CreateNewCar(Transform prefab)
    {
        var car = Instantiate(prefab);
        _pool.Add(car);
        return car;
    }
}
