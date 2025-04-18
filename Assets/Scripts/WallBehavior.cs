using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class WallBehavior : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI uiText;
    public int randomAdd;
    public static bool cloneAk = false;
    public static bool cloneShotgun = false;
    public static bool clonePistol = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateUIText();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.playerDead)
        {
            this.enabled = false;
        }
    }
    void UpdateUIText()
    {
        randomAdd = Random.Range(-2, 2); // random add value
        if (randomAdd < 0)
        {
            uiText.text = randomAdd.ToString();
        }
        if (randomAdd >= 0)
        {
            uiText.text = "+" + randomAdd.ToString();
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ak"))
        {
            cloneAk = true;
        }
        if (other.gameObject.CompareTag("shotgun"))
        {
            cloneShotgun = true;
        }
        if (other.gameObject.CompareTag("pistol"))
        {
            clonePistol = true;
        }
    }
}

