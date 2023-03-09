using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerObserver
{
    void OnPlayerDamaged(float currentHealth);
    void OnPlayerKilled();
    void OnPlayerExperienceGained(int experience);
    void OnPlayerScoreChanged(int score);
    void OnPlayerMaxHpChanged(int hp);
    void OnPlayerLevelChanged(int level);
}
