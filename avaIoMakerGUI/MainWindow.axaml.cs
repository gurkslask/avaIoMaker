using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace avaIoMakerGUI
{
    public partial class MainWindow : Window
    {
        private Button RunButton;
        private IoMaker.ioMakerLogicAB iom;
        private string inString;
        private string ioString;
        private string varString = "EEE";
        public MainWindow()

        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            Console.WriteLine("Hello");
            iom = new IoMaker.ioMakerLogicAB();
            ioString = "Initialized";
            this.DataContext = MainWindow.DataContextProperty;

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            RunButton = this.FindControl<Button>("Run");
        }
        public void RunButton_Click(object sender, RoutedEventArgs e)
        {
            iom.makeGeneral(inString);
            ioString = String.Join(' ', iom.io_list);
            Console.WriteLine("Hello");
        }
    }
}
