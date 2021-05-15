using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable
{
    void add(IObserver a);
    void remove(IObserver a);
    void notify();
}
