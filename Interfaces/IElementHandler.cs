using System;
using Godot;

public interface IElementHandler {
    void Process(Element element, int x, int y);
}