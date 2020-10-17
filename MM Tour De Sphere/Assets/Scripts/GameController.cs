﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private GameObject current_player;

    private int points;
    private int points_multiplier;
    private float multiplier_time = 5.0f;
    private int multiplier_point_bracket = 5;
    private int multiplier_point_count = 0;

    public Text scoreText;
    public Text promptText;
    public Text playerHealthText;
    private string multiplierText;

    private bool multiplier_on;


    // Start is called before the first frame update
    void Start()
    {
        multiplier_on = false;
        points = 0;
        points_multiplier = 1;
        scoreText.text = "Score: 0";
        multiplierText = "Multiplier: 1X";
        current_player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        checkMultiplier();
    }

    void checkMultiplier()
    {
        multiplier_time -= Time.deltaTime;
        if (multiplier_time < 0 && multiplier_on == true)
        {
            UnityEngine.Debug.Log("MULTIPLIER OFF");
            multiplier_point_count = 0;
            multiplier_on = false;
            this.points_multiplier = 1;
            multiplierText = "Multiplier: " + this.points_multiplier + "X";
            displayText();
        }
    }

    public void addPoints(int point)
    {
        multiplier_on = true;
        if(multiplier_time > 0)
        {
            UnityEngine.Debug.Log("MULTIPLIER ON");
            multiplier_point_count++;
            if(multiplier_point_count == multiplier_point_bracket)
            {
                multiplier_point_count = 0;
                this.points_multiplier++;
                multiplierText = "Multiplier: " + this.points_multiplier + "X";
            }
            multiplier_time = 5.0f;
        }

        this.points += point * this.points_multiplier;
        displayText();
    }

    public void displayText()
    {
        scoreText.text = "POINTS: " + this.points + getMultiplier();
        playerHealthText.text = "HEALTH: " + getHealth();
    }

    string getMultiplier()
    {
        return "\n" + multiplierText;
    }

    string getHealth()
    {
        return current_player.GetComponent<PlayerController>().getHealth().ToString();
    }

    public void SetPrompt(string text)
    {
        promptText.text = text;
    }
}
