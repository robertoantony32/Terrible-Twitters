using System.Collections;
using UnityEngine;

[SelectionBase]
public class Monster : MonoBehaviour
{
    [SerializeField] private Sprite _deadSprite;
    [SerializeField] private ParticleSystem _particleSystem;
    private bool _hasDied;

    private void OnCollisionEnter2D(Collision2D col)
    {

        if (ShouldDieFromCollision(col))
        {
            StartCoroutine(Die());
        }
        Bird bird = col.gameObject.GetComponent<Bird>();

        if (bird != null)
        {
            gameObject.SetActive(false);
        }
    }

    private bool ShouldDieFromCollision(Collision2D collision)
    {
        if (_hasDied)
            return false;
        
        Bird bird = collision.gameObject.GetComponent<Bird>();

        if (bird != null)
            return true;

        if (collision.contacts[0].normal.y < -0.5)
            return true;
        
        return false;
    }

    IEnumerator Die()
    {
        _hasDied = true;
        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        _particleSystem.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
