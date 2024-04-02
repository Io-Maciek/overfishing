using Godot;
using System;

public class TimeManager : Node
{
	Label _label;
	TimeSpan _time = TimeSpan.Zero;
	int _previous_second = 0;
	int _previous_minute = 0;

	[Signal]
	public delegate void TimeUpdateEventHandler(int minute, int second);

	public override void _Ready()
	{
		_label = GetNode<Label>("Label");
		_label.Text = "00:00";
	}

  	public override void _Process(float delta)
  	{
		_time += TimeSpan.FromSeconds(delta);
		_label.Text = _time.ToString(@"mm\:ss");

		if(_previous_second != _time.Seconds)
		{
			_previous_second = _time.Seconds;
			_previous_minute = _time.Minutes;
			

			EmitSignal("TimeUpdateEventHandler", _previous_minute, _previous_second);
		}
	}
}
