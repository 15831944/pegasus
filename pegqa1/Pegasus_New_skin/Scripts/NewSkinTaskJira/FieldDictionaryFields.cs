using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class FieldDictionaryFields : DriverTestCase
    {
        [TestMethod]
        [TestCategory("NewSkin_Task")]
        [TestCategory("All")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void fieldDictionaryFields()
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
            var office_FieldDictionary_FieldsHelper = new Office_FieldDictionary_FieldsHelper(GetWebDriver());


            String Status = "Pass";
            String JIRA = "";

            try
            {
                executionLog.Log("FieldDictionaryFields", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("FieldDictionaryFields", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("FieldDictionaryFields", "Go to Field Dictionary Fields page");
                VisitOffice("fields");

                executionLog.Log("FieldDictionaryFields", "Verify Title");
                VerifyTitle("Field Management");

                executionLog.Log("FieldDictionaryFields", "Select Module");
                office_FieldDictionary_FieldsHelper.SelectByText("FSModule", "Clients");
                office_FieldDictionary_FieldsHelper.WaitForWorkAround(3000);

                executionLog.Log("FieldDictionaryFields", "Select Tab");
                office_FieldDictionary_FieldsHelper.SelectByText("FSTab", "Company Details");

                executionLog.Log("FieldDictionaryFields", "Click on Search Button");
                office_FieldDictionary_FieldsHelper.ClickElement("ButtonSearch");

                executionLog.Log("FieldDictionaryFields", "Verify field availabe");
                office_FieldDictionary_FieldsHelper.verifyElementPresent("FSFilter");

                executionLog.Log("FieldDictionaryFields", "Enter  mail");
                office_FieldDictionary_FieldsHelper.TypeText("FSFilter", "Mail");

                executionLog.Log("FieldDictionaryFields", "Click on Mail");
                office_FieldDictionary_FieldsHelper.ClickElement("FSMail");

                executionLog.Log("FieldDictionaryFields", "Verify Manage button available");
                office_FieldDictionary_FieldsHelper.verifyElementPresent("FSManage");

                executionLog.Log("FieldDictionaryFields", "Log out from the application");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("FieldDictionaryFields");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Field Dictionary Fields");
                if (!result)
                {

                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Field Dictionary Fields", "Bug", "Medium", "Field Dictionary page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Field Dictionary Fields");
                        TakeScreenshot("FieldDictionaryFields");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\FieldDictionaryFields.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("FieldDictionaryFields");
                        string id = loginHelper.getIssueID("Field Dictionary Fields");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\FieldDictionaryFields.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Field Dictionary Fields"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Field Dictionary Fields");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("FieldDictionaryFields");
                executionLog.WriteInExcel("Field Dictionary Fields", Status, JIRA, "Field Management");
            }
        }
    }
}