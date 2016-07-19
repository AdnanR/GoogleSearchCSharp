using System;
using System.Collections.Generic;
using System.Linq;
using Coypu;
using GoogleTestProject.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace GoogleTestProject
{
    [Binding]
    public class UnitTest1 : UITestBase
    {

        [Given(@"I am on the Google home page")]
        public void GoToBaseURL()
        {
            // Visit the base url
            Browser.Visit(BaseURL());
        }

        [When(@"I enter (.*) in the search box")]
        public void EnterSearchTerm(string searchTerm)
        {
            // Find the query text box by name and fill in the searh term
            Browser.FillIn("q").With(searchTerm + Keys.Enter);
        }

        [Then(@"the (\d+)(?:st|nd|rd|th) search result should contain the (.*) text")]
        public void VerifySearchResultTextAt(int resultNumber, string searchTerm)
        {
            // Find the search section by id, all of the search results are displayed in this div (see anchorPoint.png)
            // using this as an anchor point will filter non search link (if any)
            ElementScope searchSection = Browser.FindId("search");

            // Verify some search results were displayed
            // Note: Coypu doesn't return null if the web element is not found
            Assert.IsTrue(searchSection.Exists(), "No search results were not displayed.");

            // All of the search links are present under h3[class='r'] tag (see h3a.PNG)
            // Get all the result links under searchSection
            List<SnapshotElementScope> links = searchSection.FindAllCss("h3.r a").ToList();

            // Verify number of results is at least what we expect
            if (links.Count() < resultNumber)
                Assert.Fail($"Results count is less then expected. Actual {links.Count()}, Expected {resultNumber} (at least).");

            // Results index starts from 0. An argument transformantion can also be used here
            // http://www.specflow.org/documentation/Step-Argument-Conversions/
            // https://github.com/cucumber/cucumber/wiki/Step-Argument-Transforms
            int resultIndex = resultNumber - 1;

            // Verify that required link element contains search term.
            // Currently, the comparision is case sensitive. Case can be ignored if required.
            Assert.IsTrue(links.ElementAt(resultIndex).Text.Contains(searchTerm));
        }

    }
}
