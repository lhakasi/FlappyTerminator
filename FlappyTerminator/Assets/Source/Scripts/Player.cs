using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(ScoreCounter))]
[RequireComponent(typeof(PlayerCollisionHandler))]
public class Player : MonoBehaviour, ICharacter
{
    [SerializeField] private AudioSource _dyingSound;

    private PlayerMover _playerMover;
    private ScoreCounter _scoreCounter;
    private PlayerCollisionHandler _handler;

    public event Action GameOver;

    private void Awake()
    {
        _scoreCounter = GetComponent<ScoreCounter>();
        _handler = GetComponent<PlayerCollisionHandler>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Helicopter)        
            GameOver?.Invoke();
        else if (interactable is Ground)  
            GameOver?.Invoke();
        else if(interactable is ScoreZone)        
            _scoreCounter.Add();        
    }

    public void Reset()
    {
        _scoreCounter.Reset();
        _playerMover.Reset();
    }

    public void Die()
    {
        GameOver?.Invoke();
        _dyingSound.Play();
    }
}
