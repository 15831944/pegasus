using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminMasterDataAmexRateURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adminMasterDataAmexRateURLChange()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var masterData_AmexRateHelper = new MasterData_AmexRateHelper(GetWebDriver());


            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdminMasterDataAmexRateURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminMasterDataAmexRateURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminMasterDataAmexRateURLChange", "Goto Master Data >> Amex rates");
                VisitOffice("amex_rates");

                executionLog.Log("AdminMasterDataAmexRateURLChange", "Click On any Amex Rate");
                masterData_AmexRateHelper.ClickElement("OpenAnyAmextRate");
                masterData_AmexRateHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminMasterDataAmexRateURLChange", "Change the url with the url number of another office");
                VisitOffice("manage_amex_rates/26613");
                masterData_AmexRateHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminMasterDataAmexRateURLChange", "Verify Validation");
                masterData_AmexRateHelper.WaitForText("The Amex Master Rate is does not exists.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminMasterDataAmexRateURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("AdminMaster Data Amex Rate URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("AdminMaster Data Amex Rate URL Change", "Bug", "Medium", "Amex Rates Page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("AdminMaster Data Amex Rate URL Change");
                        TakeScreenshot("AdminMasterDataAmexRateURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminMasterDataAmexRateURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminMasterDataAmexRateURLChange");
                        string id = loginHelper.getIssueID("AdminMaster Data Amex Rate URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminMasterDataAmexRateURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("AdminMaster Data Amex Rate URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("AdminMaster Data Amex Rate URL Change");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminMasterDataAmexRateURLChange");
                executionLog.WriteInExcel("AdminMaster Data Amex Rate URL Change", Status, JIRA, "Master Data");
            }
        }
    }
}
