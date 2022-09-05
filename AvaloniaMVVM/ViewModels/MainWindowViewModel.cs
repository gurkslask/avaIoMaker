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
        public string Greeting => "Welcome to Avalonia!";

        public MainWindowViewModel()
        {
            Run = ReactiveCommand.Create(runTheThing);
            IoStringOut = ReactiveCommand.Create(runIoStringOut);
            AccessOut = ReactiveCommand.Create(runAccessOut);
            InvOut = ReactiveCommand.Create(runInvOut);
            VarOut = ReactiveCommand.Create(runVarOut);
        }
        private string outstring;
        public string OutString
        {
            get => outstring;
            set => this.RaiseAndSetIfChanged(ref outstring, value);
        }

        public string InString
        {
            get => instring;
            set => this.RaiseAndSetIfChanged(ref instring, value);
        }
        void runTheThing() {
            iom.makeGeneral(InString);
            Console.WriteLine("Kört2");
            VarString = String.Join("", iom.var_list);
            IoString = String.Join("", iom.io_list);
            AccessString = String.Join("", iom.access_list);
            InvString = String.Join("", iom.inv_list);
        }
        void runIoStringOut() { OutString = IoString;}
        void runAccessOut() { OutString = AccessString;}
        void runInvOut() { OutString = VarString;}
        void runVarOut() { OutString = InvString;}
        public ReactiveCommand<Unit, Unit> Run { get; }
        public ReactiveCommand<Unit, Unit> IoStringOut { get; }
        public ReactiveCommand<Unit, Unit> AccessOut { get; }
        public ReactiveCommand<Unit, Unit> VarOut { get; }
        public ReactiveCommand<Unit, Unit> InvOut { get; }
    }
}
