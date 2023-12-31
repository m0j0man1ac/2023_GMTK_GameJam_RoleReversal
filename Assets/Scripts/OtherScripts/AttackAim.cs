using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAim : MonoBehaviour
{
    public Vector3 screenPosition;
    public Vector3 worldPosition;

    public float speed = 2.0f;
    public float closestDistance;

    public float originalX;
    public bool isMoving = true;

    public PolygonCollider2D playerCol;

    public Card card;

    public bool attackAttacked = false;
    void Awake()
    {
        playerCol = GameObject.Find("Hero").GetComponent<PolygonCollider2D>();
        originalX = this.transform.position.x;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = false;
        }
        if (isMoving)
        {
            transform.position = new Vector2(originalX + Mathf.Sin(Time.time * 5) * speed, GameObject.Find("Hero").GetComponent<Transform>().transform.position.y);
        }

        if (isMoving == false && closestDistance > 0.01f && attackAttacked == false)
        {
            Attack();
            attackAttacked = true;
            Destroy(this.gameObject);
        }
        //move attack aim with mouse x position
        /*
        screenPosition = Input.mousePosition;
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = new Vector2(worldPosition.x, 0);
        */

        GetClosestDistance(playerCol, this.GetComponentInChildren<PolygonCollider2D>());

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (closestDistance > 0.01f)
            {
                Debug.Log("miss");
            }
            else
            {
                Debug.Log("hit");
            }
        }
    }

    public void PreAttack(Card card)
    {
        this.card = card;
    }

    public void Attack()
    {
        //foreach (CardEffect effect in card.effects)
        //{
        //    effect.DoEffect(card);
        //}
    }


    private float GetClosestDistance(PolygonCollider2D playerCol, PolygonCollider2D attackCol)
    {
        Vector2[] points1 = playerCol.points;
        Vector2[] points2 = attackCol.points;
        closestDistance = Mathf.Infinity;

        foreach (Vector2 vertex1 in points1)
        {
            Vector2 worldVertex1 = playerCol.transform.TransformPoint(vertex1);

            foreach (Vector2 vertex2 in points2)
            {
                Vector2 worldVertex2 = attackCol.transform.TransformPoint(vertex2);
                float distance = Vector2.Distance(worldVertex1, worldVertex2);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                }
            }
        }

        return closestDistance;
    }
}
