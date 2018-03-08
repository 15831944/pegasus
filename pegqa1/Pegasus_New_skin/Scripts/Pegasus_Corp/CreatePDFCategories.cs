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
    public class CreatePDFCategories : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void createPDFCategories()
        {
           string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpPDFTemplate_CategoriesHelper = new CorpPDFTemplate_CategoriesHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");
            
            // Variable random

            String name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreatePDFCategories", "Login with valid credential");
                Login(username[0], password[0]);
                
                executionLog.Log("CreatePDFCategories", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreatePDFCategories", "Click on Click On Partner Agent");
                VisitCorp("pdf_templates/categories");

                executionLog.Log("CreatePDFCategories", "Verify Page title");
                VerifyTitle("PDF Categories");
                
                executionLog.Log("CreatePDFCategories", "Click on Click create button");
                corpPDFTemplate_CategoriesHelper.ClickElement("Create");
                corpPDFTemplate_CategoriesHelper.WaitForWorkAround(2000);

                executionLog.Log("CreatePDFCategories", "Enter Name");
                corpPDFTemplate_CategoriesHelper.TypeText("EnterName", name);

                executionLog.Log("CreatePDFCategories", "Click on Save Button");
                corpPDFTemplate_CategoriesHelper.ClickElement("Save");

                executionLog.Log("CreatePDFCategories", "Verify text present");
                corpPDFTemplate_CategoriesHelper.WaitForText("PDF Categories",10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreatePDFCategories");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create PDF Categories");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create PDF Categories", "Bug", "Medium", "Pdf page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create PDF Categories");
                        TakeScreenshot("CreatePDFCategories");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreatePDFCategories.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreatePDFCategories");
                        string id = loginHelper.getIssueID("Create PDF Categories");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreatePDFCategories.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create PDF Categories"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create PDF Categories");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreatePDFCategories");
                executionLog.WriteInExcel("Create PDF Categories", Status, JIRA,"Corp PDF Template");
            }
        }
    }
}
