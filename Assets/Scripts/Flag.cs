using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : CollectibleController
{
    public WallController Entrance;

    public override void OnItemCollected(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        sr.sprite = GetComponent<SpriteRenderer>().sprite;
        sr.color = Color.white;
        Entrance.GetComponent<SpriteRenderer>().color = Color.grey;
        gameObject.SetActive(false);

        // do not change player's status anymore
        // interact directly with LevelController
        LevelController.OnLevelCompleted();
    }
}
