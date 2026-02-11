using UnityEngine;
using DG.Tweening;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using System.Collections;

public class CarManager : MonoBehaviour
{
    [SerializeField]
    private float _carSpeed;
    [SerializeField]
    private float _minTimeBetweenCars = 4;
    [SerializeField]
    private float _maxTimeBetweenCars = 12;
    [SerializeField]
    private Transform[] _carPrefabs;
    [SerializeField]
    private Transform[] _firstLinePoints;
    [SerializeField]
    private Transform[] _secondLinePoints;

    private float _baseTimeToMove = 10;

    private void Start()
    {
        StartCoroutine(LifeTime());
    }

    private IEnumerator LifeTime()
    {
        while (gameObject)
        {
            CreateCar();
            yield return new WaitForSeconds(Random.Range(_minTimeBetweenCars, _maxTimeBetweenCars));
        }
    }

    private void CreateCar()
    {
        var car = Instantiate(_carPrefabs[Random.Range(0, _carPrefabs.Length)]);
        var direction = Random.Range(0, 2);
        if(direction == 0)
        {
            car.position = _firstLinePoints[0].position;
            StartMove(car, _firstLinePoints, () => Destroy(car.gameObject));
        }
        else
        {
            car.position = _secondLinePoints[0].position;
            StartMove(car, _secondLinePoints, () => Destroy(car.gameObject));
        }
    }

    private void StartMove(Transform car, Transform[] linePoints, Action callback = null)
    {
        var first = linePoints[0];
        var startPos = first.position;
        var points = linePoints.Where(x => x != first).Select(x => x.position).ToArray();
        car.DOPath(points, _baseTimeToMove / _carSpeed, PathType.CubicBezier, PathMode.TopDown2D)
            .OnComplete(() => callback?.Invoke()).SetEase(Ease.Linear);
    }
}
