using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyRemoveIconForAddressInOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Delete")]
        public void verifyRemoveIconForAddressInOffice()
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
            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Login with valid credential  Username");
            Login("newthemecorp", "pegasus");


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

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Verify Page title");
            corpOffice_OfficeHelper.TypeText("AddressLine2", "test");

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Verify Page title");
            corpOffice_OfficeHelper.ClickElement("SaveEdit");

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Verify Page title");
            corpOffice_OfficeHelper.ClickElement("EditLink");

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Verify Page title");
            corpOffice_OfficeHelper.VerifyText("ZIpCode", "60601");

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Verify Page title");
            corpOffice_OfficeHelper.VerifyText("AddressLine1", "test");

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Verify Page title");
            corpOffice_OfficeHelper.ClickElement("RemoveAddress");

            executionLog.Log("CorpOfficeSearchWithInvalidPhone", "Verify Page title");
            corpOffice_OfficeHelper.clickByText("SaveEdit");

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
