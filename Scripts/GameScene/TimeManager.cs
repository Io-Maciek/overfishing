using Godot;
using System;
using System.Diagnostics;

public class TimeManager : Label
{
    TimeSpan _time = TimeSpan.Zero;
    int _previous_second = 0;
    int _previous_minute = 0;

    public bool IsRunning = true;

    [Signal]
    public delegate void TimeUpdateEventHandler(int minute, int second);

    public override void _Ready()
    {
        Text = "00:00";
    }

    public override void _Process(float delta)
    {
        if (!IsRunning)
            return;

        _time += TimeSpan.FromSeconds(delta);
        Text = _time.ToString(@"mm\:ss");

        if (_previous_second != _time.Seconds)
        {
            _previous_second = _time.Seconds;
            _previous_minute = _time.Minutes;


            EmitSignal("TimeUpdateEventHandler", _previous_minute, _previous_second);
        }
    }

    private void _on_Scene_TimeStopsEventHandler()
    {
        IsRunning = false;
    }

}


