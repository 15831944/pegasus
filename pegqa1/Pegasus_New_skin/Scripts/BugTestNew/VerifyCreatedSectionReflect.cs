using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyCreatedSectionReflect : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifyCreatedSectionReflect()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_FieldDictionary_SectionsHelper = new Office_FieldDictionary_SectionsHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            String Status = "Pass";
            String JIRA = "";
            var secname = "Test" + GetRandomNumber();

            try
            {
                executionLog.Log("VerifyCreatedSectionReflect", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyCreatedSectionReflect", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyCreatedSectionReflect", "Go to Field Dictionary Section page");
                VisitOffice("sections");
                office_FieldDictionary_SectionsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedSectionReflect", "Verify Title");
                VerifyTitle("Section Management");

                executionLog.Log("VerifyCreatedSectionReflect", "Select Module >> Clients");
                office_FieldDictionary_SectionsHelper.Selectbytext("SelectModule", "Clients");
                office_FieldDictionary_SectionsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedSectionReflect", "Select Tab >> Company Details");
                office_FieldDictionary_SectionsHelper.Selectbytext("SelectTab", "Company Details");
                office_FieldDictionary_SectionsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyCreatedSectionReflect", "Click on Create button");
                office_FieldDictionary_SectionsHelper.ClickElement("Create");
                office_FieldDictionary_SectionsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCreatedSectionReflect", "Enter section name");
                office_FieldDictionary_SectionsHelper.TypeText("Name", secname);

                executionLog.Log("VerifyCreatedSectionReflect", "Click on Save button");
                office_FieldDictionary_SectionsHelper.ClickElement("Save");
                office_FieldDictionary_SectionsHelper.WaitForWorkAround(1000);
                office_FieldDictionary_SectionsHelper.AcceptAlert();
                office_FieldDictionary_SectionsHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyCreatedSectionReflect", "Redirect to All merchants page");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedSectionReflect", "Open any merchant");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedSectionReflect", "Go to Company Details");
                office_ClientsHelper.ClickElement("CompanyDetailsTab");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCreatedSectionReflect", "Verify created section appearing");
                office_ClientsHelper.VerifyPageText(secname);

                executionLog.Log("VerifyCreatedSectionReflect", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
             {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCreatedSectionReflect");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Created Section Reflect");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Created Section Reflect", "Bug", "Medium", "Field Section page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Created Section Reflect");
                        TakeScreenshot("VerifyCreatedSectionReflect");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatedSectionReflect.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCreatedSectionReflect");
                        string id = loginHelper.getIssueID("Verify Created Section Reflect");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatedSectionReflect.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Created Section Reflect"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Created Section Reflect");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyCreatedSectionReflect");
                executionLog.WriteInExcel("Verify Created Section Reflect", Status, JIRA, "Field Management");
            }
        }
    }
}   