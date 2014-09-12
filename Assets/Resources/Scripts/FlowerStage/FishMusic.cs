﻿using UnityEngine;
using System.Collections;

public class FishMusic : MonoBehaviour {

  public int minFishForMusic = 2;
  public int maxFishForMusic = 7;

  private int current_number_of_fish_ = 0;

  public delegate void NotifyMoreFish();
  public static event NotifyMoreFish OnMoreFish;

  void OnEnable() {
    PressAnyKeyToContinue.OnContinue += Deactivate;
  }

  void OnDisable() {
    PressAnyKeyToContinue.OnContinue -= Deactivate;
  }

  float PercentFish() {
    FishForce[] fishies = FindObjectsOfType<FishForce>();

    int num_fish = 0;
    foreach (FishForce fish in fishies) {
      if (fish.IsFish())
        num_fish++;
    }
    if (num_fish > current_number_of_fish_)
      OnMoreFish();

    current_number_of_fish_ = num_fish;
    return Mathf.Clamp((1.0f * (num_fish - minFishForMusic)) / maxFishForMusic, 0.0f, 1.0f);
  }

  void Update() {
    audio.volume = PercentFish();
  }

  void Deactivate(){
    enabled = false;
  }
}
