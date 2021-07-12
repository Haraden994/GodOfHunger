using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    // Main character reference for enemies
    public GameObject mainCharacter;
    // Actual level goal for main character
    public Transform actualGoal;

    public List<GameObject> enemies;

    public bool mainCharacterActive;

    void Start()
    {
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    public void SetPlayerActive(bool b)
    {
        mainCharacterActive = b;
    }

    private void SetGoal(Transform goal)
    {
        actualGoal = goal;
    }

    public void EnemyDead(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

}
