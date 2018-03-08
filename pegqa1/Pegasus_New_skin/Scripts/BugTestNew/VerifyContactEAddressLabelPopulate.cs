using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyContactEAddressLabelPopulate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifyContactEAddressLabelPopulate()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ContactsHelper = new Office_ContactsHelper(GetWebDriver());

            // Random Variable
            var File = GetPathToFile() + "contactsamples.csv";
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("VerifyContactEAddressLabelPopulate", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyContactEAddressLabelPopulate", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyContactEAddressLabelPopulate", "Redirect at import contacts page.");
                VisitOffice("contacts/create");
                office_ContactsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyContactEAddressLabelPopulate", "Select eAddress Type >> IM");
                office_ContactsHelper.selectByText("EaddressType", "IM");
                office_ContactsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyContactEAddressLabelPopulate", "Verify selected eAddress Label");
                office_ContactsHelper.verifyselectedOptn("EaddressLabel", "Google");

                executionLog.Log("VerifyContactEAddressLabelPopulate", "Select eAddress Type >> Social Media");
                office_ContactsHelper.selectByText("EaddressType", "Social Media");
                office_ContactsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyContactEAddressLabelPopulate", "Verify selected eAddress Label");
                office_ContactsHelper.verifyselectedOptn("EaddressLabel", "Facebook");

                executionLog.Log("VerifyContactEAddressLabelPopulate", "Select eAddress Type >> Web Links");
                office_ContactsHelper.selectByText("EaddressType", "Web Links");
                office_ContactsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyContactEAddressLabelPopulate", "Verify selected eAddress Label");
                office_ContactsHelper.verifyselectedOptn("EaddressLabel", "Weblink");
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyContactEAddressLabelPopulate");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Contact EAddress Label Populate");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Contact EAddress Label Populate", "Bug", "Medium", "Contacts page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Contact EAddress Label Populate");
                        TakeScreenshot("VerifyContactEAddressLabelPopulate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyContactEAddressLabelPopulate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyContactEAddressLabelPopulate");
                        string id = loginHelper.getIssueID("Verify Contact EAddress Label Populate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyContactEAddressLabelPopulate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Contact EAddress Label Populate"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Contact EAddress Label Populate");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyContactEAddressLabelPopulate");
                executionLog.WriteInExcel("Verify Contact EAddress Label Populate", Status, JIRA, "Contact Management");
            }
        }
    }
}