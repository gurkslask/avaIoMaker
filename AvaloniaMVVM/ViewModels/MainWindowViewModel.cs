using Avalonia.Controls;
using Avalonia.Interactivity;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace AvaloniaMVVM.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public Button RunButton;
        public IoMaker.ioMakerLogicAB iom = new IoMaker.ioMakerLogicAB();
        private string instring;

        private string varString = "Varstring";
        public string VarString
        {
            get => varString;
            set => this.RaiseAndSetIfChanged(ref varString, value);
        }

        private string ioString = "Iostringj";
        public string IoString
        {
            get => ioString;
            set => this.RaiseAndSetIfChanged(ref ioString, value);
        }
        public string Greeting => "Welcome to Avalonia!";

        public MainWindowViewModel()
        {
            Run = ReactiveCommand.Create(runTheThing);
        }

        public string InString
        {
            get => instring;
            set => this.RaiseAndSetIfChanged(ref instring, value);
        }
        void runTheThing() {
            iom.makeGeneral(InString);
            VarString = String.Join("", iom.var_list);
            IoString = String.Join("", iom.io_list);
        }
        // void runTheThing() { VarString = "KÖRT!!!"; }
        public ReactiveCommand<Unit, Unit> Run { get; }
    }
}
