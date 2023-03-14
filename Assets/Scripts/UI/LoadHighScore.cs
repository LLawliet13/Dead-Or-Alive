using Assets.Scenes.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LoadHighScore : MonoBehaviour
{
    // Start is called before the first frame update
    SaveGameManager saveGameManager;
    void Start()
    {
        saveGameManager = GetComponent<SaveGameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public TextMeshProUGUI record;
    public void LoadRecords()
    {
        List<HighScore> highScores = saveGameManager.LoadHightScore();

        if (highScores != null)
            highScores = highScores.OrderByDescending<HighScore, int>(h => h.score).Take(10).ToList();
    }

}
