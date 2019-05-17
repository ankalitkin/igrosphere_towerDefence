﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;

    private int _currentHealth;

    public int CurrentHealth => _currentHealth;

    public int MaxHealth => maxHealth;

    // Start is called before the first frame update
    private void Start()
    {
        _currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void RemoveLife()
    {
        _currentHealth--;
        if (_currentHealth == 0)
            GameManager.Instance.GameOver();
    }
}