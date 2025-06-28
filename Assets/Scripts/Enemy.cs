using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Enemy : MonoBehaviour
{
    public GameObject cubePiecePrefab;
    public float explodeForce = 800f;
    AudioSource destructionSound;
    [SerializeField] TextMeshProUGUI textScore;

    void Start()
    {
        destructionSound = GetComponent<AudioSource>();
        textScore.text = "Счет: " + Progress.Instance.PlayerInfo.Score.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            ExplodeCube();
            AddScore();
        }
    }

    private void AddScore()
    {
        if (Progress.Instance.PlayerInfo.Score == 100)
        {
            SceneManager.LoadScene("Workshop-7");
            Progress.Instance.PlayerInfo.Score = 0;
            textScore.text = "Счет: " + Progress.Instance.PlayerInfo.Score.ToString();
        }
        else
        {       
            Progress.Instance.PlayerInfo.Score += 10;
            textScore.text = "Счет: " + Progress.Instance.PlayerInfo.Score.ToString();
        }
    }

   private void ExplodeCube()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int z = 0; z < 4; z++)
                {
                    Vector3 piecePosition = transform.position + new Vector3(x, y, z) * 0.5f;
                    GameObject piece = Instantiate(cubePiecePrefab, piecePosition, Quaternion.identity);
                    Rigidbody pieceRigidbody = piece.GetComponent<Rigidbody>();
                    pieceRigidbody.AddExplosionForce(explodeForce, transform.position, 5f);
                }
            }
        }
        PlaySoundAndDestroy();
        Destroy(gameObject);
    }

    private void PlaySoundAndDestroy()
    {
        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource audioSource = tempAudio.AddComponent<AudioSource>();

        audioSource.clip = destructionSound.clip;
        audioSource.volume = destructionSound.volume;
        audioSource.pitch = destructionSound.pitch;

        audioSource.Play();

        Destroy(tempAudio, audioSource.clip.length);
    }
}