using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinApp.ViewModels
{
    public class CommandSwitch : Switch
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                nameof(Command),
                typeof(ICommand),
                typeof(CommandSwitch),
                null);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(
                nameof(CommandParameter),
                typeof(object),
                typeof(CommandSwitch),
                null);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public CommandSwitch()
        {
            Toggled += OnToggled;
        }

        private void OnToggled(object sender, ToggledEventArgs e)
        {
            if (Command != null && Command.CanExecute(CommandParameter))
            {
                Command.Execute(CommandParameter);
            }
        }
    }
}
