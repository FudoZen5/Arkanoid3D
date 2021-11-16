using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private TMP_Text toucheGates; 
    [SerializeField] private TMP_Text loseBall;
    int gatesScore = 0;
    int rocketScore = 0;

    public void AddScore()
    {
        rocketScore++;
        toucheGates.text = rocketScore.ToString();
    }
    
    public void LoseScore()
    {
        gatesScore++;
        loseBall.text = gatesScore.ToString();

    }
}
