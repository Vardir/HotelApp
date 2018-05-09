using System;
using System.Windows;
using HotelsApp.UI.WindowTools;
using HotelsApp.Core.ViewModels;
using System.Windows.Input;
using HotelsApp.Core.RelayCommands;
using System.Runtime.InteropServices;

namespace HotelsApp.UI.ViewModels
{
    public class WindowViewModel : BaseViewModel
    {
        int outerMarginSize = 10;
        bool dimmableOverlayVisible;
        string title;
        Window window;
        WindowDockPosition dockPosition = WindowDockPosition.Undocked;

        public bool Borderless => window.WindowState == WindowState.Maximized || dockPosition != WindowDockPosition.Undocked;
        public bool DimmableOverlayVisible
        {
            get => dimmableOverlayVisible;
            set
            {
                if (dimmableOverlayVisible != value)
                {
                    dimmableOverlayVisible = value;
                    OnPropertyChanged(nameof(DimmableOverlayVisible));
                }
            }
        }
        public int ResizeBorder => Borderless ? 0 : 6;
        public int OuterMarginSize
        {
            get => Borderless ? 0 : outerMarginSize;
            set => outerMarginSize = value;
        }
        public int TitleHight { get; set; } = 30;
        public double WindowMinimumWidth { get; set; } = 800;
        public double WindowMinimumHeight { get; set; } = 500;
        public string Title
        {
            get => title;
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        public GridLength TitleHeightGridLength => new GridLength(TitleHight + ResizeBorder);
        public Thickness ResizeBorderThickness => new Thickness(ResizeBorder + OuterMarginSize);
        public Thickness OuterMarginSizeThickness => new Thickness(OuterMarginSize);
        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand MenuCommand { get; set; }

        public WindowViewModel(Window window)
        {
            this.window = window;
            Title = "Hotels Manager";

            window.StateChanged += (sender, e) =>
            {
                WindowResized();
            };

            MinimizeCommand = new RelayCommand(() => window.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => window.WindowState ^= WindowState.Maximized); //if window is already maximized, makes it normal
            CloseCommand = new RelayCommand(window.Close);
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(window, GetMousePosition()));

            //Fix window resize issue
            var resizer = new WindowResizer(window);
            resizer.WindowDockChanged += (dock) =>
            {
                // Store last position
                dockPosition = dock;

                // Fire off resize events
                WindowResized();
            };
        }

        void WindowResized()
        {
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }
        Point GetMousePosition2()
        {
            var position = Mouse.GetPosition(window);
            return new Point(position.X + window.Left, position.Y + window.Top);
        }

    }
}
