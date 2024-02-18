using System;
using HtmlAgilityPack;

namespace cnauta.model.extension
{
    /// <summary>
    /// Custom extension for this model
    /// </summary>
    internal static class MHttpCnxExt
    {
        /// <summary>
        /// According to the result of scrap the specific <see cref="HtmlNode"/>, return the result of the connection as custom token
        /// </summary>
        /// <param name="nodes">Collection of <see cref="HtmlNode"/> to be processed (scraped)</param>
        /// <returns>Login result as a custom <see cref="PLoginResult"/> token</returns>
        public static PLoginResult TellMeHowItWent(this HtmlNodeCollection nodes)
        {
            var node = nodes[nodes.Count - 1];

            if (node.InnerText.Contains(Strs.PORTAL_RES_WRONG_PASS))
                return PLoginResult.WRONG_PASS;
            if (node.InnerText.Contains(Strs.PORTAL_RES_USER_INVALID))
                return PLoginResult.USER_INVALID;
            if (node.InnerText.Contains(Strs.PORTAL_RES_MANY_ATTEMPTS))
                return PLoginResult.MANY_ATTEMPTS;
            if (node.InnerText.Contains(Strs.PORTAL_RES_ALREADY_CONN))
                return PLoginResult.ALREADY_CONN;
            return node.InnerText.Contains(Strs.PORTAL_RES_ACCOUNT_OUT_TIME) 
                ? PLoginResult.ACCOUNT_OUT_TIME 
                : PLoginResult.OK;
        }

        /// <summary>
        /// Helper method the scrap the a specific data(ATTRIBUTE_UUID) from the server response
        /// </summary>
        /// <param name="nodes">Preferable filtered (or not) HTML <see cref="HtmlAgilityPack"/> nodes</param>
        /// <returns>ATTRIBUTE_UUID string, empty string if it wasn't found</returns>
        public static string hlp_GetUUID(this HtmlNodeCollection nodes)
        {
            foreach (var node in nodes)
            {
                var guuid = node.InnerText. Split(new[] { "ATTRIBUTE_UUID=" }, StringSplitOptions.None)[1].Split('&')[0];
                if (guuid.Length >= 10) return guuid;
            }

            return String.Empty;
        }
    }
}