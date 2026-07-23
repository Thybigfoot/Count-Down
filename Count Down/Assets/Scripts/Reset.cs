using UnityEngine;
using UnityEngine.SceneManagement;
public class Reset : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player")) Reseting();
    }
    private void Reseting()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
