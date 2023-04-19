using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one GameManager instances");
            return;
        }
        instance = this;
    }

    #endregion

    public GameObject infoMenu;
    public GameObject player;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI descriptionText;
   
    public GameObject inventory;
    public GameObject questWindow;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("MainTheme");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            OpenInventory();

        if (Input.GetKeyDown(KeyCode.Q))
            OpenQuestWindow();
        

        if (!DialogueManager.instance.dialogueIsStarted)
        {
            DecreaseColdOverTime(PlayerStats.instance.coldDecreaseRate * Time.deltaTime);
        }

        if (PlayerStats.instance.isFrozen && !DialogueManager.instance.dialogueIsStarted)
            DecreaseHealthOverTime(PlayerStats.instance.healthDecreaseRate * Time.deltaTime);
    }

    public void OpenInventory()
    {
        inventory.SetActive(!inventory.activeSelf);
        infoMenu.SetActive(false);
        InventoryManager.instance.selectedItem = null;
    }

    public void OpenQuestWindow()
    {
        QuestManager.instance.OnClickQuestButton();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DecreaseColdOverTime(float rate)
    {
        PlayerStats.instance.UpdateCold(-rate);
    }

    void DecreaseHealthOverTime(float rate)
    {
        PlayerStats.instance.UpdateHealth(-rate);
    }

    public GameObject FindClosestInteractableObject(float maxDistance)
    {
        GameObject closest = null;
        List<GameObject> interactableObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Interactable"));

        float distance = Mathf.Infinity;
        float curDistance = 0;
        if (interactableObjects.Count > 0)
        {
            foreach (GameObject io in interactableObjects)
            {
                Vector3 diff = io.transform.position - player.transform.position;
                curDistance = diff.magnitude;
                if (curDistance < distance)
                {
                    closest = io;
                    distance = curDistance;
                }
            }

            if (distance <= maxDistance)
            {
                return closest;
            }
            else return null;
        }
        else return null;
    }
}
