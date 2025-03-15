using CefSharp;
using CefSharp.WinForms;

public class CustomLifeSpanHandler : ILifeSpanHandler
{
    public bool OnBeforePopup(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame,
                              string targetUrl, string targetFrameName,
                              WindowOpenDisposition targetDisposition,
                              bool userGesture, IPopupFeatures popupFeatures,
                              IWindowInfo windowInfo, IBrowserSettings browserSettings,
                              ref bool noJavascriptAccess, out IWebBrowser newBrowser)
    {
        // Cast al tipo corretto di browser
        var chromeBrowser = (ChromiumWebBrowser)chromiumWebBrowser;

        // Naviga l'istanza attuale all'URL richiesto dal popup
        chromeBrowser.Load(targetUrl);

        // Impedisce la creazione di una nuova finestra
        newBrowser = null;
        return true;
    }

    public void OnAfterCreated(IWebBrowser chromiumWebBrowser, IBrowser browser) { }

    public bool DoClose(IWebBrowser chromiumWebBrowser, IBrowser browser) => false;

    public void OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser) { }
}
