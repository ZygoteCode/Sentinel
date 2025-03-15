﻿using System.Collections.Generic;
using CefSharp.WinForms;

public static class Utils
{
    public static ChromiumWebBrowser WebBrowser;
    public static Dictionary<string, string> Keywords = new Dictionary<string, string>()
    {
        ["ig"] = "https://storiesig.info/",
        ["fb"] = "https://fbtake.com/facebook-story-viewer/",
        ["yt"] = "https://ytlarge.com/youtube/channel-id-finder/",
        ["x"] = "https://twstalker.com/",
        ["instagram"] = "https://storiesig.info/",
        ["facebook"] = "https://fbtake.com/facebook-story-viewer/",
        ["youtube"] = "https://ytlarge.com/youtube/channel-id-finder/",
        ["twitter"] = "https://twstalker.com/",
        ["snapchat"] = "https://snaplytics.io/snapchat-video-downloader/",
        ["spotify"] = "https://spotify.com/",
        ["steam"] = "https://steamdb.info/",
        ["minecraft"] = "https://namemc.com/",
        ["mc"] = "https://namemc.com/",
        ["maps"] = "https://maps.google.com/",
        ["ip"] = "https://ipinfo.io/",
        ["ipinfo"] = "https://ipinfo.io/",
        ["osint"] = "https://osintframework.com/",
        ["osintframework"] = "https://osintframework.com/",
        ["ai"] = "https://perplexity.ai/",
        ["perplexity"] = "https://perplexity.ai/",
        ["name"] = "https://whatsmyname.app/",
        ["whatsmyname"] = "https://whatsmyname.app/",
        ["tineye"] = "https://www.tineye.com/",
        ["home"] = "https://priv.au/",
        ["dorks"] = "https://www.exploit-db.com/google-hacking-database/",
        ["dork"] = "https://www.exploit-db.com/google-hacking-database/",
        ["shodan"] = "https://shodan.io/",
        ["haveibeenpwned"] = "https://haveibeenpwned.com/",
        ["skype"] = "https://www.vedbex.com/skyperesolver",
        ["sk"] = "https://www.vedbex.com/skyperesolver",
        ["whois"] = "https://who.is/",
        ["archive"] = "https://web.archive.org/",
        ["internet"] = "https://web.archive.org/",
        ["web"] = "https://web.archive.org/",
        ["lens"] = "https://lens.google.com/",
        ["phone"] = "https://www.truecaller.com/",
        ["ssl"] = "https://www.ssllabs.com/ssltest/",
        ["asn"] = "https://mxtoolbox.com/asn.aspx",
        ["ports"] = "https://hackertarget.com/nmap-online-port-scanner/",
        ["nmap"] = "https://hackertarget.com/nmap-online-port-scanner/",
        ["discord"] = "https://discord.id/",
        ["dsc"] = "https://discord.id/",
        ["virus"] = "https://virustotal.com/",
        ["malware"] = "https://virustotal.com/",
        ["abuseip"] = "https://www.abuseipdb.com/",
    };
}