using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System.Diagnostics;
namespace Pegasus_New_skin.Scripts
{
  //  [TestClass]
    public class OfficeAdminHeadFoot : DriverTestCase
    {
        ExecutionLog executionLog = new ExecutionLog();
        string[] username = null;
        string[] password = null;
        //define a array which includes the path for every pages.
        string[] OAArray =
        {   "OfficeTab","OfficeTab+Users","OfficeTab+ClientUsers","OfficeTab+Departments","OfficeTab+Roles","OfficeTab+Teams","OfficeTab+Avatars","OfficeTab+Groups",
            "CorporateTab",
            "SystemTab+Modules","SystemTab+Themes","SystemTab+PickLists","SystemTab+SystemSettings","SystemTab+SystemE-MailTemplates","SystemTab+AuditTrail","SystemTab+Permissions",
            "MasterDataTab+RatesAndFees","MasterDataTab+AmexRates","MasterDataTab+MerchantTypes","MasterDataTab+PricingPlans","MasterDataTab+Processors","MasterDataTab+FrontEnd","MasterDataTab+OmahaAuthGrids","MasterDataTab+MasterDataCategories","MasterDataTab+Languages",
            "StatisticsTab+UserStatistics","StatisticsTab+UsageStatistics",
            "PDFTemplatesTab+PDFImportWizard","PDFTemplatesTab+PDFTemplates","PDFTemplatesTab+PDFCategories",
            "TicketsTab+MasterData+Category","TicketsTab+MasterData+Topic","TicketsTab+MasterData+Status","TicketsTab+MasterData+Priority","TicketsTab+MasterData+Resolution",
            "TicketsTab+TicketsSettings","TicketsTab+TicketsE-MailTemplates","TicketsTab+E-MailNotifications",
            "IntegrationsTab+VantivAPI","IntegrationsTab+OmahaAPI","IntegrationsTab+IframeApps","IntegrationsTab+APICodes",
            "ProductsTab+ProductsCategories", "ProductsTab+Products",
            "EquipmentTab+Equipment","EquipmentTab+Vendors","EquipmentTab+DownloadIDs","EquipmentTab+ShippingCarriers",
            "FieldDictionaryTab+Tabs","FieldDictionaryTab+Sections","FieldDictionaryTab+Fields+FieldProperties","FieldDictionaryTab+Fields+FieldsOrder","FieldDictionaryTab+FieldGroupingTemplates",
            

        };
        //common layout of head
        string[] OAHead = { "HeadImage", "Header", "HeadDropDown", "HeadText" };

        OfficeAdminHelper OAHelper = null;
        [TestInitialize]
        public void Initialize()
        {
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            OAHelper = new OfficeAdminHelper(GetWebDriver());


        }
        //parse the input path array and then go to each page,separate the strings by '+', 
        //mouseover the elements in each string, and click the last element
        public void iterHelper(string[] inArray)
        {

            int len = inArray.Length;
            int curlen;
            string[] current = null;
            for (int i = 0; i < len; i++)
            {
                executionLog.Log("OfficeAdminHeadFoot", inArray[i]);

                current = inArray[i].Split('+');
                curlen = current.Length;
                for (int j = 0; j < curlen - 1; j++)
                {

                    OAHelper.MouseHover(current[j]);
                    OAHelper.WaitForWorkAround(2500);
                }


                OAHelper.ClickElement(current[curlen - 1]);
                OAHelper.WaitForWorkAround(3000);
                //check the common layout for each page
                foreach (string element in OAHead)
                {
                    Assert.IsTrue(OAHelper.ElementVisible(element));
                }

            }

        }
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin1")]
        public void TestOA()
        {
            var loginHelper = new LoginHelper(GetWebDriver());
            var usernme = "Sysprins" + RandomNumber(44, 799999977);
            var name = "Test" + GetRandomNumber();
            string JIRA = "";
            string Status = "Pass";
            //try
            //{
                Login(username[0], password[0]);
                OAHelper.WaitForWorkAround(3000);
                OAHelper.WaitForWorkAround(3000);
                OAHelper.MouseHover("HeadDropDown");
                OAHelper.WaitForWorkAround(2000);
                OAHelper.ClickElement("Admin");
                OAHelper.WaitForWorkAround(3000);
                iterHelper(OAArray);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("ERRROROOR");
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("OfficeAdminHeadFoot");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("OfficeAdminHeadFoot");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("OfficeAdminHeadFoot", "Bug", "Medium", "Office page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("OfficeAdminHeadFoot");
            //            TakeScreenshot("OfficeAdminHeadFoot");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\OfficeAdminHeadFoot";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("OfficeAdminHeadFoot");
            //            string id = loginHelper.getIssueID("OfficeAdminHeadFoot");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\OfficeAdminHeadFoot";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("OfficeAdminHeadFoot"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("OfficeAdminHeadFoot");
            //    executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
            //   // executionLog.DeleteFile("OfficeAdminHeadFoot");
            //    executionLog.WriteInExcel("OfficeAdminHeadFoot", Status, JIRA, "Office");
            //}

        }
    }
}
