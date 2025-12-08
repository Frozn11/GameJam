using UnityEngine;
using UnityEngine.UI;
 
public class GiftPickUp : MonoBehaviour
{
    public Text scoreText;  
 
    public int score = 0;    
    public AudioClip pickupSound;  
 
    private AudioSource audioSource;
 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        UpdateScoreUI();
    }

    void Update() {
        if (score >= 5) {
            OptionsMenu.Instance.Win();
        }
    }
 
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Gift"))
        {
            CollectGift(other.gameObject);
        }
    }
 
    void CollectGift(GameObject giftObject)
    {
        score++;
        if (pickupSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
        HealthBar.Instance.HealMax();
        Destroy(giftObject);
        UpdateScoreUI();
    }
 
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
}