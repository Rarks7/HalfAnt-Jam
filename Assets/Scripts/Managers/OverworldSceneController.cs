using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldSceneController : SceneController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        foreach (var item in MagicUIElements)
        {
            item.SetActive(false);
        }
    }

}
