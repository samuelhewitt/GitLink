// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnterpriseGitHubProvider.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace GitLink.Providers
{
    using System;
    using System.Text.RegularExpressions;
    using GitTools.Git;
    using System.Configuration;
    using Catel.Logging;

    public class EnterpriseGitHubProvider : ProviderBase
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly Regex _gitHubRegex;
        private readonly string _rawGitUrlFormat;

        /// <summary>
        /// Loads GitHubRegex and RawGitUrlFormat from EnterpriseGitHub.config.
        /// </summary>
        public EnterpriseGitHubProvider()
            : base(new GitPreparer())
        {
            try
            {
                var regex = ConfigurationManager.AppSettings["gitLink.enterpriseGitHub.gitHubUrlRegex"];
                var format = ConfigurationManager.AppSettings["gitLink.enterpriseGitHub.rawGitUrlFormat"];

                _gitHubRegex = new Regex(regex);

                _rawGitUrlFormat = format;
            }
            catch(Exception ex)
            {
                Log.Info(ex, "EnterpriseGitHubProvider not loaded.");
                _gitHubRegex = new Regex("enterprise github provider do not match");
                _rawGitUrlFormat = "{0}/{1}";
            }
        }

        public override string RawGitUrl
        {
            get { return String.Format(_rawGitUrlFormat, CompanyName, ProjectName); }
        }

        public override bool Initialize(string url)
        {
            var match = _gitHubRegex.Match(url);

            if (!match.Success)
            {
                return false;
            }

            CompanyName = match.Groups["company"].Value;
            CompanyUrl = match.Groups["companyurl"].Value;

            ProjectName = match.Groups["project"].Value;
            ProjectUrl = match.Groups["url"].Value;

            if (!CompanyUrl.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase))
            {
                CompanyUrl = String.Concat("https://", CompanyUrl);
            }

            if (!ProjectUrl.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase))
            {
                ProjectUrl = String.Concat("https://", ProjectUrl);
            }

            return true;
        }
    }
}