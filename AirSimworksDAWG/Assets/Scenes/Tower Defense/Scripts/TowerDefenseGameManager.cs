using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class TowerDefenseGameManager : MonoBehaviour
{
    [Header("Stats")]
    public int totalKills;
    public int currentWave;
    [SerializeField] float drainRate;

    [SerializeField] int waveCredits;
    int playerCredits;

    [SerializeField] int numWaveEnemies;
    int enemiesLeft;
    [SerializeField] float timeBetweenEnemies;
    float enemySpawnTimer;

    [SerializeField] LayerMask cursorLayerMask;

    [Header("References")]
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] List<GameObject> enemies;
    [SerializeField] GameObject startNewWave;
    [SerializeField] GameObject[] turretPurchaseButtons;
    [SerializeField] TextMeshProUGUI creditDisplay;
    [SerializeField] TextMeshProUGUI storeCreditDisplay;

    [SerializeField] GameObject cursorObject;
    GameObject cursorFollow = null;

    [SerializeField] GameObject defaultSelectTurret;
    [SerializeField] GameObject defaultTurret;

    public Movement player;

    bool started;

    public int totalPoints;
    float m, s;
    private bool ended;
    private int currTurretCost;

    private void Start()
    {
        m = FindObjectOfType<StartGame>().gameLength / 60;
        Cursor.lockState = CursorLockMode.Locked;
        cursorFollow = cursorObject;

        GainCredits(waveCredits);
    }

    private void Update()
    {
        if (!player.view.IsMine) return;

        if (m > -1) s -= Time.deltaTime;
        else
        {
            // end the game if time goes below 0
            if (!ended) EndGame();
        }

        if (s <= 0f)
        {
            m--;
            s = 60;
        }

        CalculateMouse();
        StartWave();

        if (enemiesLeft <= 0 && FindObjectsOfType<EnemyHealth>().Length <= 0 && started)
        {
            EndWave();
        }
    }

    private void OnGUI()
    {
        if (!player.view.IsMine) return;

        if (pointsText != null) pointsText.text = "Total points: " + totalPoints;
        if (timeText != null && s >= 10f) timeText.text = "Time remaining " + m + ":" + (int)s;
        else if (timeText != null && s < 10f) timeText.text = "Time remaining " + m + ":0" + (int)s;
    }

    void CalculateMouse()
    {
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, cursorLayerMask))
        {
            cursorObject.transform.position = raycastHit.point;

            if(cursorFollow != cursorObject)
            {
                cursorFollow.transform.position = cursorObject.transform.position;
                if (Input.GetMouseButtonDown(0)) PlaceTurret();

                if (Input.GetMouseButtonDown(1)) CancelTurret();
            }
        }
    }

    void PlaceTurret()
    {
        GameObject previousObj = cursorFollow;
        cursorFollow = cursorObject;
        Instantiate(defaultTurret, cursorFollow.transform.position, Quaternion.identity);
        Destroy(previousObj);
        playerCredits -= currTurretCost;
        UpdateCreditDisplay();
    }

    void CancelTurret()
    {
        GameObject previousObj = cursorFollow;
        cursorFollow = cursorObject;
        Destroy(previousObj);
    }

    public void StartWave()
    {
        if (Input.GetKeyDown(KeyCode.N) && FindObjectsOfType<EnemyHealth>().Length <= 0)
        {
            started = true;
            startNewWave.SetActive(false);
            enemiesLeft = numWaveEnemies;
            SpawnEnemies();
            StartCoroutine(CountDownSpawn());
        }
    }

    IEnumerator CountDownSpawn()
    {
        enemySpawnTimer = timeBetweenEnemies;
        while (enemySpawnTimer > 0)
        {
            enemySpawnTimer -= Time.deltaTime;
            yield return null;
        }

        if (enemiesLeft > 0) {
            SpawnEnemies();
            StartCoroutine(CountDownSpawn());
        }
    }

    void SpawnEnemies()
    {
        foreach(GameObject enemy in enemies)
        {
            Instantiate(enemy);
        }

        enemiesLeft -= 1;
    }

    void EndWave()
    {
        totalPoints += 150;

        started = false;
        startNewWave.SetActive(true);
        GainCredits(waveCredits);
        currentWave++;
    }

    void GainCredits(int numCredits)
    {
        playerCredits += numCredits;
        UpdateCreditDisplay();
    }

    public void PurchaseTurret(int turretCost)
    {
        if(cursorFollow != cursorObject)
        {
            return;
        }

        if(playerCredits >= turretCost)
        {
            Debug.Log("Purchased Turret");
            MenuManager.instance.StoreOpenAndClose();
            currTurretCost = turretCost;
            cursorFollow = Instantiate(defaultSelectTurret, cursorFollow.transform.position, Quaternion.identity);
        }
    }

    void UpdateCreditDisplay()
    {
        creditDisplay.text = "Credits: " + playerCredits.ToString();
        storeCreditDisplay.text = "Credits: " + playerCredits.ToString();
    }

    void EndGame()
    {
        DataManager dm = FindObjectOfType<DataManager>();
        dm.lastGamesPoints = totalPoints; // set after every game

        // set per level
        dm.skill1 = DataManager.Skills.ResourceManagement;
        dm.skill2 = DataManager.Skills.AnalyticalThinking;

        FindObjectOfType<PointsGiver>().GiveOutPoints();

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        ended = true;

        PhotonNetwork.LoadLevel("AfterGameLobby");
    }
}
