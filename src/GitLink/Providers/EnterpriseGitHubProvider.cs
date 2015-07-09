// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnterpriseGitHubProvider.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace GitLink.Providers
{
    using System;
    using System.Configuration;
    using Catel.Logging;

    public class EnterpriseGitHubProvider : GitHubProvider
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private const string _noMatchString = ".do not match.";

        /// <summary>
        /// Loads GitHubRegex and RawGitUrlFormat from app.config.
        /// </summary>
        public EnterpriseGitHubProvider()
            : base(GetAppSetting("gitLink.enterpriseGitHub.gitHubUrlRegex"), GetAppSetting("gitLink.enterpriseGitHub.rawGitUrlFormat"))
        {
        }

        private static string GetAppSetting(string key)
        {
            string value = null;

            try
            {
                value = ConfigurationManager.AppSettings[key];
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = _noMatchString;
                }
            }
            catch (Exception ex)
            {
                Log.Debug(ex, "EnterpriseGitHubProvider not loaded, appSettings key '" + key + "' not found.");
                value = _noMatchString;
            }

            return value;
        }
    }
}