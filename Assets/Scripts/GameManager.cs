using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Door door;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void OnEnemyDie()
    {
        if (FindObjectsOfType<Enemy>().Length == 0)
        {
            door.gameObject.SetActive(true);
        }
    }

    
}
