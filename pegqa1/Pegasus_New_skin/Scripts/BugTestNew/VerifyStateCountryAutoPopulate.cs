using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyStateCountryAutoPopulate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        public void verifyStateCountryAutoPopulate()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");
            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password2");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpOffice_OfficeHelper = new CorpOffice_OfficeHelper(GetWebDriver());

            // Variable random
            var usernme = "Sysprins" + RandomNumber(44, 777);
            var name = "Test" + RandomNumber(99, 999);
            String JIRA = "";
            String Status = "Pass";

         try
           {

            executionLog.Log("VerifyStateCountryAutoPopulate", "Login with valid username and password");
            Login(username[0], password[0]);

            executionLog.Log("VerifyStateCountryAutoPopulate", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("VerifyStateCountryAutoPopulate", "Redirect to office page.");
            VisitCorp("offices");

            executionLog.Log("VerifyStateCountryAutoPopulate","Click on create buttton");
            corpOffice_OfficeHelper.ClickElement("Create");

            executionLog.Log("VerifyStateCountryAutoPopulate", "Verify title as create an office.");
            VerifyTitle("Create an Office");

            executionLog.Log("VerifyStateCountryAutoPopulate", "Click on add address button.");
            corpOffice_OfficeHelper.ClickElement("ClickAddAddressBtn");

            executionLog.Log("VerifyStateCountryAutoPopulate", "Enter a valid zip code.");
            corpOffice_OfficeHelper.TypeText("ZipCode2", "60601");
            corpOffice_OfficeHelper.WaitForWorkAround(3000);

            executionLog.Log("VerifyStateCountryAutoPopulate", "Verify country auto matically filled.");
            corpOffice_OfficeHelper.VerifyText("Country2", "United States");
            corpOffice_OfficeHelper.WaitForWorkAround(3000);

        }
       catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyStateCountryAutoPopulate");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify State Country Auto Populate");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify State Country Auto Populate", "Bug", "Medium", "Corp Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify State Country Auto Populate");
                        TakeScreenshot("VerifyStateCountryAutoPopulate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyStateCountryAutoPopulate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyStateCountryAutoPopulate");
                        string id = loginHelper.getIssueID("Verify State Country Auto Populate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyStateCountryAutoPopulate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify State Country Auto Populate"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify State Country Auto Populate");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyStateCountryAutoPopulate");
                executionLog.WriteInExcel("Verify State Country Auto Populate", Status, JIRA, "Corp Office");
            }
        }
    }
}