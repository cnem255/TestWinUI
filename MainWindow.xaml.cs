using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Capture;
using Windows.Storage;
using Windows.System;
using Windows.UI.Core;
using Windows.Graphics;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using Windows.UI.ViewManagement;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using Windows.ApplicationModel.DataTransfer;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TestWinUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private bool rgbIsCopied = false;
        private bool hexIsCopied = false;

        public MainWindow()
        {
            this.InitializeComponent();

            this.AppWindow.Resize(new SizeInt32(500, 300));
            CenterWindow();

            copyRgbButton.Content = new SymbolIcon(Symbol.Copy);
            copyHexButton.Content = new SymbolIcon(Symbol.Copy);

            copyRgbButton.Click += (sender, e) => OnCopyRgbClick(sender, e);

            copyHexButton.Click += (sender, e) => OnCopyHexClick(sender, e);
        }

        private void CenterWindow()
        {
            // Get the window handle (hWnd)
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

            // Get screen dimensions
            DisplayArea displayArea = DisplayArea.GetFromWindowId(windowId, DisplayAreaFallback.Nearest);
            int screenWidth = displayArea.WorkArea.Width;
            int screenHeight = displayArea.WorkArea.Height;

            // Get window dimensions
            int windowWidth = (int)appWindow.Size.Width;
            int windowHeight = (int)appWindow.Size.Height;

            // Calculate center coordinates
            int centerX = (screenWidth - windowWidth) / 2;
            int centerY = (screenHeight - windowHeight) / 2;

            // Set the window position
            appWindow.Move(new PointInt32(centerX, centerY));

        }

        private void OnCopyRgbClick(object sender, RoutedEventArgs e)
        {
            DataPackage rgbData = new DataPackage();
            rgbData.SetText(rgbDisplayValue.Text);

            Clipboard.SetContent(rgbData);
            rgbIsCopied = !rgbIsCopied;

            // Update symbol directly
            copyRgbButton.Content = new SymbolIcon
            {
                Symbol = rgbIsCopied ? Symbol.Accept : Symbol.Copy
            };
        }

        private void OnCopyHexClick(object sender, RoutedEventArgs e)
        {
            DataPackage hexData = new DataPackage();
            hexData.SetText(hexDisplayValue.Text);

            Clipboard.SetContent(hexData);
            rgbIsCopied = !rgbIsCopied;

            // Update symbol directly
            copyHexButton.Content = new SymbolIcon
            {
                Symbol = rgbIsCopied ? Symbol.Accept : Symbol.Copy
            };
        }
    }
}
