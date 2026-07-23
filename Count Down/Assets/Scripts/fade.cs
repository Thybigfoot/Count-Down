using UnityEngine;
using System.Collections;

public class fade : MonoBehaviour
{
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private float invisibleTime = 3f;

    private SpriteRenderer sprite;
    private Collider2D col;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void BreakCloud()
    {
        StartCoroutine(FadeCloud());
    }

    private IEnumerator FadeCloud()
    {
        // Fade out
        yield return StartCoroutine(Fade(1f, 0f));

        // Disable collision
        col.enabled = false;

        // Stay gone
        yield return new WaitForSeconds(invisibleTime);

        // Enable collision again before fading back
        col.enabled = true;

        // Fade back in
        yield return StartCoroutine(Fade(0f, 1f));
    }


    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timer = 0f;
        Color colour = sprite.color;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeTime);

            sprite.color = new Color(
                colour.r,
                colour.g,
                colour.b,
                alpha
            );

            yield return null;
        }

        // Ensure final value is exact
        sprite.color = new Color(
            colour.r,
            colour.g,
            colour.b,
            endAlpha
        );
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            BreakCloud();
        }
    }
}