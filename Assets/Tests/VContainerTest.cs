using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using VContainer;
using VContainer.Unity;

public class VContainerTest
{
    class TestLifeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<HelloWorldService>(Lifetime.Singleton);
        }
    }

    class TestClass
    {
        [Inject]
        public HelloWorldService helloWorldService;
    }

    private void InitializeLifeTimeScope(out LifetimeScope scope)
    {
        var lifeTimeScopeGO = new GameObject("LifeTimeScope");
        scope = lifeTimeScopeGO.AddComponent<TestLifeScope>();
        scope.Build();
    }

    [Test]
    public void HelloWorldServiceInjectionTest()
    {
        InitializeLifeTimeScope(out var scope);

        var testClass1 = new TestClass();
        var testClass2 = new TestClass();
        var testClass3 = new TestClass();
        scope.Container.Inject(testClass1);
        scope.Container.Inject(testClass2);
        scope.Container.Inject(testClass3);

        Assert.IsNotNull(testClass1.helloWorldService);
        Assert.IsNotNull(testClass2.helloWorldService);
        Assert.IsNotNull(testClass3.helloWorldService);
    }
}
