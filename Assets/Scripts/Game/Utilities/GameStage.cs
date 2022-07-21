using System;
using UnityEngine;
using TMPro;

public class GameStage : MonoBehaviour
{
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI timerText;
    public EnemySpawner spawner;

    [Space]
    //Time
    [SerializeField] 
    private int currentStage = 1;

    public float time;
    public float stageLenght;
    private float nextStage;

    [Space]
    //Enemy Attacking
    private float timer;
    private float showTime;
    private float timeOfAttack;
    public float lengthOfAttack;
    public bool enemyAttacking;

    [Space]
    //Difficulty
    public int nextStageChange = 4;
    private int solidStageChange;
    public int enemyToSpawn = 6;
    private int addEnemy = 1;
    void Start()
    {
        nextStage = stageLenght;
        stageText.text = currentStage.ToString();

        timeOfAttack = nextStage - lengthOfAttack;
        solidStageChange = nextStageChange;

        timer = stageLenght - lengthOfAttack;
    }
    void Update()
    {
        time = Time.time;
        time = Mathf.Round(time * 10.0f) / 10.0f;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            showTime = Mathf.FloorToInt(timer + 1);
        }
        else
        {
            timer = 0f;
        }


        timerText.text = showTime.ToString();


        if (time >= nextStage)
        {
            AddDay();
            DisableEnemySpawning();
        }

        if (time >= timeOfAttack && enemyAttacking == false)
        {
            EnableEnemySpawning();
        }
    }
    private void AddDay()
    {
        currentStage++;
        stageText.text = currentStage.ToString();
        nextStage += stageLenght;


        timeOfAttack = nextStage - lengthOfAttack;
        enemyAttacking = false;

        enemyToSpawn += addEnemy;

        if (currentStage == nextStageChange)
        {
            nextStageChange += solidStageChange;
            addEnemy++;
        }
        timer = lengthOfAttack;
    }
    private void EnableEnemySpawning()
    {
        spawner.enabled = true;
        spawner.SetNumber(enemyToSpawn);
        enemyAttacking = true;
    }
    private void DisableEnemySpawning()
    {
        spawner.enabled = false;
        enemyAttacking = false;
    }
}
