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
    public class CreateAndDeletePDFCategories : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void createAndDeletePDFCategories()
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

            // Variable random
            String name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateAndDeletePDFCategories", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateAndDeletePDFCategories", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateAndDeletePDFCategories", "Redirect To Categories");
                VisitCorp("pdf_templates/categories");
                corp_PDFTemplate_PDFCategoriesHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateAndDeletePDFCategories", "Verify page title");
                VerifyTitle("PDF Categories");

                executionLog.Log("CreateAndDeletePDFCategories", "Click on Click create button");
                corp_PDFTemplate_PDFCategoriesHelper.ClickJS("Create");
                corp_PDFTemplate_PDFCategoriesHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateAndDeletePDFCategories", "Enter Name");
                corp_PDFTemplate_PDFCategoriesHelper.TypeText("EnterName", name);

                executionLog.Log("CreateAndDeletePDFCategories", "Click on Save Button");
                corp_PDFTemplate_PDFCategoriesHelper.ClickJS("Save");
                corp_PDFTemplate_PDFCategoriesHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateAndDeletePDFCategories", "Verify text present");
                corp_PDFTemplate_PDFCategoriesHelper.WaitForText("Category Created Successfully", 5);

                executionLog.Log("CreateAndDeletePDFCategories", "Search and delete pdf.");
                corp_PDFTemplate_PDFCategoriesHelper.SearchAndClick(name);
                //corp_PDFTemplate_PDFCategoriesHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateAndDeletePDFCategories", "Select category to be replaced with.");
                corp_PDFTemplate_PDFCategoriesHelper.SelectDropDownByText("//*[@id='CategoryReplaceCategory']", "Other");
                //corp_PDFTemplate_PDFCategoriesHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateAndDeletePDFCategories", "Click on save button.");
                corp_PDFTemplate_PDFCategoriesHelper.ClickDisplayed("//a[@title='Save']");

                executionLog.Log("CreateAndDeletePDFCategories", "Category Replaced Successfully.");
                corp_PDFTemplate_PDFCategoriesHelper.WaitForText("Category Replaced Successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateAndDeletePDFCategories");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create And Delete PDF Categories");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create And Delete PDF Categories", "Bug", "Medium", "PDF Category page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create And Delete PDF Categories");
                        TakeScreenshot("CreateAndDeletePDFCategories");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateAndDeletePDFCategories.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateAndDeletePDFCategories");
                        string id = loginHelper.getIssueID("Create And Delete PDF Categories");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateAndDeletePDFCategories.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create And Delete PDF Categories"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create And Delete PDF Categories");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateAndDeletePDFCategories");
                executionLog.WriteInExcel("Create And Delete PDF Categories", Status, JIRA, "Corp PDF Templates");
            }
        }
    }
}

