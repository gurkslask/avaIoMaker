using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;
using MessageBox.Avalonia;

namespace IoMakerGUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";
        public Button? RunButton;
        public IoMaker.ioMakerLogicAB iom = new IoMaker.ioMakerLogicAB();
        private string? instring;

        private string varString = "Varstring";
        public string VarString
        {
            get => varString;
            set => this.RaiseAndSetIfChanged(ref varString, value);
        }

        private string ioString = "Iostring";
        public string IoString
        {
            get => ioString;
            set => this.RaiseAndSetIfChanged(ref ioString, value);
        }
        private string accessString = "AccessString";
        public string AccessString
        {
            get => accessString;
            set => this.RaiseAndSetIfChanged(ref accessString, value);
        }
        private string invString = "InvString";
        public string InvString
        {
            get => invString;
            set => this.RaiseAndSetIfChanged(ref invString, value);
        }

        public MainWindowViewModel()
        {
            Run = ReactiveCommand.Create(RunTheThing);
            cEnableInverter = ReactiveCommand.Create(EnableInverter);
            cEnableAccess = ReactiveCommand.Create(EnableAccess);
        }

        public int Choice
        {
            get => Choice;
            set
            {
                if (value == 0)
                {
                    showstring = InvString;
                }
                else if (value == 1)
                {
                    showstring = AccessString;
                }
                else if (value == 2)
                {
                    showstring = IoString;
                }
                else if (value == 3)
                {
                    showstring = VarString;
                }
            }
        }
        private string showstring = "Result";
        public string ShowString
        {
            set => this.RaiseAndSetIfChanged(ref showstring, value);
            get => showstring;

        }
        public string InString
        {
            get => instring;
            set => this.RaiseAndSetIfChanged(ref instring, value);
        }
        void RunTheThing() {
            iom.makeGeneral(InString);
            VarString = String.Join("", iom.var_list);
            IoString = String.Join("", iom.io_list);
            AccessString = String.Join("", iom.access_list);
            InvString = String.Join("", iom.inv_list);
            ShowMB(String.Join("", iom.var_list));
        }
        public ReactiveCommand<Unit, Unit> Run { get; }
        public ReactiveCommand<Unit, Unit> cEnableInverter { get; }
        void EnableInverter()
        {
            Choice = 0;
            ShowMB(InvString);
        }
        public ReactiveCommand<Unit, Unit> cEnableAccess { get; }
        void EnableAccess() => Choice = 1;
        static void ShowMB(string message)
        {
            var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow("title", message);
            messageBoxStandardWindow.Show();
        }
    }
}
