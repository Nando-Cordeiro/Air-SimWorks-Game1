using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDefenseGameManager : MonoBehaviour
{
    [SerializeField] Text[] gameTimeTexts;
    [SerializeField] float gameTime;
    [SerializeField] float drainRate;
    [SerializeField] List<GameObject> enemies;
    float gameTimer;

    [SerializeField] int waveCredits;
    int playerCredits;

    [SerializeField] int numWaveEnemies;
    int enemiesLeft;
    [SerializeField] float timeBetweenEnemies;
    float enemySpawnTimer;
    [SerializeField] GameObject StartNewWave;
    [SerializeField] GameObject[] turretPurchaseButtons;
    [SerializeField] Text creditDisplay;

    [SerializeField] LayerMask cursorLayerMask;

    [SerializeField] GameObject cursorObject;
    GameObject cursorFollow = null;

    [SerializeField] GameObject defaultSelectTurret;
    [SerializeField] GameObject defaultTurret;

    private void Start()
    {
        cursorFollow = cursorObject;
        GainCredits(waveCredits);
        gameTimer = gameTime;
        lockCursor();
    }

    private static void lockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        UpdateGameTimer();
        CalculateMouse();
        StartWave();
    }


    void CalculateMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, cursorLayerMask))
        {
            cursorObject.transform.position = raycastHit.point;

            if(cursorFollow != cursorObject)
            {
                cursorFollow.transform.position = cursorObject.transform.position;
                if (Input.GetMouseButtonDown(0))
                {
                    PlaceTurret();
                }
            }
        }


    }

    void PlaceTurret()
    {
        GameObject previousObj = cursorFollow;
        cursorFollow = cursorObject;
        Instantiate(defaultTurret, cursorFollow.transform.position, Quaternion.identity);
        Destroy(previousObj);
    }

    void UpdateGameTimer()
    {
        //update display
        foreach (Text gameTimeText in gameTimeTexts)
        {
            gameTimeText.text = gameTimer.ToString("F2");
        }

        //count down
        if (gameTimer >= 0)
        {
            gameTimer -= (Time.deltaTime * drainRate);
        }
        else
        {
            gameTimer = 0;
        }
    }

    public void StartWave()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SetPurchaseActive(false);
            enemiesLeft = numWaveEnemies;
            SpawnEnemies();
            StartCoroutine(CountDownSpawn());
        }
        

    }

    void SetPurchaseActive(bool enabled)
    {
        foreach(GameObject purchaseOption in turretPurchaseButtons)
        {
            purchaseOption.SetActive(enabled);
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
        else
        {
            EndWave();
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
        SetPurchaseActive(true);
        StartNewWave.SetActive(true);
        GainCredits(waveCredits);
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
            MenuManager.instance.storeOpenAndClose();
            playerCredits -= turretCost;
            UpdateCreditDisplay();
            cursorFollow = Instantiate(defaultSelectTurret, cursorFollow.transform.position, Quaternion.identity);
        }
    }

    void UpdateCreditDisplay()
    {
        creditDisplay.text = "Credits: " + playerCredits.ToString();
    }
}
