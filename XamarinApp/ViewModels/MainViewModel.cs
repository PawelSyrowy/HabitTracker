using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _screenVal="0";
        private List<string> _availableOperations = new List<string>{"+", "-", "*", "/"};
        private bool _isLastSignAnOperation;
        private DataTable _dataTable = new DataTable();

        public MainViewModel()
        {
            Title = "Kalkulator";
            AddNumberCommand = new Command(AddNumber);
            AddOperationCommand = new Command(AddOperation, CanAddOperation);
            ClearScreenCommand = new Command(ClearScreen);
            GetResultCommand = new Command(GetResult, CanGetResult);

            // Add the following lines:
            AddOperationCommand.CanExecuteChanged += (sender, e) => ((Command)GetResultCommand).ChangeCanExecute();
            AddOperationCommand.CanExecuteChanged += (sender, e) => ((Command)AddOperationCommand).ChangeCanExecute();
        }



        public ICommand AddNumberCommand { get; }
        public ICommand AddOperationCommand { get; }
        public ICommand ClearScreenCommand { get; }
        public ICommand GetResultCommand { get; }


        public bool IsLastSignAnOperation
        {
            get { return _isLastSignAnOperation; }
            set 
            { 
                SetProperty(ref _isLastSignAnOperation, value);
            }
        }


        public string ScreenVal
        {
            get { return _screenVal; }
            set { SetProperty(ref _screenVal, value); }
        }
        private void GetResult(object obj)
        {
            var result = Math.Round(Convert.ToDouble(_dataTable.Compute(ScreenVal.Replace(",", "."), "")), 2);
            ScreenVal = result.ToString();
        }

        private void ClearScreen(object obj)
        {
            ScreenVal = "0";
            IsLastSignAnOperation = false;
        }

        private void AddOperation(object obj)
        {
            var operation = obj as string;

            ScreenVal += operation;
            IsLastSignAnOperation = true;
        }

        private void AddNumber(object obj)
        {
            var number = obj as string;

            if (ScreenVal == "0" && number!=",")
            {
                ScreenVal=string.Empty;
            }
            else if(number=="," && 
                _availableOperations.Contains(ScreenVal.Substring(ScreenVal.Length-1)))
            {
                number = "0,";
            }

            ScreenVal += number;

            IsLastSignAnOperation = false;
        }
        private bool CanGetResult(object arg) => !
            IsLastSignAnOperation;

        private bool CanAddOperation(object arg) => !
            IsLastSignAnOperation;

    }
}