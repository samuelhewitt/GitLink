// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GitHubProviderFacts.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace GitLink.Tests.Providers
{
    using GitLink.Providers;
    using NUnit.Framework;
    using System.Configuration;

    public class EnterpriseGitHubProviderFacts
    {
        [TestFixture]
        public class TheEnterpriseGitHubProviderInitialization
        {
            [TestCase]
            public void ReturnsValidInitialization()
            {
                var provider = new EnterpriseGitHubProvider();
                var valid = provider.Initialize("https://catenalogic-github.catenalogic.corp/CatenaLogic/GitLink");

                Assert.IsTrue(valid);
            }

            [TestCase]
            public void ReturnsInValidInitialization()
            {
                var provider = new EnterpriseGitHubProvider();
                var valid = provider.Initialize("https://bitbucket.org/CatenaLogic/GitLink");

                Assert.IsFalse(valid);

                valid = provider.Initialize("https://github.com/CatenaLogic/GitLink");

                Assert.IsFalse(valid);
            }

            [TestCase]
            public void ReturnsValidNoMatchInitialization()
            {
                var appSettings = ConfigurationManager.AppSettings;
                var resetMethod = typeof(System.Collections.Specialized.NameObjectCollectionBase).GetMethod("Reset", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, new System.Type[] { }, null);
                resetMethod.Invoke(appSettings, null);

                var provider = new EnterpriseGitHubProvider();
                var valid = provider.Initialize("https://bitbucket.org/CatenaLogic/GitLink");

                Assert.IsFalse(valid);

                valid = provider.Initialize("https://github.com/CatenaLogic/GitLink");

                Assert.IsFalse(valid);

                valid = provider.Initialize("https://catenalogic-github.catenalogic.corp/CatenaLogic/GitLink");

                Assert.IsFalse(valid);

                valid = provider.Initialize(".do not match.{0}{1}");

                Assert.IsTrue(valid);

                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        [TestFixture]
        public class TheEnterpriseGitHubProviderProperties
        {
            [TestCase]
            public void ReturnsValidCompany()
            {
                var provider = new EnterpriseGitHubProvider();
                provider.Initialize("https://catenalogic-github.catenalogic.corp/CatenaLogic/GitLink");

                Assert.AreEqual("CatenaLogic", provider.CompanyName);
            }

            [TestCase]
            public void ReturnsValidCompanyUrl()
            {
                var provider = new EnterpriseGitHubProvider();
                provider.Initialize("https://catenalogic-github.catenalogic.corp/CatenaLogic/GitLink");

                Assert.AreEqual("https://catenalogic-github.catenalogic.corp/CatenaLogic", provider.CompanyUrl);
            }

            [TestCase]
            public void ReturnsValidProject()
            {
                var provider = new EnterpriseGitHubProvider();
                provider.Initialize("https://catenalogic-github.catenalogic.corp/CatenaLogic/GitLink");

                Assert.AreEqual("GitLink", provider.ProjectName);
            }

            [TestCase]
            public void ReturnsValidProjectUrl()
            {
                var provider = new EnterpriseGitHubProvider();
                provider.Initialize("https://catenalogic-github.catenalogic.corp/CatenaLogic/GitLink");

                Assert.AreEqual("https://catenalogic-github.catenalogic.corp/CatenaLogic/GitLink", provider.ProjectUrl);
            }

            [TestCase]
            public void ReturnsValidRawGitUrl()
            {
                var provider = new EnterpriseGitHubProvider();
                provider.Initialize("https://catenalogic-github.catenalogic.corp/CatenaLogic/GitLink");

                Assert.AreEqual("https://catenalogic-github.catenalogic.corp/CatenaLogic/GitLink/raw", provider.RawGitUrl);
            }
        }
    }
}