using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CreateMerchnatType : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void merchnatType()
        {
            string[] username = null;
            string[] username1 = null;
            string[] password = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterdata_MerchantTypeHelper = new CorpMasterdata_MerchantTypeHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_corp");
            username1 = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable random
            var name = "Test" + GetRandomNumber();
            var Test = "New" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateMerchnatType", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("CreateMerchnatType", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateMerchnatType", "Redirect to marchant type page");
                VisitCorp("masterdata/merchant_types");
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateMerchnatType", "Verify title");
                VerifyTitle("Master Merchant Types");

                executionLog.Log("CreateMerchnatType", "Click On Create");
                corpMasterdata_MerchantTypeHelper.ClickElement("Create");
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateMerchnatType", "Verify title");
                VerifyTitle("Manage Master Merchant Types");

                executionLog.Log("CreateMerchnatType", "Enter Merchant Type");
                corpMasterdata_MerchantTypeHelper.TypeText("MerchantType", name);

                executionLog.Log("CreateMerchnatType", "Click On Save Btn");
                corpMasterdata_MerchantTypeHelper.ClickElement("Save");

                executionLog.Log("CreateMerchnatType", "Wait for success message");
                corpMasterdata_MerchantTypeHelper.WaitForText("The merchant type is successfully created!!", 30);

                executionLog.Log("CreateMerchnatType", "Click On Push Office");
                corpMasterdata_MerchantTypeHelper.ClickElement("PushOffice");
                corpMasterdata_MerchantTypeHelper.AcceptAlert();

                executionLog.Log("CreateMerchnatType", "Logout from the application");
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(5000);
                VisitCorp("logout");

                executionLog.Log("CreateMerchnatType", "Login to application using office credentials.");
                Login(username1[0], password[0]);
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(2000);

                if (GetWebDriver().Title == "Login")
                {
                    Login(username1[0], password[0]);
                }

                executionLog.Log("CreateMerchnatType", "Verify title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateMerchnatType", "Redirect to mercahnt types page");
                VisitOffice("merchant_types");
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateMerchnatType", "Logout button");
                VisitOffice("logout");

                executionLog.Log("CreateMerchnatType", "Login with valid credential");
                Login(username[0], password[0]);

                executionLog.Log("CreateMerchnatType", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateMerchnatType", "Redirect to marchant type page");
                VisitCorp("masterdata/merchant_types");
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateMerchnatType", "Enter Name to search");
                corpMasterdata_MerchantTypeHelper.TypeText("SearchMerchnatType", name);
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateMerchnatType", "Click Delete btn  ");
                corpMasterdata_MerchantTypeHelper.ClickElement("DeleteMerchnatType");

                executionLog.Log("CreateMerchnatType", "Accept alert message. ");
                corpMasterdata_MerchantTypeHelper.AcceptAlert();

                executionLog.Log("CreateMerchnatType", "Wait for delete message. ");
                corpMasterdata_MerchantTypeHelper.WaitForText("The merchant type is successfully deleted!!", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateMerchnatType");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Merchnat Type");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Merchnat Type", "Bug", "Medium", "Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Merchnat Type");
                        TakeScreenshot("CreateMerchnatType");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateMerchnatType.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateMerchnatType");
                        string id = loginHelper.getIssueID("Create Merchnat Type");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateMerchnatType.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Merchnat Type"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Merchnat Type");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateMerchnatType");
                executionLog.WriteInExcel("Create Merchnat Type", Status, JIRA, "Merchant Portal");
            }
        }
    }
}
