using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class DifficultySelector : MonoBehaviour
{
    Button btn;
    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(delegate { LoadDifficulty(difficulty);});
    }
    public enum EDifficultySelector
    {
        Easy = 1,
        Normal = 2,
        Hard = 3,
        DarkSouls = 4
    } public EDifficultySelector difficulty;

    public void LoadDifficulty(EDifficultySelector d)
    {
        SceneManager.LoadScene((int)difficulty);
    }
}
