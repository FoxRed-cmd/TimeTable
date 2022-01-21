using System.Windows.Input;

namespace TimeTable
{
    public static class HotKeyCommand
    {
        public static RoutedCommand OpenMenuCommand { get; private set; }
        static HotKeyCommand()
        {
            InputGestureCollection inputs = new();
            inputs.Add(new KeyGesture(Key.Q, ModifierKeys.Control, "Ctrl+Q"));
            OpenMenuCommand = new RoutedUICommand("Some", "SomeCommand", typeof(HotKeyCommand), inputs);
        }

    }
}
