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
    public Transform TableContent;
    public Transform RowContent;
    private TextMeshProUGUI RankTxt;
    private TextMeshProUGUI ScoreTxt;
    List<HighScore> highScores;
    private void Start()
    {

        saveGameManager = GetComponent<SaveGameManager>();
        try
        {
            highScores = saveGameManager.LoadHightScore().Take(10).ToList();// gioi han hien 10 diem
        }
        catch
        {
            //truong hop high score chua co nen bi null
            return;
        }
        float tableHeight = 2f;
        for (int i = 0; i < highScores.Count; i++)
        {
            Transform Row = Instantiate(RowContent, TableContent);
            RectTransform rectTransform = Row.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, -tableHeight * i);
            //Row.gameObject.SetActive(true);
            int rank = i + 1;
            Row.transform.Find("RankTxt").GetComponent<TextMeshProUGUI>().text = rank.ToString();
            //RankTxt = gameObject.GetComponent<TextMeshProUGUI>();
            //RankTxt.text = rank.ToString();
            int score = highScores.ElementAt(i).score;
            Row.transform.Find("ScoreTxt").GetComponent<TextMeshProUGUI>().text = score.ToString();

        }
    }


    public void LoadRecords()
    {
        //record = new TextMeshProUGUI();
        List<HighScore> highScores = saveGameManager.LoadHightScore();

        if (highScores != null)
            highScores = highScores.OrderByDescending<HighScore, int>(h => h.score).Take(10).ToList();

    }

}
