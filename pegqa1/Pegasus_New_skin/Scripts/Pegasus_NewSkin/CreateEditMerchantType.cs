using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CreateEditMerchantType : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void createEditMerchantType()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterdata_MerchantTypeHelper = new CorpMasterdata_MerchantTypeHelper(GetWebDriver());

            // Variable random
            var Merchant = "Merchant" + RandomNumber(44, 777);
            var name = "TestMerchant" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateEditMerchantType", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("CreateEditMerchantType", "Verify Page title");
                VerifyTitle("Dashboard");
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEditMerchantType", "Click on Agent in Topmenu");
                VisitCorp("masterdata/manage_merchant_types");

                executionLog.Log("CreateEditMerchantType", "Enter Merchant Name");
                corpMasterdata_MerchantTypeHelper.TypeText("EnterMercahntName", Merchant);

                executionLog.Log("CreateEditMerchantType", "Click Save");
                corpMasterdata_MerchantTypeHelper.ClickElement("Save");

                executionLog.Log("CreateEditMerchantType", "Wait for confirmation.");
                corpMasterdata_MerchantTypeHelper.WaitForText("The merchant type is successfully created!!", 10);

                executionLog.Log("CreateEditMerchantType", "Search Merchant");
                corpMasterdata_MerchantTypeHelper.TypeText("SearchMerchnatType", Merchant);
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEditMerchantType", "Click Edit Mercahnt");
                corpMasterdata_MerchantTypeHelper.ClickElement("EditMerchnatType");
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEditMerchantType", "Enter Merchant Name");
                corpMasterdata_MerchantTypeHelper.TypeText("EnterMercahntName", name);

                executionLog.Log("CreateEditMerchantType", "Click Save");
                corpMasterdata_MerchantTypeHelper.ClickElement("Save");

                executionLog.Log("CreateEditMerchantType", "Wait for confirmation.");
                corpMasterdata_MerchantTypeHelper.WaitForText("The merchant type is successfully updated!!", 10);

                executionLog.Log("CreateEditMerchantType", "Search Merchant");
                corpMasterdata_MerchantTypeHelper.TypeText("SearchMerchnatType", name);
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEditMerchantType", "Click Delete Mercahnt");
                corpMasterdata_MerchantTypeHelper.ClickElement("DeleteMerchnatType");
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEditMerchantType", "Accept alert message.");
                corpMasterdata_MerchantTypeHelper.AcceptAlert();
                corpMasterdata_MerchantTypeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEditMerchantType", "Wait for confirmation.");
                corpMasterdata_MerchantTypeHelper.WaitForText("The merchant type is successfully deleted!!", 10);

            }
             catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateEditMerchantType");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Edit Merchant Type");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Edit Merchant Type", "Bug", "Medium", "Merchant type page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Edit Merchant Type");
                        TakeScreenshot("CreateEditMerchantType");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateEditMerchantType.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateEditMerchantType");
                        string id = loginHelper.getIssueID("Create Edit Merchant Type");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateEditMerchantType.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Edit Merchant Type"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Edit Merchant Type");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateEditMerchantType");
                executionLog.WriteInExcel("Create Edit Merchant Type", Status, JIRA, "Corp Master Data");
            }
        }
    }
}