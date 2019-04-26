﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalMovement : EnemyMovement
{

    public float[] m_patrolPosition;

    private int nextPosition;
    private Vector3 m_playerLastPosition;
    private Transform m_playerBodyCollider;
    private bool m_checkLastPosition; // say if checked last player position;

    private void Start()
    {
        m_playerBodyCollider = GameController.instance.player.transform.GetChild(5).GetChild(0).transform;
    }

    private void Update()
    {
        if (!m_playerInSight)
        {
            Patrol();
        }
        else
        {
            ChasePlayer();
        }
    }

    protected override void Patrol()
    {
        if (CalculateDistance())
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(m_patrolPosition[nextPosition], transform.localPosition.y), m_movementSpeed * Time.deltaTime);
        }
        else
        {
            nextPosition++;
            if (nextPosition >= m_patrolPosition.Length)
            {
                nextPosition = 0;
            }
        }
    }

    protected virtual bool CalculateDistance()
    {
        if (transform.localPosition.x == m_patrolPosition[nextPosition])
        {
            return false;
        }
        return true;
    }

    protected override void ChasePlayer()
    {
        if (m_playerInSight && !m_checkLastPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(m_playerBodyCollider.position.x, transform.position.y), m_movementSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(m_playerLastPosition.x, transform.position.y), m_movementSpeed * Time.deltaTime);
            if (Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(m_playerLastPosition.x)) < 0.1f)
            {
                m_checkLastPosition = true;
                m_playerInSight = false;
            }
        }
    }

    public override void PlayerInSight()
    {
        m_checkLastPosition = false;
        m_playerInSight = true;
    }

    public override void PlayerOutOfSight()
    {
        m_playerInSight = false;
        m_playerLastPosition = m_playerBodyCollider.position;
    }
}