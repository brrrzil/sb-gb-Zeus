using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

class Dweller : MonoBehaviour
{
   
    [SerializeField] private float leftBorder, rightBorder, topBorder, bottomBorder;
    [SerializeField, Range(0.1f, 10)] private float changeSpeed;

    private Animator animator;
    private SpriteRenderer sprite;
    private Vector3 target;
    float x, z, lastx;

    public float LeftBorder { get; private set; }
    public float RightBorder { get; private set; }
    public float TopBorder { get; private set; }
    public float BottomBorder { get; private set; }   

    Variables variables;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(NewTarget());
        StartCoroutine(Survive());
        StartCoroutine(Rest());
        StartCoroutine(Eat());
        animator = GetComponent<Animator>();
        variables = new Variables();
    }

    void Update()
    {
        if (animator.GetBool("IsWalk"))
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, Time.deltaTime * variables.MovementSpeed() / 1.5f);
        }
    }

    private IEnumerator NewTarget()
    {
        x = (float)Random.Range(leftBorder, rightBorder + 1);
        z = (float)Random.Range(bottomBorder, topBorder + 1);

        if (x > lastx) sprite.flipX = false;
        if (x < lastx) sprite.flipX = true;

        target = new Vector3(x, transform.position.y, z);
        lastx = transform.position.x;
        yield return new WaitForSeconds(changeSpeed);
        StartCoroutine(NewTarget());
    }

    private IEnumerator Survive()
    {
        yield return new WaitForSeconds(4);

        if (variables.DeathRandom() == 0)
        {
            animator.SetBool("IsWalk", false);
            animator.SetBool("isDead", true);
        }
        else StartCoroutine(Survive());        
    }

    private IEnumerator Rest()
    {
        yield return new WaitForSeconds(changeSpeed);
        if (variables.RestRandom() == 0)
        {
            animator.SetBool("IsWalk", false);
        }
        else animator.SetBool("IsWalk", true);
        StartCoroutine(Rest());
    }

    public void Death()
    {
        variables.DecreasePopulation();
        Destroy(gameObject);
    }

    private IEnumerator Eat()
    {
        yield return new WaitForSeconds(1);
        variables.DecreaseFood(0.05f);
        if (variables.Food >= 0) StartCoroutine(Eat());
    }
}