using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;

public class ControllerFactory
{
    private static Dictionary<string, Type> controllerByName;
    private static bool IsInitialized => controllerByName != null;

    private static void InitializeFactory()
    {
        if (IsInitialized)
            return;

        var controllerTypes = Assembly.GetAssembly(typeof(UnitController)).GetTypes().
            Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(UnitController)));

        controllerByName = new Dictionary<string, Type>();

        foreach(var type in controllerTypes)
        {
            var tempController = Activator.CreateInstance(type) as UnitController;
            controllerByName.Add(tempController.Name, type);
        }
    }

    public static UnitController GetController(string name)
    {
        InitializeFactory();

        if (controllerByName.ContainsKey(name))
        {
            Type controllerType = controllerByName[name];
            var controller = Activator.CreateInstance(controllerType) as UnitController;
            return controller;
        }

        return null;
    }

    public static IEnumerable<string> GetControllerTypes()
    {
        InitializeFactory();
        return controllerByName.Keys;
    }
}
