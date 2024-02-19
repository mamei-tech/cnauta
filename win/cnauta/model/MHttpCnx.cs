using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using cnauta.model.extension;

using HtmlAgilityPack;

using cnauta.model.schema;


namespace cnauta.model
{
    /// <summary>
    /// Portal Connection Logic
    /// </summary>
    public class MHttpCnx
    {
        #region ============ FIELDS ==================================================

        private uint TIMEOUT = 25000;               // milliseconds (25s)
        
        private string _csrfHwToken;
        private string _logIdToken;
        private string _uuidToken;
        
        private CookieContainer _cookies;
        
        #endregion ===================================================================

        #region ============ PROPERTIES ==============================================
        
        public string CsrfHwToken => _csrfHwToken;
        public string LogIdToken => _logIdToken;
        public string UUIDToken => _uuidToken;

        #endregion ===================================================================

        #region ============ CONSTRUCTORS ============================================

        public MHttpCnx(string csrfHwToken = "", string LogIdToken = "", string UUIDToken = "")
        {
            _csrfHwToken = csrfHwToken;
            _logIdToken = LogIdToken;
            _uuidToken = UUIDToken;
        }
        
        #endregion ===================================================================

        #region ============ METHODS =================================================

        private void mkLogIdToken()
        {
            _logIdToken = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        /// <summary>
        /// Tries to reach the captive portal landing page and setup / grasp / scarp some important data
        /// like the CSRFHW token
        /// </summary>
        /// <returns>True is all went smoothly</returns>
        private async Task<bool> Prequel()
        {
            mkLogIdToken();

            using (var hClient = new HttpClient())
            {
                hClient.Timeout = TimeSpan.FromMilliseconds(TIMEOUT);

                var r = await hClient.GetAsync(StrsHots.HOST_URL);
                r.EnsureSuccessStatusCode();

                using (var stream = await r.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                {
                    var htmlContent = await reader.ReadToEndAsync().ConfigureAwait(false);
                    var doc = new HtmlDocument();
                    doc.LoadHtml(htmlContent);

                    _csrfHwToken = doc.DocumentNode.SelectSingleNode("//input[@name='CSRFHW']").Attributes["value"].Value;
                    _cookies = new CookieContainer();
                    
                    foreach (var cookie in r.Headers.GetValues("Set-Cookie"))
                        _cookies.SetCookies(new Uri(StrsHots.HOST_URL), cookie);

                    return true;
                }
            }
        }

        /// <summary>
        /// Request the HTTP account connection to the captive portal, so the active account actually connect to the internet using the portal 
        /// </summary>
        /// <param name="ucred">User credential, account data so the user can be connected to the captive portal</param>
        /// <returns>A login token (<see cref="PLoginResult"/>) as an abstraction of the request outcome</returns>
        public async Task<PLoginResult> TryToConnect(SchCredential ucred)
        {
            var wasLandingOk = await Prequel();
            if (!wasLandingOk) throw new InvalidOperationException();
            
            using (var hClient = new HttpClient(new HttpClientHandler
            {
                CookieContainer = _cookies,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate       // TIP ! use this ONLY if we are going to set the compressions headers to the client
            }))
            {
                hClient.Timeout = TimeSpan.FromMilliseconds(TIMEOUT);
                mkHeaders(hClient);
                
                var r = await hClient.PostAsync(StrsHots.HOST_URL_LOGIN, mkReqCnxData(ucred));
                r.EnsureSuccessStatusCode();
                
                using (var stream = await r.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                {
                    var htmlContent = await reader.ReadToEndAsync().ConfigureAwait(false);
                    var doc = new HtmlDocument();
                    doc.LoadHtml(htmlContent);

                    var nodes = doc.DocumentNode.SelectNodes("//script[@type='text/javascript']");
                    var howWasIt = nodes.TellMeHowItWent();

                    if (howWasIt == PLoginResult.OK)
                    {
                        _uuidToken = nodes.hlp_GetUUID();
                    }
                    
                    return howWasIt;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ucred"></param>
        public async Task<bool> TryToDisconnect(SchCredential ucred)
        {
            using (var hClient = new HttpClient())
            {
                hClient.Timeout = TimeSpan.FromMilliseconds(TIMEOUT);
                mkHeaders(hClient);
                
                var r = await hClient.PostAsync(StrsHots.HOST_URL_LOGOUT, mkReqDisconnectData(ucred));
                r.EnsureSuccessStatusCode();
                
                using (var stream = await r.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                {
                    var htmlContent = await reader.ReadToEndAsync().ConfigureAwait(false);
                    
                    if (htmlContent.Length <= 40)
                        if (htmlContent.Contains("FAILURE"))
                            return false;

                    return true;
                }
            }
        }

        private void mkHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) Gecko/20100101 Firefox/102.0");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
        }

        /// <summary>
        /// make (setup) a login captive portal request data
        /// </summary>
        private FormUrlEncodedContent mkReqCnxData(SchCredential account)
        {
            return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("wlanacname", ""),
                new KeyValuePair<string, string>("wlanmac", ""),
                new KeyValuePair<string, string>("firsturl", "notFound.jsp"),
                new KeyValuePair<string, string>("ssid", ""),
                new KeyValuePair<string, string>("usertype", ""),
                new KeyValuePair<string, string>("gotopage", "/nauta_etecsa/LoginURL/mobile_login.jsp"),
                new KeyValuePair<string, string>("successpage", "/nauta_etecsa/OnlineURL/mobile_index.jsp"),
                new KeyValuePair<string, string>("loggerId", _logIdToken),
                new KeyValuePair<string, string>("lang", "es_ES"),
                new KeyValuePair<string, string>("username", account.User),
                new KeyValuePair<string, string>("password", account.Pass),
                new KeyValuePair<string, string>("CSRFHW", _csrfHwToken)
            });
        }
        
        /// <summary>
        /// make (setup) a logout portal request data
        /// </summary>
        private FormUrlEncodedContent mkReqDisconnectData(SchCredential account)
        {
            return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", account.User),
                new KeyValuePair<string, string>("ATTRIBUTE_UUID", _uuidToken),
                new KeyValuePair<string, string>("wlanacname", ""),
                new KeyValuePair<string, string>("domain", ""),
                new KeyValuePair<string, string>("remove", "1"),
                new KeyValuePair<string, string>("loggerId", $"{_logIdToken}+{account.User}"),
            });
        } 

        #endregion ===================================================================
    }
}
