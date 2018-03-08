using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyAllPDFCategoriesAppearOnPDFTemplates : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifyAllPDFCategoriesAppearOnPDFTemplates()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_PDFTemplate_PDFCategoriesHelper = new CorpPDFTemplate_CategoriesHelper(GetWebDriver());
            var corpPDFTemplate_TemplateHelper = new CorpPDFTemplate_TemplateHelper(GetWebDriver());

            // Variable random
            String name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            //try
            //{
                executionLog.Log("VerifyAllPDFCategoriesAppearOnPDFTemplates", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyAllPDFCategoriesAppearOnPDFTemplates", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyAllPDFCategoriesAppearOnPDFTemplates", "Redirect To Categories");
                VisitCorp("pdf_templates/categories");
                corp_PDFTemplate_PDFCategoriesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAllPDFCategoriesAppearOnPDFTemplates", "Verify page title");
                VerifyTitle("PDF Categories");

                executionLog.Log("VerifyAllPDFCategoriesAppearOnPDFTemplates", "Click on Click create button");
                corp_PDFTemplate_PDFCategoriesHelper.ClickJS("Create");
                corp_PDFTemplate_PDFCategoriesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyAllPDFCategoriesAppearOnPDFTemplates", "Enter Name");
                corp_PDFTemplate_PDFCategoriesHelper.TypeText("EnterName", name);

                executionLog.Log("VerifyAllPDFCategoriesAppearOnPDFTemplates", "Click on Save Button");
                corp_PDFTemplate_PDFCategoriesHelper.ClickJS("Save");
                corp_PDFTemplate_PDFCategoriesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyAllPDFCategoriesAppearOnPDFTemplates", "Verify text present");
                corp_PDFTemplate_PDFCategoriesHelper.WaitForText("Category Created Successfully", 5);

                executionLog.Log("VerifyAllPDFCategoriesAppearOnPDFTemplates", "Redirect To PDF templates page");
                VisitCorp("pdf_templates");
                corpPDFTemplate_TemplateHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAllPDFCategoriesAppearOnPDFTemplates", "Verify category present in drop down");
                corpPDFTemplate_TemplateHelper.VerifyText("SearchCategory", name);

                executionLog.Log("VerifyAllPDFCategoriesAppearOnPDFTemplates", "Redirect To PDF templates page");
                VisitCorp("pdf_templates/categories");
                corp_PDFTemplate_PDFCategoriesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAllPDFCategoriesAppearOnPDFTemplates", "Search and delete pdf.");
                corp_PDFTemplate_PDFCategoriesHelper.SearchAndClick(name);
                //corp_PDFTemplate_PDFCategoriesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAllPDFCategoriesAppearOnPDFTemplates", "Select category to be replaced with.");
                corp_PDFTemplate_PDFCategoriesHelper.SelectDropDownByText("//*[@id='CategoryReplaceCategory']", "Other");
                //corp_PDFTemplate_PDFCategoriesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyAllPDFCategoriesAppearOnPDFTemplates", "Click on save button.");
                corp_PDFTemplate_PDFCategoriesHelper.ClickDisplayed("//a[@title='Save']");

                executionLog.Log("VerifyAllPDFCategoriesAppearOnPDFTemplates", "Category Replaced Successfully.");
                corp_PDFTemplate_PDFCategoriesHelper.WaitForText("Category Replaced Successfully.", 10);

            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("VerifyAllPDFCategoriesAppearOnPDFTemplates");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    Console.WriteLine(Error);
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("Verify All PDF Categories Appear On PDF Templates");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("Verify All PDF Categories Appear On PDF Templates", "Bug", "Medium", "PDF Category page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("Verify All PDF Categories Appear On PDF Templates");
            //            TakeScreenshot("VerifyAllPDFCategoriesAppearOnPDFTemplates");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\VerifyAllPDFCategoriesAppearOnPDFTemplates.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("VerifyAllPDFCategoriesAppearOnPDFTemplates");
            //            string id = loginHelper.getIssueID("Verify All PDF Categories Appear On PDF Templates");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\VerifyAllPDFCategoriesAppearOnPDFTemplates.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("Verify All PDF Categories Appear On PDF Templates"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("Verify All PDF Categories Appear On PDF Templates");
            //    //    executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
            //    executionLog.DeleteFile("VerifyAllPDFCategoriesAppearOnPDFTemplates");
            //    executionLog.WriteInExcel("Verify All PDF Categories Appear On PDF Templates", Status, JIRA, "Corp PDF Templates");
            //}
        }
    }
}

