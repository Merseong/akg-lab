using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class InGameLifeScope : LifetimeScope
{
    [SerializeField]
    private TurnController turnController;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent<TurnController>(turnController)
            .As<ITurnController>();
    }
}
