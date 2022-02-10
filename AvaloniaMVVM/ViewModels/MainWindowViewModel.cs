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
        public IoMaker.ioMakerLogicAB iom;
        public string inString { get { return instring; } set { instring = value; } }
        private string instring;

        public string ioString => "iostring";
        public string varString ;
        public string Greeting => "Welcome to Avalonia!";

        public MainWindowViewModel()
        {
            Run = ReactiveCommand.Create(runTheThing);
        }

        public string InString
        {
            get => inString;
            set => this.RaiseAndSetIfChanged(ref instring, value);
        }
        void runTheThing() { iom.makeGeneral(inString); }
        public ReactiveCommand<Unit, Unit> Run { get; }
    }
}
