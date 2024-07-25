using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int score;
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        score = -1;
        change();
    }
    public void change()
    {
        score++;
        text.text = "Score : " + score;
    }
}
