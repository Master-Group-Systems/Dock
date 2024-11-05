using  System;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using Avalonia.Input;

namespace DockControlwindow;

public class Controlwindow {

public void MoveWindow(Window janela, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(janela).Properties.IsLeftButtonPressed)
        {
            janela.BeginMoveDrag(e);
        }
    }
}