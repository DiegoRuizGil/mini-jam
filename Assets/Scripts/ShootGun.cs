using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShootGun : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Transform _shootPoint;

    [SerializeField]
    private List<GameObject> _animals = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_animals.Contains(other.gameObject) && !other.CompareTag("Player"))
        {
            _animals.Add(other.gameObject);
            Debug.Log($"collider: {other.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_animals.Contains(other.gameObject) && !other.CompareTag("Player"))
        {
            _animals.Remove(other.gameObject);
        }
    }

    public void Shoot()
    {
        ShootNearestAnimal();
        Debug.Log("Shoot !");
    }

    private void ShootNearestAnimal()
    {
        if (_animals.Count <= 0) return;
        
        GameObject selectedAnimal = null;
        float nearestDistance = Mathf.Infinity;
        foreach (var animal in _animals)
        {
            float distance = Vector3.Distance(_shootPoint.position, animal.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                selectedAnimal = animal;
            }
        }
        
        // shoot selectedAnimal
        if (selectedAnimal)
        {
            Debug.Log($"Shoot -> {selectedAnimal.name}");
        }
    }
}
