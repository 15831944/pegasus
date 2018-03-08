using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class MerchnatTypePushToOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void merchnatTypePushToOffice()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterdata_MerchantTypeHelper = new CorpMasterdata_MerchantTypeHelper(GetWebDriver());
            var masterData_MerchantTypeHelper = new MasterData_MerchantTypeHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable random
            var name = "Test" + GetRandomNumber();
            var Test = "New" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("MerchnatTypePushToOffice", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("MerchnatTypePushToOffice", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MerchnatTypePushToOffice", "Redirect to marchant type page");
                VisitCorp("masterdata/merchant_types");

                executionLog.Log("MerchnatTypePushToOffice", "Verify Page title");
                VerifyTitle("Master Merchant Types");

                executionLog.Log("MerchnatTypePushToOffice", "Click On Create");
                corpMasterdata_MerchantTypeHelper.ClickElement("Create");

                executionLog.Log("MerchnatTypePushToOffice", "Verify Page title");
                VerifyTitle("Manage Master Merchant Types");

                executionLog.Log("MerchnatTypePushToOffice", "Enter Merchant Type");
                corpMasterdata_MerchantTypeHelper.TypeText("MerchantType", name);

                executionLog.Log("MerchnatTypePushToOffice", "Click On Save Btn");
                corpMasterdata_MerchantTypeHelper.ClickElement("Save");

                executionLog.Log("MerchnatTypePushToOffice", "Wait for success message");
                corpMasterdata_MerchantTypeHelper.WaitForText("The merchant type is successfully created!!", 10);

                executionLog.Log("MerchnatTypePushToOffice", "Click On Push Office");
                corpMasterdata_MerchantTypeHelper.ClickElement("PushOffice");

                executionLog.Log("MerchnatTypePushToOffice", "Accept alert message.");
                corpMasterdata_MerchantTypeHelper.AcceptAlert();

                executionLog.Log("MerchnatTypePushToOffice", "Logout from the application.");
                VisitCorp("logout");
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchnatTypePushToOffice", "Login to application using office credentials");
                Login("newthemeoffice", "pegasus");

                executionLog.Log("MerchnatTypePushToOffice", "Verify page title.");
                VerifyTitle("Dashboard");

                executionLog.Log("MerchnatTypePushToOffice", "Redirect to Merchant types");
                VisitOffice("merchant_types");

                executionLog.Log("MerchnatTypePushToOffice", "Search Merchnat Type");
                masterData_MerchantTypeHelper.TypeText("SearchMerchanttype", name);
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(10000);

                executionLog.Log("MerchnatTypePushToOffice", "Logout button");
                VisitOffice("logout");

                executionLog.Log("MerchnatTypePushToOffice", "Login with valid credential");
                Login("newthemecorp", "pegasus");

                executionLog.Log("MerchnatTypePushToOffice", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("MerchnatTypePushToOffice", "Redirect to marchant type page");
                VisitCorp("masterdata/merchant_types");

                executionLog.Log("MerchnatTypePushToOffice", "Enter Name to search");
                corpMasterdata_MerchantTypeHelper.TypeText("SearchMerchnatType", name);
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(2000);

                executionLog.Log("MerchnatTypePushToOffice", "Click Delete btn  ");
                corpMasterdata_MerchantTypeHelper.ClickElement("DeleteMerchnatType");

                executionLog.Log("MerchnatTypePushToOffice", "Accept alert message. ");
                corpMasterdata_MerchantTypeHelper.AcceptAlert();

                executionLog.Log("MerchnatTypePushToOffice", "Wait for delete message. ");
                corpMasterdata_MerchantTypeHelper.WaitForText("The merchant type is successfully deleted!!", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MerchnatTypePushToOffice");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Merchnat Type Push To Office");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Merchnat Type Push To Office", "Bug", "Medium", "Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Merchnat Type Push To Office");
                        TakeScreenshot("MerchnatTypePushToOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MerchnatTypePushToOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MerchnatTypePushToOffice");
                        string id = loginHelper.getIssueID("Merchnat Type Push To Office");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MerchnatTypePushToOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Merchnat Type Push To Office"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Merchnat Type Push To Office");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MerchnatTypePushToOffice");
                executionLog.WriteInExcel("Merchnat Type Push To Office", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
