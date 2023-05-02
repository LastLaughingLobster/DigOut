using System;
using Godot;

public interface IHandler{
    void Process(int x, int y, Element element, bool processState);
}