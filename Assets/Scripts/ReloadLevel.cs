using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevel : MonoBehaviour
{
    [SerializeField] private string LevelName;

    public void Load()
    {
        SceneManager.LoadScene(LevelName);
    }
}
